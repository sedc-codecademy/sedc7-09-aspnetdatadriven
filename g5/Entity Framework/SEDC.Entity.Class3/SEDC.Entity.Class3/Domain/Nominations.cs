using System;
using System.Collections.Generic;

namespace SEDC.Entity.Class3.Domain
{
    public partial class Nominations
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public int AwardId { get; set; }
        public int? YearNominated { get; set; }
        public bool? IsWinner { get; set; }

        public virtual Awards Award { get; set; }
        public virtual Novels Book { get; set; }
    }
}
