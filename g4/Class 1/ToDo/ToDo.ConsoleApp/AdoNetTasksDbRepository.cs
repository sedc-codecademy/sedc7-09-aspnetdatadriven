using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using ToDo.DataLayer.Contracts;
using ToDo.DataLayer.Entities;

namespace ToDo.ConsoleApp
{
    public class AdoNetTasksDbRepository : ITaskRepository
    {
        private readonly string _connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=TasksDb;Integrated Security=True;Connect Timeout=60;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public System.Threading.Tasks.Task AddAsync(DataLayer.Entities.Task task)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<DataLayer.Entities.Task>> GetAllAsync()
        {
            //var sqlConnection = new SqlConnection(_connectionString);

            //try
            //{
            //    await sqlConnection.OpenAsync();
            //}
            //finally
            //{
            //    sqlConnection.Close();
            //}
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                await sqlConnection.OpenAsync();
                var sqlCommand = new SqlCommand
                {
                    Connection = sqlConnection,
                    CommandText = "select * from dbo.Tasks"
                };

                SqlDataReader reader = await sqlCommand.ExecuteReaderAsync();

                var result = new List<DataLayer.Entities.Task>();
                while (reader.Read())
                {
                    var task = new DataLayer.Entities.Task
                    {
                        Id = (int)reader["Id"],
                        Description = (string)reader["Description"],
                        Priority = (DataLayer.Enums.Priority)reader["Priority"],
                        Status = (DataLayer.Enums.Status)reader["Status"],
                        Title = (string)reader["Title"],
                        Type = (DataLayer.Enums.TaskType)reader["Type"]
                    };
                    //select n + 1 problem, n == taskNumber
                    //task.SubTasks = GetSubTasks(task);

                    result.Add(task);
                }
                SetSubTasks(result);
                return result;
            }

            
        }

        private void SetSubTasks(List<DataLayer.Entities.Task> tasks)
        {
            var subtasks = new List<SubTask>();
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();
                var sqlCommand = new SqlCommand
                {
                    Connection = sqlConnection,
                    CommandText = "select * from dbo.SubTask"
                };

                SqlDataReader reader = sqlCommand.ExecuteReader();
                while (reader.Read())
                {
                    var subtask = new SubTask
                    {
                        Id = (int)reader["Id"],
                        Description = (string)reader["Description"],
                        Status = (DataLayer.Enums.Status)reader["Status"],
                        Title = (string)reader["Title"],
                        ParentTaskId = (int)reader["ParentTaskId"]
                    };
                    subtasks.Add(subtask);
                }
            }

            foreach (var task in tasks)
            {
                List<SubTask> thisTaskSubTasks = subtasks
                                                        .Where(s => s.ParentTaskId == task.Id)
                                                        .ToList();
                thisTaskSubTasks.ForEach(s => s.ParentTask = task);
                task.SubTasks = thisTaskSubTasks;
            }

        }

        private IEnumerable<SubTask> GetSubTasks(DataLayer.Entities.Task task)
        {
            //you should always close your sqlConnection or put it into using statement so it will be disposed
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();//it will throw exception if connection is not opened
                var sqlCommand = new SqlCommand
                {
                    Connection = sqlConnection,
                    CommandText = $"select * from dbo.SubTask where ParentTaskId={task.Id}"
                };
                var dataReader = sqlCommand.ExecuteReader();

                var result = new List<SubTask>();
                while (dataReader.Read())
                {
                    var subTask = new SubTask
                    {
                        Id = (int)dataReader["Id"],
                        Description = (string)dataReader["Description"],
                        ParentTask = task,
                        Status = (DataLayer.Enums.Status)dataReader["Status"],
                        Title = (string)dataReader["Title"]
                    };
                    result.Add(subTask);
                }
                return result;
            }
        }

        public async Task<DataLayer.Entities.Task> GetByIdAsync(int id)
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                await sqlConnection.OpenAsync();
                var sqlCommand = new SqlCommand
                {
                    Connection = sqlConnection,
                    CommandText = $"select * from dbo.Tasks where id={id}"
                };

                SqlDataReader reader = await sqlCommand.ExecuteReaderAsync();

                var result = new List<DataLayer.Entities.Task>();
                while (reader.Read())
                {
                    var task = new DataLayer.Entities.Task
                    {
                        Id = (int)reader["Id"],
                        Description = (string)reader["Description"],
                        Priority = (DataLayer.Enums.Priority)reader["Priority"],
                        Status = (DataLayer.Enums.Status)reader["Status"],
                        Title = (string)reader["Title"],
                        Type = (DataLayer.Enums.TaskType)reader["Type"]
                    };
                    //select n + 1 problem, n == taskNumber
                    //task.SubTasks = GetSubTasks(task);

                    result.Add(task);
                }
                SetSubTasks(result);
                return result.SingleOrDefault();
            }
        }

        public DataLayer.Entities.Task GetById(string id)
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();
                var sqlCommand = new SqlCommand
                {
                    Connection = sqlConnection,
                    //bad way, sql injection
                    //CommandText = $"select * from dbo.Tasks where id={id}"
                    CommandText = $"select * from dbo.Tasks where id=@id"
                };
                sqlCommand.Parameters.AddWithValue("id", id);
                SqlDataReader reader = sqlCommand.ExecuteReader();

                var result = new List<DataLayer.Entities.Task>();
                while (reader.Read())
                {
                    var task = new DataLayer.Entities.Task
                    {
                        Id = (int)reader["Id"],
                        Description = (string)reader["Description"],
                        Priority = (DataLayer.Enums.Priority)reader["Priority"],
                        Status = (DataLayer.Enums.Status)reader["Status"],
                        Title = (string)reader["Title"],
                        Type = (DataLayer.Enums.TaskType)reader["Type"]
                    };
                    //select n + 1 problem, n == taskNumber
                    //task.SubTasks = GetSubTasks(task);

                    result.Add(task);
                }
                SetSubTasks(result);
                return result.SingleOrDefault();
            }
        }

        public DataLayer.Entities.Task GetByTitle(string title)
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();
                var sqlCommand = new SqlCommand
                {
                    Connection = sqlConnection,
                    //bad way, sql injection
                    //CommandText = $"select * from dbo.Tasks where id={id}"
                    CommandText = $"select * from dbo.Tasks where Title=@title"
                };
                sqlCommand.Parameters.AddWithValue("title", title);
                SqlDataReader reader = sqlCommand.ExecuteReader();

                var result = new List<DataLayer.Entities.Task>();
                while (reader.Read())
                {
                    var task = new DataLayer.Entities.Task
                    {
                        Id = (int)reader["Id"],
                        Description = (string)reader["Description"],
                        Priority = (DataLayer.Enums.Priority)reader["Priority"],
                        Status = (DataLayer.Enums.Status)reader["Status"],
                        Title = (string)reader["Title"],
                        Type = (DataLayer.Enums.TaskType)reader["Type"]
                    };
                    //select n + 1 problem, n == taskNumber
                    //task.SubTasks = GetSubTasks(task);

                    result.Add(task);
                }
                SetSubTasks(result);
                return result.SingleOrDefault();
            }
        }

        public System.Threading.Tasks.Task RemoveAsync(DataLayer.Entities.Task task)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(DataLayer.Entities.Task task)
        {
            throw new NotImplementedException();
        }
    }
}
