using System;
using System.Text;
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
        public static readonly int HeaderLength = sizeof(uint) * 2 + sizeof(ushort);
        
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

        /// <value>
        /// Property <c>Function</c> represents the message's function code.
        /// </value>
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
                    msg = new SessionKeyReplyMessage(message);
                    break;

                case FunctionCodes.LoginReply:
                    msg = new LoginReplyMessage(message);
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
        /// This method serializes a <c>BidMessage</c> object into a byte array.
        /// </summary>
        /// <param name="bytesWritten">the actual length of the encoded <c>BidMessage</c>.</param>
        /// <returns>The byte encoding of a <c>BidMessage</c> object in an array.</returns>
        public byte[] ToBytes(out int bytesWritten)
        {
            byte[] result = new byte[2 * 1024];

            uint bodyLength = WriteBody(result, HeaderLength);
            uint length = bodyLength + (uint)HeaderLength;

            int offset = 0;
            Bytes.HostToNetworkOrder(length).GetBytes(result, offset);
            offset += sizeof(uint);
            
            Bytes.HostToNetworkOrder((ushort)Function).GetBytes(result, offset);
            offset += sizeof(ushort);
            
            Bytes.HostToNetworkOrder(bodyLength).GetBytes(result, offset);
            offset += sizeof(uint);
            Debug.Assert(offset == HeaderLength);

            bytesWritten = (int)length;
            return result;
        }

        /// <summary>
        /// This method encodes the body of a <c>BidMessage</c> object into the target byte array.
        /// </summary>
        /// <param name="bytes">the target byte array.</param>
        /// <param name="offset">the position to start writing.</param>
        /// <returns>The number of bytes written into <c>bytes</c>.</returns>
        protected abstract uint WriteBody(byte[] bytes, int offset);
        #endregion
    }
}
