namespace BidMessages
{
	// [Xu Linqiu] C#里已不再建议使用3-4个字母的缩写，且HeartbeatMsg的所有父类名字都是已Message结尾，而不是Msg
	/// <summary>
	/// Class <c>HeartbeatMsg</c> models heartbeat messages sent to the remote server.
	/// </summary>
	public class HeartbeatMsg : ControlRequestMessage
    {
		// [Xu Linqiu] 应为private
		protected string m_username;

		// [Xu Linqiu] summary注释文字不当，contain这个词泄露了你的实现细节，且意思不对
		/// <summary>
		/// This constructor initializes the new <c>HeartbeatMsg</c> to contain the username <c>username</c>.
		/// </summary>
		/// <param name="username">the username of current user.</param>
		public HeartbeatMsg(string username)
        {
            m_username = username;
        }

        /// <value>
        /// Property <c>Function</c> represents the message's function code.
        /// </value>
        public override FunctionCodes Function
        {
            get
            {
                return FunctionCodes.Heartbeat;
            }
        }

		// [Xu Linqiu] 应提供username属性

        /// <summary>
        /// This method returns the string value that is the body of a <c>ControlReplyMessage</c> object.
        /// </summary>
        /// <returns>A string value representing the body, in this case <c>m_username</c>.</returns>
        protected override string GetBody()
        {
            return m_username;
        }
    }
}
