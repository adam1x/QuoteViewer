using System;

using BidMessages;

namespace QuoteProviders
{
    /// <summary>
    /// Defines methods that a quote data listener class must implement.
    /// </summary>
    public interface IQuoteDataListener
    {
        /// <summary>
        /// The listener's name.
        /// </summary>
        string ListenerName { get; }

        /// <summary>
        /// Defines what should be done once a <c>QuoteMessage</c> is received.
        /// </summary>
        /// <param name="message">the received <c>QuoteMessage</c> object.</param>
        void OnQuoteMessageReceived(QuoteMessage message);

        /// <summary>
        /// Defines what should be done once an error occurs.
        /// </summary>
        /// <param name="ex">the error.</param>
        /// <param name="severe">the severity of this error: true means needs to abort; false means can try to recover.</param>
        void OnErrorOccurred(Exception ex, bool severe);
    }
}
