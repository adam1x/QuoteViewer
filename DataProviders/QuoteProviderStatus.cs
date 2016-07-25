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
        /// Indicates inactivity: before or after use.
        /// </summary>
        Inactive,

        /// <summary>
        /// Indicates opening the resource.
        /// </summary>
        Open,

        /// <summary>
        /// Indicates authenticating with the resource provider.
        /// </summary>
        Authenticate,

        /// <summary>
        /// Indicates reading from the resource.
        /// </summary>
        Read,
        
        /// <summary>
        /// Indicates closing the resource.
        /// </summary>
        Close,
    }
}
