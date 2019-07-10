using ViewModels;

namespace Services
{
    public interface IUserService
    {
        BasicUserViewModel GetUser(string email, string password);
        UserViewModel GetUserWithItems(string email);
    }
}
