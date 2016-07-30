using System;
using System.Net;
using System.Net.Sockets;
using System.Diagnostics;

using BidMessages;

namespace QuoteProviders
{
    /// <summary>
    /// Provides quote data through interaction with remote servers.
    /// </summary>
    public class TcpQuoteProvider : QuoteDataProvider
    {
        private Socket m_client;
        private IPEndPoint m_remoteEP;
        private string m_username;
        private string m_password;
        private byte[] m_heartbeatBytes;
        private byte[] m_buffer;
        private int m_readIndex;
        private int m_writeIndex;
        private uint m_lastReception;
        private uint m_lastHeartbeat;
        private int m_maxHeartbeatInterval;
        private int m_retryTimes;
        private Random m_rand = new Random();

        /// <summary>
        /// Initializes a new instance of the <c>TcpDataProvider</c> class with the remote server's address and credentials to login.
        /// </summary>
        /// <param name="address">the host name or IP address of the remote server.</param>
        /// <param name="port">the port number on the remote server.</param>
        /// <param name="username">the username for login.</param>
        /// <param name="password">the password for the account.</param>
        /// <exception cref="System.ArgumentNullException">The input address or username or password is null.</exception>
        public TcpQuoteProvider(string address, int port, string username, string password)
        {
            if (address == null || username == null || password == null)
            {
                throw new ArgumentNullException("the input address or username or password is null.");
            }

            IPAddress[] ipAddresses = Dns.GetHostAddresses(address);
            if (ipAddresses.Length == 0)
            {
                throw new ArgumentException("Unable to resolve the specified host name to an IP address.");
            }

            m_client = null;

            IPAddress ipAddress = ipAddresses[0];
            m_remoteEP = new IPEndPoint(ipAddress, port);

            m_username = username;
            m_password = password;
            HeartbeatMessage heartbeat = new HeartbeatMessage(m_username);
            m_heartbeatBytes = heartbeat.GetBytes();

            m_buffer = new byte[4 * 1024];
            m_readIndex = m_writeIndex = 0;
            m_lastReception = m_lastHeartbeat = 0;
            m_maxHeartbeatInterval = 0;
            m_retryTimes = 0;

            m_runByState = Create;
        }

        /// <summary>
        /// The provider's name.
        /// </summary>
        public override string ProviderName
        {
            get
            {
                return "TcpQuoteProvider";
            }
        }

        /// <summary>
        /// Creates a socket to be used to connect to the remote server.
        /// Goes to states <c>Connect</c> and <c>Close</c>.
        /// </summary>
        /// <returns>Time to wait till next state is run, in milliseconds.</returns>
        private int Create()
        {
            ChangeStatus(QuoteProviderStatus.Open);

            try
            {
                m_client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                m_client.ReceiveTimeout = 500;
                m_runByState = Connect;
            }
            catch (Exception ex)
            {
                OnErrorOccurred(ex, true);
                m_runByState = Close;
            }

            return 0;
        }

        /// <summary>
        /// Connects to the remote server.
        /// Goes to states <c>Create</c>, <c>Authenticate</c>, and <c>Close</c>.
        /// </summary>
        /// <returns>Time to wait till next state is run, in milliseconds.</returns>
        private int Connect()
        {
            try
            {
                Debug.Assert(m_remoteEP != null);
                m_client.Connect(m_remoteEP);
                m_runByState = Authenticate;
            }
            catch (SocketException se)
            {
                OnErrorOccurred(se, false);
                return RetryOrGiveup(m_status);
            }

            return 0;
        }

        /// <summary>
        /// Uses the credentials to authenticate with the remote server.
        /// Goes to states <c>Create</c>, <c>Receive</c>, and <c>Close</c>.
        /// </summary>
        /// <returns>Time to wait till next state is run, in milliseconds.</returns>
        private int Authenticate()
        {
            ChangeStatus(QuoteProviderStatus.Authenticate);

            try
            {
                SessionKeyRequestMessage sessionKeyRequest = new SessionKeyRequestMessage(m_username);
                int length = sessionKeyRequest.GetBytes(m_buffer, 0);
                int count = m_client.Send(m_buffer, 0, length, SocketFlags.None);

                count = m_client.Receive(m_buffer);
                length = IPAddress.NetworkToHostOrder(BitConverter.ToInt32(m_buffer, 0));
                SessionKeyReplyMessage sessionKeyReply = (SessionKeyReplyMessage)BidMessage.Create(FunctionCodes.SessionKeyReply, m_buffer, 0, length);
                uint sessionKey = sessionKeyReply.SessionKey;

                LoginRequestMessage LoginRequest = new LoginRequestMessage(m_username, m_password, sessionKey);
                length = LoginRequest.GetBytes(m_buffer, 0);
                count = m_client.Send(m_buffer, 0, length, SocketFlags.None);

                count = ReceiveMessage();
                length = IPAddress.NetworkToHostOrder(BitConverter.ToInt32(m_buffer, 0));
                LoginReplyMessage loginReply = (LoginReplyMessage)BidMessage.Create(FunctionCodes.LoginReply, m_buffer, 0, length);
                m_maxHeartbeatInterval = loginReply.MaxHeartbeatInterval;

                if (m_maxHeartbeatInterval > 0)
                {
                    m_retryTimes = 0;
                    m_readIndex = m_writeIndex = 0;
                    m_lastReception = m_lastHeartbeat = (uint)Environment.TickCount;
                    m_runByState = Receive;
                }
                else
                {
                    throw new Exception("Invalid login.");
                }
            }
            catch (SocketException se)
            {
                OnErrorOccurred(se, false);
                return RetryOrGiveup(m_status);
            }
            catch (Exception ex)
            {
                OnErrorOccurred(ex, true);
                m_runByState = Close;
            }

            return 0;
        }

