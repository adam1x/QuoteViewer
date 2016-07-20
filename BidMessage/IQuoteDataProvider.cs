namespace BidMessage
{
    /// <summary>
    /// Interface <c>IQuoteDataProvider</c> defines methods that a quote data provider class must implement.
    /// </summary>
    internal interface IQuoteDataProvider
    {
        /// <summary>
        /// This method, once implemented, defines how to subscribe a <c>IQuoteDataListener</c>.
        /// </summary>
        /// <param name="subscriber">the <c>IQuoteDataListener</c> subscriber.</param>
        void Subscribe(IQuoteDataListener subscriber);

        /// <summary>
        /// This method, once implemented, defines how to unsubscribe a <c>IQuoteDataListener</c>.
        /// </summary>
        /// <param name="subscriber">the <c>IQuoteDataListener</c> subscriber to be unsubscribed.</param>
        void Unsubscribe(IQuoteDataListener subscriber);
    }
}
