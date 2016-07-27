namespace BidMessages
{
    /// <summary>
    ///Models messages sent and received during connecting to and authencating on the remote server.
    /// </summary>
    public abstract class ControlMessage : BidMessage
    {
        /// <summary>
        /// Initializes a new instance of the <c>ControlMessage</c> class.
        /// </summary>
        public ControlMessage()
        {
        }
    }
}
