namespace SEDC.G2.DataDrive.Workshop.Data.Model
{
    public class DrinkOrder
    {
        public int DrinkOrderId { get; set; }
        public string Comment { get; set; }
        public int DrinkId { get; set; }
        public int OrderId { get; set; }
        public int Quantity { get; set; }

        public virtual Drink Drink { get; set; }
        public virtual Order Order { get; set; }
    }
}