using System;
using System.Text;

namespace BidMessage
{
    /// <summary>
    /// Class <c>BidMessage</c> models messages involved in bid.
    /// </summary>
    public abstract class BidMessage
    {
        public static readonly int HeaderLength = sizeof(uint) * 2 + sizeof(ushort);
        public static readonly Encoding textEncoding = Encoding.UTF8;

        /// <summary>
        /// This constructor doesn't do anything specific.
        /// </summary>
        public BidMessage()
        {
        }

        /// <value>
        /// Property <c>Function</c> represents the message's function code.
        /// </value>
        public abstract ushort Function { get; }

        #region Factory methods
        /// <summary>
        /// This factory method creates a <c>BidMessage</c> object with its encoding in a byte array.
        /// </summary>
        /// <param name="function">the message's function code.</param>
        /// <param name="body">the message's body encoded in a byte array.</param>
        /// <param name="startIndex">the start index in the byte array.</param>
        /// <param name="count">the actual length of body.</param>
        /// <returns>An initialized <c>BidMessage</c> object.</returns>
        public static BidMessage Create(Messages function, byte[] body, int startIndex, int count)
        {
            BidMessage msg;
            switch (function)
            {
                case Messages.Quote:
                    msg = CreateQuoteMessage(body, startIndex, count);
                    break;
                case Messages.SessionKeyReply:
                    msg = new SessionKeyReplyMsg(body, startIndex);
                    break;
                case Messages.LoginReply:
                    msg = new LoginReplyMsg(body, startIndex);
                    break;
                default:
                    throw new NotSupportedException("Unsuppoted message function code.");
            }
            return msg;
        }

        /// <summary>
        /// This factory method creates a <c>QuoteMessage</c> object with its encoding in a byte array.
        /// </summary>
        /// <param name="body">the message's body encoded in a byte array.</param>
        /// <param name="startIndex">the start index in the byte array.</param>
        /// <param name="count">the actual length of body.</param>
        /// <returns>An initialized <c>QuoteMessage</c> object.</returns>
        private static QuoteMessage CreateQuoteMessage(byte[] body, int startIndex, int count)
        {
            QuoteMessage msg;
            switch (QuoteMessage.PeekSession(body, startIndex))
            {
                case AuctionSessions.SessionA:
                    msg = new SessionAMsg(body, startIndex, count);
                    break;
                case AuctionSessions.SessionB:
                    msg = new SessionBMsg(body, startIndex, count);
                    break;
                case AuctionSessions.SessionC:
                    msg = new SessionCMsg(body, startIndex, count);
                    break;
                case AuctionSessions.SessionD:
                    msg = new SessionDMsg(body, startIndex, count);
                    break;
                case AuctionSessions.SessionE:
                    msg = new SessionEMsg(body, startIndex, count);
                    break;
                case AuctionSessions.SessionF:
                    msg = new SessionFMsg(body, startIndex, count);
                    break;
                case AuctionSessions.SessionG:
                    msg = new SessionGMsg(body, startIndex, count);
                    break;
                case AuctionSessions.SessionH:
                    msg = new SessionHMsg(body, startIndex, count);
                    break;
                default:
                    throw new NotSupportedException("Unsupported message session.");
            }
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

            uint bodyLength = WriteBody(result);
            uint length = bodyLength + (uint)HeaderLength;
            Bytes.HostToNetworkOrder(length).GetBytes(result, 0);
            Bytes.HostToNetworkOrder(Function).GetBytes(result, sizeof(uint));
            Bytes.HostToNetworkOrder(bodyLength).GetBytes(result, sizeof(uint) + sizeof(ushort));

            bytesWritten = (int)length;
            return result;
        }

        /// <summary>
        /// This method encodes the body of a <c>BidMessage</c> object into the target byte array.
        /// </summary>
        /// <param name="bytes">the target byte array.</param>
        /// <returns>The number of bytes written into <c>bytes</c>.</returns>
        protected abstract uint WriteBody(byte[] bytes);
        #endregion
    }
}
