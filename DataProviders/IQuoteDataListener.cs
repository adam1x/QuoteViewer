using System;

using BidMessages;

namespace QuoteProviders
{
    /// <summary>
    /// Interface <c>IQuoteDataListener</c> defines methods that a quote data listener class must implement.
    /// </summary>
    public interface IQuoteDataListener
    {
        /// <value>
        /// Property <c>ProviderName</c> represents the listener's name.
        /// </value>
        string ListenerName { get; }

        /// <summary>
        /// This method, once implemented, defines what should be done once a <c>QuoteMessage</c> is received.
        /// </summary>
        /// <param name="msg">the received <c>QuoteMessage</c>.</param>
        void OnQuoteMessageReceived(QuoteMessage msg);

        /// <summary>
        /// This method, once implemented, defines what should be done once a provider's state changes.
        /// </summary>
        /// <param name="previous">the previous state.</param>
        /// <param name="current">the current state.</param>
        void OnStatusChanged(QuoteProviderStatus previous, QuoteProviderStatus current);

        /// <summary>
        /// This method, once implemented, defines what should be done once an error occurs.
        /// </summary>
        /// <param name="ex">the error.</param>
        /// <param name="severe">the severity of this error: true means needs to abort; false means can try to recover.</param>
        void OnErrorOccurred(Exception ex, bool severe);
    }
}
