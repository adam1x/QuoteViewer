using System.Collections.Generic;

namespace BidMessage
{
    /// <summary>
    /// Class <c>SessionAMsg</c> models <c>QuoteMessage</c>s of Sessions D and G.
    /// </summary>
    public class SessionDGMsg : QuoteTextMessage
    {
        protected static Dictionary<QuoteFieldTags, int> m_tagToIndex = new Dictionary<QuoteFieldTags, int>
        {
            { QuoteFieldTags.UpdateTimestamp, 0 },
            { QuoteFieldTags.AuctionSession, 1 },
            { QuoteFieldTags.ContentText, 2 },
            { QuoteFieldTags.ProcessedCount, 3 },
            { QuoteFieldTags.PendingCount, 4 },
        };

        /// <summary>
        /// This constructor calls the base class <c>QuoteDataMessage</c>'s constructor.
        /// </summary>
        /// <param name="body">the byte array that contains the body of this <c>QuoteDataMessage</c>.</param>
        /// <param name="startIndex">the starting index to read the body.</param>
        /// <param name="count">the number of bytes to read in <c>body</c>.</param>
        public SessionDGMsg(byte[] body, int startIndex, int count)
            : base(body, startIndex, count)
        {
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
