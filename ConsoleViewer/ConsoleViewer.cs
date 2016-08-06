using System;

using BidMessages;
using QuoteProviders;

namespace ConsoleViewer
{
    /// <summary>
    /// Models a console application that displays quote data.
    /// </summary>
    class ConsoleViewer : IQuoteDataListener
    {
        private QuoteMessage m_previousMessage;

        /// <summary>
        /// Initializes a new instance of the <c>ConsoleViewer</c> class with the given quote data provider.
        /// </summary>
        public ConsoleViewer(IQuoteDataProvider provider)
        {
            m_previousMessage = null;
        }

        /// <summary>
        /// The listener's name.
        /// </summary>
        public string ListenerName
        {
            get
            {
                return "ConsoleViewer";
            }
        }

        /// <summary>
        /// Event handler for status change. It prints out notifications.
        /// </summary>
        /// <param name="e">the event args that contains the data of this event.</param>
        public void OnStatusChanged(object sender, StatusChangedEventArgs e)
        {
            QuoteProviderStatus previous = e.Old;
            QuoteProviderStatus current = e.New;

            if (previous != QuoteProviderStatus.Inactive && current == QuoteProviderStatus.Open)
            {
                Console.WriteLine("Attempting to reopen resource...");
            }
            else if ((previous == QuoteProviderStatus.Open || previous == QuoteProviderStatus.Authenticate) && current == QuoteProviderStatus.Read)
            {
                Console.WriteLine("Resource opened successfully.\n");
            }
        }

