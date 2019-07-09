using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using Models;

namespace Services
{
    public class DapperAtmService : IAtmService
    {
        private static string _connectionString = "Server=.;Database=AtmDb;Trusted_Connection=True";
        public void WithdrawMoney(string accountNumber, string pin, int money)
        {
            var account = GetAccount(accountNumber, pin);

            if (account == null)
                throw new Exception("The account does not exist, or your pin is invalid.");

            if (account.Balance < money)
                throw new Exception("Not enough money.");

            using (IDbConnection con = new SqlConnection(_connectionString))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@MoneyWithdraw", money);
                parameters.Add("@AccountNumber", accountNumber);
                con.Execute("WithdrawMoney", parameters, commandType: CommandType.StoredProcedure);
            }
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

            var transactions = new List<BankTransaction>();

            using (IDbConnection con = new SqlConnection(_connectionString))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                var sql =
                    "Select Number, FirstName, LastName, MoneyWithdraw From Transactions t INNER JOIN BankAccounts ba ON t.BankAccountId = ba.Id Where Number = @accountNumber";

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@accountNumber", accountNumber);

                transactions = con.Query<BankTransaction>(sql, parameters).ToList();
            }

            return transactions;
        }

        private BankAccount GetAccount(string accountNumber, string pin)
        {
            BankAccount bankAccount;
            using (IDbConnection con = new SqlConnection(_connectionString))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                var sql = "Select * From BankAccounts Where Number = @number AND Pin = @pin";

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@number", accountNumber);
                parameters.Add("@pin", pin);

                bankAccount = con.Query<BankAccount>(sql, parameters).FirstOrDefault();
            }

            return bankAccount;
        }
    }
}
