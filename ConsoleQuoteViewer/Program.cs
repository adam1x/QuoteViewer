using System;
using System.Threading;
using System.Diagnostics;

using QuoteProviders;

namespace ConsoleQuoteViewer
{
    /// <summary>
    /// Entry point of this console viewer program.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Entry point of this console viewer program.
        /// </summary>
        public static void Main()
        {
            IQuoteDataProvider provider = GetProvider();

            ConsoleViewer viewer = new ConsoleViewer(provider);

            provider.Subscribe(viewer);
            provider.StatusChanged += viewer.OnStatusChanged;
            provider.Start();

            while (true)
            {
                Thread.Sleep(100);

                if (Console.KeyAvailable)
                {
                    if (Console.ReadKey(true).Key == ConsoleKey.Escape)
                    {
                        Console.WriteLine("Manual abort.\nExiting...");
                        break;
                    }
                }
            }

            provider.Stop();
            provider.Unsubscribe(viewer);

            Console.Write("\nPress Enter to exit...");
            Console.ReadLine();
        }

        private static IQuoteDataProvider GetProvider()
        {
            IQuoteDataProvider provider = null;

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
                    break;
                }
            }

            Console.WriteLine();

            Debug.Assert(provider != null);
            return provider;
        }
    }
}
