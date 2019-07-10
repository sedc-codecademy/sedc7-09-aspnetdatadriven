using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEDC.G2.DataDrive.Workshop.Data.Model
{
    public class DrinkType
    {
        public int DrinkTypeId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Drink> Drinks { get; set; }
    }
}
