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
    public partial class InsertNewOrder : Form
    {
        CommonFunction commonFunction = new CommonFunction();
        Dictionary<String, String> selected = new Dictionary<string, string>();
        Double commissionRate = 0;
        public InsertNewOrder(Dictionary<String, String> selected, Double lots, Double commission)
        {
            InitializeComponent();
            this.selected = selected;
            this.commissionRate = commission;
            NewOrderCustomerName.Text = selected["name"];
        }

        private void NewOrderOKButton_Click(object sender, EventArgs e)
        {
            String action = "";
            if (NewOrderActionBUY.Checked)
            {
                action = NewOrderActionBUY.Text;
            } else
            {
                action = NewOrderActionSELL.Text;
            }
            if (NewOrderCustomerName.Text != "" && NewOrderLots.Text != "" && NewOrderPrice.Text != "" && NewOrderCommission.Text != "")
            {

                int lots = Convert.ToInt32(NewOrderLots.Text);
                int price = Convert.ToInt32(NewOrderPrice.Text);
                int contractSize = Convert.ToInt32(commonFunction.getRate()["ContractSize"]);
                int commission = Convert.ToInt32(NewOrderCommission.Text);
                int cost = price * lots * contractSize + commission;
                double remainBalance = Convert.ToDouble(selected["balance"]) - cost;
                if (remainBalance < 0)
                {
                    MessageBox.Show("Balance is not enough!");
                } else
                {
                    balance = remainBalance;
                    commonFunction.CreateOpenPosition(Convert.ToInt32(selected["id"]), lots, action, price, commission);
                    this.Close();
                    this.DialogResult = DialogResult.OK;
                }
            }
        }

        private void NewOrderCancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void InsertNewOrder_Load(object sender, EventArgs e)
        {

        }

        private void NewOrderLots_KeyPress(object sender, KeyPressEventArgs e)
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

        private void NewOrderPrice_KeyPress(object sender, KeyPressEventArgs e)
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

        private void NewOrderCommission_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void Calculate_Click(object sender, EventArgs e)
        {
            if (NewOrderLots.Text != "")
            {
                Double commission = commissionRate * Convert.ToInt32(NewOrderLots.Text); 
                NewOrderCommission.Text = commission.ToString();
            }
        }

        public Double balance { get; set; }
    }
}
