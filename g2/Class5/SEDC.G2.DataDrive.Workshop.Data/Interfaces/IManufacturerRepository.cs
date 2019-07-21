using SEDC.G2.DataDrive.Workshop.Data.Model;
using System.Collections.Generic;

namespace SEDC.G2.DataDrive.Workshop.Data.Interfaces
{
    public interface IManufacturerRepository
    {
        bool AddManufacturer(Manifacturer manufacturer);
        bool EditManufacturer(Manifacturer manufacturer);
        bool DeleteManufacturer(int manufacturarId);
        List<Manifacturer> GetAllManufacturers();
        Manifacturer GetManufacturerById(int manufacturerId);
    }
}
