namespace BidMessage
{
    /// <summary>
    /// Enumeratoin <c>QuoteFieldTags</c> represents all supported fields in the body of a <c>BodyMessage</c>, plus a reserved undefined value.
    /// </summary>
    public enum QuoteFieldTags
    {
        Undefined,
        UpdateTimestamp,
        AuctionSession,
        InitialPriceFlag,
        AuctionName,
        BidSize,
        LimitPrice,
        InitialPrice,
        AuctionBeginTime,
        AuctionEndTime,
        FirstBeginTime,
        FirstEndTime,
        SecondBeginTime,
        SecondEndTime,
        ServerTime,
        BidQuantity,
        BidPrice,
        BidTime,
        ProcessedCount,
        PendingCount,
        BidLower,
        BidUpper,
        ContentText,
        ProtocolData,
    }
}
