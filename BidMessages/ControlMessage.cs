namespace BidMessages
{
    /// <summary>
    /// Class <c>ControlMessage</c> models messages sent and received during connecting to and authencating on the remote server.
    /// </summary>
    public abstract class ControlMessage : BidMessage
    {
        /// <summary>
        /// This constructor initializes a new instance of the <c>ControlMessage</c> class.
        /// </summary>
        public ControlMessage()
        {
        }
    }
}
