using SEDC.G2.DataDrive.Workshop.Data.Interfaces;
using SEDC.G2.DataDrive.Workshop.Data.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace SEDC.G2.DataDrive.Workshop.Data.Repositories
{
    public class DrinkTypeRepository : IDrinkTypeRepository
    {
        private string _connectionString;

        public DrinkTypeRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public bool AddDrinkType(DrinkType drinkType)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var sql = $"INSERT INTO DrinkType ([Name]) VALUES ('{drinkType.Name}')";

                SqlCommand cmd = new SqlCommand(sql);
                cmd.Connection = connection;
                try
                {
                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return true;
        }

        public bool DeleteDrinkType(int drinkTypeId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var sql = $"DELETE FROM DrinkType where DrinkTypeId = {drinkTypeId}";

                SqlCommand cmd = new SqlCommand(sql);
                cmd.Connection = connection;
                try
                {
                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
            return true;
        }

        public bool EditDrinkType(DrinkType drinkType)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var sql = $"Update DrinkType SET [Name] = '{drinkType.Name}' where DrinkTypeId = {drinkType.DrinkTypeId}";

                SqlCommand cmd = new SqlCommand(sql);
                cmd.Connection = connection;
                try
                {
                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return true;
        }

        public List<DrinkType> GetAllDrinkTypes()
        {
            var drinkTypes = new List<DrinkType>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var sql = $"SELECT * FROM DrinkType";

                SqlCommand cmd = new SqlCommand(sql);
                cmd.Connection = connection;

                connection.Open();
                var dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    drinkTypes.Add(new DrinkType
                    {
                        DrinkTypeId = (int)dataReader["DrinkTypeId"],
                        Name = (string)dataReader["Name"]
                    });
                }
            }
            return drinkTypes;
        }

        public DrinkType GetDrinkTypeById(int drinkTypeId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var sql = $"SELECT TOP 1 * FROM DrinkType WHERE DrinkTypeId = {drinkTypeId}";

                SqlCommand cmd = new SqlCommand(sql);
                cmd.Connection = connection;

                connection.Open();
                var dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    return new DrinkType
                    {
                        DrinkTypeId = (int)dataReader["DrinkTypeId"],
                        Name = (string)dataReader["Name"]
                    };
                }
            }
            return null;
        }
    }
}
