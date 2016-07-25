using System;

namespace BidMessages
{
    /// <summary>
    /// Class <c>QuoteDataMessage</c> models <c>QuoteMessage</c>s that contain data.
    /// </summary>
    public abstract class QuoteDataMessage : QuoteMessage
    {
        /// <summary>
        /// This constructor initializes a new instance of the <c>QuoteDataMessage</c> class with the given byte array, start index, and number of bytes.
        /// </summary>
        /// <param name="message">the byte array that contains this <c>QuoteDataMessage</c>.</param>
        /// <param name="startIndex">the starting index.</param>
        /// <param name="count">the length of this message in bytes.</param>
        public QuoteDataMessage(byte[] message, int startIndex, int count)
            : base(message, startIndex, count)
        {
        }

        /// <summary>
        /// Property <c>InitialPriceFlag</c> represents the message's initial price flag.
        /// </summary>
        public int InitialPriceFlag
        {
            get
            {
                return GetFieldValueAsInt32(GetIndexFromTag(QuoteFieldTags.InitialPriceFlag));
            }
        }

        /// <summary>
        /// Property <c>AuctionName</c> represents the message's auction name.
        /// </summary>
        public string AuctionName
        {
            get
            {
                return GetFieldValueAsString(GetIndexFromTag(QuoteFieldTags.AuctionName));
            }
        }

        /// <summary>
        /// Property <c>BidSize</c> represents the message's bid size.
        /// </summary>
        public int BidSize
        {
            get
            {
                return GetFieldValueAsInt32(GetIndexFromTag(QuoteFieldTags.BidSize));
            }
        }

        /// <summary>
        /// Property <c>InitialPrice</c> represents the message's initial price.
        /// </summary>
        public int InitialPrice
        {
            get
            {
                return GetFieldValueAsInt32(GetIndexFromTag(QuoteFieldTags.InitialPrice));
            }
        }

        /// <summary>
        /// Property <c>AuctionBeginTime</c> represents the message's auction begin time.
        /// </summary>
        public TimeSpan AuctionBeginTime
        {
            get
            {
                return GetFieldValueAsTimeSpan(GetIndexFromTag(QuoteFieldTags.AuctionBeginTime));
            }
        }

        /// <summary>
        /// Property <c>AuctionEndTime</c> represents the auction end time.
        /// </summary>
        public TimeSpan AuctionEndTime
        {
            get
            {
                return GetFieldValueAsTimeSpan(GetIndexFromTag(QuoteFieldTags.AuctionEndTime));
            }
        }

        /// <summary>
        /// Property <c>FirstBeginTime</c> represents the message's first begin time.
        /// </summary>
        public TimeSpan FirstBeginTime
        {
            get
            {
                return GetFieldValueAsTimeSpan(GetIndexFromTag(QuoteFieldTags.FirstBeginTime));
            }
        }

        /// <summary>
        /// Property <c>FirstEndTime</c> represents the message's first end time.
        /// </summary>
        public TimeSpan FirstEndTime
        {
            get
            {
                return GetFieldValueAsTimeSpan(GetIndexFromTag(QuoteFieldTags.FirstEndTime));
            }
        }

        /// <summary>
        /// Property <c>SecondBeginTime</c> represents the message's second begin time.
        /// </summary>
        public TimeSpan SecondBeginTime
        {
            get
            {
                return GetFieldValueAsTimeSpan(GetIndexFromTag(QuoteFieldTags.SecondBeginTime));
            }
        }

        /// <summary>
        /// Property <c>SecondEndTime</c> represents the message's second end time.
        /// </summary>
        public TimeSpan SecondEndTime
        {
            get
            {
                return GetFieldValueAsTimeSpan(GetIndexFromTag(QuoteFieldTags.SecondEndTime));
            }
        }

        /// <summary>
        /// Property <c>ServerTime</c> represents the message's server time.
        /// </summary>
        public TimeSpan ServerTime
        {
            get
            {
                return GetFieldValueAsTimeSpan(GetIndexFromTag(QuoteFieldTags.ServerTime));
            }
        }

        /// <summary>
        /// Property <c>BidQuantity</c> represents the message's bid quantity.
        /// </summary>
        public int BidQuantity
        {
            get
            {
                return GetFieldValueAsInt32(GetIndexFromTag(QuoteFieldTags.BidQuantity));
            }
        }

        /// <summary>
        /// Property <c>BidPrice</c> represents the message's bid price.
        /// </summary>
        public int BidPrice
        {
            get
            {
                return GetFieldValueAsInt32(GetIndexFromTag(QuoteFieldTags.BidPrice));
            }
        }

        /// <summary>
        /// Property <c>BidTime</c> represents the message's bid time.
        /// </summary>
        public DateTime BidTime
        {
            get
            {
                return GetFieldValueAsDateTime(GetIndexFromTag(QuoteFieldTags.BidTime));
            }
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
        /// Property <c>PriceUpperBound</c> represents the message's price upper bound.
        /// </summary>
        public abstract int PriceUpperBound { get; }

        /// <summary>
        /// Property <c>PriceLowerBound</c> represents the message's price upper bound.
        /// </summary>
        public abstract int PriceLowerBound { get; }
    }
}
