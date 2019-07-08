
using System.ComponentModel.DataAnnotations;

namespace Airport.Entities
{
    public class Employee : BaseEntity
    {
        [Required]
        [StringLength(100)]
        public string FullName { get; set; }

        public BusinessObject ResponsibleFor { get; set; }
    }
}