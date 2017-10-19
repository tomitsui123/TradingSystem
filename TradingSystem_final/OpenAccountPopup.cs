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
    public partial class OpenAccountPopup : Form
    {
        public OpenAccountPopup()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String strCustomerName = TxtName.Text;
            String strAccountNo = TxtAccountNo.Text;
            CommonFunction commonFunction = new CommonFunction();
            int intInitDeposit = 0;
            if (!int.TryParse(TxtInitDeposit.Text, out intInitDeposit))
            {
                MessageBox.Show("Deposit must be a number!");
            }
            if (strCustomerName != "" && strAccountNo != "" && intInitDeposit != 0)
            {
                commonFunction.CreateNewAccount(strCustomerName, strAccountNo, intInitDeposit);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
