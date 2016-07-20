namespace BidMessages
{
    /// <summary>
    /// Class <c>LoginReplyMsg</c> models the server's response to a <c>LoginRequestMsg</c>.
    /// </summary>
    public class LoginReplyMsg : ControlReplyMessage
    {
        protected uint m_maxHeartbeatInterval;

        /// <summary>
        /// This constructor initializes the new <c>LoginReplyMsg</c> to contain max heartbeat interval.
        /// </summary>
        /// <param name="bytes">the byte array that contains the max heartbeat interval sent from the server.</param>
        /// <param name="startIndex">the starting index to read the interval.</param>
        public LoginReplyMsg(byte[] bytes, int startIndex)
        {
            m_maxHeartbeatInterval = bytes.ToUInt32(startIndex);
        }

        /// <value>
        /// Property <c>Function</c> represents the message's function code.
        /// </value>
        public override FunctionCodes Function
        {
            get
            {
                return FunctionCodes.LoginReply;
            }
        }

        /// <value>
        /// Property <c>MaxHeartbeatInterval</c> represents the message's content: max heartbeat interval.
        /// </value>
        public uint MaxHeartbeatInterval
        {
            get { return m_maxHeartbeatInterval; }
        }

        /// <summary>
        /// This method returns the uint value that is the body of a <c>LoginReplyMsg</c> object.
        /// </summary>
        /// <returns>A uint value representing the body.</returns>
        protected override uint GetBody()
        {
            return m_maxHeartbeatInterval;
        }
    }
}
