using Microsoft.EntityFrameworkCore;

namespace ToDoList.Models
{
    public class ToDoContext : DbContext
    {
        public ToDoContext(DbContextOptions<ToDoContext> options) : base(options) { }

        public DbSet<ToDoTask> Tasks { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Конфигуриране на релацията между задачи и потребители
            modelBuilder.Entity<ToDoTask>()
                .HasOne(t => t.User)
                .WithMany(u => u.Tasks)
                .HasForeignKey(t => t.UserId);

            // Seed данни за потребителите
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Username = "admin",
                    Email = "admin@example.com",
                    Password = "adminpassword",
                    Role = "Admin"
                },
                new User
                {
                    Id = 2,
                    Username = "user1",
                    Email = "user1@example.com",
                    Password = "password1",
                    Role = "Normal"
                }
            );

            // Seed данни за задачите
            modelBuilder.Entity<ToDoTask>().HasData(
                new ToDoTask
                {
                    Id = 1,
                    Title = "Buy groceries",
                    Description = "Milk, Eggs, Bread",
                    IsCompleted = false,
                    UserId = 1
                },
                new ToDoTask
                {
                    Id = 2,
                    Title = "Study for exams",
                    Description = "Revise chapters 1 to 5",
                    IsCompleted = false,
                    UserId = 2
                },
                new ToDoTask
                {
                    Id = 3,
                    Title = "Go to the gym",
                    Description = "Leg day workout",
                    IsCompleted = true,
                    UserId = 2
                }
            );
        }
    }
}