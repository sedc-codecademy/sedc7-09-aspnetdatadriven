using System.Collections.Generic;
using DtoModels;
using ViewModels;

namespace Mappers
{
    public static class UserMapper
    {
        public static BasicUserViewModel ToBasicViewModel(this User user)
        {
            return new BasicUserViewModel
            {
                Id = user.Id,
                Email = user.Email,
                FullName = user.FullName
            };
        }

        public static UserViewModel ToUserViewModel(this User user)
        {
            var userModel = new UserViewModel()
            {
                Id = user.Id,
                Email = user.Email,
                FullName = user.FullName,
                ToDoItems = new List<ToDoItemViewModel>()
            };

            foreach (var item in user.Items)
            {
                userModel.ToDoItems.Add(item.ToItemViewModel());
            }

            return userModel;
        }
    }
}
