using System;
using System.Collections.Generic;

namespace BidMessages
{
    /// <summary>
    /// Class <c>SessionAMessage</c> models <c>QuoteMessage</c>s of Session A.
    /// </summary>
    public class SessionAMessage : QuoteDataMessage
    {
        private static readonly Dictionary<QuoteFieldTags, int> m_tagToIndex = new Dictionary<QuoteFieldTags, int>
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
        /// This constructor initializes a new instance of the <c>SessionA</c> class with the given byte array, start index, and number of bytes.
        /// </summary>
        /// <param name="message">the byte array that contains this <c>SessionAMessage</c>.</param>
        /// <param name="startIndex">the starting index.</param>
        /// <param name="count">the length of this message in bytes.</param>
        /// <exception cref="System.Exception">Input byte array does not represent a session A message.</exception>
        public SessionAMessage(byte[] message, int startIndex, int count)
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
                return AuctionSessions.SessionA;
            }
        }

        /// <value>
        /// Property <c>PriceUpperBound</c> represents the message's price upper bound.
        /// </value>
        public override int PriceUpperBound
        {
            get
            {
                return LimitPrice;
            }
        }

        /// <value>
        /// Property <c>PriceLowerBound</c> represents the message's price lower bound.
        /// </value>
        public override int PriceLowerBound
        {
            get
            {
                return InitialPrice;
            }
        }

        /// <value>
        /// Property <c>LimitPrice</c> represents the message's limit price.
        /// </value>
        public int LimitPrice
        {
            get
            {
                return GetFieldValueAsInt32(GetIndexFromTag(QuoteFieldTags.LimitPrice));
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
        /// This method gets a string representation for the specified <c>SessionAMessage</c> instance.
        /// </summary>
        /// <returns>A string that contains the message type and update time.</returns>
        public override string ToString()
        {
            return "Session A: " + UpdateTimestamp;
        }
    }
}
