using System;

using BidMessages;
using QuoteProviders;

namespace ConsoleViewer
{
    /// <summary>
    /// Class <c>Viewer</c> models a console application that displays quote data.
    /// </summary>
    class Viewer : IQuoteDataListener
    {
        /// <summary>
        /// This field represents the previously received message.
        /// </summary>
        private QuoteMessage m_prevMessage;

        public Viewer()
        {
            m_prevMessage = null;
        }

        /// <value>
        /// Property <c>Listener</c> represents the listener's name.
        /// </value>
        public string ListenerName
        {
            get { return "ConsoleViewer"; }
        }

        /// <summary>
        /// This method is used to start displaying.
        /// </summary>
        public void Run()
        {
            QuoteDataProvider parser = null;

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

                        parser = new TcpQuoteProvider("180.166.86.198", 8301, username, password);
                        break;

                    case ConsoleKey.D2:
                        Console.Write("Local path: ");
                        string filePath = Console.ReadLine();

                        parser = new LocalQuoteProvider(filePath);
                        break;

                    default:
                        Console.WriteLine("Invalid response. Try Again...");
                        break;
                }

                if (parser != null)
                {
                    break;
                }
            }

            IQuoteDataProvider provider = parser;
            provider.Subscribe(this);

            while (parser.CurrentState != null)
            {
                parser.Run();

                if (Console.KeyAvailable)
                {
                    if (Console.ReadKey(true).Key == ConsoleKey.Escape)
                    {
                        Console.WriteLine("Manual abort.\nExiting...");
                        provider.Unsubscribe(this);
                        break;
                    }
                }
            }

