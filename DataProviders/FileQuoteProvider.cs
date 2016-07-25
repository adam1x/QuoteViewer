using System;
using System.IO;
using System.Collections.Generic;
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
        /// This constructor initializes the new <c>LocalDataProvider</c> with a local file path and an empty subscriber list, 
        /// and sets its state to <c>Open</c>.
        /// </summary>
        /// <param name="filePath">the path to the local file containing quote data.</param>
        public FileQuoteProvider(string filePath)
        {
            m_filePath = filePath;
            m_state = Open;
        }

        /// <summary>
        /// Property <c>ProviderName</c> represents the provider's name.
        /// </summary>
        public override string ProviderName
        {
            get { return "LocalQuoteProvider"; }
        }

        /// <summary>
        /// This method signifies the <c>Open</c> state for <c>LocalDataProvider</c>.
        /// At this state, the provider opens the target file for read.
        /// It can go to states <c>Read</c> and <c>Close</c>.
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
        /// This method signifies the <c>Read</c> state for <c>LocalDataProvider</c>.
        /// At this state, the provider reads the opened file and parses its content.
        /// It can go to states <c>Read</c> and <c>Close</c>.
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
        /// This method signifies the <c>Close</c> state for <c>LocalDataProvider</c>.
        /// At this state, the provider closes the file.
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
        /// This method parses a single quote message from a reading file.
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
