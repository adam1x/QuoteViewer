namespace BidMessage
{
    /// <summary>
    /// Enumeration <c>Messages</c> represents all supported function codes of <c>BidMessage</c>s, plus a reserved undefined value.
    /// </summary>
    public enum Messages : ushort
    {
        Undefined = 0xffff,
        Quote = 0x0301,
        Heartbeat = 0xff00,
        SessionKeyRequest = 0xff01,
        SessionKeyReply = 0xff02,
        LoginRequest = 0xff03,
        LoginReply = 0xff04,
    }
}
