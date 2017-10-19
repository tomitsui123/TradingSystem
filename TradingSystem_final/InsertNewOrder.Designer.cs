namespace TradingSystem_final
{
    partial class InsertNewOrder
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
            this.NewOrderActionBUY = new System.Windows.Forms.RadioButton();
            this.NewOrderActionSELL = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.NewOrderCustomerName = new System.Windows.Forms.TextBox();
            this.NewOrderLots = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.NewOrderCommission = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.Calculate = new System.Windows.Forms.Button();
            this.NewOrderOKButton = new System.Windows.Forms.Button();
            this.NewOrderCancelButton = new System.Windows.Forms.Button();
            this.NewOrderPrice = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(39, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "Action";
            // 
            // NewOrderActionBUY
            // 
            this.NewOrderActionBUY.AutoSize = true;
            this.NewOrderActionBUY.Location = new System.Drawing.Point(41, 37);
            this.NewOrderActionBUY.Name = "NewOrderActionBUY";
            this.NewOrderActionBUY.Size = new System.Drawing.Size(47, 16);
            this.NewOrderActionBUY.TabIndex = 1;
            this.NewOrderActionBUY.TabStop = true;
            this.NewOrderActionBUY.Text = "BUY";
            this.NewOrderActionBUY.UseVisualStyleBackColor = true;
            // 
            // NewOrderActionSELL
            // 
            this.NewOrderActionSELL.AutoSize = true;
            this.NewOrderActionSELL.Location = new System.Drawing.Point(166, 37);
            this.NewOrderActionSELL.Name = "NewOrderActionSELL";
            this.NewOrderActionSELL.Size = new System.Drawing.Size(50, 16);
            this.NewOrderActionSELL.TabIndex = 2;
            this.NewOrderActionSELL.TabStop = true;
            this.NewOrderActionSELL.Text = "SELL";
            this.NewOrderActionSELL.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(41, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "Customer Name";
            // 
            // NewOrderCustomerName
            // 
            this.NewOrderCustomerName.Location = new System.Drawing.Point(41, 76);
            this.NewOrderCustomerName.Name = "NewOrderCustomerName";
            this.NewOrderCustomerName.Size = new System.Drawing.Size(243, 22);
            this.NewOrderCustomerName.TabIndex = 4;
            // 
            // NewOrderLots
            // 
            this.NewOrderLots.Location = new System.Drawing.Point(41, 129);
            this.NewOrderLots.Name = "NewOrderLots";
            this.NewOrderLots.Size = new System.Drawing.Size(106, 22);
            this.NewOrderLots.TabIndex = 6;
            this.NewOrderLots.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.NewOrderLots_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(41, 113);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(25, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "Lots";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(178, 113);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(28, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "Price";
            // 
            // NewOrderCommission
            // 
            this.NewOrderCommission.Location = new System.Drawing.Point(41, 177);
            this.NewOrderCommission.Name = "NewOrderCommission";
            this.NewOrderCommission.Size = new System.Drawing.Size(106, 22);
            this.NewOrderCommission.TabIndex = 10;
            this.NewOrderCommission.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.NewOrderCommission_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(41, 161);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(63, 12);
            this.label5.TabIndex = 9;
            this.label5.Text = "Commission";
            // 
            // Calculate
            // 
            this.Calculate.Location = new System.Drawing.Point(178, 175);
            this.Calculate.Name = "Calculate";
            this.Calculate.Size = new System.Drawing.Size(106, 23);
            this.Calculate.TabIndex = 11;
            this.Calculate.Text = "Calculate";
            this.Calculate.UseVisualStyleBackColor = true;
            this.Calculate.Click += new System.EventHandler(this.Calculate_Click);
            // 
            // NewOrderOKButton
            // 
            this.NewOrderOKButton.Location = new System.Drawing.Point(41, 217);
            this.NewOrderOKButton.Name = "NewOrderOKButton";
            this.NewOrderOKButton.Size = new System.Drawing.Size(106, 23);
            this.NewOrderOKButton.TabIndex = 12;
            this.NewOrderOKButton.Text = "OK";
            this.NewOrderOKButton.UseVisualStyleBackColor = true;
            this.NewOrderOKButton.Click += new System.EventHandler(this.NewOrderOKButton_Click);
            // 
            // NewOrderCancelButton
            // 
            this.NewOrderCancelButton.Location = new System.Drawing.Point(178, 217);
            this.NewOrderCancelButton.Name = "NewOrderCancelButton";
            this.NewOrderCancelButton.Size = new System.Drawing.Size(106, 23);
            this.NewOrderCancelButton.TabIndex = 13;
            this.NewOrderCancelButton.Text = "Cancel";
            this.NewOrderCancelButton.UseVisualStyleBackColor = true;
            this.NewOrderCancelButton.Click += new System.EventHandler(this.NewOrderCancelButton_Click);
            // 
            // NewOrderPrice
            // 
            this.NewOrderPrice.Location = new System.Drawing.Point(178, 129);
            this.NewOrderPrice.Name = "NewOrderPrice";
            this.NewOrderPrice.Size = new System.Drawing.Size(106, 22);
            this.NewOrderPrice.TabIndex = 14;
            this.NewOrderPrice.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.NewOrderPrice_KeyPress);
            // 
            // InsertNewOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(336, 276);
            this.Controls.Add(this.NewOrderPrice);
            this.Controls.Add(this.NewOrderCancelButton);
            this.Controls.Add(this.NewOrderOKButton);
            this.Controls.Add(this.Calculate);
            this.Controls.Add(this.NewOrderCommission);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.NewOrderLots);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.NewOrderCustomerName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.NewOrderActionSELL);
            this.Controls.Add(this.NewOrderActionBUY);
            this.Controls.Add(this.label1);
            this.Name = "InsertNewOrder";
            this.Text = "Insert New Order";
            this.Load += new System.EventHandler(this.InsertNewOrder_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton NewOrderActionBUY;
        private System.Windows.Forms.RadioButton NewOrderActionSELL;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox NewOrderCustomerName;
        private System.Windows.Forms.TextBox NewOrderLots;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox NewOrderCommission;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button Calculate;
        private System.Windows.Forms.Button NewOrderOKButton;
        private System.Windows.Forms.Button NewOrderCancelButton;
        private System.Windows.Forms.TextBox NewOrderPrice;
    }
}