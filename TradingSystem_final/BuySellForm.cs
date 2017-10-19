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
    public partial class BuySellForm : Form
    {
        String action = "";
        int openPositionLots = 0;
        int totalLots = 0;
        int inputLots = 0;
        int settlementPositionLots = 0;
        Dictionary<String, String> data = new Dictionary<String, String>();
        Dictionary<String, String> selectedOpenedPosition = new Dictionary<String, String>();
        Dictionary<String, String> selectedSettlementPosition = new Dictionary<String, String>();
        CommonFunction commonFunction = new CommonFunction();
        public BuySellForm(String action, Dictionary<String, String> selectedOpenedPosition, Dictionary<String, String> selectedSettlementPosition)
        {
            InitializeComponent();
            openPositionLots = Int32.Parse(selectedOpenedPosition["lots"]);
            DataTable countLotsTable = commonFunction.getSqlData("select sum(Lots) as TotalLots  from SettlementPositions where valid = 1 AND Account_id = " + selectedOpenedPosition["id"]);
            foreach (DataRow row in countLotsTable.Rows)
            {
                if (row["TotalLots"].ToString() != "")
                {
                    settlementPositionLots = Int32.Parse(row["TotalLots"].ToString());
                }
            }
            TxtBoxLots.Text = selectedOpenedPosition["lots"];
            this.selectedOpenedPosition = selectedOpenedPosition;
            this.selectedSettlementPosition = selectedSettlementPosition;
            this.action = action;
            this.Text = action;
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            if (totalLots > openPositionLots)
            {
                MessageBox.Show("Settlement lots exceed.");
            } else
            {
               this.DialogResult = DialogResult.OK;
               setData(Convert.ToInt32(TxtBoxPrice.Text), Convert.ToInt32(TxtBoxLots.Text));
               this.Close();
            }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void TxtBoxPrice_KeyPress(object sender, KeyPressEventArgs e)
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

        private void TxtBoxLots_KeyPress(object sender, KeyPressEventArgs e)
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

        private void setData(int price, int lots)
        {
            data["price"] = price.ToString();
            data["lots"] = lots.ToString();
            data["oppositeDate"] = BuySellDate.Text;
            data["commission"] = "";
        }

        public Dictionary<String, String> getData()
        {
            return this.data;
         }

        private void TxtBoxLots_TextChanged(object sender, EventArgs e)
        {
            inputLots = Int32.Parse(TxtBoxLots.Text);
            totalLots = settlementPositionLots + inputLots;
        }
    }
}
