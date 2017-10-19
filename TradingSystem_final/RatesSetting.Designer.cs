namespace TradingSystem_final
{
    partial class RatesSetting
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
            this.label1 = new System.Windows.Forms.Label();
            this.ExchangeRate = new System.Windows.Forms.TextBox();
            this.InterestRateBUY = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.InterestRateSELL = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.ContractSize = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.CommissionRate = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.RSOKButton = new System.Windows.Forms.Button();
            this.RSCancelButton = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(127, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "USD/HKD Exchange Rate";
            // 
            // ExchangeRate
            // 
            this.ExchangeRate.Location = new System.Drawing.Point(15, 29);
            this.ExchangeRate.Name = "ExchangeRate";
            this.ExchangeRate.Size = new System.Drawing.Size(100, 22);
            this.ExchangeRate.TabIndex = 1;
            this.ExchangeRate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ExchangeRate_KeyPress);
            // 
            // InterestRateBUY
            // 
            this.InterestRateBUY.Location = new System.Drawing.Point(15, 70);
            this.InterestRateBUY.Name = "InterestRateBUY";
            this.InterestRateBUY.Size = new System.Drawing.Size(100, 22);
            this.InterestRateBUY.TabIndex = 3;
            this.InterestRateBUY.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.InterestRateBUY_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "Interest Rate (BUY)";
            // 
            // InterestRateSELL
            // 
            this.InterestRateSELL.Location = new System.Drawing.Point(15, 111);
            this.InterestRateSELL.Name = "InterestRateSELL";
            this.InterestRateSELL.Size = new System.Drawing.Size(100, 22);
            this.InterestRateSELL.TabIndex = 5;
            this.InterestRateSELL.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.InterestRateSELL_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 95);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(101, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "Interest Rate (SELL)";
            // 
            // ContractSize
            // 
            this.ContractSize.Location = new System.Drawing.Point(15, 154);
            this.ContractSize.Name = "ContractSize";
            this.ContractSize.Size = new System.Drawing.Size(100, 22);
            this.ContractSize.TabIndex = 7;
            this.ContractSize.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ContractSize_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 136);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "Contract Size";
            // 
            // CommissionRate
            // 
            this.CommissionRate.Location = new System.Drawing.Point(15, 195);
            this.CommissionRate.Name = "CommissionRate";
            this.CommissionRate.Size = new System.Drawing.Size(100, 22);
            this.CommissionRate.TabIndex = 9;
            this.CommissionRate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CommissionRate_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 179);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(87, 12);
            this.label5.TabIndex = 8;
            this.label5.Text = "Commission Rate";
            // 
            // RSOKButton
            // 
            this.RSOKButton.Location = new System.Drawing.Point(40, 226);
            this.RSOKButton.Name = "RSOKButton";
            this.RSOKButton.Size = new System.Drawing.Size(75, 23);
            this.RSOKButton.TabIndex = 10;
            this.RSOKButton.Text = "OK";
            this.RSOKButton.UseVisualStyleBackColor = true;
            this.RSOKButton.Click += new System.EventHandler(this.RSOKButton_Click);
            // 
            // RSCancelButton
            // 
            this.RSCancelButton.Location = new System.Drawing.Point(144, 226);
            this.RSCancelButton.Name = "RSCancelButton";
            this.RSCancelButton.Size = new System.Drawing.Size(75, 23);
            this.RSCancelButton.TabIndex = 11;
            this.RSCancelButton.Text = "Cancel";
            this.RSCancelButton.UseVisualStyleBackColor = true;
            this.RSCancelButton.Click += new System.EventHandler(this.RSCancelButton_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(121, 198);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(107, 12);
            this.label6.TabIndex = 12;
            this.label6.Text = " x Lots = Commission";
            // 
            // RatesSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.RSCancelButton);
            this.Controls.Add(this.RSOKButton);
            this.Controls.Add(this.CommissionRate);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.ContractSize);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.InterestRateSELL);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.InterestRateBUY);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ExchangeRate);
            this.Controls.Add(this.label1);
            this.Name = "RatesSetting";
            this.Text = "Rates Setting";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox ExchangeRate;
        private System.Windows.Forms.TextBox InterestRateBUY;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox InterestRateSELL;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox ContractSize;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox CommissionRate;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button RSOKButton;
        private System.Windows.Forms.Button RSCancelButton;
        private System.Windows.Forms.Label label6;
    }
}