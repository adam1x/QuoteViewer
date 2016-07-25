using System;

namespace BidMessages
{
    /// <summary>
    /// Class <c>SessionAMessage</c> models <c>QuoteMessage</c>s of Session A.
    /// </summary>
    public class SessionAMessage : QuoteDataMessage
    {
        private static readonly int[] m_tagToIndex = new int[]
        {
            -1, // Undefined,
            0, // UpdateTimestamp,
            1, // AuctionSession,
            2, // InitialPriceFlag,
            3, // AuctionName,
            4, // BidSize,
            5, // LimitPrice,
            6, // InitialPrice,
            7, // AuctionBeginTime,
            8, // AuctionEndTime,
            9, // FirstBeginTime,
            10, // FirstEndTime,
            11, // SecondBeginTime,
            12, // SecondEndTime,
            13, // ServerTime,
            14, // BidQuantity
            15, // BidPrice,
            16, // BidTime,
            17, // ProcessedCount,
            18, // PendingCount,
            -1, // BidLower,
            -1, // BidUpper,
            -1, // ContentText,
        };

        /// <summary>
        /// This constructor initializes a new instance of the <c>SessionA</c> class with the given byte array, start index, and number of bytes.
        /// </summary>
        /// <param name="message">the byte array that contains this <c>SessionAMessage</c>.</param>
        /// <param name="startIndex">the starting index.</param>
        /// <param name="count">the length of this message in bytes.</param>
        /// <exception cref="System.ArgumentException">Input byte array does not represent a session A message.</exception>
        public SessionAMessage(byte[] message, int startIndex, int count)
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
                return AuctionSessions.SessionA;
            }
        }

        /// <summary>
        /// Property <c>PriceUpperBound</c> represents the message's price upper bound.
        /// </summary>
        public override int PriceUpperBound
        {
            get
            {
                return LimitPrice;
            }
        }

        /// <summary>
        /// Property <c>PriceLowerBound</c> represents the message's price lower bound.
        /// </summary>
        public override int PriceLowerBound
        {
            get
            {
                return InitialPrice;
            }
        }

        /// <summary>
        /// Property <c>LimitPrice</c> represents the message's limit price.
        /// </summary>
        public int LimitPrice
        {
            get
            {
                return GetFieldValueAsInt32(GetIndexFromTag(QuoteFieldTags.LimitPrice));
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
