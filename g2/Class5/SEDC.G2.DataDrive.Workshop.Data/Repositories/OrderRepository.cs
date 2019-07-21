using Dapper;
using SEDC.G2.DataDrive.Workshop.Data.Interfaces;
using SEDC.G2.DataDrive.Workshop.Data.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace SEDC.G2.DataDrive.Workshop.Data.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private string _connectionString;

        public OrderRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public bool AddOrder(Order order)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                var sql = @"INSERT INTO [Order] (Details, EmployeeId) VALID (@Details, @EmployeeId)";
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

        public bool DeleteOrder(int id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                var sql = @"DELETE FROM [Order] WHERE OrderId = @OrderId";
                try
                {
                    conn.Execute(sql, new { @OrderId = id });
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return true;
        }

        public bool EditOrder(Order order)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                var sql = @"UPDATE [Order] 
                            SET
                                Details = @Details,
                                EmployeeId = @EmployeeId
                            WHERE
                                OrderId = @OrderId";
                try
                {
                    conn.Execute(sql, new {
                        @Details = order.Details,
                        @EmployeeId = order.EmployeeId,
                        @OrderId = order.OrderId });
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return true;
        }

        public List<Order> GetAllOrders()
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                var sql = @"Select * FROM [Order]";
                try
                {
                    return conn.Query<Order>(sql).ToList();
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public Order GetOrderById(int id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                var sql = @"SELECT TOP 1 * FROM [Order] WHERE OrderId = @OrderId";
                try
                {
                    return conn.QueryFirstOrDefault<Order>(sql, new { @OrderId = id });
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
    }
}
