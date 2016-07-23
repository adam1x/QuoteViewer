using System;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Threading;
using System.Diagnostics;

using BidMessages;

namespace QuoteProviders
{
    /// <summary>
    /// Class <c>TcpQuoteProvider</c> models a parser that interacts with a remote server to retrieve and process quote data.
    /// </summary>
    public class TcpQuoteProvider : QuoteDataProvider
    {
        private Socket m_client;
        private IPEndPoint m_remoteEP;
        private string m_username;
        private string m_password;
        private byte[] m_buffer;
        private int m_readIndex;
        private int m_writeIndex;
        private uint m_lastReception;
        private uint m_lastHeartbeat;
        private uint m_maxHeartbeatInterval;
        private byte[] m_heartbeatBytes;
        private int m_heartbeatLength;
        private uint m_trials;
        private Random m_rand = new Random();

        /// <summary>
        /// This constructor initializes the new <c>TcpDataProvider</c> object with the remote server's address and credentials to login.
        /// </summary>
        /// <param name="ipAddressString">the IP address of the remote server.</param>
        /// <param name="port">the port number on the remote server.</param>
        /// <param name="username">the username for login.</param>
        /// <param name="password">the password for the account.</param>
        public TcpQuoteProvider(string ipAddressString, int port, string username, string password)
        {
            try
            {
                IPAddress ipAddress = IPAddress.Parse(ipAddressString);
                m_remoteEP = new IPEndPoint(ipAddress, 8301);
                m_username = username;
                m_password = password;
                m_state = Create;
            }
            catch (Exception ex)
            {
                OnErrorOccurred(ex, true);
                m_state = Close;
            }

            m_buffer = new byte[800 * 1024]; // longest msg < 800 bytes
            m_readIndex = m_writeIndex = 0;
            m_listeners = new List<IQuoteDataListener>();
            m_trials = 0;
        }

        /// <summary>
        /// This constructor is an overload of <c>public TcpDataProvider(string ipAddressString, int port, string username, string password)</c>.
        /// It initializes the new <c>TcpDataProvider</c> object with credentials to login on a default server.
        /// </summary>
        /// <param name="username">the username for login.</param>
        /// <param name="password">the password for the account.</param>
        public TcpQuoteProvider(string username, string password) : this("180.166.86.198", 8301, username, password)
        {
        }

        /// <value>
        /// Property <c>ProviderName</c> represents the provider's name.
        /// </value>
        public override string ProviderName
        {
            get { return "TcpQuoteProvider"; }
        }

        /// <summary>
        /// This method is used to run this parser.
        /// </summary>
        public override void Run()
        {
            m_state();
        }

        /// <summary>
        /// This method signifies the <c>Create</c> state for <c>TcpDataProvider</c>.
        /// At this state, the provider creates a socket to be used to connect to the remote server.
        /// It can go to states <c>Connect</c> and <c>Close</c>.
        /// </summary>
        private void Create()
        {
            m_status = QuoteProviderStatus.TcpCreate;

            try
            {
                m_client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                m_client.ReceiveTimeout = 500;
                m_state = Connect;
            }
            catch (Exception ex)
            {
                OnErrorOccurred(ex, true);
                m_state = Close;
            }
        }

        /// <summary>
        /// This method signifies the <c>Connect</c> state for <c>TcpDataProvider</c>.
        /// At this state, the provider connects to the remote server.
        /// It can go to states <c>Create</c>, <c>Authenticate</c>, and <c>Close</c>.
        /// </summary>
        private void Connect()
        {
            m_status = QuoteProviderStatus.TcpConnect;

            try
            {
                Debug.Assert(m_remoteEP != null);
                m_client.Connect(m_remoteEP);
                m_state = Authenticate;
                OnStatusChanged(m_status, QuoteProviderStatus.TcpAuthenticate);
            }
            catch (SocketException se)
            {
                OnErrorOccurred(se, false);
                RetryOrGiveup(m_status);
            }
        }

        /// <summary>
        /// This method signifies the <c>Authenticate</c> state for <c>TcpDataProvider</c>.
        /// At this state, the provider uses the credentials to authenticate with the remote server.
        /// It can go to states <c>Create</c>, <c>InitReceive</c>, and <c>Close</c>.
        /// </summary>
        private void Authenticate()
        {
            m_status = QuoteProviderStatus.TcpAuthenticate;

            try
            {
                ControlMessage cmsg = new SessionKeyRequestMessage(m_username);
                int cmsgLength;
                byte[] send = cmsg.ToBytes(out cmsgLength);
                int count = m_client.Send(send, cmsgLength, SocketFlags.None);

                count = m_client.Receive(m_buffer);
                uint length = Bytes.NetworkToHostOrder(BitConverter.ToUInt32(m_buffer, 0));
                cmsg = (ControlMessage)BidMessage.Create(FunctionCodes.SessionKeyReply, m_buffer, 0, (int)length);
                uint sessionKey = ((SessionKeyReplyMessage)cmsg).SessionKey;

                cmsg = new LoginRequestMessage(m_username, m_password, sessionKey);
                count = m_client.Send(cmsg.ToBytes(out cmsgLength), cmsgLength, SocketFlags.None);

                count = ReceiveMessage();
                length = Bytes.NetworkToHostOrder(BitConverter.ToUInt32(m_buffer, 0));
                cmsg = (ControlMessage)BidMessage.Create(FunctionCodes.LoginReply, m_buffer, 0, (int)length);
                m_maxHeartbeatInterval = ((LoginReplyMessage)cmsg).MaxHeartbeatInterval;

                if (m_maxHeartbeatInterval > 0)
                {
                    m_trials = 0;
                    m_state = InitReceive;
                    OnStatusChanged(m_status, QuoteProviderStatus.TcpInitReceive);
                }
                else
                {
                    throw new ApplicationException("Invalid login.");
                }
            }
            catch (SocketException se)
            {
                OnErrorOccurred(se, false);
                RetryOrGiveup(m_status);
            }
            catch (Exception ex)
            {
                OnErrorOccurred(ex, true);
                m_state = Close;
            }
        }

