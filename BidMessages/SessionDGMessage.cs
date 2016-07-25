namespace BidMessages
{
    /// <summary>
    /// Class <c>SessionAMessage</c> models <c>QuoteMessage</c>s of Sessions D and G.
    /// </summary>
    public abstract class SessionDGMessage : QuoteTextMessage
    {
        /// <summary>
        /// This constructor initializes a new instance of the <c>SessionDGMessage</c> class with the given byte array, start index, and number of bytes.
        /// </summary>
        /// <param name="message">the byte array that contains this <c>SessionDGMessage</c>.</param>
        /// <param name="startIndex">the starting index.</param>
        /// <param name="count">the length of this message in bytes.</param>
        public SessionDGMessage(byte[] message, int startIndex, int count)
            : base(message, startIndex, count)
        {
        }

        /// <summary>
        /// Property <c>ProcessedCount</c> represents the message's processed count.
        /// </summary>
        public int ProcessedCount
        {
            get
            {
                return GetFieldValueAsInt32(GetIndexFromTag(QuoteFieldTags.ProcessedCount));
            }
        }

        /// <summary>
        /// Property <c>PendingCount</c> represents the message's pending count.
        /// </summary>
        public int PendingCount
        {
            get
            {
                return GetFieldValueAsInt32(GetIndexFromTag(QuoteFieldTags.PendingCount));
            }
        }

        /// <summary>
        /// This method gets the index to the fields array given a field tag.
        /// </summary>
        /// <param name="tag">a field tag as defined in <c>QuoteFieldTags</c>.</param>
        /// <returns>The index in the fields array or -1 if the field doesn't exist.</returns>
        public override int GetIndexFromTag(QuoteFieldTags tag)
        {
            switch (tag)
            {
                case QuoteFieldTags.UpdateTimestamp:
                    return 0;

                case QuoteFieldTags.AuctionSession:
                    return 1;

                case QuoteFieldTags.ContentText:
                    return 2;

                case QuoteFieldTags.ProcessedCount:
                    return 3;

                case QuoteFieldTags.PendingCount:
                    return 4;

                default:
                    return -1;
            }
        }
    }
}
