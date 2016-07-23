namespace BidMessages
{
    /// <summary>
    /// Class <c>HeartbeatMessage</c> models heartbeat messages sent to the remote server.
    /// </summary>
    public class HeartbeatMessage : ControlRequestMessage
    {
        private string m_username;

        /// <summary>
        /// This constructor initializes a new instance of the <c>HeartbeatMessage</c> class with the given username.
        /// </summary>
        /// <param name="username">the username of current user.</param>
        public HeartbeatMessage(string username)
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

        /// <value>
        /// Property <c>Username</c> represents username used in heartbeat message.
        /// </value>
        public string Username
        {
            get
            {
                return m_username;
            }
        }

        /// <summary>
        /// This method encodes the body of a <c>HeartbeatMessage</c> object into the target byte array.
        /// </summary>
        /// <param name="bytes">the target byte array.</param>
        /// <param name="offset">the position to start writing.</param>
        /// <returns>The number of bytes written into <c>bytes</c>.</returns>
        protected override uint WriteBody(byte[] bytes, int offset)
        {
            string body = m_username;
            return (uint)TextEncoding.GetBytes(body, 0, body.Length, bytes, offset);
        }

        /// <summary>
        /// This method gets a string representation for the specified <c>HeartbeatMessage</c> instance.
        /// </summary>
        /// <returns>A string that contains the message type and username.</returns>
        public override string ToString()
        {
            return "Heartbeat: " + m_username;
        }
    }
}
