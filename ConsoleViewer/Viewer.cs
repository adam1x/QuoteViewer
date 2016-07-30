using System;
using System.Threading;

using BidMessages;
using QuoteProviders;

namespace ConsoleViewer
{
    /// <summary>
    /// Mdels a console application that displays quote data.
    /// </summary>
    class Viewer : IQuoteDataListener
    {
        /// <summary>
        /// The previously received message.
        /// </summary>
        private QuoteMessage m_previousMessage;

        /// <summary>
        /// Initializes a new instance of the <c>ConsoleViewer</c> class.
        /// </summary>
        public Viewer()
        {
            m_previousMessage = null;
        }

        /// <summary>
        /// The listener's name.
        /// </summary>
        public string ListenerName
        {
            get { return "ConsoleViewer"; }
        }

        /// <summary>
        /// Start displaying.
        /// </summary>
        public void Run()
        {
            QuoteDataProvider provider = null;

            while (true)
            {
                Console.WriteLine("Choose source: 1. Internet. 2. Local file.");
                Console.Write("Type your choice here: ");
                ConsoleKeyInfo key = Console.ReadKey();
                Console.WriteLine();

                switch (key.Key)
                {
                    case ConsoleKey.D1:
                        Console.Write("Username: ");
                        string username = Console.ReadLine();
                        Console.Write("Password: ");
                        string password = Console.ReadLine();

                        provider = new TcpQuoteProvider("180.166.86.198", 8301, username, password);
                        break;

                    case ConsoleKey.D2:
                        Console.Write("Local path: ");
                        string filePath = Console.ReadLine();

                        provider = new FileQuoteProvider(filePath);
                        break;

                    default:
                        Console.WriteLine("Invalid response. Try Again...");
                        break;
                }

                if (provider != null)
                {
                    provider.StatusChanged += parser_StatusChanged;
                    break;
                }
            }

            ((IQuoteDataProvider)provider).Subscribe(this);

            while (true)
            {
                int sleep = provider.Run();

                if (Console.KeyAvailable)
                {
                    if (Console.ReadKey(true).Key == ConsoleKey.Escape)
                    {
                        Console.WriteLine("Manual abort.\nExiting...");
                        ((IQuoteDataProvider)provider).Unsubscribe(this);
                        break;
                    }
                }

                Thread.Sleep(sleep);
            }

            Console.Write("\nPress Enter to exit...");
            Console.ReadLine();
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

            Console.WriteLine();
        }

        /// <summary>
        /// Event handler for status change. It prints out notifications.
        /// </summary>
        /// <param name="ev">the event args that contains the data of this event.</param>
        public void parser_StatusChanged(object sender, StatusChangedEventArgs ev)
        {
            QuoteProviderStatus previous = ev.Old;
            QuoteProviderStatus next = ev.New;

            if (previous != QuoteProviderStatus.Inactive && next == QuoteProviderStatus.Open)
            {
                Console.WriteLine("Attempting to reopen resource...");
            }
            else if ((previous == QuoteProviderStatus.Open || previous == QuoteProviderStatus.Authenticate) && next == QuoteProviderStatus.Read)
            {
                Console.WriteLine("Resource opened successfully.\n");
            }
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
