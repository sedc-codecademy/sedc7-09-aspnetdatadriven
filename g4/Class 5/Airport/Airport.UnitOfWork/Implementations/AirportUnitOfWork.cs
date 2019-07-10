using Airport.Entities;
using Airport.Repositories.Contracts;
using Airport.Repositories.Implementations;
using Airport.UnitOfWork.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Airport.UnitOfWork.Implementations
{
    public class AirportUnitOfWork : IAirportUnitOfWork
    {
        private readonly AirportDbContext _dbContext;

        public AirportUnitOfWork(AirportDbContext dbContext)
        {
            _dbContext = dbContext;

            BusinessObjectRepository = new BaseRepository<BusinessObject>(_dbContext);
            EmployeeRepository = new BaseRepository<Employee>(_dbContext);
            OfferRepository = new BaseRepository<Offer>(_dbContext);
            BusinessObjectEmployeeRepository = new BaseRepository<BusinessObjectEmployee>(_dbContext);
        }

        public IBaseRepository<BusinessObject> BusinessObjectRepository { get; }

        public IBaseRepository<BusinessObjectEmployee> BusinessObjectEmployeeRepository { get; }

        public IBaseRepository<Employee> EmployeeRepository { get; }

        public IBaseRepository<Offer> OfferRepository { get; }

        public void CommitChanges()
        {
            _dbContext.SaveChanges();
        }

        public void RevertChanges()
        {
            if (_dbContext.ChangeTracker.HasChanges())
            {
                var changedEntries = _dbContext.ChangeTracker.Entries()
                                        .Where(e => e.State != EntityState.Unchanged);
                foreach (var entry in changedEntries)
                {
                    switch (entry.State)
                    {
                        case EntityState.Deleted:
                        case EntityState.Modified:
                            entry.Reload();
                            break;
                        case EntityState.Added:
                            entry.State = EntityState.Detached;
                            break;
                    }
                }
            }
        }
    }
}
