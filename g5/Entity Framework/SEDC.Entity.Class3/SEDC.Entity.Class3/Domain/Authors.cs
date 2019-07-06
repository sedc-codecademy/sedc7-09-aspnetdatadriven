using System;
using System.Collections.Generic;

namespace SEDC.Entity.Class3.Domain
{
    public partial class Authors
    {
        public Authors()
        {
            Novels = new HashSet<Novels>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime? DateOfDeath { get; set; }

        public virtual ICollection<Novels> Novels { get; set; }
    }
}
