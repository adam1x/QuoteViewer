namespace BidMessages
{
	// [Xu Linqiu] C#里已不再建议使用3-4个字母的缩写，且LoginRequestMsg的所有父类名字都是已Message结尾，而不是Msg
	//             此处为避免类名过长，也可以就叫做LoginRequest。
	/// <summary>
	/// Class <c>LoginRequestMsg</c> models the login request sent to server.
	/// </summary>
	public class LoginRequestMsg : ControlRequestMessage
    {
		// [Xu Linqiu] 应为private
		// [Xu Linqiu] C#里已不再建议使用3-4个字母的缩写，且veri非常用缩写
		protected string m_veriCode;

		// [Xu Linqiu] summary注释文字不当，contain这个词泄露了你的实现细节，且意思不对
		// [Xu Linqiu] (username, hostname) -> verification code，这个对使用者而言是不需要知道的细节，
		//             这应是LoginRequest的职责
		/// <summary>
		/// This constructor initializes the new <c>LoginRequestMsg</c> to contain a verification code.
		/// </summary>
		/// <param name="vericode">the verification code used in the login request.</param>
		public LoginRequestMsg(string vericode)
        {
            m_veriCode = vericode;
        }

        /// <value>
        /// Property <c>Function</c> represents the message's function code.
        /// </value>
        public override FunctionCodes Function
        {
            get
            {
                return FunctionCodes.LoginRequest;
            }
        }

		// [Xu Linqiu] 注释文字不当，对使用者而言，更有用的是这句话：the verification code used in the login request
		/// <value>
		/// Property <c>VeriCode</c> represents the message's content: verification code.
		/// </value>
		public string VeriCode
        {
            get { return m_veriCode; }
        }

        /// <summary>
        /// This method returns the string value that is the body of a <c>LoginRequestMsg</c> object.
        /// </summary>
        /// <returns>A string value representing the body.</returns>
        protected override string GetBody()
        {
            return m_veriCode;
        }
    }
}
