using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SEDC.AuthorsApp.Domain
{
    public class Novel
    {
        #region Properties

        public int ID { get; set; }
        public string Title { get; set; }
        // This authorId is just for database to have a one to many relationship
        public int AuthorID { get; set; }
        public bool IsRead { get; set; }
        public Author Author { get; set; }
        public List<Nomination> Nominations { get; set; }

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
