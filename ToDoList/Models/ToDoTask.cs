using System.ComponentModel.DataAnnotations;

namespace ToDoList.Models;

public class ToDoTask
{
    [Key]
        public int Id { get; set; } // Уверете се, че 'set' е наличен
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
        public bool IsCompleted { get; set; }
}
