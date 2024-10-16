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
        public IActionResult Delete(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null)
            {
                return NotFound(); // Или друго обработване, ако потребителят не съществува
            }
            _context.Users.Remove(user);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
