using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEDC.G2.DataDrive.Workshop.Data.Model
{
    public class Manifacturer
    {
        public int ManifacturerId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CountryId { get; set; }

        public virtual Country Country { get; set; }
    }
}
