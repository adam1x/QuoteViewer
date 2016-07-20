namespace BidMessages
{
    /// <summary>
    /// Class <c>HeartbeatMsg</c> models heartbeat messages sent to the remote server.
    /// </summary>
    public class HeartbeatMsg : ControlRequestMessage
    {
        protected string m_username;

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
