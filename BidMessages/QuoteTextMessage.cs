namespace BidMessages
{
    /// <summary>
    /// Class <c>QuoteTextMessage</c> models <c>QuoteMessage</c>s that contain mainly text.
    /// </summary>
    public abstract class QuoteTextMessage : QuoteMessage
    {
        /// <summary>
        /// This constructor initializes a new instance of the <c>QuoteTextMessage</c> class with the given byte array, start index, and number of bytes.
        /// </summary>
        /// <param name="message">the byte array that contains this <c>QuoteTextMessage</c>.</param>
        /// <param name="startIndex">the starting index.</param>
        /// <param name="count">the length of this message in bytes.</param>
        public QuoteTextMessage(byte[] message, int startIndex, int count)
            : base(message, startIndex, count)
        {
        }

        /// <summary>
        /// Property <c>ContentText</c> represents the message's content text.
        /// </summary>
        public string ContentText
        {
            get
            {
                return GetFieldValueAsString(GetIndexFromTag(QuoteFieldTags.ContentText));
            }
        }
    }
}
