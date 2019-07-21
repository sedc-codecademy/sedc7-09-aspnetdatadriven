using Dapper;
using SEDC.G2.DataDrive.Workshop.Data.Interfaces;
using SEDC.G2.DataDrive.Workshop.Data.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace SEDC.G2.DataDrive.Workshop.Data.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private string _connectionString;

        public EmployeeRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public bool AddEmployee(Employee employee)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                var sql = @"INSERT INTO Employee (Firstname, Lastname) VALUES (@Firstname, @Lastname)";
                try
                {
                    var affected = conn.Execute(sql, new { @Firstname = employee.Firstname, @Lastname = employee.Lastname });
                }
                catch(Exception)
                {
                    return false;
                }
            }
            return true;
        }

        public bool DeleteEmployee(int id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                var sql = @"DELETE FROM Employee WHERE EmployeeId = @EmployeeId";
                try
                {
                    var affected = conn.Execute(sql, new { @EmployeeId = id });
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return true;
        }

        public bool EditEmployee(Employee employee)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                var sql = @"UPDATE Employee SET Firstname = @Firstname, Lastname = @Lastname where EmployeeId = @EmployeeId";
                try
                {
                    var affected = conn.Execute(sql, new { @Firstname = employee.Firstname, @Lastname = employee.Lastname, @EmployeeId = employee.EmployeeId });
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return true;
        }

        public List<Employee> GetAllEmployees()
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                var sql = @"SELECT * FROM Employee";
                try
                {
                    return conn.Query<Employee>(sql).ToList();
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public Employee GetEmployeeById(int id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                var sql = @"SELECT TOP 1 * FROM Employee WHERE EmployeeId = @EmployeeId";
                try
                {
                    return conn.QueryFirstOrDefault<Employee>(sql, new { @EmployeeId = id });
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
    }
}
