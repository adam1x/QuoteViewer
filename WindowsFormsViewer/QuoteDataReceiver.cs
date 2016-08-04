using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using System.Threading;
using System.ComponentModel;
using System.Diagnostics;

using BidMessages;
using QuoteProviders;

namespace WindowsFormsViewer
{
    public class QuoteDataReceiver : INotifyPropertyChanged, IQuoteDataListener
    {
        private QuoteMessage m_previousMessage;
        private IQuoteDataProvider m_provider;
        private Thread m_providerThread;
        private AutoResetEvent m_stopSignal;
        private int m_basePrice;

        private string m_auctionDateLine1 = "N/A";
        private string m_auctionDateLine2 = "N/A";
        private string m_serverTimeLine1 = "N/A";
        private string m_serverTimeLine2 = "N/A";
        private string m_updateTimestampLine1 = "N/A";
        private string m_updateTimestampLine2 = "N/A";
        private string m_bidPrice = "N/A";
        private string m_bidQuantity = "N/A";
        private string m_priceUpper = "N/A";
        private string m_priceLower = "N/A";
        private string m_priceIncrease = "N/A";
        private string m_bidTime = "N/A";
        private string m_processedCount = "N/A";
        private string m_detailedInformation = "N/A";

        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler<ErrorOccurredEventArgs> ErrorOccurred;

