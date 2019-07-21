using System;
using System.Collections.Generic;

namespace Airport.Repositories.Contracts
{
    public interface IBaseRepository<T> : IDisposable
    {
        IEnumerable<T> GetAll();

        T GetById(int id);

        void Add(T entity);

        void Update(T entity);

        void Remove(int id);
    }
}
