using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Diagnostics;

using QuoteProviders;

namespace WindowsFormsViewer
{
    public partial class DataViewerForm : Form
    {
        private QuoteDataReceiver m_receiver;

        private delegate void SetTextCallback(Control control, string text);

        public DataViewerForm()
        {
            InitializeComponent();
            m_receiver = new QuoteDataReceiver();
            m_receiver.PropertyChanged += OnDataChanged;
            m_receiver.ErrorOccurred += OnErrorOccurred;
        }

        internal QuoteDataProvider Provider
        {
            set
            {
                m_receiver.Provider = value;
            }
        }

        private void DataViewerForm_Load(object sender, EventArgs e)
        {
            Program.FormsManager.Start(this);
        }

        private void DataViewerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            m_receiver.Stop();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime time = DateTime.Now;
            SetText(lblLocalTimeLine1, time.ToString("HH:mm"));
            SetText(lblLocalTimeLine2, time.ToString("ss"));
        }

        internal void Start()
        {
            m_receiver.Start();
        }

        private void OnDataChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(m_receiver.AuctionDateLine1):
                    SetText(lblAuctionDateLine1, m_receiver.AuctionDateLine1);
                    break;

                case nameof(m_receiver.AuctionDateLine2):
                    SetText(lblAuctionDateLine2, m_receiver.AuctionDateLine2);
                    break;

                case nameof(m_receiver.ServerTimeLine1):
                    SetText(lblServerTimeLine1, m_receiver.ServerTimeLine1);
                    break;

                case nameof(m_receiver.ServerTimeLine2):
                    SetText(lblServerTimeLine2, m_receiver.ServerTimeLine2);
                    break;

                case nameof(m_receiver.UpdateTimestampLine1):
                    SetText(lblUpdateTimestampLine1, m_receiver.UpdateTimestampLine1);
                    break;

                case nameof(m_receiver.UpdateTimestampLine2):
                    SetText(lblUpdateTimestampLine2, m_receiver.UpdateTimestampLine2);
                    break;

                case nameof(m_receiver.BidPrice):
                    SetText(lblBidPrice, m_receiver.BidPrice);
                    break;

                case nameof(m_receiver.BidQuantity):
                    SetText(lblBidQuantity, m_receiver.BidQuantity);
                    break;

                case nameof(m_receiver.PriceUpper):
                    SetText(lblPriceUpper, m_receiver.PriceUpper);
                    break;

                case nameof(m_receiver.PriceLower):
                    SetText(lblPriceLower, m_receiver.PriceLower);
                    break;

                case nameof(m_receiver.PriceIncrease):
                    SetText(lblPriceIncrease, m_receiver.PriceIncrease);
                    break;

                case nameof(m_receiver.BidTime):
                    SetText(lblBidTime, m_receiver.BidTime);
                    break;

                case nameof(m_receiver.ProcessedCount):
                    SetText(lblProcessedCount, m_receiver.ProcessedCount);
                    break;

                case nameof(m_receiver.DetailedInformation):
                    SetText(txtDetailedInformation, m_receiver.DetailedInformation);
                    break;

                default:
                    break;
            }
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

        private void OnErrorOccurred(object sender, ErrorOccurredEventArgs e)
        {
            MessageBox.Show(e.Ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            if (e.Severe)
            {
                MessageBox.Show("Aborted.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
