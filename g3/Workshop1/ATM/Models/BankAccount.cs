using System.Globalization;

namespace Models
{
    public class BankAccount
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public int Balance { get; set; }
        public string BalanceFormatted => Balance.ToString("C", new CultureInfo("mk-MK"));

        public BankAccount()
        {

        }
    }
}