        /// <summary>
        /// Receives data from the remote server and parses them for <c>QuoteMessage</c>s.
        /// Goes to states <c>Create</c> and <c>Close</c>.
        /// </summary>
        /// <returns>Time to wait till next state is run, in milliseconds.</returns>
        private int Receive()
        {
            ChangeStatus(QuoteProviderStatus.Read);

            try
            {
                try
                {
                    int count = m_client.Receive(m_buffer, m_writeIndex, m_buffer.Length - m_writeIndex, SocketFlags.None);
                    if (count <= 0)
                    {
                        m_runByState = Close;
                        throw new Exception("Connection closed by remote host.");
                    }
                    m_writeIndex += count;
                    m_lastReception = (uint)Environment.TickCount;

                    ParseMessages();
                }
                catch (SocketException se)
                {
                    if (se.SocketErrorCode == SocketError.TimedOut)
                    {
                        if (TicksSince(m_lastReception) >= m_maxHeartbeatInterval)
                        {
                            throw se;
                        }
                    }
                    else
                    {
                        throw se;
                    }
                }

                if (TicksSince(m_lastHeartbeat) >= 3000)
                {
                    m_client.Send(m_heartbeatBytes);
                    m_lastHeartbeat = (uint)Environment.TickCount;
                }
            }
            catch (SocketException se)
            {
                OnErrorOccurred(se, false);
                return RetryOrGiveup(m_status);
            }
            catch (Exception ex)
            {
                OnErrorOccurred(ex, true);
                m_runByState = Close;
            }

            return 0;
        }

        /// <summary>
        /// Closes the connection to the remote server.
        /// Goes to state <c>Idle</c>.
        /// </summary>
        /// <returns>Time to wait till next state is run, in milliseconds.</returns>
        private int Close()
        {
            ChangeStatus(QuoteProviderStatus.Close);

            try
            {
                if (m_client != null)
                {
                    m_client.Shutdown(SocketShutdown.Both);
                }
            }
            catch (Exception ex)
            {
                OnErrorOccurred(ex, false);
            }

            if (m_client != null)
            {
                m_client.Close();
            }

            m_runByState = Idle;
            ChangeStatus(QuoteProviderStatus.Inactive);
            return 0;
        }

        /// <summary>
        /// Receives a single message from the remote server.
        /// </summary>
        /// <returns>The number of bytes received.</returns>
        private int ReceiveMessage()
        {
            int count = 0;
            count = m_client.Receive(m_buffer, 0, BidMessage.HeaderLength, SocketFlags.None);
            // receive header first
            // determine length of body
            int bodyLength = Bytes.ToInt32(m_buffer, sizeof(int) + sizeof(ushort));
            // receive body
            count += m_client.Receive(m_buffer, BidMessage.HeaderLength, bodyLength, SocketFlags.None);

            return count;
        }

        /// <summary>
        /// Examines the entire buffer and parses all <c>QuoteMessage</c>s in it.
        /// </summary>
        private void ParseMessages()
        {
            while (m_writeIndex - m_readIndex > BidMessage.HeaderLength)
            {
                int length = 0;
                ushort funcCode = 0;
                int bodyLength = 0;

                for (; m_writeIndex - m_readIndex >= BidMessage.HeaderLength; m_readIndex++)
                {
                    int offset = m_readIndex;

                    length = m_buffer.ToInt32(offset);
                    offset += sizeof(int);

                    funcCode = m_buffer.ToUInt16(offset);
                    offset += sizeof(ushort);

                    bodyLength = m_buffer.ToInt32(offset);

                    if (length - bodyLength == BidMessage.HeaderLength)
                    {
                        break;
                    }
                }

                // loop guard is true iff the next message is successfully located
                // break if this fails or only part of next message is found
                if (m_writeIndex - m_readIndex < BidMessage.HeaderLength || m_writeIndex - m_readIndex < length)
                {
                    break;
                }

                if (funcCode == (ushort)FunctionCodes.Quote)
                {
                    QuoteMessage message = (QuoteMessage)BidMessage.Create(FunctionCodes.Quote, m_buffer, m_readIndex, length);
                    OnQuoteMessageReceived(message);
                }

                m_readIndex += length;
            }

            // incomplete message
            if (m_readIndex != 0)
            {
                Array.Copy(m_buffer, m_readIndex, m_buffer, 0, m_writeIndex - m_readIndex);
                m_writeIndex -= m_readIndex;
                m_readIndex = 0;
            }
        }

        /// <summary>
        /// Calculates the elapsed time since a given time.
        /// </summary>
        /// <param name="last">the given time to calculate elapsed time on.</param>
        /// <returns>The elapsed time.</returns>
        private static uint TicksSince(uint last)
        {
            return (uint)Environment.TickCount > last ? (uint)Environment.TickCount - last : (uint)Environment.TickCount - last + uint.MaxValue;
        }

        /// <summary>
        /// Tries to reconnect to the remote server, and gives up if trials exceeds a set limit.
        /// </summary>
        /// <returns>Time to sleep in milliseconds till executing next state.</returns>
        private int RetryOrGiveup(QuoteProviderStatus previous)
        {
            if (++m_retryTimes >= 3)
            {
                OnErrorOccurred(new Exception("Max number of reconnection trials reached."), true);
                m_runByState = Close;
            }

            Close(); // first, close
            m_runByState = Create; // then, re-create

            return (1000 + m_rand.Next(2000));
        }
    }
}
