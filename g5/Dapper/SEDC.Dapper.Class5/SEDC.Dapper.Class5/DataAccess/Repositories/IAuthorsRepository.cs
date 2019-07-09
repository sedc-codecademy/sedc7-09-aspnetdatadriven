using SEDC.Dapper.Class5.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace SEDC.Dapper.Class5.DataAccess
{
    public interface IAuthorsRepository
    {
        IList<Author> GetAll(string nameFragment);

        IList<Author> GetAllWithNovels();
    }
}
