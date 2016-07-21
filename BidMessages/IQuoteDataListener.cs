namespace BidMessages
{
    /// <summary>
    /// Interface <c>IQuoteDataListener</c> defines methods that a quote data listener class must implement.
    /// </summary>
    public interface IQuoteDataListener
    {
		// [Xu Linqiu] Parsed - 用词不准确，本接口是被IQuoteDataProvider接口的实现者调用，
		//             IQuoteDataProvider不是IQuoteDataParser，一般此处使用的方法名为：
		//             OnQuoteMessageReceived or OnQuoteMessageArrived

		/// <summary>
		/// This method, once implemented, defines what should be done once a <c>QuoteMessage</c> is successfully parsed.
		/// </summary>
		/// <param name="msg">the parsed <c>QuoteMessage</c>.</param>
		void OnQuoteParsed(QuoteMessage msg);

		// [Xu Linqiu] 此方法用于Provider提示错误信息，一般来说方法应提供更具体的错误信息，
		//             所以方法通常定义为：
		//             void OnErrorOccurred(Exception ex)

        /// <summary>
        /// This method, once implemented, defines what should be done once an alert is raised.
        /// </summary>
        /// <param name="alert">the alert raised.</param>
        void OnAlert(string alert);
    }
}