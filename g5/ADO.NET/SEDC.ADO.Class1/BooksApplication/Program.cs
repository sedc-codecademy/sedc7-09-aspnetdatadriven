using System;
using System.Data.SqlClient;

namespace BooksApplication
{
    class Program
    {
        private static string _connectionString = 
            "Server=.;Database=BooksDB2019;Trusted_Connection=True;";
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to ADO.NET!");
            //CountRecords();
            GetAuthors(3);
            Console.ReadLine();
        }
        private static void CountRecords()
        {
            // 1. Create new connection and give it a connection string
            SqlConnection connection = new SqlConnection(_connectionString);
            // 2. Open the connection to the database
            connection.Open();
            // 3. Create new SQL Command
            SqlCommand cmd = new SqlCommand();
            // 4. Give the command a connetion to some database
            cmd.Connection = connection;
            // 5. We write the query
            cmd.CommandText = "SELECT COUNT(*) FROM Authors";
            // 6. We execute the command, convert the result and store it in a variable
            int authorCount = (int)cmd.ExecuteScalar();
            // 7. Show/Use result
            Console.WriteLine($"There are {authorCount} authors in our DB!");
            // 8. Close the connection to the database
            connection.Close();

        }

        private static void GetAuthors(int approach)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "SELECT ID, Name, DateOfBirth FROM Authors";

            SqlDataReader dr = cmd.ExecuteReader();

            int authorId = 0;
            string name = null;
            DateTime? dob = null;

            while (dr.Read())
            {
                switch (approach)
                {
                    case 1:
                        authorId = dr.GetInt32(0);
                        name = dr.GetString(1);
                        break;
                    case 2:
                        authorId = dr.GetFieldValue<int>(0);
                        name = dr.GetFieldValue<string>(1);
                        break;
                    case 3:
                        authorId = (int)dr["ID"];
                        name = (string)dr["Name"];
                        break;
                }
                // NULL IN DATABASE AND NULL IN C# ARE NOT THE SAME
                // We check with IsDBNull if a column from a record is NULL ( DBNULL )
                dob = dr.IsDBNull(2) ? (DateTime?)null : (DateTime)dr["DateOfBirth"];

                Console.WriteLine($"Id: {authorId} - Name: {name} - Date Of Birth: {dob}");
            }

            connection.Close();
        }
    }
}
