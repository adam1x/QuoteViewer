namespace QuoteProviders
{
    /// <summary>
    /// Enumeration <c>QuoteProviderStatus</c> represents the various states a quote provider can be in.
    /// </summary>
    public enum QuoteProviderStatus
    {
        /// <summary>
        /// Indicates a reserved undefined value.
        /// </summary>
        Undefined,

        /// <summary>
        /// Indicates the <c>Open</c> state in <c>LocalQuoteProvider</c>.
        /// </summary>
        LocalOpen,

        /// <summary>
        /// Indicates the <c>Read</c> state in <c>LocalQuoteProvider</c>.
        /// </summary>
        LocalRead,

        /// <summary>
        /// Indicates the <c>Close</c> state in <c>LocalQuoteProvider</c>.
        /// </summary>
        LocalClose,

        /// <summary>
        /// Indicates the <c>Create</c> state in <c>TcpQuoteProvider</c>.
        /// </summary>
        TcpCreate,

        /// <summary>
        /// Indicates the <c>Connect</c> state in <c>TcpQuoteProvider</c>.
        /// </summary>
        TcpConnect,

        /// <summary>
        /// Indicates the <c>Authenticate</c> state in <c>TcpQuoteProvider</c>.
        /// </summary>
        TcpAuthenticate,

        /// <summary>
        /// Indicates the <c>InitReceive</c> state in <c>TcpQuoteProvider</c>.
        /// </summary>
        TcpInitReceive,

        /// <summary>
        /// Indicates the <c>Receive</c> state in <c>TcpQuoteProvider</c>.
        /// </summary>
        TcpReceive,

        /// <summary>
        /// Indicates the <c>Close</c> state in <c>TcpQuoteProvider</c>.
        /// </summary>
        TcpClose,
    }
}
