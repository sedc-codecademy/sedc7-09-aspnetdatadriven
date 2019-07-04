using System;
using System.Collections.Generic;

namespace SEDC.Entity.Class3.Domain
{
    public partial class Awards
    {
        public Awards()
        {
            Nominations = new HashSet<Nominations>();
        }

        public int Id { get; set; }
        public string AwardName { get; set; }

        public virtual ICollection<Nominations> Nominations { get; set; }
    }
}
