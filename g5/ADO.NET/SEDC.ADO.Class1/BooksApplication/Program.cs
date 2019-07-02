using System;
using System.Data.SqlClient;
using System.Data;

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
            //GetAuthors(3);
            //QueryParameterAuthors();
            //GetAllAuthorsStoredProcedure();
            //InsertAuthorWithTransaction();
            GetAwards(); // Prints all awards ( ID, Name )
            GetNominations(); // Prints all nominations ( ID, BookTitle, Year, HasWon )

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
        private static void QueryParameterAuthors()
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            // We ask the user for some input
            Console.Write("Please enter author:");
            string query = Console.ReadLine();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            // GOOD EXAMPLE
            cmd.CommandText = @"SELECT ID, Name, DateOfBirth FROM Authors
                                WHERE Name LIKE '%' + @authorName + '%'";
            cmd.Parameters.AddWithValue("@authorName", query);
            // BAD EXAMPLE. DO NOT USE PLEASE
            //cmd.CommandText = @"SELECT ID, Name, DateOfBirth FROM Authors
            //                    WHERE Name LIKE '%" + query + "%'";
            // SQL INJECTION
            // if ' OR 1 = 1--   -> All data from table
            // if ' DROP TABLE dbo.Authors--   -> Will drop the Authors table
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                int authorId = (int)dr["ID"];
                string name = (string)dr["Name"];
                DateTime dob = (DateTime)dr["DateOfBirth"];

                Console.WriteLine($"ID: {authorId} - Name: {name} : {dob}");
            }

            connection.Close();
        }
        private static void GetAllAuthorsStoredProcedure()
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            Console.Write("Enter author name:");
            string query = Console.ReadLine();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "getAuthors";
            cmd.Parameters.AddWithValue("@authorName", query);

            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                int authorId = (int)dr["ID"];
                string name = (string)dr["Name"];
                DateTime dob = (DateTime)dr["DateOfBirth"];

                Console.WriteLine($"ID: {authorId} - Name: {name} : {dob}");
            }

            connection.Close();
        }

        private static void InsertAuthorWithTransaction()
        {
            // GET DATA FROM USER
            Console.Write("Enter author name: ");
            string authorName = Console.ReadLine();
            Console.Write("Enter author Date of Birth: ");
            string dobString = Console.ReadLine();
            DateTime? dob = string.IsNullOrEmpty(dobString) ?
                null : (DateTime?)DateTime.Parse(dobString);
            Console.Write("Enter author Date of Death: ");
            string dodString = Console.ReadLine();
            DateTime? dod = string.IsNullOrEmpty(dodString) ? 
                null : (DateTime?)DateTime.Parse(dodString);
            Console.Write("Enter novel title: ");
            string novelTitle = Console.ReadLine();
            Console.Write("Is the book read: ");
            bool isRead = bool.Parse(Console.ReadLine());

            // OPEN CONNECTION
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            // BEGIN TRANSACTION
            SqlTransaction transaction = connection.BeginTransaction();
            try
            {
                // INSERT AUTHOR
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.Transaction = transaction;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "createAuthor";
                cmd.Parameters.AddWithValue("@AuthorName", authorName);
                cmd.Parameters.AddWithValue("@DateOfBirth", dob);
                cmd.Parameters.AddWithValue("@DateOfDeath", dod);
                cmd.Parameters.Add(new SqlParameter()
                {
                    ParameterName = "@ID",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.InputOutput
                });

                cmd.ExecuteNonQuery();

                int authorId = (int)cmd.Parameters["@ID"].Value;

                // INSERT NOVEL
                cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.Transaction = transaction;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "createNovel";
                cmd.Parameters.AddWithValue("@Title", novelTitle);
                cmd.Parameters.AddWithValue("@AuthorID", authorId);
                cmd.Parameters.AddWithValue("@IsRead", isRead);

                cmd.ExecuteNonQuery();

                // IF EVERYTHING IS OK THEN WE COMMIT THE CHANGES ( REFLECT ON DB )
                transaction.Commit();
            }
            catch (Exception ex)
            {
                // IF THERE IS AN ERROR WE ROLLBACK ALL CHANGES ( DB IS NOT AFFECTED )
                transaction.Rollback();
                Console.WriteLine(ex.Message);
            }

            connection.Close();
        }

    }
}
