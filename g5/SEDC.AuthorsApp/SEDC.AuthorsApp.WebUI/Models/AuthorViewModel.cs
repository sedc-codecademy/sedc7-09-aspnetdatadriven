using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SEDC.AuthorsApp.WebUI.Models
{
    public class AuthorViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string DateOfBirth { get; set; }
        public string DateOfDeath { get; set; }
        public List<NovelViewModel> Novels { get; set; } = new List<NovelViewModel>();
    }
}
