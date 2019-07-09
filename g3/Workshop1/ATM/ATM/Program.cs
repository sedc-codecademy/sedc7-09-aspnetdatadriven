using System;
using System.Globalization;
using Microsoft.Extensions.DependencyInjection;
using Services;

namespace ATM
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                .AddTransient<IAtmService, DapperAtmService>()
                .BuildServiceProvider();

            var atmService = serviceProvider.GetService<IAtmService>();
            
            do
            {
                try
                {
                    Console.WriteLine("--------------------------");
                    Console.WriteLine("Select an action:");
                    Console.WriteLine("1. Check balance");
                    Console.WriteLine("2. Withdraw money");
                    Console.WriteLine("3. View all transactions");
                    var action = int.Parse(Console.ReadLine());
                    Console.WriteLine("--------------------------");
                    Console.WriteLine("Enter account number:");
                    var accountNumber = Console.ReadLine();
                    Console.WriteLine("--------------------------");
                    Console.WriteLine("Enter pin:");
                    var pin = Console.ReadLine();

                    switch (action)
                    {
                        case 1:
                            var account = atmService.CheckState(accountNumber, pin);
                            Console.WriteLine($"{account.FullName}: {account.BalanceFormatted}");
                            break;
                        case 2:
                            Console.WriteLine("Enter amount:");
                            var amount = int.Parse(Console.ReadLine());
                            atmService.WithdrawMoney(accountNumber, pin, amount);
                            Console.WriteLine($"Withdraw finished succesfully");
                            break;
                        case 3:
                            var transactions = atmService.GetAllTransactions(accountNumber, pin);
                            foreach (var bankTransaction in transactions)
                            {
                                Console.WriteLine($"{bankTransaction.Number}: {bankTransaction.MoneyWithdraw.ToString("C", new CultureInfo("mk-MK"))}");
                            }
                            break;
                        default: throw new Exception("Select valid action");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            } while (true);

        }
    }
}
