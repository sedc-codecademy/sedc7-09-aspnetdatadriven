using System.Collections.Generic;
using ViewModels;

namespace Services
{
    public interface IItemService
    {
        void AddTodoItem(ToDoItemViewModel model, int userId);
        void UpdateItems(List<ToDoItemViewModel> items);
    }
}
