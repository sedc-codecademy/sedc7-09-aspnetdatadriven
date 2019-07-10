using System;
using System.Collections.Generic;
using System.Text;

namespace SEDC.AuthorsApp.Services
{
    public class NovelDTO
    {
        #region Properties
        public int ID { get; set; }
        public string Title { get; set; }
        public bool IsRead { get; set; }
        public AuthorDTO Author { get; set; }
        public List<NominationDTO> Nominations { get; set; } = new List<NominationDTO>();
        #endregion

        #region Overridden Methods

        // We override the system 'ToString' method so when we write Novel.ToString() to give us information
        // abut the novel instead of just giving us 'SEDC.Module03.Entities.Novel'
        public override string ToString()
        {
            return $"#{ID}: {Title}";
        }

        #endregion
    }
}
