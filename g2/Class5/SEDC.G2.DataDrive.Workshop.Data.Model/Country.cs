using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEDC.G2.DataDrive.Workshop.Data.Model
{
    public class Country
    {
        public int CountryID { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Manifacturer> Manifacturers { get; set; }
    }
}
