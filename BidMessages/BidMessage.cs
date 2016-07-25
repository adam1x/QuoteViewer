using System;
using System.Text;
using System.Net;
using System.Diagnostics;

namespace BidMessages
{
    /// <summary>
    /// Class <c>BidMessage</c> models messages involved in bid.
    /// </summary>
    public abstract class BidMessage
    {
        /// <summary>
        /// This field represents the length of a message header.
        /// </summary>
        public static readonly int HeaderLength = sizeof(int) * 2 + sizeof(ushort);
        
        /// <summary>
        /// This field represents the text encoding scheme used in a message.
        /// </summary>
        public static readonly Encoding TextEncoding = Encoding.UTF8;

        /// <summary>
        /// This constructor initializes a new instance of the <c>BidMessage</c> class.
        /// </summary>
        public BidMessage()
        {
        }

        /// <summary>
        /// Property <c>Function</c> represents the message's function code.
        /// </summary>
        public abstract FunctionCodes Function { get; }

        #region Factory methods
        /// <summary>
        /// This factory method creates a <c>BidMessage</c> object with its encoding in a byte array.
        /// </summary>
        /// <param name="function">the message's function code.</param>
        /// <param name="message">the message encoded in a byte array.</param>
        /// <param name="startIndex">the start index in the byte array.</param>
        /// <param name="count">the actual length of message.</param>
        /// <returns>An initialized <c>BidMessage</c> object.</returns>
        /// <exception cref="System.NotSupportedException"><c>function</c> is not supported or the message is malformed.</exception>
        public static BidMessage Create(FunctionCodes function, byte[] message, int startIndex, int count)
        {
            BidMessage msg = null;
            switch (function)
            {
                case FunctionCodes.Quote:
                    msg = CreateQuoteMessage(message, startIndex, count);
                    break;

                case FunctionCodes.SessionKeyReply:
                    msg = new SessionKeyReplyMessage(message, startIndex);
                    break;

                case FunctionCodes.LoginReply:
                    msg = new LoginReplyMessage(message, startIndex);
                    break;

                default:
                    throw new NotSupportedException("Unsuppoted message function code.");
            }
            Debug.Assert(msg != null);
            return msg;
        }

        /// <summary>
        /// This factory method creates a <c>QuoteMessage</c> object with its encoding in a byte array.
        /// </summary>
        /// <param name="message">the message encoded in a byte array.</param>
        /// <param name="startIndex">the start index in the byte array.</param>
        /// <param name="count">the actual length of message.</param>
        /// <returns>An initialized <c>QuoteMessage</c> object.</returns>
        /// <exception cref="System.NotSupportedException">The message is of unsupported auction session or malformed.</exception>
        private static QuoteMessage CreateQuoteMessage(byte[] message, int startIndex, int count)
        {
            QuoteMessage msg = null;
            switch (QuoteMessage.PeekSession(message, startIndex))
            {
                case AuctionSessions.SessionA:
                    msg = new SessionAMessage(message, startIndex, count);
                    break;

                case AuctionSessions.SessionB:
                    msg = new SessionBMessage(message, startIndex, count);
                    break;

                case AuctionSessions.SessionC:
                    msg = new SessionCMessage(message, startIndex, count);
                    break;

                case AuctionSessions.SessionD:
                    msg = new SessionDMessage(message, startIndex, count);
                    break;

                case AuctionSessions.SessionE:
                    msg = new SessionEMessage(message, startIndex, count);
                    break;

                case AuctionSessions.SessionF:
                    msg = new SessionFMessage(message, startIndex, count);
                    break;

                case AuctionSessions.SessionG:
                    msg = new SessionGMessage(message, startIndex, count);
                    break;

                case AuctionSessions.SessionH:
                    msg = new SessionHMessage(message, startIndex, count);
                    break;

                default:
                    throw new NotSupportedException("Unsupported auction session.");
            }
            Debug.Assert(msg != null);
            return msg;
        }
        #endregion

        #region Serialize
        /// <summary>
        /// This method serializes this <c>BidMessage</c> object into a byte array.
        /// </summary>
        /// <param name="target">the target byte array.</param>
        /// <param name="startIndex">the index to start writing.</param>
        /// <returns>The number of bytes written into the array.</returns>
        public int GetBytes(byte[] target, int startIndex)
        {
            int bodyLength = GetBodyBytes(target, startIndex + HeaderLength);
            int length = bodyLength + HeaderLength;

            IPAddress.HostToNetworkOrder(length).GetBytes(target, startIndex);
            startIndex += sizeof(int);

            Bytes.HostToNetworkOrder((ushort)Function).GetBytes(target, startIndex);
            startIndex += sizeof(ushort);

            IPAddress.HostToNetworkOrder(bodyLength).GetBytes(target, startIndex);
            startIndex += sizeof(int);
            Debug.Assert(startIndex == HeaderLength);

            return length;
        }

        /// <summary>
        /// This method serializes this <c>BidMessage</c> object into a byte array.
        /// </summary>
        /// <returns>The output array.</returns>
        public byte[] GetBytes()
        {
            byte[] target = new byte[HeaderLength + GetBodyLength()];
            GetBytes(target, 0);
            return target;
        }

        /// <summary>
        /// This method encodes the body of a <c>BidMessage</c> object into the target byte array.
        /// </summary>
        /// <param name="target">the target byte array.</param>
        /// <param name="offset">the position to start writing.</param>
        /// <returns>The number of bytes written into <c>bytes</c>.</returns>
        protected abstract int GetBodyBytes(byte[] target, int offset);

        /// <summary>
        /// Gets the length of the body of this message.
        /// </summary>
        /// <returns>The length of the body of this message.</returns>
        protected abstract int GetBodyLength();
        #endregion

        /// <summary>
        /// This method peeks at a message's function code.
        /// </summary>
        /// <param name="message">the message in a byte array.</param>
        /// <param name="offset">the position where message begins.</param>
        /// <returns>the message's function code.</returns>
        protected static FunctionCodes PeekFunctionCode(byte[] message, int offset)
        {
            return (FunctionCodes)message.ToUInt16(offset + sizeof(uint));
        }
    }
}
