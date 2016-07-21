namespace BidMessages
{
	// [Xu Linqiu] C#里已不再建议使用3-4个字母的缩写，且SessionKeyReplyMsg的所有父类名字都是已Message结尾，而不是Msg
	//             此处为避免类名过长，也可以就叫做SessionKeyReply。
	/// <summary>
	/// Class <c>SessionKeyReplyMsg</c> models the server's response to a <c>SessionKeyRequestMsg</c>.
	/// </summary>
	public class SessionKeyReplyMsg : ControlReplyMessage
    {
		// [Xu Linqiu] 应为private
		protected uint m_sessionKey;

		// [Xu Linqiu] summary注释文字不当，contain这个词泄露了你的实现细节，且意思不对
		// [Xu Linqiu] 参数不当，从哪里开始读session key值，应是session key reply的实现细节
		/// <summary>
		/// This constructor initializes the new <c>SessionKeyReplyMsg</c> to contain the session key.
		/// </summary>
		/// <param name="bytes">the byte array that contains the session key sent from the server.</param>
		/// <param name="startIndex">the starting index to read the key.</param>
		public SessionKeyReplyMsg(byte[] bytes, int startIndex)
        {
            m_sessionKey = bytes.ToUInt32(startIndex);
        }

        /// <value>
        /// Property <c>Function</c> represents the message's function code.
        /// </value>
        public override FunctionCodes Function
        {
            get
            {
                return FunctionCodes.SessionKeyReply;
            }
        }

        /// <value>
        /// Property <c>SessionKey</c> represents the message's content: session key.
        /// </value>
        public uint SessionKey
        {
            get { return m_sessionKey; }
        }

        /// <summary>
        /// This method returns the uint value that is the body of a <c>SessionKeyReplyMsg</c> object.
        /// </summary>
        /// <returns>A uint value representing the body.</returns>
        protected override uint GetBody()
        {
            return m_sessionKey;
        }
    }
}
