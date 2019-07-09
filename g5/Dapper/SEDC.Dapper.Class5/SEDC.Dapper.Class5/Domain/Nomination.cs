using System;
using System.Collections.Generic;
using System.Text;

namespace SEDC.Dapper.Class5.Domain
{
    public class Nomination
    {
        public int ID { get; set; }
        public int BookID { get; set; }
        public int AwardID
        {
            get
            {
                return (int)Award;
            }
            set
            {
                Award = (AwardType)value;
            }
        }
        public int YearNominated { get; set; }
        public bool IsWinner { get; set; }
        public AwardType Award { get; set; }
        public Novel Book { get; set; }
    }
    public enum AwardType
    {
        Hugo = 1,
        Nebula = 2
    }
}
