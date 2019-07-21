using SEDC.G2.DataDrive.Workshop.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEDC.G2.DataDrive.Workshop.Data.Interfaces
{
    public interface ICountryRepository
    {
        bool AddCountry(Country country);
        bool EditCountry(Country country);
        List<Country> GetAllCountries();
        Country GetCountryById(int countryId);
        bool DeleteCountry(int id);
    }
}
