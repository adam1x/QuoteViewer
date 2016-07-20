namespace BidMessages
{
    /// <summary>
    /// Class <c>SessionAMsg</c> models <c>QuoteMessage</c>s of Session F.
    /// </summary>
    public class SessionFMsg : SessionCEFHMsg
    {
        /// <summary>
        /// This constructor calls the base class <c>QuoteDataMessage</c>'s constructor.
        /// </summary>
        /// <param name="body">the byte array that contains the body of this <c>QuoteDataMessage</c>.</param>
        /// <param name="startIndex">the starting index to read the body.</param>
        /// <param name="count">the number of bytes to read in <c>body</c>.</param>
        public SessionFMsg(byte[] body, int startIndex, int count)
            : base(body, startIndex, count)
        {
        }
    }
}
