using TodoApi.Models;

namespace TodoApi.DTOs
{
    public class TodoItemDTO
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public bool IsComplete { get; set; }

        public static TodoItemDTO ItemToDTO(TodoItem todoItem) => new()
           {
               Id = todoItem.Id,
               Name = todoItem.Name,
               IsComplete = todoItem.IsComplete
           };
    }
}
