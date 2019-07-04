using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEDC.G2.ADONET
{
    class Program
    {
        //setup correct connection and use the database created from the previous course for PizzaApp
        private static string _connectionString = @"Server=DESKTOP-CO6AHNV\SQLEXPRESS;Database=Test1DB;Trusted_Connection=True;";

        //The call of the methids are commented if you want to test a specific one just uncomment that methid
        static void Main(string[] args)
        {
            GetNumberOffPizzas();

            //InsertPizza();

            //AddPizzaBySP();

            //GetAllPizzas();

            //var pizaSizes = GetPizzaSizes();

            //foreach(var item in pizaSizes)
            //{
            //    Console.WriteLine("Pizza: " + GetPizzaById(item.PizzaId));
            //}

            //GetPizzas(pizaSizes.Select(x => x.PizzaId).ToList());

            Console.ReadLine();
        }

        #region methods for reproducing Select N+1 issue
        private static List<string> GetPizzas(List<int> list)
        {
            string ids = string.Empty;
            var names = new List<string>();
            foreach (var item in list)
            {
                ids += item + ",";
            }

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandText = $"Select * From Pizza where PizzaId in ({ids})";

                    connection.Open();
                    var dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        names.Add((string)dr["Name"]);
                    }
                }
            }

            return names;
        }

        private static object GetPizzaById(int pizzaId)
        {
            SqlConnection connection = new SqlConnection(_connectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = $"Select Name From Pizza where PizzaId = {pizzaId}";

            connection.Open();
            string name = (string)cmd.ExecuteScalar();

            connection.Close();

            return name;
        }

        private static List<PizzaSize> GetPizzaSizes()
        {
            SqlConnection connection = new SqlConnection(_connectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "Select * From PizzaSize";

            connection.Open();
            var dataReader = cmd.ExecuteReader();

            var list = new List<PizzaSize>();

            while (dataReader.Read())
            {
                //pizzaId = (int)dataReader["PizzaId"];
                //name = (string)dataReader["Name"];
                //description = (string)dataReader["Description"];

                list.Add(new PizzaSize
                {
                    PizzaId = dataReader.GetInt32(1),
                    SizeId = dataReader.GetInt32(2),
                    Price = dataReader.GetDecimal(3)
                });
            }

            connection.Close();
            return list;
        }
        #endregion

        private static void AddPizzaBySP()
        {
            Console.Write("Pizza Name: ");
            var pizzaName = Console.ReadLine();
            Console.Write("Description: ");
            var description = Console.ReadLine();

            SqlConnection connection = new SqlConnection(_connectionString);

            SqlCommand cmd = new SqlCommand("sp_AddPizza", connection);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@Name", SqlDbType.VarChar).Value = pizzaName;
            cmd.Parameters.Add("@Description", SqlDbType.VarChar).Value = description;

            connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();
        }

        private static void GetAllPizzas()
        {
            SqlConnection connection = new SqlConnection(_connectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "Select * From Pizza";

            connection.Open();
            var dataReader = cmd.ExecuteReader();

            int pizzaId = 0;
            string name = string.Empty;
            string description = string.Empty;

            while (dataReader.Read())
            {
                //pizzaId = (int)dataReader["PizzaId"];
                //name = (string)dataReader["Name"];
                //description = (string)dataReader["Description"];

                pizzaId = dataReader.GetInt32(0);
                name = dataReader.GetString(1);
                description = dataReader.GetString(2);

                Console.WriteLine($"Pizza ID: {pizzaId}, Pizza Name: {name}, Pizza Description: {description}");
            }
            connection.Close();
        }

        private static void InsertPizza()
        {
            Console.Write("Pizza Name: ");
            var pizzaName = Console.ReadLine();
            Console.Write("Description: ");
            var description = Console.ReadLine();

            SqlConnection connection = new SqlConnection(_connectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            //cmd.CommandText = @"Insert into Pizza (Name, Description) values ('" + pizzaName + "','" + description + "')";
            cmd.CommandText = string.Format("Insert into Pizza (Name, Description) values ('{0}','{1}')", pizzaName, description);

            connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();
            Console.WriteLine("Pizza inserted!");
        }

        private static void GetNumberOffPizzas()
        {
            SqlConnection connection = new SqlConnection(_connectionString);

            Console.WriteLine(connection.State.ToString());

            //your code goes here
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "Select Count(*) From Pizza";

            connection.Open();
            var count = (int)cmd.ExecuteScalar();

            Console.WriteLine("There are in total " + count + " pizzas in database");

            connection.Close();
            Console.WriteLine(connection.State.ToString());
        }
    }
}
