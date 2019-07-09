using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SEDC.Dapper.Class5.Domain
{
    public class Novel
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public int AuthorId { get; set; }
        public Author Author { get; set; }
        public List<Nomination> Nominations { get; set; }
        public int NominationsCount
        {
            get
            {
                return Nominations != null ? Nominations.Count : 0;
            }
        }
        public int WonCount
        {
            get
            {
                return Nominations != null ? Nominations.Count(x => x.IsWinner) : 0;
            }
        }

        public override string ToString()
        {
            return $"#{ID}: {Title}";
        }
    }
}
