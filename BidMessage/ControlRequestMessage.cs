namespace BidMessage
{
    /// <summary>
    /// Class <c>ControlReplyMessage</c> models all the <c>ControlMessage</c>s that are requests sent to the server.
    /// </summary>
    public abstract class ControlRequestMessage : ControlMessage
    {
        /// <summary>
        /// This constructor doesn't do anything specific.
        /// </summary>
        public ControlRequestMessage()
        {
        }

        /// <summary>
        /// This method encodes the body of a <c>BidMessage</c> object into the target byte array.
        /// </summary>
        /// <param name="bytes">the target byte array.</param>
        /// <returns>The number of bytes written into <c>bytes</c>.</returns>
        protected override uint WriteBody(byte[] bytes)
        {
            string body = GetBody();
            return (uint)textEncoding.GetBytes(body, 0, body.Length, bytes, HeaderLength);
        }

        /// <summary>
        /// This method returns the string value that is the body of a <c>ControlRequestMessage</c> object.
        /// </summary>
        /// <returns>A string value representing the body.</returns>
        protected abstract string GetBody();
    }
}
