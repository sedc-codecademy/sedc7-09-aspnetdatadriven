using Dapper;
using SEDC.G2.DataDrive.Workshop.Data.Interfaces;
using SEDC.G2.DataDrive.Workshop.Data.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace SEDC.G2.DataDrive.Workshop.Data.Repositories
{
    public class DrinkOrderRepository : IDrinkOrderRepositpry
    {
        private string _connectionString;

        public DrinkOrderRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public bool AddDrinkOrder(DrinkOrder order)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                var sql = @"INSERT INTO DrinkOrder (Comment, DrinkId, OrderId, Quantity) VALID (@Comment, @DrinkId, @OrderId, @Quantity)";
                try
                {
                    conn.Execute(sql, new {
                        @Comment = order.Comment,
                        @DrinkId = order.DrinkId,
                        @OrderId = order.OrderId,
                        @Quantity = order.Quantity
                    });
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return true;
        }

        public bool DeleteDrinkOrder(int id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                var sql = $"DELETE FROM DrinkOrder WHERE DrinkOrderId = {id}";
                try
                {
                    conn.Execute(sql);
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return true;
        }

        public bool EditDrinkOrder(DrinkOrder order)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                var sql = @"UPDATE DrinkOrder SET
                                Comment = @Comment,
                                DrinkId = @DrinkId,
                                OrderId = @OrderId,
                                Quantity = @Quantity
                            WHERE
                                DrinkOrderId = @DrinkOrderId";
                try
                {
                    conn.Execute(sql, new {
                        @Comment = order.Comment,
                        @DrinkId = order.DrinkId,
                        @OrderId = order.OrderId,
                        @Quantity = order.Quantity,
                        @DrinkOrderId = order.DrinkOrderId
                    });
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return true;
        }

        public List<Drink> GetDrinksInOrder(int orderId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                var sql = $"select d.* from Drink d inner join DrinkOrder do on d.DrinkId = do.DrinkId where do.OrderId = {orderId}";
                try
                {
                    return conn.Query<Drink>(sql).ToList();
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public List<DrinkOrder> GetWholeDrinkOrder(int orderId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                var sql = $"SELECT * FROM DrinkOrder do INNER JOIN Drink d ON d.DrinkId = do.DrinkId INNER JOIN [Order] o on do.OrderId = o.OrderId WHERE o.OrderId = {orderId}";
                try
                {
                    return conn.Query<DrinkOrder, Drink, Order, DrinkOrder>(sql, 
                        (drinkOrder, drink, order) =>
                        {
                            drinkOrder.Drink = drink;
                            drinkOrder.Order = order;
                            return drinkOrder;
                        },
                        splitOn:"DrinkId,OrderId").Distinct().ToList();
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
    }
}
