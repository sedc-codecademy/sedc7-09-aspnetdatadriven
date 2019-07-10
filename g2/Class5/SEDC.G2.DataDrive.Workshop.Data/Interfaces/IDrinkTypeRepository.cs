using SEDC.G2.DataDrive.Workshop.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEDC.G2.DataDrive.Workshop.Data.Interfaces
{
    public interface IDrinkTypeRepository
    {
        bool AddDrinkType(DrinkType drinkType);
        bool EditDrinkType(DrinkType drinkType);
        bool DeleteDrinkType(int drinkTypeId);
        DrinkType GetDrinkTypeById(int drinkTypeId);
        List<DrinkType> GetAllDrinkTypes();
    }
}
