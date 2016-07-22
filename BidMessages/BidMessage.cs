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
        public static readonly int HeaderLength = sizeof(uint) * 2 + sizeof(ushort);
        public static readonly Encoding textEncoding = Encoding.UTF8; // [Xu Linqiu] 首字母应大写

		// [Xu Linqiu] 绝对不应出现这样的注释！ctor的实现是你的内部细节，你怎么做都行，但就是不能公开出来。
        /// <summary>
        /// This constructor doesn't do anything specific.
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
        /// <param name="body">the message's body encoded in a byte array.</param>
        /// <param name="startIndex">the start index in the byte array.</param>
        /// <param name="count">the actual length of body.</param>
        /// <returns>An initialized <c>BidMessage</c> object.</returns>
		/// <exception cref="System.NotImplementedException"></exception>
        public static BidMessage Create(FunctionCodes function, byte[] body, int startIndex, int count)
        {
            BidMessage msg = null;

			// [Xu Linqiu] switch的每个分支间应插入空行
            switch (function)
            {
                case FunctionCodes.Quote:
                    msg = CreateQuoteMessage(body, startIndex, count);
                    break;

                case FunctionCodes.SessionKeyReply:
                    msg = new SessionKeyReplyMsg(body, startIndex);
                    break;

                case FunctionCodes.LoginReply:
                    msg = new LoginReplyMsg(body, startIndex);
                    break;

                default:
                    throw new NotSupportedException("Unsuppoted message function code.");
            }

			Debug.Assert(msg != null);
            return msg;
        }

		// [Xu Linqiu] 说明：private方法是不会被外部使用的方法，可以不写方法注释，但重要方法(如下面这个方法)，建议写。

        /// <summary>
        /// This factory method creates a <c>QuoteMessage</c> object with its encoding in a byte array.
        /// </summary>
        /// <param name="body">the message's body encoded in a byte array.</param>
        /// <param name="startIndex">the start index in the byte array.</param>
        /// <param name="count">the actual length of body.</param>
        /// <returns>An initialized <c>QuoteMessage</c> object.</returns>
        private static QuoteMessage CreateQuoteMessage(byte[] body, int startIndex, int count)
        {
			QuoteMessage msg = null;

			// [Xu Linqiu] switch的每个分支间应插入空行
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

            uint bodyLength = WriteBody(result);
            uint length = bodyLength + (uint)HeaderLength;

			// [Xu Linqiu]
			//Bytes.HostToNetworkOrder(length).GetBytes(result, 0);
			//Bytes.HostToNetworkOrder((ushort)Function).GetBytes(result, sizeof(uint));
			//Bytes.HostToNetworkOrder(bodyLength).GetBytes(result, sizeof(uint) + sizeof(ushort));

			int offset = 0;
			Bytes.HostToNetworkOrder(length).GetBytes(result, offset);
			offset += sizeof(uint);

			Bytes.HostToNetworkOrder((ushort)Function).GetBytes(result, offset);
			offset += sizeof(ushort);

			Bytes.HostToNetworkOrder(bodyLength).GetBytes(result, sizeof(uint) + sizeof(ushort));
			offset += sizeof(uint);
			Debug.Assert(offset == HeaderLength);

			bytesWritten = (int)length;
            return result;
        }

		// [Xu Linqiu] 应增加一个offset参数，指明从哪里开始写入body，各个子类的具体实现中就不需要关心这个细节 - 一致性原则
        /// <summary>
        /// This method encodes the body of a <c>BidMessage</c> object into the target byte array.
        /// </summary>
        /// <param name="bytes">the target byte array.</param>
        /// <returns>The number of bytes written into <c>bytes</c>.</returns>
        protected abstract uint WriteBody(byte[] bytes);
        #endregion
    }
}
