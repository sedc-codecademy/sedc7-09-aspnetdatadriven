using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace SEDC.Dapper.Class5.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private IDbConnection _connection;
        private IAuthorsRepository _authorsRepository;
        private INovelsRepository _novelsRepository;

        public UnitOfWork(string connectionString)
        {
            _connection = new SqlConnection(connectionString);
            _connection.Open();
        }

        ~UnitOfWork()
        {
            dispose();
        }

        public IAuthorsRepository AuthorsRepository
        {
            get
            {
                return _authorsRepository ?? (_authorsRepository = new AuthorsRepository(_connection));
            }
        }

        public INovelsRepository NovelsRepository
        {
            get
            {
                return _novelsRepository ?? (_novelsRepository = new NovelsRepository(_connection));
            }
        }

        public void Dispose()
        {
            dispose();
            GC.SuppressFinalize(this);
        }

        private void dispose()
        {
            _connection.Close();
            _connection.Dispose();
            _connection = null;
        }
    }
}
