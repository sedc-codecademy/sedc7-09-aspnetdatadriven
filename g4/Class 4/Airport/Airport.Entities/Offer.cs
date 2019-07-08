using System.ComponentModel.DataAnnotations;

namespace Airport.Entities
{
    public class Offer : BaseEntity
    {
        [Required, StringLength(100)]
        public string Title { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public int BusinessObjectId { get; set; }

        public BusinessObject BusinessObject { get; set; }

        public decimal Price { get; set; }

        public float DiscountPercentage { get; set; }
    }
}