namespace BidMessage
{
    /// <summary>
    /// Class <c>SessionKeyRequestMsg</c> models the session key request sent to server.
    /// </summary>
    class SessionKeyRequestMsg : ControlRequestMessage
    {
        protected string m_username;

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
        public override ushort Function
        {
            get
            {
                return (ushort)Messages.SessionKeyRequest;
            }
        }

        /// <value>
        /// Property <c>Username</c> represents the message's content: username.
        /// </value>
        public string Username
        {
            get { return m_username; }
        }

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
