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

        /// <value>
        /// Property <c>InitialPriceFlag</c> represents the message's initial price flag.
        /// </value>
        public int InitialPriceFlag
        {
            get
            {
                return GetFieldValueAsInt32(GetIndexFromTag(QuoteFieldTags.InitialPriceFlag));
            }
        }

        /// <value>
        /// Property <c>AuctionName</c> represents the message's auction name.
        /// </value>
        public string AuctionName
        {
            get
            {
                return GetFieldValueAsString(GetIndexFromTag(QuoteFieldTags.AuctionName));
            }
        }

        /// <value>
        /// Property <c>BidSize</c> represents the message's bid size.
        /// </value>
        public int BidSize
        {
            get
            {
                return GetFieldValueAsInt32(GetIndexFromTag(QuoteFieldTags.BidSize));
            }
        }

        /// <value>
        /// Property <c>InitialPrice</c> represents the message's initial price.
        /// </value>
        public int InitialPrice
        {
            get
            {
                return GetFieldValueAsInt32(GetIndexFromTag(QuoteFieldTags.InitialPrice));
            }
        }

        /// <value>
        /// Property <c>AuctionBeginTime</c> represents the message's auction begin time.
        /// </value>
        public TimeSpan AuctionBeginTime
        {
            get
            {
                return GetFieldValueAsTimeSpan(GetIndexFromTag(QuoteFieldTags.AuctionBeginTime));
            }
        }

        /// <value>
        /// Property <c>AuctionEndTime</c> represents the auction end time.
        /// </value>
        public TimeSpan AuctionEndTime
        {
            get
            {
                return GetFieldValueAsTimeSpan(GetIndexFromTag(QuoteFieldTags.AuctionEndTime));
            }
        }

        /// <value>
        /// Property <c>FirstBeginTime</c> represents the message's first begin time.
        /// </value>
        public TimeSpan FirstBeginTime
        {
            get
            {
                return GetFieldValueAsTimeSpan(GetIndexFromTag(QuoteFieldTags.FirstBeginTime));
            }
        }

        /// <value>
        /// Property <c>FirstEndTime</c> represents the message's first end time.
        /// </value>
        public TimeSpan FirstEndTime
        {
            get
            {
                return GetFieldValueAsTimeSpan(GetIndexFromTag(QuoteFieldTags.FirstEndTime));
            }
        }

        /// <value>
        /// Property <c>SecondBeginTime</c> represents the message's second begin time.
        /// </value>
        public TimeSpan SecondBeginTime
        {
            get
            {
                return GetFieldValueAsTimeSpan(GetIndexFromTag(QuoteFieldTags.SecondBeginTime));
            }
        }

        /// <value>
        /// Property <c>SecondEndTime</c> represents the message's second end time.
        /// </value>
        public TimeSpan SecondEndTime
        {
            get
            {
                return GetFieldValueAsTimeSpan(GetIndexFromTag(QuoteFieldTags.SecondEndTime));
            }
        }

        /// <value>
        /// Property <c>ServerTime</c> represents the message's server time.
        /// </value>
        public TimeSpan ServerTime
        {
            get
            {
                return GetFieldValueAsTimeSpan(GetIndexFromTag(QuoteFieldTags.ServerTime));
            }
        }

        /// <value>
        /// Property <c>BidQuantity</c> represents the message's bid quantity.
        /// </value>
        public int BidQuantity
        {
            get
            {
                return GetFieldValueAsInt32(GetIndexFromTag(QuoteFieldTags.BidQuantity));
            }
        }

        /// <value>
        /// Property <c>BidPrice</c> represents the message's bid price.
        /// </value>
        public int BidPrice
        {
            get
            {
                return GetFieldValueAsInt32(GetIndexFromTag(QuoteFieldTags.BidPrice));
            }
        }

        /// <value>
        /// Property <c>BidTime</c> represents the message's bid time.
        /// </value>
        public DateTime BidTime
        {
            get
            {
                return GetFieldValueAsDateTime(GetIndexFromTag(QuoteFieldTags.BidTime));
            }
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

        /// <value>
        /// Property <c>PriceUpperBound</c> represents the message's price upper bound.
        /// </value>
        public abstract int PriceUpperBound { get; }

        /// <value>
        /// Property <c>PriceLowerBound</c> represents the message's price upper bound.
        /// </value>
        public abstract int PriceLowerBound { get; }
    }
}
