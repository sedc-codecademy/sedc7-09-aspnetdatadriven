using System.Collections.Generic;

namespace SEDC.G2.DataDrive.Workshop.Data.Model
{
    public class Drink
    {
        public int DrinkId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int ManifacturerId { get; set; }
        public bool IsHot { get; set; }
        public bool IsAlchoholic { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public int DrinkTypeId { get; set; }

        public virtual Manifacturer Manifacturer { get; set; }
        public virtual DrinkType DrinkType { get; set; }
        public virtual ICollection<DrinkOrder> DrinkOrders { get; set; }
    }
}