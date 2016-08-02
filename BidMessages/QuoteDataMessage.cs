using System;
using System.Globalization;

namespace BidMessages
{
    /// <summary>
    /// Class <c>QuoteDataMessage</c> models <c>QuoteMessage</c>s that contain data.
    /// </summary>
    public abstract class QuoteDataMessage : QuoteMessage
    {
        /// <summary>
        /// Initializes a new instance of the <c>QuoteDataMessage</c> class with the given byte array, start index, and number of bytes.
        /// </summary>
        /// <param name="message">the byte array that contains this <c>QuoteDataMessage</c>.</param>
        /// <param name="startIndex">the starting index.</param>
        /// <param name="count">the length of this message in bytes.</param>
        public QuoteDataMessage(byte[] message, int startIndex, int count)
            : base(message, startIndex, count)
        {
        }

        /// <summary>
        /// The message's auction date.
        /// </summary>
        public override DateTime AuctionDate
        {
            get
            {
                return DateTime.Parse(AuctionName.Substring(5, 5), new CultureInfo("zh-CN"));
            }
        }

        /// <summary>
        /// The message's initial price flag.
        /// </summary>
        public int InitialPriceFlag
        {
            get
            {
                return GetFieldValueAsInt32(GetIndexFromTag(QuoteFieldTags.InitialPriceFlag));
            }
        }

        /// <summary>
        /// The message's auction name.
        /// </summary>
        public string AuctionName
        {
            get
            {
                return GetFieldValueAsString(GetIndexFromTag(QuoteFieldTags.AuctionName));
            }
        }

        /// <summary>
        /// The message's bid size.
        /// </summary>
        public int BidSize
        {
            get
            {
                return GetFieldValueAsInt32(GetIndexFromTag(QuoteFieldTags.BidSize));
            }
        }

        /// <summary>
        /// The message's initial price.
        /// </summary>
        public int InitialPrice
        {
            get
            {
                return GetFieldValueAsInt32(GetIndexFromTag(QuoteFieldTags.InitialPrice));
            }
        }

        /// <summary>
        /// The message's auction begin time.
        /// </summary>
        public TimeSpan AuctionBeginTime
        {
            get
            {
                return GetFieldValueAsTimeSpan(GetIndexFromTag(QuoteFieldTags.AuctionBeginTime));
            }
        }

        /// <summary>
        /// The auction end time.
        /// </summary>
        public TimeSpan AuctionEndTime
        {
            get
            {
                return GetFieldValueAsTimeSpan(GetIndexFromTag(QuoteFieldTags.AuctionEndTime));
            }
        }

        /// <summary>
        /// The message's first begin time.
        /// </summary>
        public TimeSpan FirstBeginTime
        {
            get
            {
                return GetFieldValueAsTimeSpan(GetIndexFromTag(QuoteFieldTags.FirstBeginTime));
            }
        }

        /// <summary>
        /// The message's first end time.
        /// </summary>
        public TimeSpan FirstEndTime
        {
            get
            {
                return GetFieldValueAsTimeSpan(GetIndexFromTag(QuoteFieldTags.FirstEndTime));
            }
        }

        /// <summary>
        /// The message's second begin time.
        /// </summary>
        public TimeSpan SecondBeginTime
        {
            get
            {
                return GetFieldValueAsTimeSpan(GetIndexFromTag(QuoteFieldTags.SecondBeginTime));
            }
        }

        /// <summary>
        /// The message's second end time.
        /// </summary>
        public TimeSpan SecondEndTime
        {
            get
            {
                return GetFieldValueAsTimeSpan(GetIndexFromTag(QuoteFieldTags.SecondEndTime));
            }
        }

        /// <summary>
        /// The message's server time.
        /// </summary>
        public TimeSpan ServerTime
        {
            get
            {
                return GetFieldValueAsTimeSpan(GetIndexFromTag(QuoteFieldTags.ServerTime));
            }
        }

        /// <summary>
        /// The message's bid quantity.
        /// </summary>
        public int BidQuantity
        {
            get
            {
                return GetFieldValueAsInt32(GetIndexFromTag(QuoteFieldTags.BidQuantity));
            }
        }

        /// <summary>
        /// The message's bid price.
        /// </summary>
        public int BidPrice
        {
            get
            {
                return GetFieldValueAsInt32(GetIndexFromTag(QuoteFieldTags.BidPrice));
            }
        }

        /// <summary>
        /// The message's bid time.
        /// </summary>
        public DateTime BidTime
        {
            get
            {
                return GetFieldValueAsDateTime(GetIndexFromTag(QuoteFieldTags.BidTime));
            }
        }

        /// <summary>
        /// The message's processed count.
        /// </summary>
        public int ProcessedCount
        {
            get
            {
                return GetFieldValueAsInt32(GetIndexFromTag(QuoteFieldTags.ProcessedCount));
            }
        }

        /// <summary>
        /// The message's pending count.
        /// </summary>
        public int PendingCount
        {
            get
            {
                return GetFieldValueAsInt32(GetIndexFromTag(QuoteFieldTags.PendingCount));
            }
        }

        /// <summary>
        /// The message's price upper bound.
        /// </summary>
        public abstract int PriceUpperBound { get; }

        /// <summary>
        /// The message's price upper bound.
        /// </summary>
        public abstract int PriceLowerBound { get; }
    }
}
