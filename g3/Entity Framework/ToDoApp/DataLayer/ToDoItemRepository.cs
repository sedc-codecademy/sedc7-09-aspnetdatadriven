using System.Collections.Generic;
using System.Linq;
using DtoModels;
using Microsoft.EntityFrameworkCore;

namespace DataLayer
{
    public class ToDoItemRepository : IRepository<ToDoItem>
    {
        private readonly ToDoContext _dbContext;

        public ToDoItemRepository(ToDoContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<ToDoItem> GetAll()
        {
            return _dbContext.ToDoItems.ToList();
        }

        public ToDoItem GetById(int id)
        {
            return _dbContext.ToDoItems.Include(x => x.User).FirstOrDefault(x => x.Id == id);
        }

        public void Create(ToDoItem obj)
        {
            _dbContext.ToDoItems.Add(obj);
            _dbContext.SaveChanges();
        }

        public void Delete(ToDoItem obj)
        {
            _dbContext.ToDoItems.Remove(obj);
            _dbContext.SaveChanges();
        }

        public void Update(ToDoItem obj)
        {
            _dbContext.ToDoItems.Update(obj);
            _dbContext.SaveChanges();
        }
    }
}
