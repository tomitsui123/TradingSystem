using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TradingSystem_final
{
    public partial class ClosingPriceForm : Form
    {
        public ClosingPriceForm()
        {
            InitializeComponent();
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            closingPrice = Convert.ToInt32(txtBoxClosingPrice.Text);
            this.DialogResult = DialogResult.OK;
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public int closingPrice {get;set;}
}
}
