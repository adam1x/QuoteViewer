namespace BidMessages
{
    /// <summary>
    /// Enumeration <c>FunctionCodes</c> represents supported function codes of <c>BidMessage</c>s.
    /// </summary>
    public enum FunctionCodes : ushort
    {
		// [Xu Linqiu] 各个枚举值定义代码之间应插入空行。

		/// <summary>
		/// Indicates a reserved undefined value.
		/// </summary>
		Undefined = 0xffff,
        /// <summary>
        /// Indicates that the message is a quote message.
        /// </summary>
        Quote = 0x0301,
        /// <summary>
        /// Indicates that the message is a heartbeat message.
        /// </summary>
        Heartbeat = 0xff00,
        /// <summary>
        /// Indicates that the message is a session key request message.
        /// </summary>
        SessionKeyRequest = 0xff01,
        /// <summary>
        /// Indicates that the message is a session key reply message.
        /// </summary>
        SessionKeyReply = 0xff02,
        /// <summary>
        /// Indicates that the message is a login request message.
        /// </summary>
        LoginRequest = 0xff03,
        /// <summary>
        /// Indicates that the message is a login reply message.
        /// </summary>
        LoginReply = 0xff04,
    }
}