        public QuoteDataReceiver()
        {
            m_previousMessage = null;
            m_provider = null;
            m_providerThread = null;
            m_stopSignal = new AutoResetEvent(false);
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

        internal IQuoteDataProvider Provider
        {
            set
            {
                m_provider = value;
            }
        }

        internal string AuctionDateLine1
        {
            get
            {
                return m_auctionDateLine1;
            }
            private set
            {
                if (m_auctionDateLine1 != value)
                {
                    m_auctionDateLine1 = value;
                    OnPropertyChanged(nameof(AuctionDateLine1));
                }
            }
        }

        internal string AuctionDateLine2
        {
            get
            {
                return m_auctionDateLine2;
            }
            private set
            {
                if (m_auctionDateLine2 != value)
                {
                    m_auctionDateLine2 = value;
                    OnPropertyChanged(nameof(AuctionDateLine2));
                }
            }
        }

        internal string ServerTimeLine1
        {
            get
            {
                return m_serverTimeLine1;
            }
            private set
            {
                if (m_serverTimeLine1 != value)
                {
                    m_serverTimeLine1 = value;
                    OnPropertyChanged(nameof(ServerTimeLine1));
                }
            }
        }

        internal string ServerTimeLine2
        {
            get
            {
                return m_serverTimeLine2;
            }
            private set
            {
                if (m_serverTimeLine2 != value)
                {
                    m_serverTimeLine2 = value;
                    OnPropertyChanged(nameof(ServerTimeLine2));
                }
            }
        }

        internal string UpdateTimestampLine1
        {
            get
            {
                return m_updateTimestampLine1;
            }
            private set
            {
                if (m_updateTimestampLine1 != value)
                {
                    m_updateTimestampLine1 = value;
                    OnPropertyChanged(nameof(UpdateTimestampLine1));
                }
            }
        }

        internal string UpdateTimestampLine2
        {
            get
            {
                return m_updateTimestampLine2;
            }
            private set
            {
                if (m_updateTimestampLine2 != value)
                {
                    m_updateTimestampLine2 = value;
                    OnPropertyChanged(nameof(UpdateTimestampLine2));
                }
            }
        }

        internal string BidPrice
        {
            get
            {
                return m_bidPrice;
            }
            private set
            {
                if (m_bidPrice != value)
                {
                    m_bidPrice = value;
                    OnPropertyChanged(nameof(BidPrice));
                }
            }
        }

        internal string BidQuantity
        {
            get
            {
                return m_bidQuantity;
            }
            private set
            {
                if (m_bidQuantity != value)
                {
                    m_bidQuantity = value;
                    OnPropertyChanged(nameof(BidQuantity));
                }
            }
        }

        internal string PriceUpper
        {
            get
            {
                return m_priceUpper;
            }
            private set
            {
                if (m_priceUpper != value)
                {
                    m_priceUpper = value;
                    OnPropertyChanged(nameof(PriceUpper));
                }
            }
        }

        internal string PriceLower
        {
            get
            {
                return m_priceLower;
            }
            private set
            {
                if (m_priceLower != value)
                {
                    m_priceLower = value;
                    OnPropertyChanged(nameof(PriceLower));
                }
            }
        }

        internal string PriceIncrease
        {
            get
            {
                return m_priceIncrease;
            }
            private set
            {
                if (m_priceIncrease != value)
                {
                    m_priceIncrease = value;
                    OnPropertyChanged(nameof(PriceIncrease));
                }
            }
        }

        internal string BidTime
        {
            get
            {
                return m_bidTime;
            }
            private set
            {
                if (m_bidTime != value)
                {
                    m_bidTime = value;
                    OnPropertyChanged(nameof(BidTime));
                }
            }
        }

        internal string ProcessedCount
        {
            get
            {
                return m_processedCount;
            }
            private set
            {
                if (m_processedCount != value)
                {
                    m_processedCount = value;
                    OnPropertyChanged(nameof(ProcessedCount));
                }
            }
        }

        internal string DetailedInformation
        {
            get
            {
                return m_detailedInformation;
            }
            private set
            {
                if (m_detailedInformation != value)
                {
                    m_detailedInformation = value;
                    OnPropertyChanged(nameof(DetailedInformation));
                }
            }
        }

        internal void Start()
        {
            Debug.Assert(m_provider != null);
            m_provider.Subscribe(this);
            m_provider.StatusChanged += OnStatusChanged;

            m_providerThread = new Thread(RunProvider);
            m_providerThread.Start();
        }

        internal void Stop()
        {
            m_provider.Unsubscribe(this);
            m_stopSignal = new AutoResetEvent(true);

            if (m_providerThread != null)
            {
                m_providerThread.Join();
            }
        }

        /// <summary>
        /// Runs a quote data provider.
        /// </summary>
        private void RunProvider()
        {
            int sleep = 0;

            while (!m_stopSignal.WaitOne(sleep))
            {
                sleep = m_provider.Run();
            }
        }
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        protected virtual void OnErrorOccurred(ErrorOccurredEventArgs e)
        {
            EventHandler<ErrorOccurredEventArgs> handler = ErrorOccurred;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public void OnQuoteMessageReceived(QuoteMessage message)
        {
            if ((object)m_previousMessage != null && !(message > m_previousMessage))
            {
                return;
            }

            AuctionDateLine1 = message.AuctionDate.ToString("MMMM", new CultureInfo("zh-CN"));
            AuctionDateLine2 = message.AuctionDate.ToString("dd");
            UpdateTimestampLine1 = message.UpdateTimestamp.ToString("HH:mm");
            UpdateTimestampLine2 = message.UpdateTimestamp.ToString("ss");

            if (message is QuoteDataMessage)
            {
                QuoteDataMessage dataMessage = (QuoteDataMessage)message;

                ServerTimeLine1 = dataMessage.ServerTime.ToString(@"hh\:mm");
                ServerTimeLine2 = dataMessage.ServerTime.ToString("ss");

                BidPrice = dataMessage.BidPrice.ToString("N0");
                BidQuantity = dataMessage.BidQuantity.ToString();
                PriceUpper = dataMessage.PriceUpperBound.ToString();
                PriceLower = dataMessage.PriceLowerBound.ToString();

                string priceIncreaseText;
                if (m_basePrice < 0)
                {
                    priceIncreaseText = "N/A";
                }
                else
                {
                    int priceIncrease = dataMessage.BidPrice - m_basePrice;
                    priceIncreaseText = priceIncrease < 0 ? "---" : priceIncrease.ToString();
                    PriceIncrease = priceIncreaseText;
                }

                BidTime = dataMessage.BidTime.ToString("HH:mm:ss");
                ProcessedCount = dataMessage.ProcessedCount.ToString();

                if (message is SessionAMessage)
                {
                    SessionAMessage aMessage = (SessionAMessage)message;
                    m_basePrice = aMessage.LimitPrice;

                    string template = "$(AuctionName)\r\n投放额度数：$(BidSize)\r\n本场拍卖会警示价：$(LimitPrice)\r\n拍卖会起止时间：$(AuctionBeginTime)至$(AuctionEndTime)\r\n首次出价时段：$(FirstBeginTime)至$(FirstEndTime)\r\n修改出价时段：$(SecondBeginTime)至$(SecondEndTime)\r\n\r\n    目前为首次出价时段\r\n系统目前时间：$(ServerTime)\r\n目前已投标人数：$(BidQuantity)\r\n目前最低可成交价：$(BidPrice)\r\n最低可成交价出价时间：$(BidTime)\r\n\r\n已处理出价数量：$(ProcessedCount)\r\n待处理出价数量：$(PendingCount)\r\n时段/更新时间：$(AuctionSession)$(UpdateTimestamp)";
                    DetailedInformation = ConstructDetailFromTemplate(template, message);
                }
                else if (message is SessionBMessage)
                {
                    SessionBMessage bMessage = (SessionBMessage)message;

                    string template = "$(AuctionName)\r\n投放额度数：$(BidSize)\r\n目前已投标人数：$(BidQuantity)\r\n拍卖会起止时间：$(AuctionBeginTime)至$(AuctionEndTime)\r\n首次出价时段：$(FirstBeginTime)至$(FirstEndTime)\r\n修改出价时段：$(SecondBeginTime)至$(SecondEndTime)\r\n\r\n    目前为修改出价时段\r\n系统目前时间：$(ServerTime)\r\n目前最低可成交价：$(BidPrice)\r\n最低可成交价出价时间：$(BidTime)\r\n目前数据库接受处理价格区间：$(BidLower)至$(BidUpper)\r\n\r\n已处理出价数量：$(ProcessedCount)\r\n待处理出价数量：$(PendingCount)\r\n时段/更新时间：$(AuctionSession)$(UpdateTimestamp)";
                    DetailedInformation = ConstructDetailFromTemplate(template, message);
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
                        ServerTimeLine1 = cMessage.ServerTime.ToString(@"hh\:mm");
                        ServerTimeLine2 = cMessage.ServerTime.ToString("ss");
                    }

                    SessionCEFHMessage cefhMessage = (SessionCEFHMessage)message;

                    string template = "$(ContentText)\r\n\r\n时段/更新时间：$(AuctionSession)$(UpdateTimestamp)";
                    DetailedInformation = ConstructDetailFromTemplate(template, message);
                }
                else if (message is SessionDGMessage)
                {
                    SessionDGMessage dgMessage = (SessionDGMessage)message;
                    ProcessedCount = dgMessage.ProcessedCount.ToString();

                    string template = "$(ContentText)\r\n\r\n已处理出价数量：$(ProcessedCount)\r\n待处理出价数量：$(PendingCount)\r\n时段/更新时间：$(AuctionSession)$(UpdateTimestamp)";
                    DetailedInformation = ConstructDetailFromTemplate(template, message);
                }
            }

            m_previousMessage = message;
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

        public void OnStatusChanged(object sender, StatusChangedEventArgs e)
        {
            return;
        }

        public void OnErrorOccurred(Exception ex, bool severe)
        {
            OnErrorOccurred(new ErrorOccurredEventArgs(ex, severe));
        }
    }
}
