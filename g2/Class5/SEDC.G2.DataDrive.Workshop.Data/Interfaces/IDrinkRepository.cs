using SEDC.G2.DataDrive.Workshop.Data.Model;
using System.Collections.Generic;

namespace SEDC.G2.DataDrive.Workshop.Data.Interfaces
{
    public interface IDrinkRepository
    {
        bool AddDrink(Drink drink);
        bool EditDrink(Drink drink);
        bool DeleteDrink(int id);
        List<Drink> GetAllDrinks();
        List<Drink> GetDrinksByType(int drinkTypeId);
        List<Drink> GetAlchoholicDrinks();
        List<Drink> GetNonAlchoholicDrinks();
        List<Drink> GetHotDrinks();
        List<Drink> GetColdDrinks();
        Drink GetDrinkById(int id);
    }
}
