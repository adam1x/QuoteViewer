namespace BidMessages
{
    /// <summary>
    /// Interface <c>IQuoteDataProvider</c> defines methods that a quote data provider class must implement.
    /// </summary>
    internal interface IQuoteDataProvider
    {
		// [Xu Linqiu] 以下两个方法的参数subscriber的命名，应按其类型命名为listener，这样更为直观

		/// <summary>
		/// 
		/// </summary>
		string ProviderName { get; }

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
