using Dapper;
using SEDC.G2.DataDrive.Workshop.Data.Interfaces;
using SEDC.G2.DataDrive.Workshop.Data.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace SEDC.G2.DataDrive.Workshop.Data.Repositories
{
    public class DrinkRepository : IDrinkRepository
    {
        private string _connectionString;

        public DrinkRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public bool AddDrink(Drink drink)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                var sql = @"INSERT INTO Drink ([Name], Description, ManifacturerId, IsHot, IsAlchoholic, Quantity, Price, DrinkTypeId) 
                            VALUES (@Name, @Desvription, @MId, @IsHot, @IsAlchoholic, @Quantity, @Price, @DrinkTypeId)";
                try
                {
                    var affected = conn.Execute(sql, new {
                        @Name = drink.Name,
                        @Description = drink.Description,
                        @MId = drink.ManifacturerId,
                        @IsHot = drink.IsHot,
                        @IsAlchoholic = drink.IsAlchoholic,
                        @Quantity = drink.Quantity, 
                        @Price = drink.Price,
                        @DrinkTypeId = drink.DrinkTypeId
                    });
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return true;
        }

        public bool DeleteDrink(int id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                var sql = @"DELETE FROM Drink WHERE DrinkId = @DrinkId";
                try
                {
                    var affected = conn.Execute(sql, new { @DrinkId = id });
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return true;
            throw new NotImplementedException();
        }

        public bool EditDrink(Drink drink)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                var sql = @"UPDATE Drink 
                        SET 
                        Name = @Name,
                        Description = @Description,
                        ManifacturerId = @MId,
                        IsHot = @IsHot,
                        IsAlchoholic = @IsAlchoholic,
                        Quantity = @Quantity,
                        Price = @Price,
                        DrinkTypeId = @DrinkTypeId";
                try
                {
                    var affected = conn.Execute(sql, new
                    {
                        @Name = drink.Name,
                        @Description = drink.Description,
                        @MId = drink.ManifacturerId,
                        @IsHot = drink.IsHot,
                        @IsAlchoholic = drink.IsAlchoholic,
                        @Quantity = drink.Quantity,
                        @Price = drink.Price,
                        @DrinkTypeId = drink.DrinkTypeId
                    });
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return true;
        }

        public List<Drink> GetAlchoholicDrinks()
        {
            var drinks = new List<Drink>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                var sql = @"SELECT * FROM Drink WHERE IsAlchoholic = 1";
                try
                {
                    drinks = conn.Query<Drink>(sql).ToList();
                }
                catch (Exception)
                {
                    return drinks;
                }
            }
            return drinks;
        }
        public List<Drink> GetNonAlchoholicDrinks()
        {
            var drinks = new List<Drink>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                var sql = @"SELECT * FROM Drink WHERE IsAlchoholic = 0";
                try
                {
                    drinks = conn.Query<Drink>(sql).ToList();
                }
                catch (Exception)
                {
                    return drinks;
                }
            }
            return drinks;
        }

        public List<Drink> GetAllDrinks()
        {
            var drinks = new List<Drink>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                var sql = @"SELECT * FROM Drink";
                try
                {
                    drinks = conn.Query<Drink>(sql).ToList();
                }
                catch (Exception)
                {
                    return drinks;
                }
            }
            return drinks;
        }

        public List<Drink> GetHotDrinks()
        {
            var drinks = new List<Drink>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                var sql = @"SELECT * FROM Drink WHERE IsHot = 1";
                try
                {
                    drinks = conn.Query<Drink>(sql).ToList();
                }
                catch (Exception)
                {
                    return drinks;
                }
            }
            return drinks;
        }

        public List<Drink> GetColdDrinks()
        {
            var drinks = new List<Drink>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                var sql = @"SELECT * FROM Drink WHERE IsHot = 0";
                try
                {
                    drinks = conn.Query<Drink>(sql).ToList();
                }
                catch (Exception)
                {
                    return drinks;
                }
            }
            return drinks;
        }

        public Drink GetDrinkById(int id)
        {
            var drink = new Drink();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                var sql = @"SELECT TOP 1 * FROM Drink WHERE DrinkId = @DrinkId";
                try
                {
                    drink = conn.QueryFirstOrDefault<Drink>(sql, new { @DrinkId = id});
                }
                catch (Exception)
                {
                    return null;
                }
            }
            return drink;
        }

        public List<Drink> GetDrinksByType(int drinkTypeId)
        {
            var drinks = new List<Drink>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                var sql = @"SELECT * FROM Drink WHERE DrinkTypeId = @DrinkTypeId";
                try
                {
                    drinks = conn.Query<Drink>(sql, new { @DrinkTypeId = drinkTypeId }).ToList();
                }
                catch (Exception)
                {
                    return drinks;
                }
            }
            return drinks;
        }
    }
}
