using SEDC.G2.DataDrive.Workshop.Data.Interfaces;
using SEDC.G2.DataDrive.Workshop.Data.Model;
using SEDC.G2.DataDrive.Workshop.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEDC.G2.DataDrive.Workshop.App
{
    class Program
    {
        IDrinkTypeRepository _drinkTypeRepository = new DrinkTypeRepository(ConfigurationManager.ConnectionStrings["CoffeeBarDb"].ConnectionString);
        static void Main(string[] args)
        {
            Program program = new Program();
            //program.GetDrinkById();
            //program.AddDrinkType();
            //program.RemoveDrinkType();
            program.GetAllDrinkTypes();
        }

        private void GetAllDrinkTypes()
        {
            var drinkTypes = _drinkTypeRepository.GetAllDrinkTypes();

            Console.WriteLine("Available DrinkTypes");
            Console.WriteLine("===============================");
            foreach(var item in drinkTypes)
            {
                Console.WriteLine(item.Name);
            }
        }

        private void RemoveDrinkType()
        {
            Console.Write("Drink type id: ");
            var id = int.Parse(Console.ReadLine());

            var result = _drinkTypeRepository.DeleteDrinkType(id);
            if (result)
            {
                Console.WriteLine("The drink type has been removed!");
            }
            else
            {
                Console.WriteLine($"Drink with ID={id} does not exists!");
            }
        }

        private void AddDrinkType()
        {
            Console.Write("Drink type: ");
            var name = Console.ReadLine();

            var drinkType = new DrinkType
            { Name = name};

            var result = _drinkTypeRepository.AddDrinkType(drinkType);

            if(result)
            {
                Console.WriteLine("New DrinkType has been added!");
            }
            else
            {
                Console.WriteLine("DrinkType insertion failed!");
            }
        }

        private void GetDrinkById()
        {
            Console.Write("Drink type id: ");
            var id = int.Parse(Console.ReadLine());

            var drink = _drinkTypeRepository.GetDrinkTypeById(id);
            if (drink != null)
            {
                Console.WriteLine("The drink type that you have requested is: " + drink.Name);
            }
            else
            {
                Console.WriteLine($"Drink with ID={id} does not exists!");
            }
        }
    }
}
