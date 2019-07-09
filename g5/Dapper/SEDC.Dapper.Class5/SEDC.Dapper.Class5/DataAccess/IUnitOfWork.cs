using System;
using System.Collections.Generic;
using System.Text;

namespace SEDC.Dapper.Class5.DataAccess
{
    public interface IUnitOfWork : IDisposable
    {
        IAuthorsRepository AuthorsRepository { get; }
        INovelsRepository NovelsRepository { get; }
    }
}
