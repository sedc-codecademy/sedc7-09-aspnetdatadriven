using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace SEDC.Dapper.Class5.DataAccess
{
    public class BaseRepository
    {
        public BaseRepository(IDbConnection connection)
        {
            Connection = connection;
        }

        protected IDbConnection Connection
        {
            get;
            private set;
        }
    }
}
