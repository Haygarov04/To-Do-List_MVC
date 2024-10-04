using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoList.Models;

namespace ToDoList.Controllers
{
    namespace ToDoList.Controllers
    {
        [Route("Тasks")] // Основен маршрут за контролера
        public class TaskController : Controller
        {

            private readonly ToDoContext _context;

            public TaskController(ToDoContext context)
            {
                _context = context;
            }

            // Добави методи тук
            [HttpGet] // За GET заявки
            [Route("")] // Празен маршрут за показване на списък

            public async Task<IActionResult> Index()
            {
                var tasks = await _context.Tasks.ToListAsync();
                return View(tasks);
            }

            [Route("ChangeStatus/{id}")]
            [HttpPost]
            public IActionResult ToggleStatus(int id)
            {
                var task = _context.Tasks.FirstOrDefault(t => t.Id == id);
                if (task != null)
                {
                    task.IsCompleted = !task.IsCompleted; // Променя статуса
                    _context.SaveChanges(); // Записва промените в базата данни
                }

                return RedirectToAction("Index"); // Пренасочва обратно към изгледа с задачите
            }

            [Route("Task/Create")]
            public IActionResult Create()
            {
                return View();
            }

            [HttpPost]
            [Route("Task/Create")]
            [ValidateAntiForgeryToken]
            public IActionResult Create(ToDoTask task)
            {
                if (ModelState.IsValid)
                {
                    _context.Tasks.Add(task);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View();
            }

            [HttpGet]
            [Route("Delete/{id?}")]
            public IActionResult Delete(int? id)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var task = _context.Tasks.FirstOrDefault(t => t.Id == id);
                if (task == null)
                {
                    return NotFound();
                }

                return View(task); // Това ще покаже страницата за потвърждение на изтриване
            }


            [HttpPost, ActionName("DeleteConfirm")]
            [ValidateAntiForgeryToken]
            public IActionResult DeleteConfirm(int id)
            {
                var task = _context.Tasks.Find(id);
                if (task == null)
                {
                    return NotFound();
                }

                _context.Tasks.Remove(task);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }





            [HttpGet]
            [Route("Edit/{id}")]
            public async Task<IActionResult> Edit(int id)
            {
                var task = await _context.Tasks.FindAsync(id);
                if (task == null)
                {
                    return NotFound();
                }
                return View(task);
            }

            // POST: tasks/Edit/5
            [HttpPost]
            [Route("Edit/{id}")]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description")] ToDoTask task)
            {
                if (id != task.Id)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(task);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!TaskExists(task.Id))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                }
                return View(task);
            }

            private bool TaskExists(int id)
            {
                return _context.Tasks.Any(e => e.Id == id);
            }




    }
    }
}
