namespace WindowsFormsViewer
{
    partial class DataViewerForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lblAuctionDateName = new System.Windows.Forms.Label();
            this.lblServerTimeName = new System.Windows.Forms.Label();
            this.lblUpdateTimestampName = new System.Windows.Forms.Label();
            this.lblLocalTimeName = new System.Windows.Forms.Label();
            this.lblQuotePriceDataName = new System.Windows.Forms.Label();
            this.lblAuctionDateLine1 = new System.Windows.Forms.Label();
            this.lblServerTimeLine1 = new System.Windows.Forms.Label();
            this.lblUpdateTimestampLine1 = new System.Windows.Forms.Label();
            this.lblLocalTimeLine1 = new System.Windows.Forms.Label();
            this.lblBidPrice = new System.Windows.Forms.Label();
            this.lblBidQuantityName = new System.Windows.Forms.Label();
            this.lblPriceUpperName = new System.Windows.Forms.Label();
            this.lblPriceLowerName = new System.Windows.Forms.Label();
            this.lblPriceIncreaseName = new System.Windows.Forms.Label();
            this.lblBidTimeName = new System.Windows.Forms.Label();
            this.lblProcessedCountName = new System.Windows.Forms.Label();
            this.lblBidQuantity = new System.Windows.Forms.Label();
            this.lblPriceUpper = new System.Windows.Forms.Label();
            this.lblPriceLower = new System.Windows.Forms.Label();
            this.lblPriceIncrease = new System.Windows.Forms.Label();
            this.lblBidTime = new System.Windows.Forms.Label();
            this.lblProcessedCount = new System.Windows.Forms.Label();
            this.txtDetailedInformation = new System.Windows.Forms.TextBox();
            this.lblLocalTimeLine2 = new System.Windows.Forms.Label();
            this.lblUpdateTimestampLine2 = new System.Windows.Forms.Label();
            this.lblServerTimeLine2 = new System.Windows.Forms.Label();
            this.lblAuctionDateLine2 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // lblAuctionDateName
            // 
            this.lblAuctionDateName.AutoSize = true;
            this.lblAuctionDateName.Location = new System.Drawing.Point(53, 46);
            this.lblAuctionDateName.Name = "lblAuctionDateName";
            this.lblAuctionDateName.Size = new System.Drawing.Size(55, 13);
            this.lblAuctionDateName.TabIndex = 0;
            this.lblAuctionDateName.Text = "拍卖日期";
            // 
            // lblServerTimeName
            // 
            this.lblServerTimeName.AutoSize = true;
            this.lblServerTimeName.Location = new System.Drawing.Point(145, 46);
            this.lblServerTimeName.Name = "lblServerTimeName";
            this.lblServerTimeName.Size = new System.Drawing.Size(55, 13);
            this.lblServerTimeName.TabIndex = 1;
            this.lblServerTimeName.Text = "系统时间";
            // 
            // lblUpdateTimestampName
            // 
            this.lblUpdateTimestampName.AutoSize = true;
            this.lblUpdateTimestampName.Location = new System.Drawing.Point(239, 46);
            this.lblUpdateTimestampName.Name = "lblUpdateTimestampName";
            this.lblUpdateTimestampName.Size = new System.Drawing.Size(55, 13);
            this.lblUpdateTimestampName.TabIndex = 2;
            this.lblUpdateTimestampName.Text = "更新时间";
            // 
            // lblLocalTimeName
            // 
            this.lblLocalTimeName.AutoSize = true;
            this.lblLocalTimeName.Location = new System.Drawing.Point(329, 46);
            this.lblLocalTimeName.Name = "lblLocalTimeName";
            this.lblLocalTimeName.Size = new System.Drawing.Size(55, 13);
            this.lblLocalTimeName.TabIndex = 3;
            this.lblLocalTimeName.Text = "本机时间";
            // 
            // lblQuotePriceDataName
            // 
            this.lblQuotePriceDataName.AutoSize = true;
            this.lblQuotePriceDataName.Location = new System.Drawing.Point(177, 189);
            this.lblQuotePriceDataName.Name = "lblQuotePriceDataName";
            this.lblQuotePriceDataName.Size = new System.Drawing.Size(79, 13);
            this.lblQuotePriceDataName.TabIndex = 4;
            this.lblQuotePriceDataName.Text = "投标价格信息";
            // 
            // lblAuctionDateLine1
            // 
            this.lblAuctionDateLine1.AutoSize = true;
            this.lblAuctionDateLine1.Location = new System.Drawing.Point(67, 82);
            this.lblAuctionDateLine1.Name = "lblAuctionDateLine1";
            this.lblAuctionDateLine1.Size = new System.Drawing.Size(27, 13);
            this.lblAuctionDateLine1.TabIndex = 5;
            this.lblAuctionDateLine1.Text = "N/A";
            this.lblAuctionDateLine1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblServerTimeLine1
            // 
            this.lblServerTimeLine1.AutoSize = true;
            this.lblServerTimeLine1.Location = new System.Drawing.Point(156, 82);
            this.lblServerTimeLine1.Name = "lblServerTimeLine1";
            this.lblServerTimeLine1.Size = new System.Drawing.Size(27, 13);
            this.lblServerTimeLine1.TabIndex = 6;
            this.lblServerTimeLine1.Text = "N/A";
            this.lblServerTimeLine1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblUpdateTimestampLine1
            // 
            this.lblUpdateTimestampLine1.AutoSize = true;
            this.lblUpdateTimestampLine1.Location = new System.Drawing.Point(250, 82);
            this.lblUpdateTimestampLine1.Name = "lblUpdateTimestampLine1";
            this.lblUpdateTimestampLine1.Size = new System.Drawing.Size(27, 13);
            this.lblUpdateTimestampLine1.TabIndex = 7;
            this.lblUpdateTimestampLine1.Text = "N/A";
            this.lblUpdateTimestampLine1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblLocalTimeLine1
            // 
            this.lblLocalTimeLine1.AutoSize = true;
            this.lblLocalTimeLine1.Location = new System.Drawing.Point(339, 82);
            this.lblLocalTimeLine1.Name = "lblLocalTimeLine1";
            this.lblLocalTimeLine1.Size = new System.Drawing.Size(27, 13);
            this.lblLocalTimeLine1.TabIndex = 8;
            this.lblLocalTimeLine1.Text = "N/A";
            this.lblLocalTimeLine1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblBidPrice
            // 
            this.lblBidPrice.AutoSize = true;
            this.lblBidPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBidPrice.Location = new System.Drawing.Point(85, 303);
            this.lblBidPrice.Name = "lblBidPrice";
            this.lblBidPrice.Size = new System.Drawing.Size(60, 31);
            this.lblBidPrice.TabIndex = 9;
            this.lblBidPrice.Text = "N/A";
            this.lblBidPrice.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblBidQuantityName
            // 
            this.lblBidQuantityName.AutoSize = true;
            this.lblBidQuantityName.Location = new System.Drawing.Point(218, 255);
            this.lblBidQuantityName.Name = "lblBidQuantityName";
            this.lblBidQuantityName.Size = new System.Drawing.Size(61, 13);
            this.lblBidQuantityName.TabIndex = 10;
            this.lblBidQuantityName.Text = "投标人数：";
            // 
            // lblPriceUpperName
            // 
            this.lblPriceUpperName.AutoSize = true;
            this.lblPriceUpperName.Location = new System.Drawing.Point(218, 279);
            this.lblPriceUpperName.Name = "lblPriceUpperName";
            this.lblPriceUpperName.Size = new System.Drawing.Size(61, 13);
            this.lblPriceUpperName.TabIndex = 11;
            this.lblPriceUpperName.Text = "出价上限：";
            // 
            // lblPriceLowerName
            // 
            this.lblPriceLowerName.AutoSize = true;
            this.lblPriceLowerName.Location = new System.Drawing.Point(218, 303);
            this.lblPriceLowerName.Name = "lblPriceLowerName";
            this.lblPriceLowerName.Size = new System.Drawing.Size(61, 13);
            this.lblPriceLowerName.TabIndex = 12;
            this.lblPriceLowerName.Text = "出价下限：";
            // 
            // lblPriceIncreaseName
            // 
            this.lblPriceIncreaseName.AutoSize = true;
            this.lblPriceIncreaseName.Location = new System.Drawing.Point(218, 327);
            this.lblPriceIncreaseName.Name = "lblPriceIncreaseName";
            this.lblPriceIncreaseName.Size = new System.Drawing.Size(61, 13);
            this.lblPriceIncreaseName.TabIndex = 13;
            this.lblPriceIncreaseName.Text = "价格涨幅：";
            // 
            // lblBidTimeName
            // 
            this.lblBidTimeName.AutoSize = true;
            this.lblBidTimeName.Location = new System.Drawing.Point(218, 349);
            this.lblBidTimeName.Name = "lblBidTimeName";
            this.lblBidTimeName.Size = new System.Drawing.Size(61, 13);
            this.lblBidTimeName.TabIndex = 14;
            this.lblBidTimeName.Text = "中标时间：";
            // 
            // lblProcessedCountName
            // 
            this.lblProcessedCountName.AutoSize = true;
            this.lblProcessedCountName.Location = new System.Drawing.Point(218, 373);
            this.lblProcessedCountName.Name = "lblProcessedCountName";
            this.lblProcessedCountName.Size = new System.Drawing.Size(61, 13);
            this.lblProcessedCountName.TabIndex = 15;
            this.lblProcessedCountName.Text = "处理数量：";
            // 
            // lblBidQuantity
            // 
            this.lblBidQuantity.AutoSize = true;
            this.lblBidQuantity.Location = new System.Drawing.Point(306, 255);
            this.lblBidQuantity.Name = "lblBidQuantity";
            this.lblBidQuantity.Size = new System.Drawing.Size(27, 13);
            this.lblBidQuantity.TabIndex = 16;
            this.lblBidQuantity.Text = "N/A";
            this.lblBidQuantity.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPriceUpper
            // 
            this.lblPriceUpper.AutoSize = true;
            this.lblPriceUpper.Location = new System.Drawing.Point(306, 279);
            this.lblPriceUpper.Name = "lblPriceUpper";
            this.lblPriceUpper.Size = new System.Drawing.Size(27, 13);
            this.lblPriceUpper.TabIndex = 17;
            this.lblPriceUpper.Text = "N/A";
            this.lblPriceUpper.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPriceLower
            // 
            this.lblPriceLower.AutoSize = true;
            this.lblPriceLower.Location = new System.Drawing.Point(305, 303);
            this.lblPriceLower.Name = "lblPriceLower";
            this.lblPriceLower.Size = new System.Drawing.Size(27, 13);
            this.lblPriceLower.TabIndex = 18;
            this.lblPriceLower.Text = "N/A";
            this.lblPriceLower.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPriceIncrease
            // 
            this.lblPriceIncrease.AutoSize = true;
            this.lblPriceIncrease.Location = new System.Drawing.Point(306, 326);
            this.lblPriceIncrease.Name = "lblPriceIncrease";
            this.lblPriceIncrease.Size = new System.Drawing.Size(27, 13);
            this.lblPriceIncrease.TabIndex = 19;
            this.lblPriceIncrease.Text = "N/A";
            this.lblPriceIncrease.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblBidTime
            // 
            this.lblBidTime.AutoSize = true;
            this.lblBidTime.Location = new System.Drawing.Point(305, 350);
            this.lblBidTime.Name = "lblBidTime";
            this.lblBidTime.Size = new System.Drawing.Size(27, 13);
            this.lblBidTime.TabIndex = 20;
            this.lblBidTime.Text = "N/A";
            this.lblBidTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblProcessedCount
            // 
            this.lblProcessedCount.AutoSize = true;
            this.lblProcessedCount.Location = new System.Drawing.Point(305, 374);
            this.lblProcessedCount.Name = "lblProcessedCount";
            this.lblProcessedCount.Size = new System.Drawing.Size(27, 13);
            this.lblProcessedCount.TabIndex = 21;
            this.lblProcessedCount.Text = "N/A";
            this.lblProcessedCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtDetailedInformation
            // 
            this.txtDetailedInformation.Location = new System.Drawing.Point(56, 431);
            this.txtDetailedInformation.Multiline = true;
            this.txtDetailedInformation.Name = "txtDetailedInformation";
            this.txtDetailedInformation.ReadOnly = true;
            this.txtDetailedInformation.Size = new System.Drawing.Size(334, 297);
            this.txtDetailedInformation.TabIndex = 22;
            // 
            // lblLocalTimeLine2
            // 
            this.lblLocalTimeLine2.AutoSize = true;
            this.lblLocalTimeLine2.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLocalTimeLine2.Location = new System.Drawing.Point(336, 101);
            this.lblLocalTimeLine2.Name = "lblLocalTimeLine2";
            this.lblLocalTimeLine2.Size = new System.Drawing.Size(60, 31);
            this.lblLocalTimeLine2.TabIndex = 26;
            this.lblLocalTimeLine2.Text = "N/A";
            this.lblLocalTimeLine2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblUpdateTimestampLine2
            // 
            this.lblUpdateTimestampLine2.AutoSize = true;
            this.lblUpdateTimestampLine2.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUpdateTimestampLine2.Location = new System.Drawing.Point(247, 101);
            this.lblUpdateTimestampLine2.Name = "lblUpdateTimestampLine2";
            this.lblUpdateTimestampLine2.Size = new System.Drawing.Size(60, 31);
            this.lblUpdateTimestampLine2.TabIndex = 25;
            this.lblUpdateTimestampLine2.Text = "N/A";
            this.lblUpdateTimestampLine2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblServerTimeLine2
            // 
            this.lblServerTimeLine2.AutoSize = true;
            this.lblServerTimeLine2.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblServerTimeLine2.Location = new System.Drawing.Point(153, 101);
            this.lblServerTimeLine2.Name = "lblServerTimeLine2";
            this.lblServerTimeLine2.Size = new System.Drawing.Size(60, 31);
            this.lblServerTimeLine2.TabIndex = 24;
            this.lblServerTimeLine2.Text = "N/A";
            this.lblServerTimeLine2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblAuctionDateLine2
            // 
            this.lblAuctionDateLine2.AutoSize = true;
            this.lblAuctionDateLine2.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAuctionDateLine2.Location = new System.Drawing.Point(64, 101);
            this.lblAuctionDateLine2.Name = "lblAuctionDateLine2";
            this.lblAuctionDateLine2.Size = new System.Drawing.Size(60, 31);
            this.lblAuctionDateLine2.TabIndex = 23;
            this.lblAuctionDateLine2.Text = "N/A";
            this.lblAuctionDateLine2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // DataViewerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(434, 761);
            this.Controls.Add(this.lblLocalTimeLine2);
            this.Controls.Add(this.lblUpdateTimestampLine2);
            this.Controls.Add(this.lblServerTimeLine2);
            this.Controls.Add(this.lblAuctionDateLine2);
            this.Controls.Add(this.txtDetailedInformation);
            this.Controls.Add(this.lblProcessedCount);
            this.Controls.Add(this.lblBidTime);
            this.Controls.Add(this.lblPriceIncrease);
            this.Controls.Add(this.lblPriceLower);
            this.Controls.Add(this.lblPriceUpper);
            this.Controls.Add(this.lblBidQuantity);
            this.Controls.Add(this.lblProcessedCountName);
            this.Controls.Add(this.lblBidTimeName);
            this.Controls.Add(this.lblPriceIncreaseName);
            this.Controls.Add(this.lblPriceLowerName);
            this.Controls.Add(this.lblPriceUpperName);
            this.Controls.Add(this.lblBidQuantityName);
            this.Controls.Add(this.lblBidPrice);
            this.Controls.Add(this.lblLocalTimeLine1);
            this.Controls.Add(this.lblUpdateTimestampLine1);
            this.Controls.Add(this.lblServerTimeLine1);
            this.Controls.Add(this.lblAuctionDateLine1);
            this.Controls.Add(this.lblQuotePriceDataName);
            this.Controls.Add(this.lblLocalTimeName);
            this.Controls.Add(this.lblUpdateTimestampName);
            this.Controls.Add(this.lblServerTimeName);
            this.Controls.Add(this.lblAuctionDateName);
            this.Name = "DataViewerForm";
            this.Text = "Quote Data Viewer";
            this.Load += new System.EventHandler(this.DataViewerForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblAuctionDateName;
        private System.Windows.Forms.Label lblServerTimeName;
        private System.Windows.Forms.Label lblUpdateTimestampName;
        private System.Windows.Forms.Label lblLocalTimeName;
        private System.Windows.Forms.Label lblQuotePriceDataName;
        private System.Windows.Forms.Label lblAuctionDateLine1;
        private System.Windows.Forms.Label lblServerTimeLine1;
        private System.Windows.Forms.Label lblUpdateTimestampLine1;
        private System.Windows.Forms.Label lblLocalTimeLine1;
        private System.Windows.Forms.Label lblBidPrice;
        private System.Windows.Forms.Label lblBidQuantityName;
        private System.Windows.Forms.Label lblPriceUpperName;
        private System.Windows.Forms.Label lblPriceLowerName;
        private System.Windows.Forms.Label lblPriceIncreaseName;
        private System.Windows.Forms.Label lblBidTimeName;
        private System.Windows.Forms.Label lblProcessedCountName;
        private System.Windows.Forms.Label lblBidQuantity;
        private System.Windows.Forms.Label lblPriceUpper;
        private System.Windows.Forms.Label lblPriceLower;
        private System.Windows.Forms.Label lblPriceIncrease;
        private System.Windows.Forms.Label lblBidTime;
        private System.Windows.Forms.Label lblProcessedCount;
        private System.Windows.Forms.TextBox txtDetailedInformation;
        private System.Windows.Forms.Label lblLocalTimeLine2;
        private System.Windows.Forms.Label lblUpdateTimestampLine2;
        private System.Windows.Forms.Label lblServerTimeLine2;
        private System.Windows.Forms.Label lblAuctionDateLine2;
        private System.Windows.Forms.Timer timer1;
    }
}