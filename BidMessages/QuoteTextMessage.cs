namespace BidMessages
{
    /// <summary>
    /// Class <c>QuoteTextMessage</c> models <c>QuoteMessage</c>s that contain mainly text.
    /// </summary>
    public abstract class QuoteTextMessage : QuoteMessage
    {
		// [Xu Linqiu] summary注释内容不当
		/// <summary>
		/// This constructor calls the base class <c>QuoteMessage</c>'s constructor.
		/// </summary>
		/// <param name="body">the byte array that contains the body of this <c>QuoteDataMessage</c>.</param>
		/// <param name="startIndex">the starting index to read the body.</param>
		/// <param name="count">the number of bytes to read in <c>body</c>.</param>
		public QuoteTextMessage(byte[] body, int startIndex, int count)
            : base(body, startIndex, count)
        {
        }
    }
}
