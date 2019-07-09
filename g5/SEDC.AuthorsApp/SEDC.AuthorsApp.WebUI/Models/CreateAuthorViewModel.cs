using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SEDC.AuthorsApp.WebUI.Models
{
    public class CreateAuthorViewModel
    {
        public AuthorViewModel Author { get; set; }
        public NovelViewModel Novel { get; set; }
    }
}
