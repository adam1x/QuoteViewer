using System;
using System.IO;
using System.Net;

using BidMessages;

namespace QuoteProviders
{
    /// <summary>
    /// Provides quote data from files.
    /// </summary>
    public class FileQuoteProvider : QuoteDataProvider
    {
        private string m_filePath;
        private FileStream m_fs;
        private BinaryReader m_reader;

        /// <summary>
        /// Initializes the new <c>LocalDataProvider</c> with a local file path and an empty subscriber list, 
        /// and sets its state to <c>Open</c>.
        /// </summary>
        /// <param name="filePath">the path to the local file containing quote data.</param>
        public FileQuoteProvider(string filePath)
        {
            m_filePath = filePath;
            m_state = Open;
        }

        /// <summary>
        /// The provider's name.
        /// </summary>
        public override string ProviderName
        {
            get { return "LocalQuoteProvider"; }
        }

        /// <summary>
        /// Opens the target file for read.
        /// Goes to states <c>Read</c> and <c>Close</c>.
        /// </summary>
        private int Open()
        {
            ChangeStatus(QuoteProviderStatus.Open);

            try
            {
                m_fs = new FileStream(m_filePath, FileMode.Open, FileAccess.Read);
                m_reader = new BinaryReader(m_fs);
                m_state = Read;
            }
            catch (Exception ex)
            {
                OnErrorOccurred(ex, true);
                m_state = Close;
            }

            return 0;
        }

        /// <summary>
        /// Reads the opened file and parses its content.
        /// Goes to states <c>Read</c> and <c>Close</c>.
        /// </summary>
        private int Read()
        {
            ChangeStatus(QuoteProviderStatus.Read);

            try
            {
                if (m_fs.Position == m_fs.Length)
                {
                    m_state = Close;
                    return 0;
                }

                QuoteMessage msg = ParseQuote();
                OnMessageParsed(msg);
            }
            catch (Exception ex)
            {
                OnErrorOccurred(ex, true);
                m_state = Close;
            }

            return 0;
        }

        /// <summary>
        /// Closes the file.
        /// </summary>
        private int Close()
        {
            ChangeStatus(QuoteProviderStatus.Close);
            
            if (m_reader != null)
            {
                m_reader.Close();
            }

            if (m_fs != null)
            {
                m_fs.Close();
            }

            m_state = null;
            ChangeStatus(QuoteProviderStatus.Inactive);
            return 0;
        }

        /// <summary>
        /// Parses a single quote message from a reading file.
        /// </summary>
        /// <param name="r">the <c>BinaryReader</c> object reading the target file.</param>
        /// <returns>The parsed <c>QuoteMessage</c> object.</returns>
        private QuoteMessage ParseQuote()
        {
            int length = IPAddress.NetworkToHostOrder(m_reader.ReadInt32());
            ushort funcCode = Bytes.NetworkToHostOrder(m_reader.ReadUInt16());
            int bodyLength = IPAddress.NetworkToHostOrder(m_reader.ReadInt32());

            byte[] body = m_reader.ReadBytes(bodyLength);

            return (QuoteMessage)BidMessage.Create(FunctionCodes.Quote, body, -BidMessage.HeaderLength, length);
        }
    }
}
