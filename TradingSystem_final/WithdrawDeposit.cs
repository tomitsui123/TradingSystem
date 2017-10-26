using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TradingSystem_final
{
    public partial class WithdrawDeposit : Form
    {
        int amount = 0;
        public WithdrawDeposit()
        {
            InitializeComponent();
        }

        private void DWOKButton_Click(object sender, EventArgs e)
        {
            String action = "";
            if (Deposit.Checked) {
                action = Deposit.Text;
            } else
            {
                action = Withdraw.Text;
            }
            this.action = action;
            
            setData(action, Convert.ToInt32(DepositWithdrawAmount.Text));
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void DWCancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
        private void DepositWithdrawAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void setData(String action, int amount)
        {
            if (action == "Deposit")
            {
                this.amount = amount;
            } else
            {
                this.amount = -amount;
            }
        }

        public int getData()
        {
            return this.amount;
        }

        public String action { get; set; }
    }
}
