using SEDC.Entity.Class3.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace SEDC.Entity.Class3.DataAccess.Repositories
{
    public interface IRepository<T>
    {
        List<T> GetAll();
        T GetById(int id);
        void Insert(T entity);
        List<Authors> GetAllWithMembers();
    }
}
