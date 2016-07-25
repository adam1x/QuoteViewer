using System;

namespace BidMessages
{
    /// <summary>
    /// Class <c>SessionKeyRequestMessage</c> models the session key request sent to server.
    /// </summary>
    public class SessionKeyRequestMessage : ControlRequestMessage
    {
        private string m_username;

        /// <summary>
        /// This constructor initializes a new instance of the <c>SessionKeyRequestMessage</c> class with the given username.
        /// </summary>
        /// <param name="username">the username used in the session key request.</param>
        /// <exception cref="System.ArgumentNullException">The input byte array is null.</exception>
        public SessionKeyRequestMessage(string username)
        {
            if (username == null)
            {
                throw new ArgumentNullException();
            }
            m_username = username;
        }

        /// <summary>
        /// Property <c>Function</c> represents the message's function code.
        /// </summary>
        public override FunctionCodes Function
        {
            get
            {
                return FunctionCodes.SessionKeyRequest;
            }
        }

        /// <summary>
        /// Property <c>Username</c> represents the username used in the session key request.
        /// </summary>
        public string Username
        {
            get { return m_username; }
        }

        /// <summary>
        /// This method encodes the body of a <c>SessionKeyRequestMessage</c> object into the target byte array.
        /// </summary>
        /// <param name="target">the target byte array.</param>
        /// <param name="offset">the position to start writing.</param>
        /// <returns>The number of bytes written into <c>bytes</c>.</returns>
        protected override int GetBodyBytes(byte[] target, int offset)
        {
            string body = m_username;
            return TextEncoding.GetBytes(body, 0, body.Length, target, offset);
        }

        /// <summary>
        /// Gets the length of the body of this message.
        /// </summary>
        /// <returns>The length of the body of this message.</returns>
        protected override int GetBodyLength()
        {
            return TextEncoding.GetByteCount(m_username);
        }

        /// <summary>
        /// This method gets a string representation for the specified <c>SessionKeyRequestMessage</c> instance.
        /// </summary>
        /// <returns>A string that contains the message type and username.</returns>
        public override string ToString()
        {
            return string.Format("{0}<{1}>", GetType().Name, m_username);
        }
    }
}
