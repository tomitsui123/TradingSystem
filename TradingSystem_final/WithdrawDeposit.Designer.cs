namespace TradingSystem_final
{
    partial class WithdrawDeposit
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
            this.Deposit = new System.Windows.Forms.RadioButton();
            this.Withdraw = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.DepositWithdrawAmount = new System.Windows.Forms.TextBox();
            this.DWOKButton = new System.Windows.Forms.Button();
            this.DWCancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "Action";
            // 
            // Deposit
            // 
            this.Deposit.AutoSize = true;
            this.Deposit.Location = new System.Drawing.Point(29, 51);
            this.Deposit.Name = "Deposit";
            this.Deposit.Size = new System.Drawing.Size(58, 16);
            this.Deposit.TabIndex = 1;
            this.Deposit.TabStop = true;
            this.Deposit.Text = "Deposit";
            this.Deposit.UseVisualStyleBackColor = true;
            // 
            // Withdraw
            // 
            this.Withdraw.AutoSize = true;
            this.Withdraw.Location = new System.Drawing.Point(160, 51);
            this.Withdraw.Name = "Withdraw";
            this.Withdraw.Size = new System.Drawing.Size(69, 16);
            this.Withdraw.TabIndex = 2;
            this.Withdraw.TabStop = true;
            this.Withdraw.Text = "Withdraw";
            this.Withdraw.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(27, 105);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "Amount:";
            // 
            // DepositWithdrawAmount
            // 
            this.DepositWithdrawAmount.Location = new System.Drawing.Point(79, 102);
            this.DepositWithdrawAmount.Name = "DepositWithdrawAmount";
            this.DepositWithdrawAmount.Size = new System.Drawing.Size(150, 22);
            this.DepositWithdrawAmount.TabIndex = 4;
            this.DepositWithdrawAmount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.DepositWithdrawAmount_KeyPress);
            // 
            // DWOKButton
            // 
            this.DWOKButton.Location = new System.Drawing.Point(29, 187);
            this.DWOKButton.Name = "DWOKButton";
            this.DWOKButton.Size = new System.Drawing.Size(75, 23);
            this.DWOKButton.TabIndex = 5;
            this.DWOKButton.Text = "OK";
            this.DWOKButton.UseVisualStyleBackColor = true;
            this.DWOKButton.Click += new System.EventHandler(this.DWOKButton_Click);
            // 
            // DWCancelButton
            // 
            this.DWCancelButton.Location = new System.Drawing.Point(154, 187);
            this.DWCancelButton.Name = "DWCancelButton";
            this.DWCancelButton.Size = new System.Drawing.Size(75, 23);
            this.DWCancelButton.TabIndex = 6;
            this.DWCancelButton.Text = "Cancel";
            this.DWCancelButton.UseVisualStyleBackColor = true;
            this.DWCancelButton.Click += new System.EventHandler(this.DWCancelButton_Click);
            // 
            // WithdrawDeposit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(266, 244);
            this.Controls.Add(this.DWCancelButton);
            this.Controls.Add(this.DWOKButton);
            this.Controls.Add(this.DepositWithdrawAmount);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Withdraw);
            this.Controls.Add(this.Deposit);
            this.Controls.Add(this.label1);
            this.Name = "WithdrawDeposit";
            this.Text = "Deposit/Withdraw";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton Deposit;
        private System.Windows.Forms.RadioButton Withdraw;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox DepositWithdrawAmount;
        private System.Windows.Forms.Button DWOKButton;
        private System.Windows.Forms.Button DWCancelButton;
    }
}