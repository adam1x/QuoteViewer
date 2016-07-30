using System;

using QuoteProviders;

namespace ConsoleViewer
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
                    break;
                }
            }

            Viewer consoleViewer = new Viewer(provider);
            consoleViewer.Run();
        }
    }
}
