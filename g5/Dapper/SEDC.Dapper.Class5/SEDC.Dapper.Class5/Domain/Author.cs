using System;
using System.Collections.Generic;
using System.Text;

namespace SEDC.Dapper.Class5.Domain
{
    public class Author
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public DateTime? DateOfDeath { get; set; }
        public List<Novel> Novels { get; set; }

        public override string ToString()
        {
            return $"#{ID}: {Name} ({Novels.Count} novels)";
        }
    }
}
