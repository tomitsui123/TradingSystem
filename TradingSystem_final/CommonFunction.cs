using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace TradingSystem_final
{
    class CommonFunction
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\USERS\TOMI\DESKTOP\TRADINGSYSTEM_FINAL\TRADINGSYSTEM_FINAL\TRADINGSYSTEM_FINAL\DATABASE1.MDF;Integrated Security=True");

        String[] settingWhiteList = { "CommissionRate", "ContractSize", "ExchangeRate", "InterestRateBUY", "InterestRateSELL" };

        public void CreateNewAccount(String accountName, String accountNo, int initDeposit)
        {
            string strCommand = "INSERT INTO dbo.Account (Name, AccountNo, Balance) VALUES(@Name, @No,  @Balance)";
            SqlCommand cmdInsert = new SqlCommand(strCommand, con);
            cmdInsert.Parameters.AddWithValue("@No", accountNo);
            cmdInsert.Parameters.AddWithValue("@Name", accountName);
            cmdInsert.Parameters.AddWithValue("@Balance", initDeposit);
            try
            {
                con.Open();
                cmdInsert.ExecuteNonQuery();
            }
            finally
            {
                con.Close();
            }
        }

        public void CreateOpenPosition(int accountId, int lots, String action, int price, int commission)
        {
            Dictionary<String, Double> interestRateList = getRate();
            Double interestRate = interestRateList["InterestRate" + action] / 100;
            Double exchangeRate = interestRateList["ExchangeRate"];
            Double contractSize = interestRateList["ContractSize"];
            Double interest = (price * contractSize * lots * interestRate * exchangeRate) / 360;
            Double profitLoss = 0;
            if (action == "BUY")
            {
                interest = -interest;
            }
            string strCommand = "INSERT INTO dbo.OpenPosition (Account_id, Date, Lots, Action, Price, Interest, ProfitLoss, Commission) VALUES(@Account_id, @Date,  @Lots, @Action, @Price, @Interest, @ProfitLoss, @Commission)";
            SqlCommand cmdInsert = new SqlCommand(strCommand, con);
            cmdInsert.Parameters.AddWithValue("@Account_id", accountId);
            cmdInsert.Parameters.AddWithValue("@Action", action);
            cmdInsert.Parameters.AddWithValue("@Date", DateTime.Now);
            cmdInsert.Parameters.AddWithValue("@Lots", lots);
            cmdInsert.Parameters.AddWithValue("@Price", price);
            cmdInsert.Parameters.AddWithValue("@Interest", interest);
            cmdInsert.Parameters.AddWithValue("@ProfitLoss", profitLoss);
            cmdInsert.Parameters.AddWithValue("@Commission", commission);
            try
            {
                con.Open();
                cmdInsert.ExecuteNonQuery();
            }
            finally
            {
                con.Close();
            }
        }

        public void updateLots(int amount, int id)
        {
            int valid = 1;
            if (amount == 0)
            {
                valid = 0;
            }
            SqlCommand cmd = new SqlCommand("update OpenPosition set lots = @Lots, valid = @Valid where id = @id", con);
            cmd.Parameters.AddWithValue("@Lots", amount);
            cmd.Parameters.AddWithValue("@Valid", valid);
            cmd.Parameters.AddWithValue("@id", id);
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
            finally
            {
                con.Close();
            }
        }

        public void updateInterest()
        {
            Dictionary<String, Double> rateSet = getRate();

            SqlCommand cmd = new SqlCommand("select * from OpenPosition", con);
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                Double interest = 0.0;
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        SqlConnection con2 = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\USERS\TOMI\DESKTOP\TRADINGSYSTEM_FINAL\TRADINGSYSTEM_FINAL\TRADINGSYSTEM_FINAL\DATABASE1.MDF;Integrated Security=True");
                        SqlCommand updateCmd = new SqlCommand("Update OpenPosition set Interest = @Interest where Id = @Id", con2);
                        Double price = Convert.ToDouble(reader["Price"]);
                        int lots = Convert.ToInt32(reader["Lots"]);
                        String action = reader["Action"].ToString().Trim();
                        Double interestRate = Convert.ToDouble(rateSet["InterestRate" + action]) / 100;
                        Double contractSize = rateSet["ContractSize"];
                        Double exchangeRate = rateSet["ExchangeRate"];
                        int numOfDay = (DateTime.Today - Convert.ToDateTime(reader["Date"])).Days + 1;
                        interest = (price * contractSize * lots * interestRate * exchangeRate) / 360 * numOfDay;
                        if (action == "BUY")
                        {
                            interest = -interest;
                        }

                        updateCmd.Parameters.AddWithValue("@Interest", interest);
                        updateCmd.Parameters.AddWithValue("@Id", reader["Id"]);
                        try
                        {
                            con2.Open();
                            updateCmd.ExecuteNonQuery();
                        }
                        catch
                        {
                            break;
                        }
                        finally
                        {
                            con2.Close();
                        }
                    }
                }
            }
            finally
            {
                con.Close();
            }
        }

        public void changeOPProfitLoss(int closingPrice, String id)
        {
            Dictionary<String, Double> config = getRate();

            SqlCommand cmd = new SqlCommand("update OpenPosition " +
                "set ProfitLoss = (@closingPrice - Price) * needed.ContractSize * needed.ExchangeRate * Lots " +
                "from( select sum(rate.ContractSize) as ContractSize, " +
                "sum(rate.ExchangeRate) as ExchangeRate from(select case when name = 'ContractSize' then CONVERT(decimal(10, 5), (CAST(value AS varchar(10)))) end as ContractSize," +
                "case when name = 'ExchangeRate' then CONVERT(decimal(10, 5), (CAST(value AS varchar(10)))) end as ExchangeRate from RatesSetting where name in ('ContractSize', 'ExchangeRate')  " +
                ") rate) needed " +
                "where Account_id = @id", con);
            cmd.Parameters.AddWithValue("@closingPrice", closingPrice);
            cmd.Parameters.AddWithValue("@id", id);
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
            finally
            {
                con.Close();
            }
        }

        public void updateBalance(Double amount, int id)
        {
            SqlCommand cmd = new SqlCommand("update Account set balance = @Balance where id = @id", con);
            cmd.Parameters.AddWithValue("@Balance", amount);
            cmd.Parameters.AddWithValue("@id", id);
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
            finally
            {
                con.Close();
            }
        }

        public Double createProfitLoss(int buyPrice, int sellPrice, int lots, Double exchangeRate)
        {
            return (sellPrice - buyPrice) * lots * exchangeRate;
        }

        public DataTable getSqlData(String sql)
        {
            DataSet ds;
            try
            {
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(sql, con);
                ds = new DataSet();
                adapter.Fill(ds, "Data");
            }
            finally
            {
                con.Close();
            }
            return ds.Tables[0];
        }

        public void deleteData(String table, int id)
        {
            string strCommand = "update dbo." + table + " set valid = 0 where id=@id";
            SqlCommand cmdUpdate = new SqlCommand(strCommand, con);
            cmdUpdate.Parameters.AddWithValue("@id", id);
            try
            {
                con.Open();
                cmdUpdate.ExecuteNonQuery();
            }
            finally
            {
                con.Close();
            }
        }

        public void deleteAccount(String id)
        {
            string strCommand = "update Account set valid = 0 where id=@id; " +
                "update SettlementPositions set valid = 0 where Account_id=@id;" +
                "update OpenPosition set valid = 0 where Account_id=@id";
            SqlCommand cmdUpdate = new SqlCommand(strCommand, con);
            cmdUpdate.Parameters.AddWithValue("@id", id);
            try
            {
                con.Open();
                cmdUpdate.ExecuteNonQuery();
            }
            finally
            {
                con.Close();
            }
        }


        public int getCount(String table)
        {
            SqlCommand cmd = new SqlCommand("select count(Id) from " + table + " where valid = 1", con);
            Int32 count;
            try
            {
                con.Open();
                count = (Int32)cmd.ExecuteScalar();
            }
            finally
            {
                con.Close();
            }
            return count;
        }

        public void CreateSettlementPosition(String accountId, String lots, String dateBought,
            String priceBought, String dateSold, String priceSold, int commission, Double profit, String interest, String OpenPositionId)
        {
            string strCommand = "INSERT INTO dbo.SettlementPositions (Account_id, Lots, DateBought, PriceBought, " +
                "DateSold, PriceSold, Commision, Interest, Profit, OpenPosition_id) VALUES (@Account_id, @Lots, @DateBought, @PriceBought, " +
                "@DateSold, @PriceSold, @Commission, @Interest, @Profit, @OpenPositionId)";
            SqlCommand cmdInsert = new SqlCommand(strCommand, con);
            cmdInsert.Parameters.AddWithValue("@Account_id", Convert.ToInt32(accountId));
            cmdInsert.Parameters.AddWithValue("@Lots", Convert.ToInt32(lots));
            cmdInsert.Parameters.AddWithValue("@DateBought", Convert.ToDateTime(dateBought));
            cmdInsert.Parameters.AddWithValue("@PriceBought", Convert.ToInt32(priceBought));
            cmdInsert.Parameters.AddWithValue("@DateSold", Convert.ToDateTime(dateSold));
            cmdInsert.Parameters.AddWithValue("@PriceSold", Convert.ToInt32(priceSold));
            cmdInsert.Parameters.AddWithValue("@Commission", commission);
            cmdInsert.Parameters.AddWithValue("@Interest", interest);
            cmdInsert.Parameters.AddWithValue("@Profit", profit);
            cmdInsert.Parameters.AddWithValue("@OpenPositionId", OpenPositionId);
            try
            {
                con.Open();
                cmdInsert.ExecuteNonQuery();
            }
            finally
            {
                con.Close();
            }
        }

        public void changeBalance(int balance, int id)
        {
            string strCommand = "update dbo.Account set Balance = @Balance where id=@id";
            SqlCommand cmdUpdate = new SqlCommand(strCommand, con);
            cmdUpdate.Parameters.AddWithValue("@Balance", balance);
            cmdUpdate.Parameters.AddWithValue("@id", id);
            try
            {
                con.Open();
                cmdUpdate.ExecuteNonQuery();
            }
            finally
            {
                con.Close();
            }
        }

        public void changeRates(Double exchangeRate, Double interestRateBuy,
            Double interestRateSell, Double ContractSize, Double CommissionRate)
        {
            try
            {
                con.Open();
                string strCommand = "update dbo.RatesSetting set value = @ExchangeRate where name='ExchangeRate'";
                SqlCommand cmdUpdate = new SqlCommand(strCommand, con);
                cmdUpdate.Parameters.AddWithValue("@ExchangeRate", exchangeRate.ToString());
                cmdUpdate.ExecuteNonQuery();

                strCommand = "update dbo.RatesSetting set value = @InterestRateBuy where name='InterestRateBuy'";
                cmdUpdate = new SqlCommand(strCommand, con);
                cmdUpdate.Parameters.AddWithValue("@InterestRateBuy", interestRateBuy.ToString());
                cmdUpdate.ExecuteNonQuery();

                strCommand = "update dbo.RatesSetting set value = @InterestRateSell where name='InterestRateSell'";
                cmdUpdate = new SqlCommand(strCommand, con);
                cmdUpdate.Parameters.AddWithValue("@InterestRateSell", interestRateSell.ToString());
                cmdUpdate.ExecuteNonQuery();

                strCommand = "update dbo.RatesSetting set value = @ContractSize where name='ContractSize'";
                cmdUpdate = new SqlCommand(strCommand, con);
                cmdUpdate.Parameters.AddWithValue("@ContractSize", ContractSize.ToString());
                cmdUpdate.ExecuteNonQuery();

                strCommand = "update dbo.RatesSetting set value = @CommissionRate where name='CommissionRate'";
                cmdUpdate = new SqlCommand(strCommand, con);
                cmdUpdate.Parameters.AddWithValue("@CommissionRate", CommissionRate.ToString());
                cmdUpdate.ExecuteNonQuery();
            }
            finally
            {
                con.Close();
            }
        }

        public Dictionary<String, Double> getRate()
        {
            Dictionary<String, Double> data = new Dictionary<string, Double>();
            SqlCommand cmd = new SqlCommand("select * from RatesSetting", con);
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        String key = reader["name"].ToString().Trim();
                        if (settingWhiteList.Contains(key))
                        {
                            data[key] = Convert.ToDouble(reader["value"].ToString().Trim());
                        }
                    }
                }
            }
            finally
            {
                con.Close();
            }

            return data;
        }

        public Dictionary<String, String> getCompanyInfo()
        {
            Dictionary<String, String> data = new Dictionary<String, String>();
            SqlCommand cmd = new SqlCommand("select * from RatesSetting", con);
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        String key = reader["name"].ToString().Trim();
                        if (!settingWhiteList.Contains(key))
                        {
                            data[key] = reader["value"].ToString().Trim();
                        }
                    }
                }
            }
            finally
            {
                con.Close();
            }

            return data;
        }

        public void changeCompanyInformation(String companyName, String address, String tel, String fax)
        {
            try
            {
                con.Open();
                SqlCommand sqlCommand = new SqlCommand("Update RatesSetting set value=@CompanyName where name='CompanyName'", con);
                sqlCommand.Parameters.AddWithValue("@CompanyName", companyName);
                sqlCommand.ExecuteNonQuery();

                sqlCommand = new SqlCommand("Update RatesSetting set value=@Address where name='Address'", con);
                sqlCommand.Parameters.AddWithValue("@Address", address);
                sqlCommand.ExecuteNonQuery();

                sqlCommand = new SqlCommand("Update RatesSetting set value=@Tel where name='Tel'", con);
                sqlCommand.Parameters.AddWithValue("@Tel", tel);
                sqlCommand.ExecuteNonQuery();

                sqlCommand = new SqlCommand("Update RatesSetting set value=@Fax where name='Fax'", con);
                sqlCommand.Parameters.AddWithValue("@Fax", fax);
                sqlCommand.ExecuteNonQuery();
            }
            finally
            {
                con.Close();
            }
        }

        public void depositWithdrawalBalance(String action, int amount, int accountId)
        {
            string strCommand = "INSERT INTO dbo.AccountAction (Account_id, Action, Amount) VALUES (@Account_id, @Action, @Amount)";
            SqlCommand cmdInsert = new SqlCommand(strCommand, con);
            cmdInsert.Parameters.AddWithValue("@Account_id", Convert.ToInt32(accountId));
            cmdInsert.Parameters.AddWithValue("@Action", action);
            cmdInsert.Parameters.AddWithValue("@Amount", Convert.ToInt32(amount));
            try
            {
                con.Open();
                cmdInsert.ExecuteNonQuery();
            }
            finally
            {
                con.Close();
            }
        }
    }
}
