using SEDC.Entity.Class3.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SEDC.Entity.Class3.DataAccess.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private DbContext _context; // Only to show includes
        private DbSet<T> _dbSet;
        public Repository(DbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }
        public List<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public List<Authors> GetAllWithMembers()
        {
            IQueryable<Authors> result = _context.Set<Authors>()
                .Include(x => x.Novels)
                    .ThenInclude(x => x.Nominations)
                        .ThenInclude(x => x.Award);
            return result.ToList();
        }

        public T GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public void Insert(T entity)
        {
            _dbSet.Add(entity);
        }
    }
}
