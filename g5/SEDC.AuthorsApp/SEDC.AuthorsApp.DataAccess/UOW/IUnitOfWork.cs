using SEDC.AuthorsApp.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace SEDC.AuthorsApp.DataAccess
{
    public interface IUnitOfWork
    {
        IRepository<Author> Authors { get; }
        IRepository<Novel> Novels { get; }
        IDbCommand CreateCommand();
        void SaveChanges();
        void Reject();
    }
}
