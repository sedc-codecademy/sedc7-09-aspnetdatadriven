using SEDC.G2.DataDrive.Workshop.Data.Model;
using System.Collections.Generic;

namespace SEDC.G2.DataDrive.Workshop.Data.Interfaces
{
    public interface IOrderRepository
    {
        bool AddOrder(Order order);
        bool EditOrder(Order order);
        bool DeleteOrder(int id);
        List<Order> GetAllOrders();
        Order GetOrderById(int id);
    }
}
