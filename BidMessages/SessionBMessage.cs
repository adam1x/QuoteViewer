using System;
using System.Collections.Generic;

namespace BidMessages
{
    /// <summary>
    /// Class <c>SessionAMessage</c> models <c>QuoteMessage</c>s of Session B.
    /// </summary>
    public class SessionBMessage : QuoteDataMessage
    {
        private static readonly Dictionary<QuoteFieldTags, int> m_tagToIndex = new Dictionary<QuoteFieldTags, int>
        {
            { QuoteFieldTags.UpdateTimestamp, 0 },
            { QuoteFieldTags.AuctionSession, 1 },
            { QuoteFieldTags.InitialPriceFlag, 2 },
            { QuoteFieldTags.AuctionName, 3 },
            { QuoteFieldTags.BidSize, 4 },
            { QuoteFieldTags.BidQuantity, 5 },
            { QuoteFieldTags.InitialPrice, 6 },
            { QuoteFieldTags.AuctionBeginTime, 7 },
            { QuoteFieldTags.AuctionEndTime, 8 },
            { QuoteFieldTags.FirstBeginTime, 9 },
            { QuoteFieldTags.FirstEndTime, 10 },
            { QuoteFieldTags.SecondBeginTime, 11 },
            { QuoteFieldTags.SecondEndTime, 12 },
            { QuoteFieldTags.ServerTime, 13 },
            { QuoteFieldTags.BidPrice, 14 },
            { QuoteFieldTags.BidTime, 15 },
            { QuoteFieldTags.BidLower, 16 },
            { QuoteFieldTags.BidUpper, 17 },
            { QuoteFieldTags.ProcessedCount, 18 },
            { QuoteFieldTags.PendingCount, 19 },
        };

        /// <summary>
        /// This constructor initializes a new instance of the <c>SessionBMessage</c> class with the given byte array, start index, and number of bytes.
        /// </summary>
        /// <param name="message">the byte array that contains this <c>SessionBMessage</c>.</param>
        /// <param name="startIndex">the starting index.</param>
        /// <param name="count">the length of this message in bytes.</param>
        /// <exception cref="System.Exception">Input byte array does not represent a session B message.</exception>
        public SessionBMessage(byte[] message, int startIndex, int count)
            : base(message, startIndex, count)
        {
            if (PeekSession(message, startIndex) != AuctionSession)
            {
                throw new Exception("Session mismatch.");
            }
        }

        /// <value>
        /// Property <c>AuctionSession</c> represents the message's auction session.
        /// </value>
        public override AuctionSessions AuctionSession
        {
            get
            {
                return AuctionSessions.SessionB;
            }
        }

        /// <value>
        /// Property <c>PriceUpperBound</c> represents the message's price upper bound.
        /// </value>
        public override int PriceUpperBound
        {
            get
            {
                return BidUpper;
            }
        }

        /// <value>
        /// Property <c>PriceLowerBound</c> represents the message's price lower bound.
        /// </value>
        public override int PriceLowerBound
        {
            get
            {
                return BidLower;
            }
        }

        /// <value>
        /// Property <c>BidUpper</c> represents the message's bid upper.
        /// </value>
        public int BidUpper
        {
            get
            {
                return GetFieldValueAsInt32(GetIndexFromTag(QuoteFieldTags.BidUpper));
            }
        }

        /// <value>
        /// Property <c>BidLower</c> represents the message's bid lower.
        /// </value>
        public int BidLower
        {
            get
            {
                return GetFieldValueAsInt32(GetIndexFromTag(QuoteFieldTags.BidLower));
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

        /// <summary>
        /// This method gets a string representation for the specified <c>SessionBMessage</c> instance.
        /// </summary>
        /// <returns>A string that contains the message type and update time.</returns>
        public override string ToString()
        {
            return "Session B: " + UpdateTimestamp;
        }
    }
}
