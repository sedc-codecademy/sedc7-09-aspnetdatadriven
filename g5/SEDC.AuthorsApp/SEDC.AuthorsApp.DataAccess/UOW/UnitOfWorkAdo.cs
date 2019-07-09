using SEDC.AuthorsApp.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace SEDC.AuthorsApp.DataAccess
{
    public class UnitOfWorkAdo : IUnitOfWork, IDisposable
    {
        private IDbConnection _connection;
        private IDbTransaction _transaction;
        public IRepository<Author> Authors { get; private set; }
        public IRepository<Novel> Novels { get; private set; }

        public UnitOfWorkAdo(IDbConnection connection)
        {
            _connection = connection;
            _connection.Open();
            _transaction = connection.BeginTransaction();
            Authors = new AuthorsAdoRepository(this);
            Novels = new NovelsAdoRepository(this);
        }

        public IDbCommand CreateCommand()
        {
            var command = _connection.CreateCommand();
            command.Transaction = _transaction;
            return command;
        }

        public void SaveChanges()
        {
            if (_transaction == null)
                throw new InvalidOperationException
                 ("Transaction have already been committed. Check your transaction handling.");

            _transaction.Commit();
            _transaction = null;
        }
        public void Reject()
        {
            _transaction.Rollback();
        }
        public void Dispose()
        {
            if (_transaction != null)
            {
                _transaction = null;
            }

            if (_connection != null)
            {
                _connection.Close();
                _connection = null;
            }
        }
    }
}
