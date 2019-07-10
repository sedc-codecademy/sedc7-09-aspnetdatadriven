using System.Collections.Generic;
using System.Linq;
using DtoModels;
using Microsoft.EntityFrameworkCore;

namespace DataLayer
{
    public class UserRepository : IRepository<User>
    {
        private readonly ToDoContext _dbContext;

        public UserRepository(ToDoContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<User> GetAll()
        {
           return _dbContext.Users.Include(x => x.Items).ToList();
        }

        public User GetById(int id)
        {
            return _dbContext.Users.Include(x => x.Items).FirstOrDefault(x => x.Id == id);
        }

        public void Create(User obj)
        {
            _dbContext.Users.Add(obj);
            _dbContext.SaveChanges();
        }

        public void Delete(User obj)
        {
            _dbContext.Users.Remove(obj);
            _dbContext.SaveChanges();
        }

        public void Update(User obj)
        {
            _dbContext.Users.Update(obj);
            _dbContext.SaveChanges();
        }
    }
}
