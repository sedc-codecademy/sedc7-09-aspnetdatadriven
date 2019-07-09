using System;
using System.Collections.Generic;
using System.Text;

namespace SEDC.AuthorsApp.Services
{
    public class AuthorDTO
    {
        #region Properties

        public int ID { get; set; }
        public string Name { get; set; }
        public string DateOfBirth { get; set; }
        public string DateOfDeath { get; set; }
        public List<NovelDTO> Novels { get; set; } = new List<NovelDTO>();
        #endregion

        #region Overridden Methods

        // We override the system 'ToString' method so when we write Author.ToString() to give us information
        // abut the author instead of just giving us 'SEDC.Module03.Entities.Author'
        public override string ToString()
        {
            return $"#{ID}: {Name} ({Novels.Count} novels)";
        }
        #endregion
    }
}
