namespace Airport.Entities
{
    public class Offer : BaseEntity
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public int BusinessObjectId { get; set; }

        public BusinessObject BusinessObject { get; set; }

        public decimal Price { get; set; }

        public float DiscountPercentage { get; set; }
    }
}