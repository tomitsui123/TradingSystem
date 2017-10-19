using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf; 

namespace TradingSystem_final
{
    public partial class Form1 : Form
    {
        CommonFunction commonFunction = new CommonFunction();
        Dictionary<String, String> selectedCustomer = new Dictionary<string, string>();
        Dictionary<String, String> selectedOpenedPosition = new Dictionary<string, string>();
        Dictionary<String, String> selectedSettlementPosition = new Dictionary<string, string>();
        Dictionary<String, Double> ratesSetting = new Dictionary<string, Double>();
        public Form1()
        {
            InitializeComponent();
            TotalAmount.Text = commonFunction.getCount("Account").ToString();
            this.ratesSetting = commonFunction.getRate();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_Load_1(object sender, EventArgs e)
        {
            // TODO: 這行程式碼會將資料載入 'database1DataSet3.Account' 資料表。您可以視需要進行移動或移除。
            this.accountTableAdapter1.Fill(this.database1DataSet3.Account);
            String sql = "select * from Account where valid = 1";
            DataTable dt = commonFunction.getSqlData(sql);
            CustomerAccountTable.DataSource = dt;
            if (CustomerAccountTable.Rows.Count != 0)
            {
                selectedCustomer["id"] = CustomerAccountTable.Rows[0].Cells[0].Value.ToString();
                selectedCustomer["name"] = CustomerAccountTable.Rows[0].Cells[1].Value.ToString().Trim();
                selectedCustomer["accountNo"] = CustomerAccountTable.Rows[0].Cells[2].Value.ToString();
                selectedCustomer["balance"] = CustomerAccountTable.Rows[0].Cells[3].Value.ToString();
                BalanceAmount.Text = CustomerAccountTable.Rows[0].Cells[3].Value.ToString();
            }

        }

        private void OpenAccount_Click(object sender, EventArgs e)
        {
            OpenAccountPopup openAccountPopup = new OpenAccountPopup();
            DialogResult dialogResult = openAccountPopup.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                reloadCustomerAccount();
            }

        }

        private void CloseAccount_Click(object sender, EventArgs e)
        {
            commonFunction.deleteData("Account", Convert.ToInt32(selectedCustomer["id"]));
            reloadCustomerAccount();
        }

        private void DayEndProcess_Click(object sender, EventArgs e)
        {

        }

        private void AllStatements_Click(object sender, EventArgs e)
        {

        }

        private void CustomerAccountTable_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                String balance = CustomerAccountTable.Rows[e.RowIndex].Cells[3].Value.ToString();
                String accountId = CustomerAccountTable.Rows[e.RowIndex].Cells[0].Value.ToString();
                selectedCustomer["id"] = accountId;
                selectedCustomer["name"] = CustomerAccountTable.Rows[e.RowIndex].Cells[1].Value.ToString().Trim();
                selectedCustomer["accountNo"] = CustomerAccountTable.Rows[e.RowIndex].Cells[2].Value.ToString();
                selectedCustomer["balance"] = balance;
                BalanceAmount.Text = balance;

