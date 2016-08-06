using System;
using System.ComponentModel;
using System.Windows.Forms;

using QuoteProviders;
using BidMessages;

namespace FormViewer
{
    public partial class DataViewerForm : Form, IQuoteDataListener
    {
        private DataViewModel m_viewModel;
        private IQuoteDataProvider m_provider;

        private delegate void SetTextCallback(Control control, string text);

        public DataViewerForm()
        {
            InitializeComponent();
            m_viewModel = new DataViewModel();
            m_viewModel.PropertyChanged += OnDataChanged;
            m_provider = null;
        }

        public string ListenerName
        {
            get
            {
                return "WinFormsViewer";
            }
        }

        private void DataViewerForm_Load(object sender, EventArgs e)
        {
            m_provider = FormsManager.UniqueInstance.GetProvider();

            if (m_provider == null)
            {
                Close();
                return;
            }

            m_provider.Subscribe(this);
            m_provider.StatusChanged += OnStatusChanged;
            m_provider.Start();
        }

        private void DataViewerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (m_provider == null)
            {
                return;
            }

            m_provider.Stop();
            m_provider.Unsubscribe(this);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime time = DateTime.Now;
            SetText(lblLocalTimeLine1, time.ToString("HH:mm"));
            SetText(lblLocalTimeLine2, time.ToString("ss"));
        }

        private void SetText(Control control, string text)
        {
            if (control.InvokeRequired)
            {
                BeginInvoke(new SetTextCallback(SetText), control, text); // avoids deadlock when UI calls stop on provider
                return;
            }

            control.Text = text;
        }

        public void OnQuoteMessageReceived(QuoteMessage message)
        {
            m_viewModel.OnQuoteMessageReceived(message);
        }

        private void OnDataChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(m_viewModel.AuctionDateLine1):
                    SetText(lblAuctionDateLine1, m_viewModel.AuctionDateLine1);
                    break;

                case nameof(m_viewModel.AuctionDateLine2):
                    SetText(lblAuctionDateLine2, m_viewModel.AuctionDateLine2);
                    break;

                case nameof(m_viewModel.ServerTimeLine1):
                    SetText(lblServerTimeLine1, m_viewModel.ServerTimeLine1);
                    break;

                case nameof(m_viewModel.ServerTimeLine2):
                    SetText(lblServerTimeLine2, m_viewModel.ServerTimeLine2);
                    break;

                case nameof(m_viewModel.UpdateTimestampLine1):
                    SetText(lblUpdateTimestampLine1, m_viewModel.UpdateTimestampLine1);
                    break;

                case nameof(m_viewModel.UpdateTimestampLine2):
                    SetText(lblUpdateTimestampLine2, m_viewModel.UpdateTimestampLine2);
                    break;

                case nameof(m_viewModel.BidPrice):
                    SetText(lblBidPrice, m_viewModel.BidPrice);
                    break;

                case nameof(m_viewModel.BidQuantity):
                    SetText(lblBidQuantity, m_viewModel.BidQuantity);
                    break;

                case nameof(m_viewModel.PriceUpper):
                    SetText(lblPriceUpper, m_viewModel.PriceUpper);
                    break;

                case nameof(m_viewModel.PriceLower):
                    SetText(lblPriceLower, m_viewModel.PriceLower);
                    break;

                case nameof(m_viewModel.PriceIncrease):
                    SetText(lblPriceIncrease, m_viewModel.PriceIncrease);
                    break;

                case nameof(m_viewModel.BidTime):
                    SetText(lblBidTime, m_viewModel.BidTime);
                    break;

                case nameof(m_viewModel.ProcessedCount):
                    SetText(lblProcessedCount, m_viewModel.ProcessedCount);
                    break;

                case nameof(m_viewModel.DetailedInformation):
                    SetText(txtDetailedInformation, m_viewModel.DetailedInformation);
                    break;

                default:
                    break;
            }
        }

        public void OnStatusChanged(object sender, StatusChangedEventArgs e)
        {
            return;
        }

        public void OnErrorOccurred(Exception ex, bool severe)
        {
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            if (severe)
            {
                MessageBox.Show("Aborted.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
