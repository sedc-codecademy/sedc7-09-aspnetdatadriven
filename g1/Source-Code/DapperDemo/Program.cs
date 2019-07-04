using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using da
using Dapper;

namespace DapperDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var connection = new SqlConnection(""))
            {
                connection.Open();
               IEnumerable<dynamic> todos = 
                    connection.Query<Todo>("select * from Todos");
                foreach (dynamic todo in todos)
                {
                    todo.ClimbTheMountain();
                }
            }
            Console.WriteLine("Hello World!");
        }
    }
}
