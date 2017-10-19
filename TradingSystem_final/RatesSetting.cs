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
    public partial class RatesSetting : Form
    {
        CommonFunction commonFunction = new CommonFunction();
        Dictionary<String, Double> dictionary = new Dictionary<string, double>();
        public RatesSetting()
        {
            InitializeComponent();
            dictionary = commonFunction.getRate();
            ExchangeRate.Text = dictionary["ExchangeRate"].ToString();
            InterestRateBUY.Text = dictionary["InterestRateBUY"].ToString();
            InterestRateSELL.Text = dictionary["InterestRateSELL"].ToString();
            ContractSize.Text = dictionary["ContractSize"].ToString();
            CommissionRate.Text = dictionary["ContractSize"].ToString();
        }

        private void ExchangeRate_KeyPress(object sender, KeyPressEventArgs e)
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

        private void InterestRateSELL_KeyPress(object sender, KeyPressEventArgs e)
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

        private void InterestRateBUY_KeyPress(object sender, KeyPressEventArgs e)
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

        private void ContractSize_KeyPress(object sender, KeyPressEventArgs e)
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

        private void CommissionRate_KeyPress(object sender, KeyPressEventArgs e)
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
        
        private void RSOKButton_Click(object sender, EventArgs e)
        {
            
            commonFunction.changeRates(Convert.ToDouble(ExchangeRate.Text), Convert.ToDouble(InterestRateBUY.Text), 
                Convert.ToDouble(InterestRateSELL.Text), Convert.ToDouble(ContractSize.Text), Convert.ToDouble(CommissionRate.Text));
            this.Close();
        }

        private void RSCancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
