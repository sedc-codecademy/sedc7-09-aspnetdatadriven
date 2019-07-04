using SEDC.Entity.Class3.DataAccess.Repositories;
using SEDC.Entity.Class3.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace SEDC.Entity.Class3.DataAccess.UOW
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Authors> AuthorsRepository { get; }
        IRepository<Novels> NovelsRepository { get;  }
        int Commit();
        void Reject();
    }
}
