namespace BidMessage
{
    /// <summary>
    /// Interface <c>IQuoteDataListener</c> defines methods that a quote data listener class must implement.
    /// </summary>
    public interface IQuoteDataListener
    {
        /// <summary>
        /// This method, once implemented, defines what should be done once a <c>QuoteMessage</c> is successfully parsed.
        /// </summary>
        /// <param name="msg">the parsed <c>QuoteMessage</c>.</param>
        void OnQuoteParsed(QuoteMessage msg);

        /// <summary>
        /// This method, once implemented, defines what should be done once an alert is raised.
        /// </summary>
        /// <param name="alert">the alert raised.</param>
        void OnAlert(string alert);
    }
}