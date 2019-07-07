using System.Collections.Generic;
using System.Transactions;
using Models;

namespace Services
{
    public interface IAtmService
    {
        void WithdrawMoney(string accountNumber, string pin, int money);
        BankAccount CheckState(string accountNumber, string pin);
        List<BankTransaction> GetAllTransactions(string accountNumber, string pin);
    }
}
