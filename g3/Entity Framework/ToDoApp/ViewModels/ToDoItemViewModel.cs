using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ViewModels
{
    public class ToDoItemViewModel
    {
        public int Id { get; set; }
        [DisplayName("Item name/description")]
        [Required]
        public string Name { get; set; }
        public bool Completed { get; set; }
        public BasicUserViewModel User { get; set; }
    }
}
