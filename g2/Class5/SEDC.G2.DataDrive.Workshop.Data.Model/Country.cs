using System.Collections.Generic;

namespace SEDC.G2.DataDrive.Workshop.Data.Model
{
    public class Country
    {
        public int CountryId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Manifacturer> Manifacturers { get; set; }
    }
}
