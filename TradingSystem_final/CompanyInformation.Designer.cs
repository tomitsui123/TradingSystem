namespace TradingSystem_final
{
    partial class CompanyInformation
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
            this.txtBoxCompanyName = new System.Windows.Forms.TextBox();
            this.txtBoxAddress = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.TxtBoxTel = new System.Windows.Forms.TextBox();
            this.txtBoxFax = new System.Windows.Forms.TextBox();
            this.CIOKButton = new System.Windows.Forms.Button();
            this.CICancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(35, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "Company Name";
            // 
            // txtBoxCompanyName
            // 
            this.txtBoxCompanyName.Location = new System.Drawing.Point(37, 45);
            this.txtBoxCompanyName.Name = "txtBoxCompanyName";
            this.txtBoxCompanyName.Size = new System.Drawing.Size(518, 22);
            this.txtBoxCompanyName.TabIndex = 1;
            // 
            // txtBoxAddress
            // 
            this.txtBoxAddress.Location = new System.Drawing.Point(37, 89);
            this.txtBoxAddress.Name = "txtBoxAddress";
            this.txtBoxAddress.Size = new System.Drawing.Size(518, 22);
            this.txtBoxAddress.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(35, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "Address";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(35, 116);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(20, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "Tel";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(311, 116);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(22, 12);
            this.label4.TabIndex = 5;
            this.label4.Text = "Fax";
            // 
            // TxtBoxTel
            // 
            this.TxtBoxTel.Location = new System.Drawing.Point(37, 131);
            this.TxtBoxTel.Name = "TxtBoxTel";
            this.TxtBoxTel.Size = new System.Drawing.Size(250, 22);
            this.TxtBoxTel.TabIndex = 6;
            this.TxtBoxTel.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBoxTel_KeyPress);
            // 
            // txtBoxFax
            // 
            this.txtBoxFax.Location = new System.Drawing.Point(313, 131);
            this.txtBoxFax.Name = "txtBoxFax";
            this.txtBoxFax.Size = new System.Drawing.Size(242, 22);
            this.txtBoxFax.TabIndex = 7;
            this.txtBoxFax.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBoxFax_KeyPress);
            // 
            // CIOKButton
            // 
            this.CIOKButton.Location = new System.Drawing.Point(313, 218);
            this.CIOKButton.Name = "CIOKButton";
            this.CIOKButton.Size = new System.Drawing.Size(109, 23);
            this.CIOKButton.TabIndex = 8;
            this.CIOKButton.Text = "OK";
            this.CIOKButton.UseVisualStyleBackColor = true;
            this.CIOKButton.Click += new System.EventHandler(this.CIOKButton_Click);
            // 
            // CICancelButton
            // 
            this.CICancelButton.Location = new System.Drawing.Point(453, 218);
            this.CICancelButton.Name = "CICancelButton";
            this.CICancelButton.Size = new System.Drawing.Size(102, 23);
            this.CICancelButton.TabIndex = 9;
            this.CICancelButton.Text = "Cancel";
            this.CICancelButton.UseVisualStyleBackColor = true;
            this.CICancelButton.Click += new System.EventHandler(this.CICancelButton_Click);
            // 
            // CompanyInformation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(596, 255);
            this.Controls.Add(this.CICancelButton);
            this.Controls.Add(this.CIOKButton);
            this.Controls.Add(this.txtBoxFax);
            this.Controls.Add(this.TxtBoxTel);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtBoxAddress);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtBoxCompanyName);
            this.Controls.Add(this.label1);
            this.Name = "CompanyInformation";
            this.Text = "Company Information";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtBoxCompanyName;
        private System.Windows.Forms.TextBox txtBoxAddress;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox TxtBoxTel;
        private System.Windows.Forms.TextBox txtBoxFax;
        private System.Windows.Forms.Button CIOKButton;
        private System.Windows.Forms.Button CICancelButton;
    }
}