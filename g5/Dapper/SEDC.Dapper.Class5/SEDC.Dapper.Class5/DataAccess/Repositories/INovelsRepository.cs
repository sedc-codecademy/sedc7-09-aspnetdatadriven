using SEDC.Dapper.Class5.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace SEDC.Dapper.Class5.DataAccess
{
    public interface INovelsRepository
    {
        IList<Novel> GetAll();
    }
}
