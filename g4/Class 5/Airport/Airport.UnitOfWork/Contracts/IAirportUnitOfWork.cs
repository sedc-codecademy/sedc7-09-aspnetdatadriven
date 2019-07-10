using Airport.Entities;
using Airport.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Airport.UnitOfWork.Contracts
{
    public interface IAirportUnitOfWork
    {
        IBaseRepository<BusinessObject> BusinessObjectRepository { get; }
        IBaseRepository<BusinessObjectEmployee> BusinessObjectEmployeeRepository { get; }
        IBaseRepository<Employee> EmployeeRepository { get; }
        IBaseRepository<Offer> OfferRepository { get; }

        void CommitChanges();
        void RevertChanges();
    }
}
