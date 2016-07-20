using System.Collections.Generic;

namespace BidMessage
{
    /// <summary>
    /// Class <c>SessionAMsg</c> models <c>QuoteMessage</c>s of Session A.
    /// </summary>
    public class SessionAMsg : QuoteDataMessage
    {
        protected static Dictionary<QuoteFieldTags, int> m_tagToIndex = new Dictionary<QuoteFieldTags, int>
        {
            { QuoteFieldTags.UpdateTimestamp, 0 },
            { QuoteFieldTags.AuctionSession, 1 },
            { QuoteFieldTags.InitialPriceFlag, 2 },
            { QuoteFieldTags.AuctionName, 3 },
            { QuoteFieldTags.BidSize, 4 },
            { QuoteFieldTags.LimitPrice, 5 },
            { QuoteFieldTags.InitialPrice, 6 },
            { QuoteFieldTags.AuctionBeginTime, 7 },
            { QuoteFieldTags.AuctionEndTime, 8 },
            { QuoteFieldTags.FirstBeginTime, 9 },
            { QuoteFieldTags.FirstEndTime, 10 },
            { QuoteFieldTags.SecondBeginTime, 11 },
            { QuoteFieldTags.SecondEndTime, 12 },
            { QuoteFieldTags.ServerTime, 13 },
            { QuoteFieldTags.BidQuantity, 14 },
            { QuoteFieldTags.BidPrice, 15 },
            { QuoteFieldTags.BidTime, 16 },
            { QuoteFieldTags.ProcessedCount, 17 },
            { QuoteFieldTags.PendingCount, 18 },
        };

        /// <summary>
        /// This constructor calls the base class <c>QuoteDataMessage</c>'s constructor.
        /// </summary>
        /// <param name="body">the byte array that contains the body of this <c>QuoteDataMessage</c>.</param>
        /// <param name="startIndex">the starting index to read the body.</param>
        /// <param name="count">the number of bytes to read in <c>body</c>.</param>
        public SessionAMsg(byte[] body, int startIndex, int count)
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
