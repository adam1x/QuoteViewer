using System.Text;
using System.Security.Cryptography;
using System.Diagnostics;

namespace BidMessages
{
    /// <summary>
    /// Class <c>LoginRequestMessage</c> models the login request sent to server.
    /// </summary>
    public class LoginRequestMessage : ControlRequestMessage
    {
        private string m_verificatonCode;

        /// <summary>
        /// This constructor initializes a new instance of the <c>LoginRequest</c> class with the given verification code.
        /// </summary>
        /// <param name="username">the username for the remote server.</param>
        /// <param name="password">the password to the account.</param>
        /// <param name="sessionKey">the session key for the current login session.</param>
        public LoginRequestMessage(string username, string password, uint sessionKey)
        {
            m_verificatonCode = CreateVerificationCode(username, password, sessionKey);
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
        /// Property <c>VerificationCode</c> represents the verification code used in login request.
        /// </value>
        public string VerificationCode
        {
            get { return m_verificatonCode; }
        }

        /// <summary>
        /// This method creates a verification code with the given username, password, and session key.
        /// </summary>
        /// <param name="sessionKey">the session key used to create the verification code.</param>
        /// <returns>The verification code.</returns>
        private string CreateVerificationCode(string username, string password, uint sessionKey)
        {
            byte[] input, output;

            input = TextEncoding.GetBytes(username + sessionKey.ToString("x8") + password);

            using (MD5 md5 = MD5.Create())
            {
                output = md5.ComputeHash(input);
                Debug.Assert(output != null && output.Length > 0);
            }

            StringBuilder builder = new StringBuilder(32);
            foreach (byte b in output)
            {
                builder.Append(b.ToString("x2"));
            }

            return builder.ToString();
        }

        /// <summary>
        /// This method encodes the body of a <c>LoginRequestMessage</c> object into the target byte array.
        /// </summary>
        /// <param name="bytes">the target byte array.</param>
        /// <param name="offset">the position to start writing.</param>
        /// <returns>The number of bytes written into <c>bytes</c>.</returns>
        protected override uint WriteBody(byte[] bytes, int offset)
        {
            string body = m_verificatonCode;
            return (uint)TextEncoding.GetBytes(body, 0, body.Length, bytes, offset);
        }

        /// <summary>
        /// This method gets a string representation for the specified <c>LoginRequestMessage</c> instance.
        /// </summary>
        /// <returns>A string that contains the message type and verificaton code.</returns>
        public override string ToString()
        {
            return "Login request: " + m_verificatonCode;
        }
    }
}
