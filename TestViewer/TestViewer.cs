using System;
using System.IO;

using BidMessages;

namespace TestViewer
{
    /// <summary>
    /// Class <c>TestViewer</c> is a test for <c>BidMessages</c>.
    /// </summary>
    class TestViewer
    {
        /// <summary>
        /// This method is the entry point of <c>TestViewer</c>.
        /// </summary>
        public static void Main()
        {
            string testDirectory = @"C:\Users\Adam Xu\Downloads\TestMessages";
            string[] testFiles = Directory.GetFiles(testDirectory, "*.dat");

            foreach (string fileName in testFiles)
            {
                byte[] message = File.ReadAllBytes(fileName);
                int length = message.ToInt32(0);
                ushort funcCode = message.ToUInt16(sizeof(int));
                BidMessage msg = BidMessage.Create((FunctionCodes)funcCode, message, 0, length);
                PrintQuoteMessage((QuoteMessage)msg);
            }

            Console.WriteLine("Press Enter to exit...\n");
            Console.ReadLine();
        }

        /// <summary>
        /// This method prints a <c>QuoteMessage</c>.
        /// </summary>
        /// <param name="msg">the <c>QuoteMessage</c> object to be printed.</param>
        private static void PrintQuoteMessage(QuoteMessage msg)
        {
            Console.WriteLine("Quote Message:");
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
    }
}
