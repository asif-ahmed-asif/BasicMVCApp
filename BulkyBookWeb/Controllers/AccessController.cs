using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using BulkyBookWeb.Models;
using BulkyBookWeb.Data;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;

namespace BulkyBookWeb.Controllers
{
    public class AccessController : Controller
    {
        private readonly ApplicationDbContext _db;

        public AccessController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Registration()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        [HttpPost]
        public IActionResult Registration(User user)
        {
            var email = _db.Users.Where(e => e.Email == user.Email);
            if (email.Count() > 0)
            {
                ModelState.AddModelError("Email", "This Email is already exists!");
            }

            if (ModelState.IsValid)
            {
                _db.Users.Add(user);
                _db.SaveChanges();
                TempData["success"] = "User successfully Registered";
                this.cookieSetUp(user);
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        [HttpPost]
        public IActionResult Login(Login login)
        {
            var valid = _db.Users.Where(v => v.Email == login.LEmail && v.Password == login.LPassword);
            if (valid.Count() > 0)
            {
                var user = new User()
                {
                    Email = login.LEmail,
                    Password = login.LPassword,
                };
                this.cookieSetUp(user);
                return RedirectToAction("Index", "Home");
            }
            TempData["LoginError"] = "Wrong Email or Password!";
            return View();
        }
        [Authorize]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }

        private async void cookieSetUp(User user)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Email)
            };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(principal));
        }
    }
}
