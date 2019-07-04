using System;

namespace DapperDemo
{
    public class Todo
    {
        public int Id { get; set; }
        public DateTime? DueDate { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public bool IsCompleted { get; set; }
        public string UserId { get; set; }
    }
}
