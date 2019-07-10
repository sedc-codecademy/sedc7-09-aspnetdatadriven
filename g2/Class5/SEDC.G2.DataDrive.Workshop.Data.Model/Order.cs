using System.Collections.Generic;

namespace SEDC.G2.DataDrive.Workshop.Data.Model
{
    public class Order
    {
        public int OrderId { get; set; }
        public string Details { get; set; }
        public int EmployeeId { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual ICollection<DrinkOrder> DrinkOrders { get; set; }
    }
}