            Console.Write("\nPress Enter to exit...");
            Console.ReadLine();
        }

        /// <summary>
        /// This method displays the received <c>QuoteMessage</c> object in a console window.
        /// </summary>
        /// <param name="msg">the received <c>QuoteMessage</c>.</param>
        public void OnQuoteMessageReceived(QuoteMessage msg)
        {
            if ((object)m_prevMessage != null && !(msg > m_prevMessage))
            {
                return;
            }

            Console.WriteLine("Message:");
            Console.WriteLine("--Update timestamp: {0}", msg.GetFieldValueAsDateTime(msg.GetIndexFromTag(QuoteFieldTags.UpdateTimestamp)));
            Console.WriteLine("--Auction session: {0}", msg.GetFieldValueAsAuctionSessions());

            if (msg is QuoteDataMessage)
            {
                Console.WriteLine("--Initial price flag: {0}", msg.GetFieldValueAsInt32(msg.GetIndexFromTag(QuoteFieldTags.InitialPriceFlag)));
                Console.WriteLine("--Auction name: {0}", msg.GetFieldValueAsString(msg.GetIndexFromTag(QuoteFieldTags.AuctionName)));
                Console.WriteLine("--Bid size: {0}", msg.GetFieldValueAsInt32(msg.GetIndexFromTag(QuoteFieldTags.BidSize)));

                if (msg is SessionAMessage)
                {
                    Console.WriteLine("--Limit price: {0}", msg.GetFieldValueAsInt32(msg.GetIndexFromTag(QuoteFieldTags.LimitPrice)));
                    Console.WriteLine("--Initial price: {0}", msg.GetFieldValueAsInt32(msg.GetIndexFromTag(QuoteFieldTags.InitialPrice)));
                    Console.WriteLine("--Auction begin time: {0}", msg.GetFieldValueAsTimeSpan(msg.GetIndexFromTag(QuoteFieldTags.AuctionBeginTime)));
                    Console.WriteLine("--Auction end time: {0}", msg.GetFieldValueAsTimeSpan(msg.GetIndexFromTag(QuoteFieldTags.AuctionEndTime)));
                    Console.WriteLine("--First begin time: {0}", msg.GetFieldValueAsTimeSpan(msg.GetIndexFromTag(QuoteFieldTags.FirstBeginTime)));
                    Console.WriteLine("--First end time: {0}", msg.GetFieldValueAsTimeSpan(msg.GetIndexFromTag(QuoteFieldTags.FirstEndTime)));
                    Console.WriteLine("--Second begin time: {0}", msg.GetFieldValueAsTimeSpan(msg.GetIndexFromTag(QuoteFieldTags.SecondBeginTime)));
                    Console.WriteLine("--Second end time: {0}", msg.GetFieldValueAsTimeSpan(msg.GetIndexFromTag(QuoteFieldTags.SecondEndTime)));
                    Console.WriteLine("--Server time: {0}", msg.GetFieldValueAsTimeSpan(msg.GetIndexFromTag(QuoteFieldTags.ServerTime)));
                    Console.WriteLine("--Bid quantity: {0}", msg.GetFieldValueAsInt32(msg.GetIndexFromTag(QuoteFieldTags.BidQuantity)));
                    Console.WriteLine("--Bid price: {0}", msg.GetFieldValueAsInt32(msg.GetIndexFromTag(QuoteFieldTags.BidPrice)));
                    Console.WriteLine("--Bid time: {0}", msg.GetFieldValueAsDateTime(msg.GetIndexFromTag(QuoteFieldTags.BidTime)));
                    Console.WriteLine("--Processed count: {0}", msg.GetFieldValueAsInt32(msg.GetIndexFromTag(QuoteFieldTags.ProcessedCount)));
                    Console.WriteLine("--Pending count: {0}", msg.GetFieldValueAsInt32(msg.GetIndexFromTag(QuoteFieldTags.PendingCount)));
                }
                else if (msg is SessionBMessage)
                {
                    Console.WriteLine("--Bid quantity: {0}", msg.GetFieldValueAsInt32(msg.GetIndexFromTag(QuoteFieldTags.BidQuantity)));
                    Console.WriteLine("--Initial price: {0}", msg.GetFieldValueAsInt32(msg.GetIndexFromTag(QuoteFieldTags.InitialPrice)));
                    Console.WriteLine("--Auction begin time: {0}", msg.GetFieldValueAsTimeSpan(msg.GetIndexFromTag(QuoteFieldTags.AuctionBeginTime)));
                    Console.WriteLine("--Auction end time: {0}", msg.GetFieldValueAsTimeSpan(msg.GetIndexFromTag(QuoteFieldTags.AuctionEndTime)));
                    Console.WriteLine("--First begin time: {0}", msg.GetFieldValueAsTimeSpan(msg.GetIndexFromTag(QuoteFieldTags.FirstBeginTime)));
                    Console.WriteLine("--First end time: {0}", msg.GetFieldValueAsTimeSpan(msg.GetIndexFromTag(QuoteFieldTags.FirstEndTime)));
                    Console.WriteLine("--Second begin time: {0}", msg.GetFieldValueAsTimeSpan(msg.GetIndexFromTag(QuoteFieldTags.SecondBeginTime)));
                    Console.WriteLine("--Second end time: {0}", msg.GetFieldValueAsTimeSpan(msg.GetIndexFromTag(QuoteFieldTags.SecondEndTime)));
                    Console.WriteLine("--Server time: {0}", msg.GetFieldValueAsTimeSpan(msg.GetIndexFromTag(QuoteFieldTags.ServerTime)));
                    Console.WriteLine("--Bid price: {0}", msg.GetFieldValueAsInt32(msg.GetIndexFromTag(QuoteFieldTags.BidPrice)));
                    Console.WriteLine("--Bid time: {0}", msg.GetFieldValueAsDateTime(msg.GetIndexFromTag(QuoteFieldTags.BidTime)));
                    Console.WriteLine("--Bid lower: {0}", msg.GetFieldValueAsInt32(msg.GetIndexFromTag(QuoteFieldTags.BidLower)));
                    Console.WriteLine("--Bid upper: {0}", msg.GetFieldValueAsInt32(msg.GetIndexFromTag(QuoteFieldTags.BidUpper)));
                    Console.WriteLine("--Processed count: {0}", msg.GetFieldValueAsInt32(msg.GetIndexFromTag(QuoteFieldTags.ProcessedCount)));
                    Console.WriteLine("--Pending count: {0}", msg.GetFieldValueAsInt32(msg.GetIndexFromTag(QuoteFieldTags.PendingCount)));
                }
            }
            else if (msg is QuoteTextMessage)
            {
                Console.WriteLine("--Content text: {0}", msg.GetFieldValueAsString(msg.GetIndexFromTag(QuoteFieldTags.ContentText)));

                if (msg is SessionCEFHMessage)
                {
                    // nothing more
                }
                else if (msg is SessionDGMessage)
                {
                    Console.WriteLine("--Processed count: {0}", msg.GetFieldValueAsInt32(msg.GetIndexFromTag(QuoteFieldTags.ProcessedCount)));
                    Console.WriteLine("--Pending count: {0}", msg.GetFieldValueAsInt32(msg.GetIndexFromTag(QuoteFieldTags.PendingCount)));
                }
            }

            Console.WriteLine();
        }

        /// <summary>
        /// This method prints out notifications based on status change.
        /// </summary>
        /// <param name="previous">the previous state.</param>
        /// <param name="current">the current state.</param>
        public void OnStatusChanged(QuoteProviderStatus previous, QuoteProviderStatus current)
        {
            if (previous == QuoteProviderStatus.TcpConnect && current == QuoteProviderStatus.TcpAuthenticate)
            {
                Console.WriteLine("Connected to remote host.");
            }
            else if (previous == QuoteProviderStatus.TcpAuthenticate && current == QuoteProviderStatus.TcpInitReceive)
            {
                Console.WriteLine("Successfully logged in.\n");
            }
            else if (current == QuoteProviderStatus.TcpCreate)
            {
                Console.WriteLine("Attempting reconnection...");
            }
        }

        /// <summary>
        /// This method prints out the received error.
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
