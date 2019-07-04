using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using CompanyApp_Dapper.Models;
using Dapper;

namespace CompanyApp_Dapper
{
    class Program
    {
        private static string _connectionString = "Server=.;Database=SEDCCompanyDb-Dapper;Trusted_Connection=True";

        static void Main(string[] args)
        {
            var employeeCount = CountEmployees();
            Console.WriteLine($"No. of employees: {employeeCount}");

            var departments = GetDepartments();
            foreach (var department in departments)
            {
                Console.WriteLine($"{department.Id}: {department.Name}");
            }

            var filter = Console.ReadLine();
            var companies = CallSp(filter);
            foreach (var company in companies)
            {
               Console.WriteLine($"{company.Id}: {company.Name} [{company.Address}]"); 
            }

            Console.ReadKey();
        }

        private static int CountEmployees()
        {
            int count;
            using (IDbConnection con = new SqlConnection(_connectionString))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                var sql = "Select Count(*) From Employees";

                count = con.ExecuteScalar<int>(sql);
            }

            return count;
        }

        private static List<Department> GetDepartments()
        {
            List<Department> departments;
            using (IDbConnection con = new SqlConnection(_connectionString))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                var sql = "Select * From Departments";

                departments = con.Query<Department>(sql).ToList();
            }

            return departments;
        }

        private static List<Company> CallSp(string filter)
        {
            List<Company> companies;
            using (IDbConnection con = new SqlConnection(_connectionString))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@companyName", filter);

                companies = con.Query<Company>("GetCompanies", parameters, commandType: CommandType.StoredProcedure)
                    .ToList();
            }

            return companies;
        }
    }
}
