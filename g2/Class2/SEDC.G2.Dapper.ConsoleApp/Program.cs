using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEDC.G2.Dapper.ConsoleApp
{
    class Program
    {
        private static string _connectionString = @"Server=DESKTOP-CO6AHNV\SQLEXPRESS;Database=Test1DB;Trusted_Connection=True;";
        static void Main(string[] args)
        {
            Console.WriteLine("Dapper Demo project");
            Console.WriteLine("======================================================================");
            Console.WriteLine("Insert the number in front of the option for execution of that method:");
            Console.WriteLine("1 > Insert pizza using Execute SQL");
            Console.WriteLine("2 > Insert pizza using Execute Stored Procedure");
            Console.WriteLine("3 > Insert multiple pizzas using single Execute SQL");
            Console.WriteLine("4 > Get single pizza using Query with anonymous response");
            Console.WriteLine("5 > Get all pizzas using Query with strongly typed response");
            Console.WriteLine("6 > Get all PizzaSize >> Pizza relations using Query with strongly typed response");
            Console.WriteLine("7 > Get single item using QueryFirst with anonymous response");
            Console.WriteLine("8 > Get single item using QueryFirst with strongly typed response");
            Console.WriteLine("9 > Get single item using QueryFirstOrDefault with anonymous response");
            Console.WriteLine("10 > Get single item using QueryFirstOrDefault with strongly typed response");
            Console.WriteLine("11 > Example of QueryMultiple");

            var selection = Console.ReadLine();
            switch (selection)
            {
                #region Execute Implementation
                case "1":
                    {
                        InsertPizzaWithExecuteSQL();
                        break;
                    };
                case "2":
                    {
                        InsertPizzaWuthExecuteSP();
                        break;
                    }
                case "3":
                    {
                        MultiInsertPizzaWithExecuteSQL();
                        break;
                    };
                #endregion Execute Implementation

                #region Query Implementation
                case "4":
                    {
                        GetSinglePizzaQueryAnonymous();
                        break;
                    };
                case "5":
                    {
                        GetAllPizzasQueryStronglyTyped();
                        break;
                    };
                case "6":
                    {
                        GetQueryStringlyTypedOneToOne();
                        break;
                    };
                #endregion Query Implementation

                #region QueryFirst Implementation
                case "7":
                    {
                        GetSingleSizeQueryFirstAnonymous();
                        break;
                    };
                case "8":
                    {
                        GetSingleSizeQueryFirstStronglyTyped();
                        break;
                    };
                #endregion QueryFirst Implementation

                #region QueryFirstOrDefault Implementation
                case "9":
                    {
                        GetSingleSizeQueryFirstAnonymous();
                        break;
                    };
                case "10":
                    {
                        GetSingleSizeQueryFirstOrDefaultStronglyTyped();
                        break;
                    };
                #endregion QueryFirstOrDefault Implementation

                #region QueryMultiple
                case "11":
                    {
                        GetPizzasAndSizesQueryMultiple();
                        break;
                    };
                #endregion QueryMultiple
            }
        }

        private static void GetPizzasAndSizesQueryMultiple()
        {
            var selectQuery = @"Select * From Pizza;Select * From Size;";
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var multi = connection.QueryMultiple(selectQuery))
                {
                    var pizzas = multi.Read<Pizza>().ToList();
                    var sizes = multi.Read<Size>().ToList();

                    Console.WriteLine("==================================");
                    Console.WriteLine("Sizes List");
                    Console.WriteLine("==================================");
                    foreach (var size in sizes)
                    {
                        Console.WriteLine($"Size ID: {size.SizeId}, Size Name: {size.Name}, Diametar: {size.Dimension}");
                    }

                    Console.WriteLine();
                    Console.WriteLine("==================================");
                    Console.WriteLine("Pizzas List");
                    Console.WriteLine("==================================");
                    foreach (var pizza in pizzas)
                    {
                        Console.WriteLine($"Pizza ID: {pizza.PizzaId}, Pizza Name: {pizza.Name}, Description: {pizza.Description}");
                    }
                }
            }
        }

        private static void GetSingleSizeQueryFirstOrDefaultStronglyTyped()
        {
            var selectQuery = @"Select TOP 5 * From Size";
            using (var connection = new SqlConnection(_connectionString))
            {
                var size = connection.QueryFirstOrDefault<Size>(selectQuery);

                Console.WriteLine($"Size ID: {size.SizeId}, Size Name: {size.Name}, Diametar: {size.Dimension}");
            }
        }

        private static void GetSingleSizeQueryFirstOrDefaultAnonymous()
        {
            var selectQuery = @"Select TOP 5 * From Size";
            using (var connection = new SqlConnection(_connectionString))
            {
                var size = connection.QueryFirstOrDefault(selectQuery);

                foreach (var prop in size)
                {
                    Console.WriteLine(prop.Key + " --> " + prop.Value);
                }
            }
        }

        private static void GetSingleSizeQueryFirstStronglyTyped()
        {
            var selectQuery = @"Select TOP 5 * From Size";
            using (var connection = new SqlConnection(_connectionString))
            {
                var size = connection.QueryFirst<Size>(selectQuery);

                Console.WriteLine($"Size ID: {size.SizeId}, Size Name: {size.Name}, Diametar: {size.Dimension}");
            }
        }

        private static void GetSingleSizeQueryFirstAnonymous()
        {
            var selectQuery = @"Select TOP 5 * From Size";
            using (var connection = new SqlConnection(_connectionString))
            {
                var size = connection.QueryFirst(selectQuery);

                foreach (var prop in size)
                {
                    Console.WriteLine(prop.Key + " --> " + prop.Value);
                }
            }
        }

        private static void GetQueryStringlyTypedOneToOne()
        {
            string sql = "SELECT * FROM PizzaSize AS ps INNER JOIN Pizza AS p ON ps.PizzaId = p.PizzaId;";
            using (var connection = new SqlConnection(_connectionString))
            {
                var pizzaSizeDictionary = new Dictionary<int, PizzaSize>();

                var pizzaSizes = connection.Query<PizzaSize, Pizza, PizzaSize>(sql,
                    (pizzaSize, pizza) =>
                    {
                        pizzaSize.Pizza = pizza;
                        return pizzaSize;
                    },
                    splitOn: "PizzaId")
                    .Distinct().ToList();

                foreach (var item in pizzaSizes)
                {
                    Console.WriteLine($"Pizza Size ID: {item.PizzaSizeId}, Pizza Name: {item.Pizza.Name}");
                }
            }
        }

        private static void GetAllPizzasQueryStronglyTyped()
        {
            var selectQuery = @"Select * From Pizza";
            using (var connection = new SqlConnection(_connectionString))
            {
                var pizzas = connection.Query<Pizza>(selectQuery).ToList();
                foreach (var pizza in pizzas)
                {
                    Console.WriteLine($"Pizza ID: {pizza.PizzaId}, Pizza Name: {pizza.Name}, Description: {pizza.Description}");
                }
            }
        }

        private static void GetSinglePizzaQueryAnonymous()
        {
            var selectQuery = @"Select TOP 5 * From Pizza";
            using (var connection = new SqlConnection(_connectionString))
            {
                var pizza = connection.Query(selectQuery).FirstOrDefault();

                foreach (var prop in pizza)
                {
                    Console.WriteLine(prop.Key + " --> " + prop.Value);
                }
            }
        }

        private static void InsertPizzaWuthExecuteSP()
        {
            Console.Write("Pizza Name: ");
            var pizzaName = Console.ReadLine();
            Console.Write("Description: ");
            var description = Console.ReadLine();

            using (var connection = new SqlConnection(_connectionString))
            {
                var affectedRows = connection.Execute("sp_AddPizza", new { Name = pizzaName, Description = description }, commandType: CommandType.StoredProcedure);

                Console.WriteLine($"Inserted {affectedRows} item");
            }
        }

        private static void InsertPizzaWithExecuteSQL()
        {
            Console.WriteLine("Insert Pizza With Execute SQL!");
            const string insertQuery = @"INSERT INTO Pizza (Name, Description) VALUES (@PizzaName, @Description)";

            Console.Write("Insert Pizza Name: ");
            var pizzaName = Console.ReadLine();
            Console.Write("Insert Pizza Description: ");
            var desc = Console.ReadLine();

            using (var connection = new SqlConnection(_connectionString))
            {
                var affectedRows = connection.Execute(insertQuery, new { PizzaName = pizzaName, Description = desc });
            }
        }

        private static void MultiInsertPizzaWithExecuteSQL()
        {
            Console.WriteLine("Insert Pizza With Execute SQL!");
            const string insertQuery = @"INSERT INTO Pizza (Name, Description) VALUES (@PizzaName, @Description)";

            Console.Write("Insert Pizza Name: ");
            var pizzaName = Console.ReadLine();
            Console.Write("Insert Pizza Description: ");
            var desc = Console.ReadLine();
            Console.Write("Insert second Pizza Name: ");
            var pizza2Name = Console.ReadLine();
            Console.Write("Insert second Pizza Description: ");
            var desc2 = Console.ReadLine();

            using (var connection = new SqlConnection(_connectionString))
            {
                var affectedRows = connection.Execute(insertQuery, new[]
                {
                    new { PizzaName = pizzaName, Description = desc },
                    new { PizzaName = pizza2Name, Description = desc2 }
                });
            }
        }
    }
}
