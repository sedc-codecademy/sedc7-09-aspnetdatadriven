using SEDC.G2.DataDrive.Workshop.Data.Model;
using System.Collections.Generic;

namespace SEDC.G2.DataDrive.Workshop.Data.Interfaces
{
    public interface IDrinkOrderRepositpry
    {
        bool AddDrinkOrder(DrinkOrder order);
        bool EditDrinkOrder(DrinkOrder order);
        bool DeleteDrinkOrder(int id);
        List<Drink> GetDrinksInOrder(int orderId);
        List<DrinkOrder> GetWholeDrinkOrder(int orderId);
    }
}
