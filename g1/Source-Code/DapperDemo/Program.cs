using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Dapper;

namespace DapperDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var connection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ToDoDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
            {
                connection.Open();
                IEnumerable<Todo> todos =
                     connection.Query<Todo>("select * from Todos");
                foreach (Todo todo in todos)
                {
                    Console.WriteLine($"#{todo.Id}, {todo.Description}, {   (todo.IsCompleted ? "completed" : "not completed") }");
                }
            }
            Console.WriteLine("Hello World!");
        }
    }
}
