using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ToDoList.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Username { get; set; } = string.Empty;

        [Required]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;

        public string Role { get; set; } = "Normal";

        // Навигационно свойство за задачите
        public ICollection<ToDoTask> Tasks { get; set; } = new List<ToDoTask>();
    }
}