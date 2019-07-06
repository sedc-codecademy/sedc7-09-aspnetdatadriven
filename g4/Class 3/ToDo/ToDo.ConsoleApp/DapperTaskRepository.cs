using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDo.DataLayer.Contracts;
using Dapper;
using System.Data.SqlClient;
using System.Linq;

namespace ToDo.ConsoleApp
{
    public class DapperTaskRepository : ITaskRepository
    {
        private readonly string _connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=TasksDb;Integrated Security=True;Connect Timeout=60;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public async Task AddAsync(DataLayer.Entities.Task task)
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("Id", dbType: System.Data.DbType.Int32, 
                                direction: System.Data.ParameterDirection.Output);
                parameters.Add("Title", task.Title);
                parameters.Add("Description", task.Description);
                parameters.Add("Priority", task.Priority);
                parameters.Add("Status", task.Status);
                parameters.Add("Type", task.Type);

                int insertedRows = 
                    await sqlConnection.ExecuteAsync("sp_InsertTask", parameters, 
                                                        commandType: System.Data.CommandType.StoredProcedure);

                if (insertedRows != 1)
                    throw new Exception("Insert was unsuccessful");

                task.Id = parameters.Get<int>("Id");
            }
        }

        public async Task<IEnumerable<DataLayer.Entities.Task>> GetAllAsync()
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                //without subtasks
                //var sql = "select * from dbo.Tasks";
                //IEnumerable<DataLayer.Entities.Task> tasks = 
                //    await sqlConnection.QueryAsync<DataLayer.Entities.Task>(sql);

                //return tasks;

                //with subtasks
                var sql = "select * from dbo.Tasks;select * from dbo.SubTask;";
                var multiResult = await sqlConnection.QueryMultipleAsync(sql);

                var tasks = await multiResult.ReadAsync<DataLayer.Entities.Task>();
                var subTasks = await multiResult.ReadAsync<DataLayer.Entities.SubTask>();

                foreach (var task in tasks)
                {
                    task.SubTasks = subTasks.Where(s => s.ParentTaskId == task.Id)
                                            .Select(s => { s.ParentTask = task; return s; });
                }

                return tasks;
            }
        }

        public async Task<DataLayer.Entities.Task> GetByIdAsync(int id)
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                var sql = "select * from dbo.Tasks where Id=@Id;select * from dbo.SubTask where ParentTaskId=@Id";
                var multiple = await sqlConnection.QueryMultipleAsync(sql, new { Id = id });

                var task = await multiple.ReadSingleOrDefaultAsync<DataLayer.Entities.Task>();
                var subtasks = await multiple.ReadAsync<DataLayer.Entities.SubTask>();

                task.SubTasks = subtasks.Select(s => { s.ParentTask = task; return s; });
                return task;
            }
        }

        public async Task RemoveAsync(DataLayer.Entities.Task task)
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                var sql = "delete from dbo.SubTask where ParentTaskId=@Id;delete from dbo.Tasks where Id=@Id";
                int affectedRows = await sqlConnection.ExecuteAsync(sql, new { task.Id });

                if (affectedRows == 0)
                    throw new Exception("Delete was unsuccessful");
            }
        }

        public async Task<bool> UpdateAsync(DataLayer.Entities.Task task)
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("Id", task.Id);
                parameters.Add("Title", task.Title);
                parameters.Add("Description", task.Description);
                parameters.Add("Priority", task.Priority);
                parameters.Add("Status", task.Status);
                parameters.Add("Type", task.Type);

                int updatedRows =
                    await sqlConnection.ExecuteAsync("sp_UpdateTask", parameters,
                                                        commandType: System.Data.CommandType.StoredProcedure);

                return updatedRows == 1;
            }
        }
    }
}
