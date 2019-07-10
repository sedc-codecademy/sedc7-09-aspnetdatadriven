using SEDC.AuthorsApp.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace SEDC.AuthorsApp.Services
{
    public class NominationDTO
    {
        #region Properties

        public int ID { get; set; }
        public int YearNominated { get; set; }
        public bool IsWinner { get; set; }
        public string Award { get; set; }
        public Novel Book { get; set; }

        #endregion
    }
}
