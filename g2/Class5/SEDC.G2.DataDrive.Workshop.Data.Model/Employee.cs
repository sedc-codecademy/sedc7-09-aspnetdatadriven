using System.Collections.Generic;

namespace SEDC.G2.DataDrive.Workshop.Data.Model
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
}
}
