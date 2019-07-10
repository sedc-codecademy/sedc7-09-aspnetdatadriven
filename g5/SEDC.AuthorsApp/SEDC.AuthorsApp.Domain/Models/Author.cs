using System;
using System.Collections.Generic;
using System.Text;

namespace SEDC.AuthorsApp.Domain
{
    public class Author
    {
        #region Properties

        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime? DateOfDeath { get; set; }
        public List<Novel> Novels { get; set; }

        #endregion

    }
}