        /// <summary>
        /// This method signifies the <c>InitReceive</c> state for <c>TcpDataProvider</c>.
        /// At this state, the provider prepares for the <c>Receive</c> state.
        /// It can go to states <c>Create</c>, <c>Receive</c>, and <c>Close</c>.
        /// </summary>
        private void InitReceive()
        {
            m_status = QuoteProviderStatus.TcpInitReceive;

            m_readIndex = 0;
            m_writeIndex = 0;
            m_lastReception = (uint)Environment.TickCount;
            m_lastHeartbeat = (uint)Environment.TickCount;

            ControlMessage pmsg = new HeartbeatMessage(m_username);
            m_heartbeatBytes = pmsg.ToBytes(out m_heartbeatLength);

            m_state = Receive;
        }

        /// <summary>
        /// This method signifies the <c>Receive</c> state for <c>TcpDataProvider</c>.
        /// At this state, the provider receives data from the remote server and parses them for <c>QuoteMessage</c>s.
        /// It can go to states <c>Create</c> and <c>Close</c>.
        /// </summary>
        private void Receive()
        {
            m_status = QuoteProviderStatus.TcpReceive;

            try
            {
                try
                {
                    int count = m_client.Receive(m_buffer, m_writeIndex, m_buffer.Length - m_writeIndex, SocketFlags.None);
                    if (count <= 0)
                    {
                        m_state = Close;
                        throw new ApplicationException("Connection closed by remote host.");
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
                    m_client.Send(m_heartbeatBytes, m_heartbeatLength, SocketFlags.None);
                    m_lastHeartbeat = (uint)Environment.TickCount;
                }
            }
            catch (SocketException se)
            {
                OnErrorOccurred(se, false);
                RetryOrGiveup(m_status);
            }
            catch (Exception ex)
            {
                OnErrorOccurred(ex, true);
                m_state = Close;
            }
        }

        /// <summary>
        /// This method signifies the <c>Close</c> state for <c>TcpDataProvider</c>.
        /// At this state, the provider closes the connection to the remote server.
        /// </summary>
        private void Close()
        {
            m_status = QuoteProviderStatus.TcpClose;

            try
            {
                m_client.Shutdown(SocketShutdown.Both);
            }
            catch (Exception ex)
            {
                OnErrorOccurred(ex, false);
            }

            m_client.Close();
            m_state = null;
        }

        /// <summary>
        /// This method receives a single message from the remote server.
        /// </summary>
        /// <returns>The number of bytes received.</returns>
        private int ReceiveMessage()
        {
            int count = 0;
            count = m_client.Receive(m_buffer, 0, BidMessage.HeaderLength, SocketFlags.None);
            // receive header first
            // determine length of body
            uint bodyLength = Bytes.NetworkToHostOrder(BitConverter.ToUInt32(m_buffer, sizeof(uint) + sizeof(ushort)));
            // receive body
            count += m_client.Receive(m_buffer, BidMessage.HeaderLength, (int)bodyLength, SocketFlags.None);

            return count;
        }

        /// <summary>
        /// This method examines the entire buffer and parses all <c>QuoteMessage</c>s in it.
        /// </summary>
        private void ParseMessages()
        {
            while (m_writeIndex - m_readIndex > BidMessage.HeaderLength)
            {
                uint length = 0;
                ushort funcCode = 0;
                uint bodyLength = 0;

                for (; m_writeIndex - m_readIndex >= BidMessage.HeaderLength; m_readIndex++)
                {
                    length = Bytes.ToUInt32(m_buffer, m_readIndex);
                    funcCode = Bytes.ToUInt16(m_buffer, m_readIndex + sizeof(uint));
                    bodyLength = Bytes.ToUInt32(m_buffer, m_readIndex + sizeof(uint) + sizeof(ushort));

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
                    OnMessageParsed((QuoteMessage)BidMessage.Create(FunctionCodes.Quote, m_buffer, m_readIndex, (int)length));
                }

                m_readIndex += (int)length;
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
        /// This method calculates the elapsed time since a given time.
        /// </summary>
        /// <param name="last">the given time to calculate elapsed time on.</param>
        /// <returns>The elapsed time.</returns>
        private static uint TicksSince(uint last)
        {
            return (uint)Environment.TickCount > last ? (uint)Environment.TickCount - last : (uint)Environment.TickCount - last + uint.MaxValue;
        }

        /// <summary>
        /// This method tries to reconnect to the remote server, and gives up if trials exceeds a set limit.
        /// </summary>
        private void RetryOrGiveup(QuoteProviderStatus previous)
        {
            if (++m_trials >= 3)
            {
                OnErrorOccurred(new Exception("Max number of reconnection trials reached.\nExiting..."), true);
                m_state = Close;
            }
            Close(); // first, close
            m_state = Create; // then, recreate
            Thread.Sleep(1000 + m_rand.Next(2000));
            OnStatusChanged(previous, QuoteProviderStatus.TcpCreate);
        }
    }
}
