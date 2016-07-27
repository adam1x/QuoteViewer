namespace QuoteProviders
{
    /// <summary>
    /// Defines methods that a quote data provider class must implement.
    /// </summary>
    public interface IQuoteDataProvider
    {
        /// <summary>
        /// The provider's name.
        /// </summary>
        string ProviderName { get; }

        /// <summary>
        /// Defines how to subscribe a <c>IQuoteDataListener</c>.
        /// </summary>
        /// <param name="listener">the <c>IQuoteDataListener</c> listener.</param>
        void Subscribe(IQuoteDataListener listener);

        /// <summary>
        /// Defines how to unsubscribe a <c>IQuoteDataListener</c>.
        /// </summary>
        /// <param name="listener">the <c>IQuoteDataListener</c> listener to be unsubscribed.</param>
        void Unsubscribe(IQuoteDataListener listener);
    }
}
