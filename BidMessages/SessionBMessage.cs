using System;

namespace BidMessages
{
    /// <summary>
    /// Models <c>QuoteMessage</c>s of Session B.
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
        /// Initializes a new instance of the <c>SessionBMessage</c> class with the given byte array, start index, and number of bytes.
        /// </summary>
        /// <param name="message">the byte array that contains this <c>SessionBMessage</c>.</param>
        /// <param name="offset">the starting index.</param>
        /// <param name="count">the length of this message in bytes.</param>
        /// <exception cref="System.ArgumentException">Input byte array does not represent a session B message.</exception>
        public SessionBMessage(byte[] message, int offset, int count)
            : base(message, offset, count)
        {
            if (PeekSession(message, offset) != AuctionSession)
            {
                throw new ArgumentException("Session mismatch.");
            }
        }

        /// <summary>
        /// The message's auction session.
        /// </summary>
        public override AuctionSessions AuctionSession
        {
            get
            {
                return AuctionSessions.SessionB;
            }
        }

        /// <summary>
        /// The message's price upper bound.
        /// </summary>
        public override int PriceUpperBound
        {
            get
            {
                return BidUpper;
            }
        }

        /// <summary>
        /// The message's price lower bound.
        /// </summary>
        public override int PriceLowerBound
        {
            get
            {
                return BidLower;
            }
        }

        /// <summary>
        /// The message's bid upper.
        /// </summary>
        public int BidUpper
        {
            get
            {
                return GetFieldValueAsInt32(GetIndexFromTag(QuoteFieldTags.BidUpper));
            }
        }

        /// <summary>
        /// The message's bid lower.
        /// </summary>
        public int BidLower
        {
            get
            {
                return GetFieldValueAsInt32(GetIndexFromTag(QuoteFieldTags.BidLower));
            }
        }

        /// <summary>
        /// Gets the index to the fields array given a field tag.
        /// </summary>
        /// <param name="tag">a field tag as defined in <c>QuoteFieldTags</c>.</param>
        /// <returns>The index in the fields array or -1 if the field doesn't exist.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">The input tag is out of range.</exception>
        public override int GetIndexFromTag(QuoteFieldTags tag)
        {
            int index = (int)tag;

            if (index < 0 || index >= m_tagToIndex.Length)
            {
                throw new ArgumentOutOfRangeException("tag out of range.");
            }

            return m_tagToIndex[index];
        }
    }
}
