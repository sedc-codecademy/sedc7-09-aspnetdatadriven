using System.Collections.Generic;

namespace SEDC.G2.DataDrive.Workshop.Data.Model
{
    public class DrinkType
    {
        public int DrinkTypeId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Drink> Drinks { get; set; }
    }
}
