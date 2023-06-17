using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using BulkyBookWeb.Models;
using BulkyBookWeb.Data;
using Microsoft.IdentityModel.Tokens;

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
            return View();
        }
        [HttpPost]
        public IActionResult Registration(User user)
        {
            if(user.Email != null)
            {
                var email = _db.Users.Where(e => e.Email == user.Email);
                if(email.Count() > 0)
                {
                    ModelState.AddModelError("Email", "This Email is already exists!");
                }
            }
            if (ModelState.IsValid)
            {
                _db.Users.Add(user);
                _db.SaveChanges();
                TempData["success"] = "User successfully Registered";
                return RedirectToAction("Registration");
            }
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(User user)
        {
            if (ModelState.IsValid)
            {
                
            }
            return View();
        }
    }
}
