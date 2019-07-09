using DtoModels;
using ViewModels;

namespace Mappers
{
    public static class ItemMapper
    {
        public static ToDoItemViewModel ToItemViewModel(this ToDoItem item)
        {
            return new ToDoItemViewModel
            {
                Id = item.Id,
                Name = item.Name,
                Completed = item.Completed
            };
        }
    }
}
