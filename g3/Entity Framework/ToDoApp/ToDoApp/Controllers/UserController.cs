using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Services;
using ViewModels;

namespace ToDoApp.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        public IActionResult Login()
        {
            if (HttpContext.User.FindFirst(ClaimTypes.Email) != null)
            {
                return RedirectToAction("UserItems", "User");
            }

            return View(new LoginViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            
            var user = _userService.GetUser(model.Email, model.Password);

            if (user == null)
            {
                ModelState.AddModelError("", "Wrong username or password");
                return View(model);
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email)
            };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties();

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

            return RedirectToAction("UserItems");
        }

        [Authorize]
        public IActionResult UserItems()
        {
            var userEmail = HttpContext.User.FindFirst(ClaimTypes.Email).Value;

            var user = _userService.GetUserWithItems(userEmail);
            return View(user);
        }
    }
}