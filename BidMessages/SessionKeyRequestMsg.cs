namespace BidMessages
{
	// [Xu Linqiu] C#里已不再建议使用3-4个字母的缩写，且SessionKeyRequestMsg的所有父类名字都是已Message结尾，而不是Msg
	//             此处为避免类名过长，也可以就叫做SessionKeyRequest。
	/// <summary>
	/// Class <c>SessionKeyRequestMsg</c> models the session key request sent to server.
	/// </summary>
	public class SessionKeyRequestMsg : ControlRequestMessage
    {
		// [Xu Linqiu] 应为private
		protected string m_username;

		// [Xu Linqiu] summary注释文字不当，contain这个词泄露了你的实现细节，且意思不对
		// [Xu Linqiu] 参数注释错误
		/// <summary>
		/// This constructor initializes the new <c>SessionKeyRequestMsg</c> to contain a username.
		/// </summary>
		/// <param name="vericode">the username used in the session key request.</param>
		public SessionKeyRequestMsg(string username)
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
                return FunctionCodes.SessionKeyRequest;
            }
        }

		// [Xu Linqiu] 注释文字不当，使用者不会关心username是不是the message's content；
		//             对使用者而言，更有用的是这句话：the username used in the session key request
		/// <value>
		/// Property <c>Username</c> represents the message's content: username.
		/// </value>
		public string Username
        {
            get { return m_username; }
        }

		// [Xu Linqiu] 注释文字错误
        /// <summary>
        /// This method returns the string value that is the body of a <c>LoginRequestMsg</c> object.
        /// </summary>
        /// <returns>A string value representing the body.</returns>
        protected override string GetBody()
        {
            return m_username;
        }
    }
}
