using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Dapper;

namespace DapperDemo
{
    class Program
    {
        static SqlConnection connection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ToDoDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        static void Main(string[] args)
        {
            using (connection)
            {
                connection.Open();
                //GetAllTodos();
                var todo = new Todo
                {
                    Id = 100,
                    Description = "napolni voda",
                    Title = "odma vazno!!!",
                    IsCompleted = false,
                    DueDate = DateTime.UtcNow.AddDays(2),
                };

                //how dapper uses reflection
                //Type type = typeof(Todo);
                //var property = type.GetProperties()
                //    .First(p => p.Name == "Id");
                //property.SetValue(todo, 0);
                //todo.Id = 0;

                //var rowsAffected = InsertTodo(todo);
                //Console.WriteLine($"Rows affected: {rowsAffected}");
            }
        }

        static int InsertTodo(Todo todo)
        {
            var query = "insert into Todos(Description,Title,DueDate,IsCompleted) values (@description,@title,@duedate,@isCompleted)";
            var affectedRows = connection.Execute(query, new
            {
                description = todo.Description,
                title = todo.Title,
                duedate = todo.DueDate,
                isCompleted = todo.IsCompleted
            });
            return affectedRows;
        }

        private static List<Todo> GetAllTodos()
        {
            IEnumerable<Todo> todos =
                 connection.Query<Todo>("select * from Todos");
            return todos.ToList();
        }

        //private static List<Todo> GetAllTodos()
        //{
        //    using (var connection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ToDoDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
        //    {
        //        connection.Open();
        //        IEnumerable<Todo> todos =
        //             connection.Query<Todo>("select * from Todos");
        //        return todos.ToList();
        //    }
        //}
    }
}
