using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DtoModels
{
    public class ToDoItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public bool Completed { get; set; }

        public ToDoItem(string name, int userId)
        {
            Name = name;
            UserId = userId;
            Completed = false;
        }

        public ToDoItem()
        {
        }
    }
}
