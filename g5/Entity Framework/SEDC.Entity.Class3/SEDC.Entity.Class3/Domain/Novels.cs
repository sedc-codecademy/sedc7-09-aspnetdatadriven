using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SEDC.Entity.Class3.Domain
{
    [Table("Novels")]
    public partial class Novels
    {
        public Novels()
        {
            Nominations = new HashSet<Nominations>();
        }
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Required]
        [MaxLength(150)]
        public string Title { get; set; }
        [ForeignKey("Author")]
        public int AuthorId { get; set; }
        public bool? IsRead { get; set; }
        [NotMapped]
        public int BooksCount { get; set; }

        public virtual Authors Author { get; set; }
        public virtual ICollection<Nominations> Nominations { get; set; }
    }
}
