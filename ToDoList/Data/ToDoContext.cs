using Microsoft.EntityFrameworkCore;
using ToDoList.Models;


public class ToDoContext : DbContext
{
    public ToDoContext(DbContextOptions<ToDoContext> options) : base(options) { }

    public DbSet<ToDoTask> Tasks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ToDoTask>().HasKey(t => t.Id); // Определя основния ключ

        // Seed data
        modelBuilder.Entity<ToDoTask>().HasData(
            new ToDoTask { Id = 1, Title = "Buy groceries", Description = "Milk, Eggs, Bread", IsCompleted = false },
            new ToDoTask { Id = 2, Title = "Study for exams", Description = "Revise chapters 1 to 5", IsCompleted = false },
            new ToDoTask { Id = 3, Title = "Go to the gym", Description = "Leg day workout", IsCompleted = true }
        );

    }
}