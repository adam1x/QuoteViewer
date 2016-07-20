namespace BidMessages
{
    /// <summary>
    /// Class <c>ControlReplyMessage</c> models all the <c>ControlMessage</c>s that are replies from the server.
    /// </summary>
    public abstract class ControlReplyMessage : ControlMessage
    {
        /// <summary>
        /// This constructor doesn't do anything specific.
        /// </summary>
        public ControlReplyMessage()
        {
        }

        /// <summary>
        /// This method encodes the body of a <c>BidMessage</c> object into the target byte array.
        /// </summary>
        /// <param name="bytes">the target byte array.</param>
        /// <returns>The number of bytes written into <c>bytes</c>.</returns>
        protected override uint WriteBody(byte[] bytes)
        {
            uint body = GetBody();
            body.GetBytes(bytes, HeaderLength);
            return sizeof(uint);
        }
        
        /// <summary>
        /// This method returns the uint value that is the body of a <c>ControlReplyMessage</c> object.
        /// </summary>
        /// <returns>A uint value representing the body.</returns>
        protected abstract uint GetBody();
    }
}
