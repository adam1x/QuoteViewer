namespace BidMessages
{
    /// <summary>
    /// Class <c>ControlReplyMessage</c> models all the <c>ControlMessage</c>s that are replies from the server.
    /// </summary>
    public abstract class ControlReplyMessage : ControlMessage
    {
        /// <summary>
        /// This constructor initializes a new instance of the <c>ControlReplyMessage</c> class.
        /// </summary>
        public ControlReplyMessage()
        {
        }

        /// <summary>
        /// This method peeks at a message's function code.
        /// </summary>
        /// <param name="message">the message in a byte array.</param>
        /// <returns>the message's function code.</returns>
        protected static FunctionCodes PeekFunctionCode(byte[] message)
        {
            return (FunctionCodes)message.ToUInt16(sizeof(uint));
        }
    }
}
