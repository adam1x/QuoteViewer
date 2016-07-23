using System.Collections.Generic;

namespace BidMessages
{
    /// <summary>
    /// Class <c>SessionAMessage</c> models <c>QuoteMessage</c>s of Sessions D and G.
    /// </summary>
    public abstract class SessionDGMessage : QuoteTextMessage
    {
        private static readonly Dictionary<QuoteFieldTags, int> m_tagToIndex = new Dictionary<QuoteFieldTags, int>
        {
            { QuoteFieldTags.UpdateTimestamp, 0 },
            { QuoteFieldTags.AuctionSession, 1 },
            { QuoteFieldTags.ContentText, 2 },
            { QuoteFieldTags.ProcessedCount, 3 },
            { QuoteFieldTags.PendingCount, 4 },
        };

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

        /// <value>
        /// Property <c>ProcessedCount</c> represents the message's processed count.
        /// </value>
        public int ProcessedCount
        {
            get
            {
                return GetFieldValueAsInt32(GetIndexFromTag(QuoteFieldTags.ProcessedCount));
            }
        }

        /// <value>
        /// Property <c>PendingCount</c> represents the message's pending count.
        /// </value>
        public int PendingCount
        {
            get
            {
                return GetFieldValueAsInt32(GetIndexFromTag(QuoteFieldTags.PendingCount));
            }
        }

        /// <summary>
        /// This method generates a dictionary from field tags as defined in <c>QuoteFieldTags</c> to indices in <c>m_body</c>.
        /// </summary>
        /// <returns>A dictionary from field tags as defined in <c>QuoteFieldTags</c> to indices in <c>m_body</c>.</returns>
        protected override Dictionary<QuoteFieldTags, int> GetTagToIndexMap()
        {
            return m_tagToIndex;
        }
    }
}
