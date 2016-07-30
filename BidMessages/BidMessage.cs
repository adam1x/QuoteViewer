using System;
using System.Text;
using System.Net;
using System.Diagnostics;

namespace BidMessages
{
    /// <summary>
    /// Models messages involved in bid.
    /// </summary>
    public abstract class BidMessage
    {
        /// <summary>
        /// The length of a message header.
        /// </summary>
        public const int HeaderLength = sizeof(int) * 2 + sizeof(ushort);

        /// <summary>
        /// The minimal length of a message.
        /// </summary>
        public const int MinLength = HeaderLength + 1;
        
        /// <summary>
        /// The text encoding scheme used in a message.
        /// </summary>
        public static readonly Encoding TextEncoding = Encoding.UTF8;

        /// <summary>
        /// Initializes a new instance of the <c>BidMessage</c> class.
        /// </summary>
        public BidMessage()
        {
        }

        /// <summary>
        /// Represents the message's function code.
        /// </summary>
        public abstract FunctionCodes Function { get; }

        #region Factory methods
        /// <summary>
        /// Creates a <c>BidMessage</c> object with its encoding in a byte array.
        /// </summary>
        /// <param name="function">the message's function code.</param>
        /// <param name="message">the message encoded in a byte array.</param>
        /// <param name="offset">the start index in the byte array.</param>
        /// <param name="count">the actual length of message.</param>
        /// <returns>An initialized <c>BidMessage</c> object.</returns>
        /// <exception cref="System.NotSupportedException"><c>function</c> is not supported or the message is malformed.</exception>
        public static BidMessage Create(FunctionCodes function, byte[] message, int offset, int count)
        {
            BidMessage result = null;

            switch (function)
            {
                case FunctionCodes.Quote:
                    result = CreateQuoteMessage(message, offset, count);
                    break;

                case FunctionCodes.SessionKeyReply:
                    result = new SessionKeyReplyMessage(message, offset);
                    break;

                case FunctionCodes.LoginReply:
                    result = new LoginReplyMessage(message, offset);
                    break;

                default:
                    throw new NotSupportedException("Unsuppoted message function code.");
            }

            Debug.Assert(result != null);
            return result;
        }

        /// <summary>
        /// Creates a <c>QuoteMessage</c> object with its encoding in a byte array.
        /// </summary>
        /// <param name="message">the message encoded in a byte array.</param>
        /// <param name="offset">the start index in the byte array.</param>
        /// <param name="count">the actual length of message.</param>
        /// <returns>An initialized <c>QuoteMessage</c> object.</returns>
        /// <exception cref="System.NotSupportedException">The message is of unsupported auction session or malformed.</exception>
        private static QuoteMessage CreateQuoteMessage(byte[] message, int offset, int count)
        {
            QuoteMessage result = null;

            switch (QuoteMessage.PeekSession(message, offset))
            {
                case AuctionSessions.SessionA:
                    result = new SessionAMessage(message, offset, count);
                    break;

                case AuctionSessions.SessionB:
                    result = new SessionBMessage(message, offset, count);
                    break;

                case AuctionSessions.SessionC:
                    result = new SessionCMessage(message, offset, count);
                    break;

                case AuctionSessions.SessionD:
                    result = new SessionDMessage(message, offset, count);
                    break;

                case AuctionSessions.SessionE:
                    result = new SessionEMessage(message, offset, count);
                    break;

                case AuctionSessions.SessionF:
                    result = new SessionFMessage(message, offset, count);
                    break;

                case AuctionSessions.SessionG:
                    result = new SessionGMessage(message, offset, count);
                    break;

                case AuctionSessions.SessionH:
                    result = new SessionHMessage(message, offset, count);
                    break;

                default:
                    throw new NotSupportedException("Unsupported auction session.");
            }

            Debug.Assert(result != null);
            return result;
        }
        #endregion

        #region Serialize
        /// <summary>
        /// Serializes this <c>BidMessage</c> object into a byte array.
        /// </summary>
        /// <param name="target">the target byte array.</param>
        /// <param name="offset">the index to start writing.</param>
        /// <returns>The number of bytes written into the array.</returns>
        public int GetBytes(byte[] target, int offset)
        {
            int bodyLength = GetBodyBytes(target, offset + HeaderLength);
            int length = bodyLength + HeaderLength;

            offset += IPAddress.HostToNetworkOrder(length).GetBytes(target, offset);
            offset += Bytes.HostToNetworkOrder((ushort)Function).GetBytes(target, offset);
            offset += IPAddress.HostToNetworkOrder(bodyLength).GetBytes(target, offset);
            Debug.Assert(offset == HeaderLength);

            return length;
        }

        /// <summary>
        /// Serializes this <c>BidMessage</c> object into a byte array.
        /// </summary>
        /// <returns>The output array.</returns>
        public byte[] GetBytes()
        {
            byte[] target = new byte[HeaderLength + GetBodyLength()];
            GetBytes(target, 0);
            return target;
        }

        /// <summary>
        /// Encodes the body of a <c>BidMessage</c> object into the target byte array.
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
        /// Peeks at a message's function code.
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
