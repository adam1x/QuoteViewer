using System;
using System.IO;
using System.Collections.Generic;

using BidMessages;

namespace QuoteProviders
{
    /// <summary>
    /// Class <c>LocalQuoteProvider</c> models a parser that processes local files containing quote data.
    /// </summary>
    public class LocalQuoteProvider : QuoteDataProvider
    {
        private string m_filePath;
        private FileStream m_fs;
        private BinaryReader m_reader;

        /// <summary>
        /// This constructor initializes the new <c>LocalDataProvider</c> with a local file path and an empty subscriber list, 
        /// and sets its state to <c>Open</c>.
        /// </summary>
        /// <param name="filePath">the path to the local file containing quote data.</param>
        public LocalQuoteProvider(string filePath)
        {
            m_filePath = filePath;
            m_listeners = new List<IQuoteDataListener>();
            m_state = Open;
            m_status = QuoteProviderStatus.Undefined;
        }

        /// <value>
        /// Property <c>ProviderName</c> represents the provider's name.
        /// </value>
        public override string ProviderName
        {
            get { return "LocalQuoteProvider"; }
        }

        /// <summary>
        /// This method is used to run this parser.
        /// </summary>
        public override void Run()
        {
            m_state();
        }

        /// <summary>
        /// This method signifies the <c>Open</c> state for <c>LocalDataProvider</c>.
        /// At this state, the provider opens the target file for read.
        /// It can go to states <c>Read</c> and <c>Close</c>.
        /// </summary>
        private void Open()
        {
            Status = QuoteProviderStatus.LocalOpen;

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
        }

        /// <summary>
        /// This method signifies the <c>Read</c> state for <c>LocalDataProvider</c>.
        /// At this state, the provider reads the opened file and parses its content.
        /// It can go to states <c>Read</c> and <c>Close</c>.
        /// </summary>
        private void Read()
        {
            Status = QuoteProviderStatus.LocalRead;

            try
            {
                if (m_fs.Position == m_fs.Length)
                {
                    m_state = Close;
                    return;
                }

                QuoteMessage msg = ParseQuote(m_reader);
                OnMessageParsed(msg);
            }
            catch (Exception ex)
            {
                OnErrorOccurred(ex, true);
                m_state = Close;
            }
        }

        /// <summary>
        /// This method signifies the <c>Close</c> state for <c>LocalDataProvider</c>.
        /// At this state, the provider closes the file.
        /// </summary>
        private void Close()
        {
            Status = QuoteProviderStatus.LocalClose;

            try
            {
                m_reader.Close();
            }
            catch (Exception ex)
            {
                OnErrorOccurred(ex, false);
            }
            finally
            {
                if (m_fs != null)
                {
                    m_fs.Close();
                }
            }

            m_state = null;
        }

        /// <summary>
        /// This method parses a single quote message from a reading file.
        /// </summary>
        /// <param name="r">the <c>BinaryReader</c> object reading the target file.</param>
        /// <returns>The parsed <c>QuoteMessage</c> object.</returns>
        private static QuoteMessage ParseQuote(BinaryReader r)
        {
            uint length = Bytes.NetworkToHostOrder(r.ReadUInt32());
            ushort funcCode = Bytes.NetworkToHostOrder(r.ReadUInt16());
            uint bodyLength = Bytes.NetworkToHostOrder(r.ReadUInt32());

            byte[] body = r.ReadBytes((int)bodyLength);

            return (QuoteMessage)BidMessage.Create(FunctionCodes.Quote, body, -BidMessage.HeaderLength, (int)length);
        }
    }
}
