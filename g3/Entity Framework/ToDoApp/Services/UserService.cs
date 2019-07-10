using System;
using System.Linq;
using DataLayer;
using DtoModels;
using Mappers;
using ViewModels;

namespace Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;

        public UserService(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public BasicUserViewModel GetUser(string email, string password)
        {
            var user = _userRepository.GetAll().FirstOrDefault(x => x.Email == email && x.Password == password);

            return user?.ToBasicViewModel();
        }

        public UserViewModel GetUserWithItems(string email)
        {
            var user = _userRepository.GetAll().FirstOrDefault(x => x.Email == email)?.ToUserViewModel();

            return user;
        }
    }
}
