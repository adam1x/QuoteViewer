namespace BidMessages
{
    /// <summary>
    /// Enumeratoin <c>QuoteFieldTags</c> represents all supported fields in the body of a <c>BodyMessage</c>.
    /// </summary>
    public enum QuoteFieldTags
    {
        /// <summary>
        /// Indicates a reserved undefined value.
        /// </summary>
        Undefined,

        /// <summary>
        /// Indicates that the field represents update timestamp.
        /// </summary>
        UpdateTimestamp,

        /// <summary>
        /// Indicates that the field represents auction session.
        /// </summary>
        AuctionSession,

        /// <summary>
        /// Indicates that the field represents initial price flag.
        /// </summary>
        InitialPriceFlag,

        /// <summary>
        /// Indicates that the field represents auction name.
        /// </summary>
        AuctionName,

        /// <summary>
        /// Indicates that the field represents bid size.
        /// </summary>
        BidSize,

        /// <summary>
        /// Indicates that the field represents limit price.
        /// </summary>
        LimitPrice,

        /// <summary>
        /// Indicates that the field represents initial price.
        /// </summary>
        InitialPrice,

        /// <summary>
        /// Indicates that the field represents auction begin time.
        /// </summary>
        AuctionBeginTime,

        /// <summary>
        /// Indicates that the field represents auction end time.
        /// </summary>
        AuctionEndTime,

        /// <summary>
        /// Indicates that the field represents first begin time.
        /// </summary>
        FirstBeginTime,

        /// <summary>
        /// Indicates that the field represents first end time.
        /// </summary>
        FirstEndTime,

        /// <summary>
        /// Indicates that the field represents second begin time.
        /// </summary>
        SecondBeginTime,

        /// <summary>
        /// Indicates that the field represents second end time.
        /// </summary>
        SecondEndTime,

        /// <summary>
        /// Indicates that the field represents server time.
        /// </summary>
        ServerTime,
        /// <summary>
        /// Indicates that the field represents bid quantity.
        /// </summary>
        BidQuantity,

        /// <summary>
        /// Indicates that the field represents bid price.
        /// </summary>
        BidPrice,

        /// <summary>
        /// Indicates that the field represents bid time.
        /// </summary>
        BidTime,

        /// <summary>
        /// Indicates that the field represents processed count.
        /// </summary>
        ProcessedCount,

        /// <summary>
        /// Indicates that the field represents pending count.
        /// </summary>
        PendingCount,

        /// <summary>
        /// Indicates that the field represents bid lower.
        /// </summary>
        BidLower,

        /// <summary>
        /// Indicates that the field represents bid upper.
        /// </summary>
        BidUpper,

        /// <summary>
        /// Indicates that the field represents content text.
        /// </summary>
        ContentText,

        /// <summary>
        /// Indicates that the field represents control data.
        /// </summary>
        ControlData,
    }
}
