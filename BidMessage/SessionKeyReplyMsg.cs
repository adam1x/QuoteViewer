namespace BidMessage
{
    /// <summary>
    /// Class <c>SessionKeyReplyMsg</c> models the server's response to a <c>SessionKeyRequestMsg</c>.
    /// </summary>
    public class SessionKeyReplyMsg : ControlReplyMessage
    {
        protected uint m_sessionKey;

        /// <summary>
        /// This constructor initializes the new <c>SessionKeyReplyMsg</c> to contain the session key.
        /// </summary>
        /// <param name="bytes">the byte array that contains the session key sent from the server.</param>
        /// <param name="startIndex">the starting index to read the key.</param>
        public SessionKeyReplyMsg(byte[] bytes, int startIndex)
        {
            m_sessionKey = bytes.ToUInt32(startIndex);
        }

        /// <value>
        /// Property <c>Function</c> represents the message's function code.
        /// </value>
        public override ushort Function
        {
            get
            {
                return (ushort)Messages.SessionKeyReply;
            }
        }

        /// <value>
        /// Property <c>SessionKey</c> represents the message's content: session key.
        /// </value>
        public uint SessionKey
        {
            get { return m_sessionKey; }
        }

        /// <summary>
        /// This method returns the uint value that is the body of a <c>SessionKeyReplyMsg</c> object.
        /// </summary>
        /// <returns>A uint value representing the body.</returns>
        protected override uint GetBody()
        {
            return m_sessionKey;
        }
    }
}
