using Microsoft.AspNetCore.Mvc;
using ToDoList.Models;
using System.Linq;

namespace ToDoList.Controllers
{
    public class TaskController : Controller
    {
        private readonly ToDoContext _context;

        public TaskController(ToDoContext context)
        {
            _context = context;
        }

        // Показване на задачите на конкретен потребител
        public IActionResult UserTasks(int userId)
        {
            var tasks = _context.Tasks.Where(t => t.UserId == userId).ToList();
            return View(tasks);
        }

        // Създаване на нова задача

        
        public IActionResult Create(int userId)
        {
            // Създаваме нова задача и предаваме userId към изгледа
            var task = new ToDoTask { UserId = userId };
            return View(task); // Предаваме модела на изгледа
        }

        [HttpPost]

        [ValidateAntiForgeryToken]
        public IActionResult Create(ToDoTask task)
        {
            if (ModelState.IsValid)
            {
                _context.Tasks.Add(task);
                _context.SaveChanges();
                return RedirectToAction("UserTasks", new { userId = task.UserId });
            }
            return View(task);
        }

        // Редакция на задача
        public IActionResult Edit(int id)
        {
            var task = _context.Tasks.Find(id);
            if (task == null)
            {
                return NotFound();
            }
            return View(task);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, ToDoTask task)
        {
            if (id != task.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Tasks.Update(task);
                _context.SaveChanges();
                return RedirectToAction("UserTasks", new { userId = task.UserId });
            }
            return View(task);
        }

        // Изтриване на задача
        public IActionResult Delete(int id)
        {
            var task = _context.Tasks.Find(id);
            if (task == null)
            {
                return NotFound();
            }
            return View(task);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var task = _context.Tasks.Find(id);
            var userId = task.UserId;
            _context.Tasks.Remove(task);
            _context.SaveChanges();
            return RedirectToAction("UserTasks", new { userId = userId });
        }

        [HttpPost]
        public IActionResult ToggleStatus(int id)
        {
            // Намираме задачата по Id
            var task = _context.Tasks.Find(id);

            if (task == null)
            {
                return NotFound();
            }

            // Проверяваме дали задачата принадлежи на текущия потребител
            var currentUserId = User.FindFirst("UserId").Value;
            if (task.UserId.ToString() != currentUserId)
            {
                return Unauthorized(); // Ако задачата не принадлежи на потребителя
            }

            // Променяме статуса на задачата
            task.IsCompleted = !task.IsCompleted;
            _context.SaveChanges();

            // Пренасочваме обратно към списъка със задачи на потребителя
            return RedirectToAction("UserTasks", new { userId = task.UserId });
        }
    }
}