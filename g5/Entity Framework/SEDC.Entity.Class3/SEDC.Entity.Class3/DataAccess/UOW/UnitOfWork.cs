using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SEDC.Entity.Class3.DataAccess.Repositories;
using SEDC.Entity.Class3.Domain;
using Microsoft.EntityFrameworkCore;

namespace SEDC.Entity.Class3.DataAccess.UOW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _dbContext;
        public IRepository<Authors> AuthorsRepository { get; private set; }
        public IRepository<Novels> NovelsRepository { get; private set; }

        public UnitOfWork()
        {
            _dbContext = new BooksDB2019Context("Server=.;Database=BooksDB2019;Trusted_Connection=True;");
            AuthorsRepository = new Repository<Authors>(_dbContext);
            NovelsRepository = new Repository<Novels>(_dbContext);
        }
        public int Commit()
        {
            return _dbContext.SaveChanges();
        }

        public void Reject()
        {
            foreach (var entry in _dbContext.ChangeTracker.Entries()
                .Where(e => e.State != EntityState.Unchanged))
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;
                    case EntityState.Modified:
                    case EntityState.Deleted:
                        entry.Reload();
                        break;
                }
            }
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
