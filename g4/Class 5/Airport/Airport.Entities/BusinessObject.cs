using Airport.Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Airport.Entities
{
    public class BusinessObject : BaseEntity
    {
        [Required, StringLength(100)]
        public string Name { get; set; }

        public BusinessObjectType Type { get; set; }

        //public int ResponsibleEmployeeId { get; set; }

        //public Employee ResponsibleEmployee { get; set; }

        public IList<BusinessObjectEmployee> ResponsibleEmployees { get; set; }

        public DateTime OpeningTime { get; set; }

        public TimeSpan WorkingTime { get; set; }

        public IList<Offer> Offers { get; set; }
    }
}
