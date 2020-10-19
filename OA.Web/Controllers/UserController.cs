using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using OA.Service;
using OA.Web.Models;
using System.Collections.Generic;
using System.Security.Claims;

namespace OA.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService userService;

        public UserController(IUserService _userService)
        {
            userService = _userService;
        }

        public IActionResult Index()
        {
            return Redirect("User/" + nameof(Login));
        }

        public IActionResult Login()
        {
            var user = new UserViewModel();
            return View(user);
        }

        [HttpPost]
        public IActionResult Login([Bind("Email,Password")] UserViewModel user)
        {
            var currentUser = userService.GetUser(user.Email);
            if (currentUser != null && currentUser.Password.Equals(user.Password))
            {
                var userClaims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name , currentUser.FirstName),
                    new Claim(ClaimTypes.Email, currentUser.Email),
                };

                var Identity = new ClaimsIdentity(userClaims, "User Identity");

                var userPrinciple = new ClaimsPrincipal(new[] { Identity });

                HttpContext.SignInAsync(userPrinciple);

                return RedirectToAction("Index", "Place");
            }
            else
            {
                user.ErrorMsg = "Invalid username or password. Try again..";
            }
            return View(user);
        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction("Index");
        }
    }
}
