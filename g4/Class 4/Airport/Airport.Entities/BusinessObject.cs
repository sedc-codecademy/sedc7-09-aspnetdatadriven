using Airport.Entities.Enums;
using System;
using System.Collections.Generic;

namespace Airport.Entities
{
    public class BusinessObject : BaseEntity
    {
        public string Name { get; set; }

        public BusinessObjectType Type { get; set; }

        public int ResponsibleEmployeeId { get; set; }

        public Employee ResponsibleEmployee { get; set; }

        public DateTime OpeningTime { get; set; }

        public TimeSpan WorkingTime { get; set; }

        public IList<Offer> Offers { get; set; }
    }
}
