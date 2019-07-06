using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Models;

namespace Services
{
    public class AdoNetAtmService : IAtmService
    {
        private static string _connectionString = "Server=.;Database=AtmDb;Trusted_Connection=True";

        public void WithdrawMoney(string accountNumber, string pin, int money)
        {
            var account = GetAccount(accountNumber, pin);

            if (account == null)
                throw new Exception("The account does not exist, or your pin is invalid.");

            if(account.Balance < money)
                throw new Exception("Not enough money.");

            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            SqlCommand cmd = new SqlCommand
            {
                Connection = connection,
                CommandText = "WithdrawMoney",
                CommandType = CommandType.StoredProcedure
            };

            var moneyParameter = cmd.Parameters.Add("@MoneyWithdraw", SqlDbType.Int);
            moneyParameter.Value = money;

            var accountParameter = cmd.Parameters.Add("@AccountNumber", SqlDbType.NVarChar, 50);
            accountParameter.Value = accountNumber;

            cmd.ExecuteScalar();
            
            connection.Close();
        }

        public BankAccount CheckState(string accountNumber, string pin)
        {
            var account = GetAccount(accountNumber, pin);

            if (account == null)
                throw new Exception("The account does not exist, or your pin is invalid.");

            return account;
        }

        public List<BankTransaction> GetAllTransactions(string accountNumber, string pin)
        {
            var account = GetAccount(accountNumber, pin);

            if (account == null)
                throw new Exception("The account does not exist, or your pin is invalid.");

            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            SqlCommand cmd = new SqlCommand
            {
                Connection = connection,
                CommandText = "Select Number, FirstName, LastName, MoneyWithdraw From Transactions t INNER JOIN BankAccounts ba ON t.BankAccountId = ba.Id Where Number = @accountNumber"
            };

            cmd.Parameters.AddWithValue("@accountNumber", accountNumber);

            SqlDataReader reader = cmd.ExecuteReader();

            var transactions = new List<BankTransaction>();

            while (reader.Read())
            {
                transactions.Add(new BankTransaction
                {
                    FirstName = (string)reader["FirstName"],
                    LastName = (string)reader["LastName"],
                    Number = (string)reader["Number"],
                    MoneyWithdraw = (int)reader["MoneyWithdraw"]
                });
            }

            connection.Close();

            return transactions;
        }

        private BankAccount GetAccount(string accountNumber, string pin)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            SqlCommand cmd = new SqlCommand
            {
                Connection = connection,
                CommandText = "Select * From BankAccounts Where Number = @number AND Pin = @pin"
            };

            var accountParameter = new SqlParameter("@number", accountNumber);
            cmd.Parameters.Add(accountParameter);

            var pinParameter = new SqlParameter("@pin", pin);
            cmd.Parameters.Add(pinParameter);

            SqlDataReader reader = cmd.ExecuteReader();

            var bankAccounts = new List<BankAccount>();
            while (reader.Read())
            {
                bankAccounts.Add(new BankAccount
                {
                    Id = (int)reader["Id"],
                    FirstName = (string)reader["FirstName"],
                    LastName = (string)reader["LastName"],
                    Number = (string)reader["Number"],
                    Balance = (int)reader["Balance"]
                });
            }

            connection.Close();

            return bankAccounts.FirstOrDefault();
        }
    }
}
