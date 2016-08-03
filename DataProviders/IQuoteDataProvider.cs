using System;

namespace QuoteProviders
{
    /// <summary>
    /// Defines methods that a quote data provider class must implement.
    /// </summary>
    public interface IQuoteDataProvider
    {
        /// <summary>
        /// Triggered when the provider changes status.
        /// </summary>
        event EventHandler<StatusChangedEventArgs> StatusChanged;

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

        /// <summary>
        /// Runs a quote data provider.
        /// </summary>
        /// <returns>Time to wait till next state is run, in milliseconds.</returns>
        int Run();
    }
}
