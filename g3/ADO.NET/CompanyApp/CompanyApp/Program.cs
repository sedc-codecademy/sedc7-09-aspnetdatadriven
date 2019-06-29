using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using CompanyApp.Models;

namespace CompanyApp
{
    class Program
    {
        private static string _connectionString = "Server=.;Database=SEDCCompanyDb;Trusted_Connection=True";
        static void Main(string[] args)
        {
            Console.WriteLine($"We have {CountEmployees()} employees");
            //var departments = GetDepartments();

            //var filter = Console.ReadLine();
            //var departments = FilterDepartmentsGoodExample(filter);

            //foreach (var department in departments)
            //{
            //    Console.WriteLine($"{department.Id}: {department.Name}");
            //}

            //Console.ReadKey();

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
            SqlConnection connection = new SqlConnection(_connectionString);

            connection.Open();

            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = connection;
            sqlCommand.CommandText = "Select Count(*) From Employees";

            var count = (int)sqlCommand.ExecuteScalar();
            connection.Close();
            return count;
        }

        private static List<Department> GetDepartments()
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "Select * From Departments";

            SqlDataReader reader = cmd.ExecuteReader();

            var departments = new List<Department>();
            while (reader.Read())
            {
                //var id = reader.GetInt32(0);
                //var name = reader.GetString(1);

                //var id = reader.GetFieldValue<int>(0);
                //var name = reader.GetFieldValue<string>(1);

                var id = (int)reader["Id"];
                var name = (string) reader["Name"];

                departments.Add(new Department(id, name));
            }

            connection.Close();

            return departments;
        }

        private static List<Department> FilterDepartmentsBadExample(string filter)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "Select * From Departments Where Name Like '%" + filter + "%'";

            SqlDataReader reader = cmd.ExecuteReader();

            var departments = new List<Department>();
            while (reader.Read())
            {
                var id = (int)reader["Id"];
                var name = (string)reader["Name"];

                departments.Add(new Department(id, name));
            }

            connection.Close();

            return departments;
        }

        private static List<Department> FilterDepartmentsGoodExample(string filter)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "Select * From Departments Where Name Like '%' + @filter + '%'";
            cmd.Parameters.AddWithValue("@filter", filter);

            SqlDataReader reader = cmd.ExecuteReader();

            var departments = new List<Department>();
            while (reader.Read())
            {
                var id = (int)reader["Id"];
                var name = (string)reader["Name"];

                departments.Add(new Department(id, name));
            }

            connection.Close();

            return departments;
        }

        private static List<Company> CallSp(string filter)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "GetCompanies";
            cmd.CommandType = CommandType.StoredProcedure;

            var param = cmd.Parameters.Add("@companyName", SqlDbType.NVarChar, 100);
            param.Value = filter;

            var reader = cmd.ExecuteReader();

            var companies = new List<Company>();
            while (reader.Read())
            {
                var id = (int)reader["Id"];
                var name = (string)reader["Name"];
                var address = (string)reader["Address"];

                companies.Add(new Company(id, name, address));
            }

            connection.Close();

            return companies;
        }
    }
}
