using Microsoft.AspNetCore.Mvc;
using ToDoList.Models;
using System.Linq;

namespace ToDoList.Controllers
{
    public class UserController : Controller
    {
        private readonly ToDoContext _context;

        public UserController(ToDoContext context)
        {
            _context = context;
        }

        // Показване на всички потребители
        public IActionResult Index()
        {
            var users = _context.Users.ToList();
            return View(users);
        }
        public IActionResult Profile()
        {
            // Вземи ID на текущо логнатия потребител от claims
            var userId = User.FindFirst("UserId")?.Value;

            if (userId == null)
            {
                // Ако потребителят не е логнат, пренасочваме го към страницата за логване
                return RedirectToAction("Login", "Account");
            }

            // Търсим потребителя в базата данни по неговото ID
            var user = _context.Users.Find(int.Parse(userId));
            if (user == null)
            {
                return NotFound(); // Ако потребителят не е намерен, връщаме грешка 404
            }

            return View(user); // Предаваме потребителския модел на изгледа
        }

        // Създаване на нов потребител
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                _context.Users.Add(user);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // Редакция на потребител
        public IActionResult Edit(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Users.Update(user);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // Изтриване на потребител
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteProfile(int id)
        {
            // Вземи ID на текущо логнатия потребител
            var userId = User.FindFirst("UserId")?.Value;

            if (userId == null || int.Parse(userId) != id)
            {
                return Unauthorized(); // Ако няма логнат потребител или ID-то не съвпада, връщаме Unauthorized
            }

            var user = _context.Users.Find(id);
            if (user == null)
            {
                return NotFound(); // Ако потребителят не е намерен
            }

            _context.Users.Remove(user);
            _context.SaveChanges();
            return RedirectToAction("Logout", "Account");
        }
    }
}
