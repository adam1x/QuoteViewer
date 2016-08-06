using System;
using System.IO;
using System.Net;
using System.Threading;

using BidMessages;

namespace QuoteProviders
{
    /// <summary>
    /// Provides quote data from files.
    /// </summary>
    public class FileQuoteProvider : QuoteDataProvider
    {
        private string m_filePath;
        private FileStream m_stream;
        private BinaryReader m_reader;

        /// <summary>
        /// Initializes the new <c>LocalDataProvider</c> with a local file path and an empty subscriber list, 
        /// and sets its state to <c>Open</c>.
        /// </summary>
        /// <param name="filePath">the path to the local file containing quote data.</param>
        /// <exception cref="System.ArgumentNullException">The input filePath is null or empty.</exception>
        public FileQuoteProvider(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                throw new ArgumentNullException("filePath cannot be null or empty.");
            }

            m_filePath = filePath;
            m_stream = null;
            m_reader = null;
            m_runByState = Open;
        }

        /// <summary>
        /// The provider's name.
        /// </summary>
        public override string ProviderName
        {
            get
            {
                return "LocalQuoteProvider";
            }
        }

        /// <summary>
        /// Opens the target file for read.
        /// Goes to states <c>Read</c> and <c>Close</c>.
        /// </summary>
        /// <returns>Time to wait till next state is run, in milliseconds.</returns>
        private int Open()
        {
            ChangeStatus(QuoteProviderStatus.Open);

            try
            {
                m_stream = new FileStream(m_filePath, FileMode.Open, FileAccess.Read);
                m_reader = new BinaryReader(m_stream);
                m_runByState = Read;
            }
            catch (Exception ex)
            {
                OnErrorOccurred(ex, true);
                m_runByState = Close;
            }

            return 0;
        }

        /// <summary>
        /// Reads the opened file and parses its content.
        /// Goes to states <c>Read</c> and <c>Close</c>.
        /// </summary>
        /// <returns>Time to wait till next state is run, in milliseconds.</returns>
        private int Read()
        {
            ChangeStatus(QuoteProviderStatus.Read);

            Thread.Sleep(10); // release CPU time

            try
            {
                QuoteMessage message = ReadQuoteMessage();

                if (message == null)
                {
                    m_runByState = Close;
                    return 0;
                }

                OnQuoteMessageReceived(message);
            }
            catch (Exception ex)
            {
                OnErrorOccurred(ex, true);
                m_runByState = Close;
            }

            return 0;
        }

        /// <summary>
        /// Closes the file.
        /// Goes to state <c>Idle</c>.
        /// </summary>
        /// <returns>Time to wait till next state is run, in milliseconds.</returns>
        private int Close()
        {
            ChangeStatus(QuoteProviderStatus.Close);
            
            if (m_reader != null)
            {
                m_reader.Close();
            }

            if (m_stream != null)
            {
                m_stream.Close();
            }

            m_runByState = Idle;
            ChangeStatus(QuoteProviderStatus.Inactive);

            return 0;
        }

        /// <summary>
        /// Parses a single quote message from a reading file.
        /// </summary>
        /// <param name="r">the <c>BinaryReader</c> object reading the target file.</param>
        /// <returns>The parsed <c>QuoteMessage</c> object or null if all available objects have been read.</returns>
        private QuoteMessage ReadQuoteMessage()
        {
            try
            {
                QuoteMessage result = null;

                int length = IPAddress.NetworkToHostOrder(m_reader.ReadInt32());
                ushort funcCode = Bytes.NetworkToHostOrder(m_reader.ReadUInt16());
                int bodyLength = IPAddress.NetworkToHostOrder(m_reader.ReadInt32());

                byte[] body = new byte[length];
                int count = m_reader.Read(body, BidMessage.HeaderLength, bodyLength);

                if (count == bodyLength)
                {
                    result = (QuoteMessage)BidMessage.Create(FunctionCodes.Quote, body, 0, length);
                }

                return result;
            }
            catch (EndOfStreamException)
            {
                return null;
            }
        }
    }
}
