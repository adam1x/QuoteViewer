namespace QuoteProviders
{
    /// <summary>
    /// Interface <c>IQuoteDataProvider</c> defines methods that a quote data provider class must implement.
    /// </summary>
    public interface IQuoteDataProvider
    {
        /// <value>
        /// Property <c>ProviderName</c> represents the provider's name.
        /// </value>
        string ProviderName { get; }

        /// <summary>
        /// This method, once implemented, defines how to subscribe a <c>IQuoteDataListener</c>.
        /// </summary>
        /// <param name="listener">the <c>IQuoteDataListener</c> listener.</param>
        void Subscribe(IQuoteDataListener listener);

        /// <summary>
        /// This method, once implemented, defines how to unsubscribe a <c>IQuoteDataListener</c>.
        /// </summary>
        /// <param name="listener">the <c>IQuoteDataListener</c> listener to be unsubscribed.</param>
        void Unsubscribe(IQuoteDataListener listener);
    }
}
