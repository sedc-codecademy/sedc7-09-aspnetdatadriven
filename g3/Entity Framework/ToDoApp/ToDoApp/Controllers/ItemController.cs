using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;
using ViewModels;

namespace ToDoApp.Controllers
{
    public class ItemController : Controller
    {
        private readonly IUserService _userService;
        private readonly IItemService _itemService;

        public ItemController(IUserService userService, IItemService itemService)
        {
            _userService = userService;
            _itemService = itemService;
        }
        
        [Authorize]
        [HttpPost]
        public IActionResult Update(UserViewModel model)
        {
            _itemService.UpdateItems(model.ToDoItems);
            return RedirectToAction("UserItems", "User");
        }

        [Authorize]
        [HttpPost]
        public IActionResult Add(ToDoItemViewModel model)
        {
            var userEmail = HttpContext.User.FindFirst(ClaimTypes.Email).Value;

            var user = _userService.GetUserWithItems(userEmail);

            _itemService.AddTodoItem(model, user.Id);

            return RedirectToAction("UserItems", "User");
        }
    }
}
