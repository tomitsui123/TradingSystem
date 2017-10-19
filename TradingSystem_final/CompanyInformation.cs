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
    public partial class CompanyInformation : Form
    {
        CommonFunction commonFunction = new CommonFunction();
        public CompanyInformation()
        {
            Dictionary<String, String> data = commonFunction.getCompanyInfo();
            InitializeComponent();

            txtBoxCompanyName.Text = data["CompanyName"];
            txtBoxAddress.Text = data["Address"];
            TxtBoxTel.Text = data["Tel"];
            txtBoxFax.Text = data["Fax"];

        }

        private void CICancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CIOKButton_Click(object sender, EventArgs e)
        {
            String companyName = txtBoxCompanyName.Text;
            String address = txtBoxAddress.Text;
            String tel = TxtBoxTel.Text;
            String fax = txtBoxFax.Text;
            commonFunction.changeCompanyInformation(companyName, address, tel, fax);
            this.Close();
            this.DialogResult = DialogResult.OK;
        }

        private void txtBoxTel_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtBoxFax_KeyPress(object sender, KeyPressEventArgs e)
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
    }
}
