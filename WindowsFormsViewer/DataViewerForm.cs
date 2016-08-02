using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using System.Diagnostics;

using BidMessages;
using QuoteProviders;

namespace WindowsFormsViewer
{
    public partial class DataViewerForm : Form, IQuoteDataListener
    {
        private QuoteMessage m_previousMessage;
        private QuoteDataProvider m_provider;
        private int m_basePrice;

        private delegate void SetTextCallback(Control control, string text);

        public DataViewerForm()
        {
            InitializeComponent();
            m_previousMessage = null;
            m_provider = null;
            m_basePrice = -1;
        }

        /// <summary>
        /// The listener's name.
        /// </summary>
        public string ListenerName
        {
            get
            {
                return "WinFormsViewer";
            }
        }

        private void DataViewerForm_Load(object sender, EventArgs e)
        {
            SourceSelectionForm sourceForm = new SourceSelectionForm(this);
            sourceForm.ShowDialog();
        }

        internal void SetQuoteDataProvider(string filePath)
        {
            m_provider = new FileQuoteProvider(filePath);
        }

        internal void SetQuoteDataProvider(string serverAddress, int port, string username, string password)
        {
            m_provider = new TcpQuoteProvider(serverAddress, port, username, password);
        }

        internal void Run()
        {
            Debug.Assert(m_provider != null);
            ((IQuoteDataProvider)m_provider).Subscribe(this);
            m_provider.StatusChanged += parser_StatusChanged;

            Thread dataProvider = new Thread(RunProvider);
            dataProvider.IsBackground = true;
            dataProvider.Start();
        }

