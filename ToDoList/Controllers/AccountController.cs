using Microsoft.AspNetCore.Mvc;
using ToDoList.Models;
using System.Linq;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using System.Collections.Generic;

namespace ToDoList.Controllers
{
    public class AccountController : Controller
    {
        private readonly ToDoContext _context;

        public AccountController(ToDoContext context)
        {
            _context = context;
        }

        // Логване
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            // Търсим потребителя в базата данни
            var user = _context.Users.FirstOrDefault(u => u.Username == username && u.Password == password);
            if (user != null)
            {
                // Създаваме claims за потребителя
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim("UserId", user.Id.ToString())
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true // Задаваме да запазва сесията
                };

                // Логваме потребителя с cookies
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
                return RedirectToAction("UserTasks", "Task", new { userId = user.Id });
            }

            ViewBag.Error = "Invalid credentials";
            return View();
        }

        // Регистрация
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                // Проверка дали съществува потребител със същото потребителско име или имейл
                if (_context.Users.Any(u => u.Username == user.Username))
                {
                    ModelState.AddModelError("", "Username already exists.");
                    return View(user);
                }

                if (_context.Users.Any(u => u.Email == user.Email))
                {
                    ModelState.AddModelError("", "Email already exists.");
                    return View(user);
                }

                // Добавяме потребителя в базата данни
                _context.Users.Add(user);
                _context.SaveChanges();

                // Пренасочваме към логин страницата
                return RedirectToAction("Login");
            }
            return View(user);
        }

        // Изход
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
    }
}