        /// <summary>
        /// Displays the received <c>QuoteMessage</c> object in a console window.
        /// </summary>
        /// <param name="message">the received <c>QuoteMessage</c> object.</param>
        public void OnQuoteMessageReceived(QuoteMessage message)
        {
            if ((object)m_previousMessage != null && !(message > m_previousMessage))
            {
                return;
            }

            Console.WriteLine("Message:");
            Console.WriteLine("--Update timestamp: {0}", message.GetFieldValueAsDateTime(message.GetIndexFromTag(QuoteFieldTags.UpdateTimestamp)));
            Console.WriteLine("--Auction session: {0}", message.AuctionSession);

            if (message is QuoteDataMessage)
            {
                Console.WriteLine("--Initial price flag: {0}", message.GetFieldValueAsInt32(message.GetIndexFromTag(QuoteFieldTags.InitialPriceFlag)));
                Console.WriteLine("--Auction name: {0}", message.GetFieldValueAsString(message.GetIndexFromTag(QuoteFieldTags.AuctionName)));
                Console.WriteLine("--Bid size: {0}", message.GetFieldValueAsInt32(message.GetIndexFromTag(QuoteFieldTags.BidSize)));

                if (message is SessionAMessage)
                {
                    Console.WriteLine("--Limit price: {0}", message.GetFieldValueAsInt32(message.GetIndexFromTag(QuoteFieldTags.LimitPrice)));
                    Console.WriteLine("--Initial price: {0}", message.GetFieldValueAsInt32(message.GetIndexFromTag(QuoteFieldTags.InitialPrice)));
                    Console.WriteLine("--Auction begin time: {0}", message.GetFieldValueAsTimeSpan(message.GetIndexFromTag(QuoteFieldTags.AuctionBeginTime)));
                    Console.WriteLine("--Auction end time: {0}", message.GetFieldValueAsTimeSpan(message.GetIndexFromTag(QuoteFieldTags.AuctionEndTime)));
                    Console.WriteLine("--First begin time: {0}", message.GetFieldValueAsTimeSpan(message.GetIndexFromTag(QuoteFieldTags.FirstBeginTime)));
                    Console.WriteLine("--First end time: {0}", message.GetFieldValueAsTimeSpan(message.GetIndexFromTag(QuoteFieldTags.FirstEndTime)));
                    Console.WriteLine("--Second begin time: {0}", message.GetFieldValueAsTimeSpan(message.GetIndexFromTag(QuoteFieldTags.SecondBeginTime)));
                    Console.WriteLine("--Second end time: {0}", message.GetFieldValueAsTimeSpan(message.GetIndexFromTag(QuoteFieldTags.SecondEndTime)));
                    Console.WriteLine("--Server time: {0}", message.GetFieldValueAsTimeSpan(message.GetIndexFromTag(QuoteFieldTags.ServerTime)));
                    Console.WriteLine("--Bid quantity: {0}", message.GetFieldValueAsInt32(message.GetIndexFromTag(QuoteFieldTags.BidQuantity)));
                    Console.WriteLine("--Bid price: {0}", message.GetFieldValueAsInt32(message.GetIndexFromTag(QuoteFieldTags.BidPrice)));
                    Console.WriteLine("--Bid time: {0}", message.GetFieldValueAsDateTime(message.GetIndexFromTag(QuoteFieldTags.BidTime)));
                    Console.WriteLine("--Processed count: {0}", message.GetFieldValueAsInt32(message.GetIndexFromTag(QuoteFieldTags.ProcessedCount)));
                    Console.WriteLine("--Pending count: {0}", message.GetFieldValueAsInt32(message.GetIndexFromTag(QuoteFieldTags.PendingCount)));
                }
                else if (message is SessionBMessage)
                {
                    Console.WriteLine("--Bid quantity: {0}", message.GetFieldValueAsInt32(message.GetIndexFromTag(QuoteFieldTags.BidQuantity)));
                    Console.WriteLine("--Initial price: {0}", message.GetFieldValueAsInt32(message.GetIndexFromTag(QuoteFieldTags.InitialPrice)));
                    Console.WriteLine("--Auction begin time: {0}", message.GetFieldValueAsTimeSpan(message.GetIndexFromTag(QuoteFieldTags.AuctionBeginTime)));
                    Console.WriteLine("--Auction end time: {0}", message.GetFieldValueAsTimeSpan(message.GetIndexFromTag(QuoteFieldTags.AuctionEndTime)));
                    Console.WriteLine("--First begin time: {0}", message.GetFieldValueAsTimeSpan(message.GetIndexFromTag(QuoteFieldTags.FirstBeginTime)));
                    Console.WriteLine("--First end time: {0}", message.GetFieldValueAsTimeSpan(message.GetIndexFromTag(QuoteFieldTags.FirstEndTime)));
                    Console.WriteLine("--Second begin time: {0}", message.GetFieldValueAsTimeSpan(message.GetIndexFromTag(QuoteFieldTags.SecondBeginTime)));
                    Console.WriteLine("--Second end time: {0}", message.GetFieldValueAsTimeSpan(message.GetIndexFromTag(QuoteFieldTags.SecondEndTime)));
                    Console.WriteLine("--Server time: {0}", message.GetFieldValueAsTimeSpan(message.GetIndexFromTag(QuoteFieldTags.ServerTime)));
                    Console.WriteLine("--Bid price: {0}", message.GetFieldValueAsInt32(message.GetIndexFromTag(QuoteFieldTags.BidPrice)));
                    Console.WriteLine("--Bid time: {0}", message.GetFieldValueAsDateTime(message.GetIndexFromTag(QuoteFieldTags.BidTime)));
                    Console.WriteLine("--Bid lower: {0}", message.GetFieldValueAsInt32(message.GetIndexFromTag(QuoteFieldTags.BidLower)));
                    Console.WriteLine("--Bid upper: {0}", message.GetFieldValueAsInt32(message.GetIndexFromTag(QuoteFieldTags.BidUpper)));
                    Console.WriteLine("--Processed count: {0}", message.GetFieldValueAsInt32(message.GetIndexFromTag(QuoteFieldTags.ProcessedCount)));
                    Console.WriteLine("--Pending count: {0}", message.GetFieldValueAsInt32(message.GetIndexFromTag(QuoteFieldTags.PendingCount)));
                }
            }
            else if (message is QuoteTextMessage)
            {
                Console.WriteLine("--Content text: {0}", message.GetFieldValueAsString(message.GetIndexFromTag(QuoteFieldTags.ContentText)));

                if (message is SessionCEFHMessage)
                {
                    // nothing more
                }
                else if (message is SessionDGMessage)
                {
                    Console.WriteLine("--Processed count: {0}", message.GetFieldValueAsInt32(message.GetIndexFromTag(QuoteFieldTags.ProcessedCount)));
                    Console.WriteLine("--Pending count: {0}", message.GetFieldValueAsInt32(message.GetIndexFromTag(QuoteFieldTags.PendingCount)));
                }
            }

            m_previousMessage = message;
            Console.WriteLine();
        }

        /// <summary>
        /// Prints out the received error.
        /// </summary>
        /// <param name="ex">the error.</param>
        /// <param name="severe">the severity of this error: true means needs to abort; false means can try to recover.</param>
        public void OnErrorOccurred(Exception ex, bool severe)
        {
            Console.WriteLine("Error: " + ex.Message);
            if (severe)
            {
                Console.WriteLine("Unable to recover from error.");
            }
            else
            {
                Console.WriteLine("Attempting to recover from error...");
            }
        }
    }
}
