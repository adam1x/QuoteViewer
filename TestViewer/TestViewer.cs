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
                byte[] bytes = File.ReadAllBytes(fileName);
                ushort funcCode = Bytes.ToUInt16(bytes, sizeof(uint));
                uint bodyLength = Bytes.ToUInt32(bytes, sizeof(uint) + sizeof(ushort));
                BidMessage msg = BidMessage.Create((FunctionCodes)funcCode, bytes, BidMessage.HeaderLength, (int)bodyLength);
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
            Console.WriteLine("--Update timestamp: {0}", msg.GetDateTimeValue(msg.GetIndexFromTag(QuoteFieldTags.UpdateTimestamp)));
            Console.WriteLine("--Auction session: {0}", msg.GetAuctionSessionsValue());

            if (msg is QuoteDataMessage)
            {
                Console.WriteLine("--Initial price flag: {0}", msg.GetIntValue(msg.GetIndexFromTag(QuoteFieldTags.InitialPriceFlag)));
                Console.WriteLine("--Auction name: {0}", msg.GetStringValue(msg.GetIndexFromTag(QuoteFieldTags.AuctionName)));
                Console.WriteLine("--Bid size: {0}", msg.GetIntValue(msg.GetIndexFromTag(QuoteFieldTags.BidSize)));

                if (msg is SessionAMsg)
                {
                    Console.WriteLine("--Limit price: {0}", msg.GetIntValue(msg.GetIndexFromTag(QuoteFieldTags.LimitPrice)));
                    Console.WriteLine("--Initial price: {0}", msg.GetIntValue(msg.GetIndexFromTag(QuoteFieldTags.InitialPrice)));
                    Console.WriteLine("--Auction begin time: {0}", msg.GetTimeSpanValue(msg.GetIndexFromTag(QuoteFieldTags.AuctionBeginTime)));
                    Console.WriteLine("--Auction end time: {0}", msg.GetTimeSpanValue(msg.GetIndexFromTag(QuoteFieldTags.AuctionEndTime)));
                    Console.WriteLine("--First begin time: {0}", msg.GetTimeSpanValue(msg.GetIndexFromTag(QuoteFieldTags.FirstBeginTime)));
                    Console.WriteLine("--First end time: {0}", msg.GetTimeSpanValue(msg.GetIndexFromTag(QuoteFieldTags.FirstEndTime)));
                    Console.WriteLine("--Second begin time: {0}", msg.GetTimeSpanValue(msg.GetIndexFromTag(QuoteFieldTags.SecondBeginTime)));
                    Console.WriteLine("--Second end time: {0}", msg.GetTimeSpanValue(msg.GetIndexFromTag(QuoteFieldTags.SecondEndTime)));
                    Console.WriteLine("--Server time: {0}", msg.GetTimeSpanValue(msg.GetIndexFromTag(QuoteFieldTags.ServerTime)));
                    Console.WriteLine("--Bid quantity: {0}", msg.GetIntValue(msg.GetIndexFromTag(QuoteFieldTags.BidQuantity)));
                    Console.WriteLine("--Bid price: {0}", msg.GetIntValue(msg.GetIndexFromTag(QuoteFieldTags.BidPrice)));
                    Console.WriteLine("--Bid time: {0}", msg.GetDateTimeValue(msg.GetIndexFromTag(QuoteFieldTags.BidTime)));
                    Console.WriteLine("--Processed count: {0}", msg.GetIntValue(msg.GetIndexFromTag(QuoteFieldTags.ProcessedCount)));
                    Console.WriteLine("--Pending count: {0}", msg.GetIntValue(msg.GetIndexFromTag(QuoteFieldTags.PendingCount)));
                }
                else if (msg is SessionBMsg)
                {
                    Console.WriteLine("--Bid quantity: {0}", msg.GetIntValue(msg.GetIndexFromTag(QuoteFieldTags.BidQuantity)));
                    Console.WriteLine("--Initial price: {0}", msg.GetIntValue(msg.GetIndexFromTag(QuoteFieldTags.InitialPrice)));
                    Console.WriteLine("--Auction begin time: {0}", msg.GetTimeSpanValue(msg.GetIndexFromTag(QuoteFieldTags.AuctionBeginTime)));
                    Console.WriteLine("--Auction end time: {0}", msg.GetTimeSpanValue(msg.GetIndexFromTag(QuoteFieldTags.AuctionEndTime)));
                    Console.WriteLine("--First begin time: {0}", msg.GetTimeSpanValue(msg.GetIndexFromTag(QuoteFieldTags.FirstBeginTime)));
                    Console.WriteLine("--First end time: {0}", msg.GetTimeSpanValue(msg.GetIndexFromTag(QuoteFieldTags.FirstEndTime)));
                    Console.WriteLine("--Second begin time: {0}", msg.GetTimeSpanValue(msg.GetIndexFromTag(QuoteFieldTags.SecondBeginTime)));
                    Console.WriteLine("--Second end time: {0}", msg.GetTimeSpanValue(msg.GetIndexFromTag(QuoteFieldTags.SecondEndTime)));
                    Console.WriteLine("--Server time: {0}", msg.GetTimeSpanValue(msg.GetIndexFromTag(QuoteFieldTags.ServerTime)));
                    Console.WriteLine("--Bid price: {0}", msg.GetIntValue(msg.GetIndexFromTag(QuoteFieldTags.BidPrice)));
                    Console.WriteLine("--Bid time: {0}", msg.GetDateTimeValue(msg.GetIndexFromTag(QuoteFieldTags.BidTime)));
                    Console.WriteLine("--Bid lower: {0}", msg.GetIntValue(msg.GetIndexFromTag(QuoteFieldTags.BidLower)));
                    Console.WriteLine("--Bid upper: {0}", msg.GetIntValue(msg.GetIndexFromTag(QuoteFieldTags.BidUpper)));
                    Console.WriteLine("--Processed count: {0}", msg.GetIntValue(msg.GetIndexFromTag(QuoteFieldTags.ProcessedCount)));
                    Console.WriteLine("--Pending count: {0}", msg.GetIntValue(msg.GetIndexFromTag(QuoteFieldTags.PendingCount)));
                }
            }
            else if (msg is QuoteTextMessage)
            {
                Console.WriteLine("--Content text: {0}", msg.GetStringValue(msg.GetIndexFromTag(QuoteFieldTags.ContentText)));

                if (msg is SessionCEFHMsg)
                {
                    // nothing more
                }
                else if (msg is SessionDGMsg)
                {
                    Console.WriteLine("--Processed count: {0}", msg.GetIntValue(msg.GetIndexFromTag(QuoteFieldTags.ProcessedCount)));
                    Console.WriteLine("--Pending count: {0}", msg.GetIntValue(msg.GetIndexFromTag(QuoteFieldTags.PendingCount)));
                }
            }

            Console.WriteLine();
        }
    }
}
