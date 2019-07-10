using System.Collections.Generic;
using DataLayer;
using DtoModels;
using ViewModels;

namespace Services
{
    public class ItemService : IItemService
    {
        private readonly IRepository<ToDoItem> _toDoItemRepository;
        private readonly IRepository<User> _userRepository;

        public ItemService(IRepository<ToDoItem> doItemRepository, IRepository<User> userRepository)
        {
            _toDoItemRepository = doItemRepository;
            _userRepository = userRepository;
        }

        public void AddTodoItem(ToDoItemViewModel model, int userId)
        {
            var toDoItem = new ToDoItem(model.Name, userId);
            _toDoItemRepository.Create(toDoItem);
        }

        public void UpdateItems(List<ToDoItemViewModel> items)
        {
            foreach (var modelItem in items)
            {
                var todoItem = _toDoItemRepository.GetById(modelItem.Id);
                todoItem.Completed = modelItem.Completed;
                _toDoItemRepository.Update(todoItem);
            }
        }
    }
}
