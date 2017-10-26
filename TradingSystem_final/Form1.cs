using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Novacode;
using System.Diagnostics;


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
            System.Data.DataTable dt = commonFunction.getSqlData(sql);
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
                System.Data.DataTable dt = commonFunction.getSqlData(sql);
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
                    netProfit = Convert.ToDouble(profitLoss) - Convert.ToInt32(commission);
                    commonFunction.CreateSettlementPosition(selectedCustomer["id"], dataFromChild["lots"], dataFromChild["oppositeDate"],
                        dataFromChild["price"], selectedOpenedPosition["date"], selectedOpenedPosition["price"], commission, netProfit, interest.ToString(), selectedOpenedPosition["id"]);
                } else
                {
                    buyPrice = Convert.ToInt32(selectedOpenedPosition["price"]);
                    sellPrice = Convert.ToInt32(dataFromChild["price"]);
                    profitLoss = commonFunction.createProfitLoss(buyPrice, sellPrice, inputLots, exchangeRate);
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
            System.Data.DataTable dt = commonFunction.getSqlData(sql);
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
            System.Data.DataTable dt = commonFunction.getSqlData(sql);
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
            System.Data.DataTable dt = commonFunction.getSqlData(sql);
            SettlementPositions.DataSource = dt;
        }

        private void enableButton()
        {
            NewOrder.Enabled = true;
            BtnOpenPositionClear.Enabled = true;
            BtnDepositWithdraw.Enabled = true;
            closeAccountToolStripMenuItem.Enabled = true;
            OneStatement.Enabled = true;
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
                System.Data.DataTable openPosition = commonFunction.getSqlData("select * from OpenPosition where id = " + selectedSettlementPosition["OpenPosition_id"]);
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
                    selectedCustomer["balance"] = (currentAmount + change).ToString();
                    commonFunction.depositWithdrawalBalance(withdrawDeposit.action, change, Convert.ToInt32(selectedCustomer["id"]));
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
            ClosingPriceForm closingPriceForm = new ClosingPriceForm();
            DialogResult dialogResult = closingPriceForm.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                commonFunction.changeOPProfitLoss(closingPriceForm.closingPrice, selectedCustomer["id"]);
                reloadOpenPosition(selectedCustomer["id"]);
                prepareStatement(closingPriceForm.closingPrice, "./simpleStatement.docx");
            }
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
                TotalAmount.Text = commonFunction.getCount("Account").ToString();
                BalanceAmount.Text = "0";
            }
        }

        private void openAccountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenAccountPopup openAccountPopup = new OpenAccountPopup();
            DialogResult dialogResult = openAccountPopup.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                reloadCustomerAccount();
                TotalAmount.Text = commonFunction.getCount("Account").ToString();
            }
        }

        private void setMyButtonIcon()
        {
            BtnSettlemenePositionUp.Image = System.Drawing.Image.FromFile("C:\\Graphics\\My.ico");
        }

        private void simpleStatementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            prepareStatement(300, "./simpleStatement.docx");
        }

        public void prepareStatement(int closingPrice, String path)
        {
            //Document doc = new Document(iTextSharp.text.PageSize.LETTER, 10, 10, 42, 35);
            //String output = "one_statement.pdf";
            //PdfWriter wri = PdfWriter.GetInstance(doc, new FileStream(output, FileMode.Create));
            //doc.Open();

            int totalFloating = 0;
            int totalCommission = 0;
            int totalInterest = 0;
            int totalProfitLoss = 0;
            DocX docx = DocX.Load(path);
            System.Data.DataTable dataTable = commonFunction.getSqlData("SELECT name, value FROM RatesSetting " +
                "WHERE name in ('Address', 'CompanyName', 'Fax', 'Tel', 'ExchangeRate') ORDER BY name");
            Dictionary<String, String> config = new Dictionary<string, string>();
            foreach (DataRow row in dataTable.Rows)
            {
                config[row["name"].ToString().Trim()] = row["value"].ToString();
            }
            docx.ReplaceText("<COMPANY_NAME_H>", config["CompanyName"]);
            docx.ReplaceText("<COMPANY_NAME_S>", config["CompanyName"]);
            docx.ReplaceText("<Address>", config["Address"]);
            docx.ReplaceText("<TEL_NUMBER>", config["Tel"]);
            docx.ReplaceText("<FAX_NUMBER>", config["Fax"]);
            docx.ReplaceText("<RATE>", config["ExchangeRate"]);

            dataTable = commonFunction.getSqlData("SELECT a.Name, a.AccountNo, SUBSTRING(CONVERT(varchar(10), " +
                "a.AccountNo), 2, 100) as 'AE_CODE', convert(varchar, getdate(), 101) AS 'DD/MM/YYYY' FROM Account a");
            docx.ReplaceText("<ACCOUNT_NO>", dataTable.Rows[0]["AccountNo"].ToString());
            docx.ReplaceText("<DD/MM/YY>", dataTable.Rows[0]["DD/MM/YYYY"].ToString());
            docx.ReplaceText("<AE_CODE>", dataTable.Rows[0]["AE_CODE"].ToString());
            docx.ReplaceText("<NAME>", dataTable.Rows[0]["Name"].ToString());
            docx.ReplaceText("<PAGE>", "1");


            dataTable = commonFunction.getSqlData("SELECT convert(varchar, DateBought, 101) AS DateBought, " +
                "PriceBought, Lots AS  'BoughtLots', convert(varchar, DateSold, 101) AS DateSold, PriceSold, " +
                "Lots AS 'SoldLots', Commision, Interest, Profit AS 'Profit/Loss' FROM SettlementPositions WHERE valid = 1 and Account_id=" + selectedCustomer["id"]);
            Dictionary<String, String> settlementPosition = new Dictionary<string, string>();
            settlementPosition["DateBought"] = "";
            settlementPosition["PriceBought"] = "";
            settlementPosition["BoughtLots"] = "";
            settlementPosition["DateSold"] = "";
            settlementPosition["PriceSold"] = "";
            settlementPosition["SoldLots"] = "";
            settlementPosition["Commision"] = "";
            settlementPosition["Interest"] = "";
            settlementPosition["Profit/Loss"] = "";
            foreach (DataRow row in dataTable.Rows)
            {
                settlementPosition["DateBought"] = settlementPosition["DateBought"] + row["DateBought"] + "\n";
                settlementPosition["PriceBought"] = settlementPosition["PriceBought"] + row["PriceBought"] + "\n";
                settlementPosition["BoughtLots"] = settlementPosition["BoughtLots"] + row["BoughtLots"] + "\n";
                settlementPosition["DateSold"] = settlementPosition["DateSold"] + row["DateSold"] + "\n";
                settlementPosition["PriceSold"] = settlementPosition["PriceSold"] + row["PriceSold"] + "\n";
                settlementPosition["SoldLots"] = settlementPosition["SoldLots"] + row["SoldLots"] + "\n";
                settlementPosition["Commision"] = settlementPosition["Commision"] + row["Commision"] + "\n";
                settlementPosition["Interest"] = settlementPosition["Interest"] + row["Interest"] + "\n";
                settlementPosition["Profit/Loss"] = settlementPosition["Profit/Loss"] + row["Profit/Loss"] + "\n";
                totalCommission = totalCommission + Convert.ToInt32(row["Commision"]);
                totalInterest = totalInterest + Convert.ToInt32(row["Interest"]);
                totalProfitLoss = totalProfitLoss + Convert.ToInt32(row["Profit/Loss"]);
            }
            docx.ReplaceText("<BDATES>", settlementPosition["DateBought"]);
            docx.ReplaceText("<BPRICES>", settlementPosition["PriceBought"]);
            docx.ReplaceText("<BLOTSS>", settlementPosition["BoughtLots"]);
            docx.ReplaceText("<SDATES>", settlementPosition["DateSold"]);
            docx.ReplaceText("<SPRICES>", settlementPosition["PriceSold"]);
            docx.ReplaceText("<SLOTSS>", settlementPosition["SoldLots"]);
            docx.ReplaceText("<INTERESTS>", settlementPosition["Interest"]);
            docx.ReplaceText("<COMMISSIONS>", settlementPosition["Commision"]);
            docx.ReplaceText("<P/LS>", settlementPosition["Profit/Loss"]);


            dataTable = commonFunction.getSqlData("SELECT convert(varchar, Date, 101) AS Date, Price, Lots, Interest, Commission, ProfitLoss AS 'FP/LO' " +
                "FROM OpenPosition WHERE valid = 1 and Account_id = " + selectedCustomer["id"]);
            Dictionary <String, String> openPosition = new Dictionary<string, string>();
            openPosition["Date"] = "";
            openPosition["Price"] = "";
            openPosition["Lots"] = "";
            openPosition["Interest"] = "";
            openPosition["Commission"] = "";
            openPosition["FP/LO"] = "";
            foreach (DataRow row in dataTable.Rows)
            {
                openPosition["Date"] = openPosition["Date"] + row["Date"] + "\n";
                openPosition["Price"] = openPosition["Price"] + row["Price"] + "\n";
                openPosition["Lots"] = openPosition["Lots"] + row["Lots"] + "\n";
                openPosition["Interest"] = openPosition["Interest"] + row["Interest"] + "\n";
                openPosition["Commission"] = openPosition["Commission"] + row["Commission"] + "\n";
                openPosition["FP/LO"] = openPosition["FP/LO"] + row["FP/LO"] + "\n";
                totalFloating = totalFloating + Convert.ToInt32(row["FP/LO"]);
            }
            docx.ReplaceText("<DATEO>", openPosition["Date"]);
            docx.ReplaceText("<PRICEO>", openPosition["Price"]);
            docx.ReplaceText("<CPRICEO>", closingPrice.ToString());
            docx.ReplaceText("<LOTSO>", openPosition["Lots"]);
            docx.ReplaceText("<INTERESTO>", openPosition["Interest"]);
            docx.ReplaceText("<COMMISSIONO>", openPosition["Commission"]);
            docx.ReplaceText("<FP/LO>", openPosition["FP/LO"]);

            dataTable = commonFunction.getSqlData("SELECT a.Balance - aa.Deposit + aa.Withdrawal as 'PREVIOUS_BALANCE', a.Balance as 'NEW_BALANCE',aa.Deposit as  'DEPOSIT', " +
    "a.Balance AS 'EQUITY', aa.Withdrawal AS 'WITHDRAWAL' " +
    "FROM Account a " +
    "JOIN(select a.Account_id, sum(a.Deposit) as Deposit, sum(a.Withdrawal) as Withdrawal from(select Account_id, CASE WHEN action = 'Deposit' THEN sum(amount) end as Deposit," +
    "CASE WHEN action = 'Withdrawal' THEN - sum(amount) end as Withdrawal FROM AccountAction " +
    "GROUP BY Account_id, action) a Group by a.Account_id) aa on aa.Account_id = a.id " +
    "WHERE a.id = " + selectedCustomer["id"]);
            docx.ReplaceText("<DESO>", "");
            docx.ReplaceText("<DESS>", "");
            docx.ReplaceText("<PREVIOUS_BALANCE>", dataTable.Rows[0]["PREVIOUS_BALANCE"].ToString());
            docx.ReplaceText("<NEW_BALANCE>", dataTable.Rows[0]["NEW_BALANCE"].ToString());
            docx.ReplaceText("<DEPOSIT>", dataTable.Rows[0]["DEPOSIT"].ToString());
            docx.ReplaceText("<WITHDRAWAL>", dataTable.Rows[0]["WITHDRAWAL"].ToString());
            docx.ReplaceText("<EQUITY>", dataTable.Rows[0]["EQUITY"].ToString());
            docx.ReplaceText("<F_P/L>", totalFloating.ToString());
            docx.ReplaceText("<INTEREST>", totalInterest.ToString());
            docx.ReplaceText("<REQUIRE>", "0.00");
            docx.ReplaceText("<COMMISSION>", totalCommission.ToString());
            docx.ReplaceText("<EFFECTIVE>", "0.00");
            docx.ReplaceText("<P/L>", totalProfitLoss.ToString());

            docx.SaveAs("output.docx");
            Process.Start("output.docx");
            //DocumentModel.Load("tommy2.docx").Save("Document.pdf");
            //doc.Close();
        }

        public void prepareSimpleStatement()
        {

        }
    }
}