                reloadOpenPosition(accountId);
                reloadSettlementPositions(accountId);
                enableButton();
            }
        }

        private void NewOrder_Click(object sender, EventArgs e)
        {
            InsertNewOrder insertNewOrder = new InsertNewOrder(selectedCustomer, ratesSetting["ContractSize"], ratesSetting["CommissionRate"]);
            DialogResult dialogResult = insertNewOrder.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                String sql = "select * from OpenPosition where valid = 1 and Account_id=" + selectedCustomer["id"];
                DataTable dt = commonFunction.getSqlData(sql);
                OpenPosition.DataSource = dt;
                commonFunction.updateBalance(insertNewOrder.balance, Convert.ToInt32(selectedCustomer["id"]));
                reloadCustomerAccount();
                NewOrder.Enabled = false;
            }
        }

        private void ButtonBuy_Click(object sender, EventArgs e)
        {
            openBuySellForm("BUY");
            ButtonBuy.Enabled = false;
        }

        private void ButtonSell_Click(object sender, EventArgs e)
        {
            openBuySellForm("SELL");
            ButtonSell.Enabled = false;
        }

        public void controlBuySellButton(String action)
        {
            if (action == "BUY")
            {
                ButtonBuy.Enabled = false;
                ButtonSell.Enabled = true;
            }
            else if (action == "SELL")
            {
                ButtonBuy.Enabled = true;
                ButtonSell.Enabled = false;
            }
        }

        public void openBuySellForm(String action)
        {
            BuySellForm buySellForm = new BuySellForm(action, selectedOpenedPosition, selectedSettlementPosition);
            DialogResult dialogResult = buySellForm.ShowDialog();

            if (dialogResult == DialogResult.OK)
            {
                Dictionary<String, String> dataFromChild = buySellForm.getData();
                int buyPrice = 0;
                int sellPrice = 0;
                int inputLots = Convert.ToInt32(dataFromChild["lots"]);
                int currentLots = Convert.ToInt32(selectedOpenedPosition["lots"]);
                Double exchangeRate = Convert.ToDouble(ratesSetting["ExchangeRate"]);
                int id = Convert.ToInt32(selectedOpenedPosition["id"]);
                Double profitLoss = 0;
                Double netProfit = 0;
                int commission = Convert.ToInt32(selectedOpenedPosition["commission"]);
                int numOfDay = (Convert.ToDateTime(dataFromChild["oppositeDate"]) - DateTime.Today).Days;
                Double interest = Convert.ToInt32(selectedOpenedPosition["interest"]) * numOfDay;
                if (action == "BUY")
                {
                    buyPrice = Convert.ToInt32(dataFromChild["price"]);
                    sellPrice = Convert.ToInt32(selectedOpenedPosition["price"]);
                    profitLoss = commonFunction.createProfitLoss(buyPrice, sellPrice, inputLots, exchangeRate);
                    commonFunction.changeProfitLoss(profitLoss, id);
                    netProfit = Convert.ToDouble(profitLoss) - Convert.ToInt32(commission);
                    commonFunction.CreateSettlementPosition(selectedCustomer["id"], dataFromChild["lots"], dataFromChild["oppositeDate"],
                        dataFromChild["price"], selectedOpenedPosition["date"], selectedOpenedPosition["price"], commission, netProfit, interest.ToString(), selectedOpenedPosition["id"]);
                } else
                {
                    buyPrice = Convert.ToInt32(selectedOpenedPosition["price"]);
                    sellPrice = Convert.ToInt32(dataFromChild["price"]);
                    profitLoss = commonFunction.createProfitLoss(buyPrice, sellPrice, inputLots, exchangeRate);
                    commonFunction.changeProfitLoss(profitLoss, id);
                    netProfit = Convert.ToDouble(profitLoss) - Convert.ToInt32(commission);
                    commonFunction.CreateSettlementPosition(selectedCustomer["id"], dataFromChild["lots"], selectedOpenedPosition["date"], selectedOpenedPosition["price"],
                        dataFromChild["oppositeDate"], dataFromChild["price"], commission, netProfit, interest.ToString(), selectedOpenedPosition["id"]);
                }
                int remainLots = currentLots - inputLots;
                selectedOpenedPosition["lots"] = remainLots.ToString();
                commonFunction.updateLots(remainLots, Convert.ToInt32(selectedOpenedPosition["id"]));
                if (remainLots == 0)
                {
                    commonFunction.deleteData("OpenPosition", Convert.ToInt32(selectedOpenedPosition["id"]));
                }
                commonFunction.updateBalance(Convert.ToInt32(selectedCustomer["balance"]) + netProfit, Convert.ToInt32(selectedCustomer["id"]));
                reloadOpenPosition(selectedCustomer["id"]);
                reloadSettlementPositions(selectedCustomer["id"]);
                reloadCustomerAccount();
            }
        }

        private void BtnOpenPositionClear_Click(object sender, EventArgs e)
        {
            if (selectedOpenedPosition.ContainsKey("id"))
            {
                commonFunction.deleteData("OpenPosition", Convert.ToInt32(selectedOpenedPosition["id"]));
                reloadOpenPosition("");
            }
        }

        private void reloadCustomerAccount()
        {
            String sql = "select * from Account where valid = 1";
            DataTable dt = commonFunction.getSqlData(sql);
            CustomerAccountTable.DataSource = dt;
        }

        private void reloadOpenPosition(String id)
        {
            commonFunction.updateInterest();
            String sql = "select * from OpenPosition where valid = 1";
            if (id != "")
            {
                sql = sql + " and Account_id = " + id;
            }
            DataTable dt = commonFunction.getSqlData(sql);
            OpenPosition.DataSource = dt;
        }

        private void reloadSettlementPositions(String id)
        {
            String sql = "select * from SettlementPositions where valid = 1";
            if (id == "")
            {
                id = "-1";
            }
            sql = sql + " and Account_id = " + id;
            DataTable dt = commonFunction.getSqlData(sql);
            SettlementPositions.DataSource = dt;
        }

        private void enableButton()
        {
            NewOrder.Enabled = true;
            BtnOpenPositionClear.Enabled = true;
            BtnDepositWithdraw.Enabled = true;
        }

        private void BtnSettlemenePositionClear_Click(object sender, EventArgs e)
        {
            if (selectedSettlementPosition.ContainsKey("id"))
            {
                commonFunction.deleteData("SettlementPositions", Convert.ToInt32(selectedSettlementPosition["id"]));
                reloadSettlementPositions(selectedCustomer["id"]);
            }
        }

        private void SettlementPositions_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                BtnSettlemenePositionUp.Enabled = true;
                BtnSettlemenePositionClear.Enabled = true;
                selectedSettlementPosition["id"] = SettlementPositions.Rows[e.RowIndex].Cells[0].Value.ToString();
                selectedSettlementPosition["Account_id"] = SettlementPositions.Rows[e.RowIndex].Cells[1].Value.ToString();
                selectedSettlementPosition["lots"] = SettlementPositions.Rows[e.RowIndex].Cells[2].Value.ToString();
                selectedSettlementPosition["dateBought"] = SettlementPositions.Rows[e.RowIndex].Cells[3].Value.ToString();
                selectedSettlementPosition["priceBought"] = SettlementPositions.Rows[e.RowIndex].Cells[4].Value.ToString();
                selectedSettlementPosition["dateSold"] = SettlementPositions.Rows[e.RowIndex].Cells[5].Value.ToString();
                selectedSettlementPosition["priceSold"] = SettlementPositions.Rows[e.RowIndex].Cells[6].Value.ToString();
                selectedSettlementPosition["commission"] = SettlementPositions.Rows[e.RowIndex].Cells[7].Value.ToString();
                selectedSettlementPosition["interest"] = SettlementPositions.Rows[e.RowIndex].Cells[8].Value.ToString();
                selectedSettlementPosition["profit"] = SettlementPositions.Rows[e.RowIndex].Cells[9].Value.ToString();
                selectedSettlementPosition["OpenPosition_id"] = SettlementPositions.Rows[e.RowIndex].Cells[10].Value.ToString();
                DataTable openPosition = commonFunction.getSqlData("select * from OpenPosition where id = " + selectedSettlementPosition["OpenPosition_id"]);
                selectedOpenedPosition["id"] = openPosition.Rows[0].ItemArray[0].ToString();
                selectedOpenedPosition["date"] = openPosition.Rows[0].ItemArray[2].ToString().Trim();
                selectedOpenedPosition["lots"] = openPosition.Rows[0].ItemArray[3].ToString();
                selectedOpenedPosition["price"] = openPosition.Rows[0].ItemArray[5].ToString();
                selectedOpenedPosition["commission"] = openPosition.Rows[0].ItemArray[9].ToString();
                selectedOpenedPosition["interest"] = openPosition.Rows[0].ItemArray[6].ToString();

            }

        }

        private void BtnDepositWithdraw_Click(object sender, EventArgs e)
        {
            WithdrawDeposit withdrawDeposit = new WithdrawDeposit();
            DialogResult dialogResult = withdrawDeposit.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                int change = withdrawDeposit.getData();
                int currentAmount = Convert.ToInt32(selectedCustomer["balance"]);
                if (currentAmount + change < 0)
                {
                    MessageBox.Show("Account does not have enough balance.");
                } else
                {
                    commonFunction.changeBalance(currentAmount + change, Convert.ToInt32(selectedCustomer["id"]));
                    reloadCustomerAccount();
                }
            }
        }

        private void ratesSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RatesSetting ratesSetting = new RatesSetting();
            DialogResult dialogResult = ratesSetting.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                this.ratesSetting = commonFunction.getRate();
            }
        }

        private void dsToolStripMenuItem_Click(object sender, EventArgs e)
        {

            OpenAccountPopup openAccountPopup = new OpenAccountPopup();
            DialogResult dialogResult = openAccountPopup.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                reloadCustomerAccount();
            }
        }

        private void OneStatement_Click(object sender, EventArgs e)
        {
            Document doc = new Document(iTextSharp.text.PageSize.LETTER, 10, 10, 42, 35);
            String output = "one_statement.pdf";
            PdfWriter wri = PdfWriter.GetInstance(doc, new FileStream(output, FileMode.Create));
            doc.Open();

            String path = "./replacetest.txt";
            string text = File.ReadAllText(path);
            text = text.Replace("<replaceMe>", "new value");
            File.WriteAllText("replacetest.txt", text);
            StreamReader rdr = new StreamReader(path);
            doc.Add(new Paragraph(rdr.ReadToEnd()));
            rdr.Close();
            doc.Close();
            System.Diagnostics.Process.Start(output);
            
        }

        private void BtnSettlemenePositionUp_Click(object sender, EventArgs e)
        {
            if (selectedSettlementPosition.ContainsKey("id"))
            {
                BtnSettlemenePositionClear.Enabled = false;
                BtnSettlemenePositionUp.Enabled = false;
                commonFunction.deleteData("SettlementPositions", Convert.ToInt32(selectedSettlementPosition["id"]));
                commonFunction.updateLots(Convert.ToInt32(selectedOpenedPosition["lots"]) + Convert.ToInt32(selectedSettlementPosition["lots"]),
                    Convert.ToInt32(selectedSettlementPosition["OpenPosition_id"]));
                commonFunction.updateBalance(Convert.ToInt32(selectedCustomer["balance"]) - Convert.ToDouble(selectedSettlementPosition["profit"]), Convert.ToInt32(selectedCustomer["id"]));
                reloadOpenPosition(selectedCustomer["id"]);
                reloadSettlementPositions(selectedCustomer["id"]);
                reloadCustomerAccount();
            }
        }

        private void companyInformationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CompanyInformation companyInformation = new CompanyInformation();
            DialogResult dialogResult = companyInformation.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {

            }
        }

        private void OpenPosition_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                String action = OpenPosition.Rows[e.RowIndex].Cells[4].Value.ToString().Trim();
                selectedOpenedPosition["id"] = OpenPosition.Rows[e.RowIndex].Cells[0].Value.ToString().Trim();
                selectedOpenedPosition["date"] = OpenPosition.Rows[e.RowIndex].Cells[2].Value.ToString().Trim();
                selectedOpenedPosition["lots"] = OpenPosition.Rows[e.RowIndex].Cells[3].Value.ToString();
                selectedOpenedPosition["price"] = OpenPosition.Rows[e.RowIndex].Cells[5].Value.ToString();
                selectedOpenedPosition["commission"] = OpenPosition.Rows[e.RowIndex].Cells[8].Value.ToString();
                selectedOpenedPosition["interest"] = OpenPosition.Rows[e.RowIndex].Cells[6].Value.ToString();

                controlBuySellButton(action);
            }
        }

        private void rateSettingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RatesSetting ratesSetting = new RatesSetting();
            DialogResult dialogResult = ratesSetting.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                this.ratesSetting = commonFunction.getRate();
            }
        }

        private void companyInformationToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            CompanyInformation companyInformation = new CompanyInformation();
            DialogResult dialogResult = companyInformation.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {

            }
        }

        private void closeAccountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (selectedCustomer["id"] != "")
            {
                commonFunction.deleteAccount(selectedCustomer["id"]);
                reloadCustomerAccount();
                reloadOpenPosition(selectedCustomer["id"]);
                reloadSettlementPositions(selectedCustomer["id"]);
            }
        }

        private void openAccountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenAccountPopup openAccountPopup = new OpenAccountPopup();
            DialogResult dialogResult = openAccountPopup.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                reloadCustomerAccount();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void setMyButtonIcon()
        {
            BtnSettlemenePositionUp.Image = System.Drawing.Image.FromFile("C:\\Graphics\\My.ico");
        }
    }
}
