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
        public Task AddAsync(DataLayer.Entities.Task task)
        {
            throw new NotImplementedException();
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

        public Task<DataLayer.Entities.Task> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
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