        /// <summary>
        /// Runs a <c>QuoteDataProvider</c> object.
        /// </summary>
        private void RunProvider()
        {
            while (true)
            {
                int sleep = m_provider.Run();
                Thread.Sleep(sleep);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime time = DateTime.Now;
            SetText(lblLocalTimeLine1, time.ToString("HH:mm"));
            SetText(lblLocalTimeLine2, time.ToString("ss"));
        }

        private void parser_StatusChanged(object sender, StatusChangedEventArgs e)
        {
            return;
        }

        public void OnQuoteMessageReceived(QuoteMessage message)
        {
            if ((object)m_previousMessage != null && !(message > m_previousMessage))
            {
                return;
            }

            SetText(lblAuctionDateLine1, message.AuctionDate.ToString("MMMM", new CultureInfo("zh-CN")));
            SetText(lblAuctionDateLine2, message.AuctionDate.ToString("dd"));
            SetText(lblUpdateTimestampLine1, message.UpdateTimestamp.ToString("HH:mm"));
            SetText(lblUpdateTimestampLine2, message.UpdateTimestamp.ToString("ss"));

            if (message is QuoteDataMessage)
            {
                QuoteDataMessage dataMessage = (QuoteDataMessage)message;

                SetText(lblServerTimeLine1, dataMessage.ServerTime.ToString(@"hh\:mm"));
                SetText(lblServerTimeLine2, dataMessage.ServerTime.ToString("ss"));

                SetText(lblBidPrice, dataMessage.BidPrice.ToString("N0"));
                SetText(lblBidQuantity, dataMessage.BidQuantity.ToString());
                SetText(lblPriceUpper, dataMessage.PriceUpperBound.ToString());
                SetText(lblPriceLower, dataMessage.PriceLowerBound.ToString());

                string priceIncreaseText;
                if (m_basePrice < 0)
                {
                    priceIncreaseText = "N/A";
                }
                else
                {
                    int priceIncrease = dataMessage.BidPrice - m_basePrice;
                    priceIncreaseText = priceIncrease < 0 ? "---" : priceIncrease.ToString();
                    SetText(lblPriceIncrease, priceIncreaseText);
                }

                SetText(lblBidTime, dataMessage.BidTime.ToString("HH:mm:ss"));
                SetText(lblProcessedCount, dataMessage.ProcessedCount.ToString());

                if (message is SessionAMessage)
                {
                    SessionAMessage aMessage = (SessionAMessage)message;
                    m_basePrice = aMessage.LimitPrice;

                    string template = "$(AuctionName)\r\n投放额度数：$(BidSize)\r\n本场拍卖会警示价：$(LimitPrice)\r\n拍卖会起止时间：$(AuctionBeginTime)至$(AuctionEndTime)\r\n首次出价时段：$(FirstBeginTime)至$(FirstEndTime)\r\n修改出价时段：$(SecondBeginTime)至$(SecondEndTime)\r\n\r\n    目前为首次出价时段\r\n系统目前时间：$(ServerTime)\r\n目前已投标人数：$(BidQuantity)\r\n目前最低可成交价：$(BidPrice)\r\n最低可成交价出价时间：$(BidTime)\r\n\r\n已处理出价数量：$(ProcessedCount)\r\n待处理出价数量：$(PendingCount)\r\n时段/更新时间：$(AuctionSession)$(UpdateTimestamp)";
                    SetText(txtDetailedInformation, ConstructDetailFromTemplate(template, message));
                }
                else if (message is SessionBMessage)
                {
                    SessionBMessage bMessage = (SessionBMessage)message;

                    string template = "$(AuctionName)\r\n投放额度数：$(BidSize)\r\n目前已投标人数：$(BidQuantity)\r\n拍卖会起止时间：$(AuctionBeginTime)至$(AuctionEndTime)\r\n首次出价时段：$(FirstBeginTime)至$(FirstEndTime)\r\n修改出价时段：$(SecondBeginTime)至$(SecondEndTime)\r\n\r\n    目前为修改出价时段\r\n系统目前时间：$(ServerTime)\r\n目前最低可成交价：$(BidPrice)\r\n最低可成交价出价时间：$(BidTime)\r\n目前数据库接受处理价格区间：$(BidLower)至$(BidUpper)\r\n\r\n已处理出价数量：$(ProcessedCount)\r\n待处理出价数量：$(PendingCount)\r\n时段/更新时间：$(AuctionSession)$(UpdateTimestamp)";
                    SetText(txtDetailedInformation, ConstructDetailFromTemplate(template, message));
                }
            }
            else if (message is QuoteTextMessage)
            {
                QuoteTextMessage textMessage = (QuoteTextMessage)message;

                if (message is SessionCEFHMessage)
                {
                    if (message is SessionCMessage)
                    {
                        SessionCMessage cMessage = (SessionCMessage)message;
                        SetText(lblServerTimeLine1, cMessage.ServerTime.ToString(@"hh\:mm"));
                        SetText(lblServerTimeLine2, cMessage.ServerTime.ToString("ss"));
                    }

                    SessionCEFHMessage cefhMessage = (SessionCEFHMessage)message;

                    string template = "$(ContentText)\r\n\r\n时段/更新时间：$(AuctionSession)$(UpdateTimestamp)";
                    SetText(txtDetailedInformation, ConstructDetailFromTemplate(template, message));
                }
                else if (message is SessionDGMessage)
                {
                    SessionDGMessage dgMessage = (SessionDGMessage)message;
                    SetText(lblProcessedCount, dgMessage.ProcessedCount.ToString());

                    string template = "$(ContentText)\r\n\r\n已处理出价数量：$(ProcessedCount)\r\n待处理出价数量：$(PendingCount)\r\n时段/更新时间：$(AuctionSession)$(UpdateTimestamp)";
                    SetText(txtDetailedInformation, ConstructDetailFromTemplate(template, message));
                }
            }

            m_previousMessage = message;
        }

        private void SetText(Control control, string text)
        {
            if (control.InvokeRequired)
            {
                SetTextCallback d = SetText;
                Invoke(d, control, text);
            }
            else
            {
                control.Text = text;
            }
        }

        private string ConstructDetailFromTemplate(string template, QuoteMessage message)
        {
            string result = template;

            foreach (QuoteFieldTags tag in Enum.GetValues(typeof(QuoteFieldTags)))
            {
                string tagText = Enum.GetName(typeof(QuoteFieldTags), tag);
                if (result.Contains(tagText))
                {
                    result = result.Replace(string.Format("$({0})", tagText), message.GetFieldValueAsString(message.GetIndexFromTag(tag)));
                }
            }

            return result;
        }

        public void OnErrorOccurred(Exception ex, bool severe)
        {
            MessageBox.Show("Error: " + ex.Message);
        }
    }
}
