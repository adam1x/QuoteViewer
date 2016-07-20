namespace BidMessages
{
    /// <summary>
    /// Class <c>LoginRequestMsg</c> models the login request sent to server.
    /// </summary>
    public class LoginRequestMsg : ControlRequestMessage
    {
        protected string m_veriCode;

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
