using System;

namespace BidMessages
{
    /// <summary>
    /// Class <c>SessionAMessage</c> models <c>QuoteMessage</c>s of Session B.
    /// </summary>
    public class SessionBMessage : QuoteDataMessage
    {
        private static readonly int[] m_tagToIndex = new int[]
        {
            -1, // Undefined,
            0, // UpdateTimestamp,
            1, // AuctionSession,
            2, // InitialPriceFlag,
            3, // AuctionName,
            4, // BidSize,
            -1, // LimitPrice,
            6, // InitialPrice,
            7, // AuctionBeginTime,
            8, // AuctionEndTime,
            9, // FirstBeginTime,
            10, // FirstEndTime,
            11, // SecondBeginTime,
            12, // SecondEndTime,
            13, // ServerTime,
            5, // BidQuantity
            14, // BidPrice,
            15, // BidTime,
            18, // ProcessedCount,
            19, // PendingCount,
            16, // BidLower,
            17, // BidUpper,
            -1, // ContentText,
        };

        /// <summary>
        /// This constructor initializes a new instance of the <c>SessionBMessage</c> class with the given byte array, start index, and number of bytes.
        /// </summary>
        /// <param name="message">the byte array that contains this <c>SessionBMessage</c>.</param>
        /// <param name="startIndex">the starting index.</param>
        /// <param name="count">the length of this message in bytes.</param>
        /// <exception cref="System.ArgumentException">Input byte array does not represent a session B message.</exception>
        public SessionBMessage(byte[] message, int startIndex, int count)
            : base(message, startIndex, count)
        {
            if (PeekSession(message, startIndex) != AuctionSession)
            {
                throw new ArgumentException("Session mismatch.");
            }
        }

        /// <summary>
        /// Property <c>AuctionSession</c> represents the message's auction session.
        /// </summary>
        public override AuctionSessions AuctionSession
        {
            get
            {
                return AuctionSessions.SessionB;
            }
        }

        /// <summary>
        /// Property <c>PriceUpperBound</c> represents the message's price upper bound.
        /// </summary>
        public override int PriceUpperBound
        {
            get
            {
                return BidUpper;
            }
        }

        /// <summary>
        /// Property <c>PriceLowerBound</c> represents the message's price lower bound.
        /// </summary>
        public override int PriceLowerBound
        {
            get
            {
                return BidLower;
            }
        }

        /// <summary>
        /// Property <c>BidUpper</c> represents the message's bid upper.
        /// </summary>
        public int BidUpper
        {
            get
            {
                return GetFieldValueAsInt32(GetIndexFromTag(QuoteFieldTags.BidUpper));
            }
        }

        /// <summary>
        /// Property <c>BidLower</c> represents the message's bid lower.
        /// </summary>
        public int BidLower
        {
            get
            {
                return GetFieldValueAsInt32(GetIndexFromTag(QuoteFieldTags.BidLower));
            }
        }

        /// <summary>
        /// This method gets the index to the fields array given a field tag.
        /// </summary>
        /// <param name="tag">a field tag as defined in <c>QuoteFieldTags</c>.</param>
        /// <returns>The index in the fields array or -1 if the field doesn't exist.</returns>
        public override int GetIndexFromTag(QuoteFieldTags tag)
        {
            return m_tagToIndex[(int)tag];
        }
    }
}
