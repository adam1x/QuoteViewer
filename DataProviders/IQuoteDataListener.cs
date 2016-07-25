using System;

using BidMessages;

namespace QuoteProviders
{
    /// <summary>
    /// Interface <c>IQuoteDataListener</c> defines methods that a quote data listener class must implement.
    /// </summary>
    public interface IQuoteDataListener
    {
        /// <summary>
        /// Property <c>ListenerName</c> represents the listener's name.
        /// </summary>
        string ListenerName { get; }

        /// <summary>
        /// This method, once implemented, defines what should be done once a <c>QuoteMessage</c> is received.
        /// </summary>
        /// <param name="message">the received <c>QuoteMessage</c> object.</param>
        void OnQuoteMessageReceived(QuoteMessage message);

        /// <summary>
        /// This method, once implemented, defines what should be done once an error occurs.
        /// </summary>
        /// <param name="ex">the error.</param>
        /// <param name="severe">the severity of this error: true means needs to abort; false means can try to recover.</param>
        void OnErrorOccurred(Exception ex, bool severe);
    }
}
