using System.Collections.Generic;

namespace ViewModels
{
    public class UserViewModel : BasicUserViewModel
    {

        public List<ToDoItemViewModel> ToDoItems { get; set; }
    }
}
