using System.ComponentModel.DataAnnotations;

namespace ToDoList.Models
{
    public class ToDoTask
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        public bool IsCompleted { get; set; }

        // Foreign key за потребител
        public int UserId { get; set; }

        // Правим навигационното свойство nullable
        public User? User { get; set; }  // Навигационно свойство за потребителя (nuAllable)
    }
}