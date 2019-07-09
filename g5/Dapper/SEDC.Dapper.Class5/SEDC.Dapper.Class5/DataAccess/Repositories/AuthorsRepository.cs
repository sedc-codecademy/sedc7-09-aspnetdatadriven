using Dapper;
using SEDC.Dapper.Class5.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace SEDC.Dapper.Class5.DataAccess
{
    public class AuthorsRepository : BaseRepository, IAuthorsRepository
    {
        private static readonly string SpGetAllAuthors = "dbo.getAuthors";

        public AuthorsRepository(IDbConnection connection)
            : base(connection)
        {

        }

        public IList<Author> GetAll(string nameFragment)
        {
            return Connection.Query<Author>(SpGetAllAuthors,
                new { authorName = nameFragment }, commandType: CommandType.StoredProcedure).ToList();
        }

        public IList<Author> GetAllWithNovels()
        {
            var authors = Connection.Query<Author>(SpGetAllAuthors,
                new { authorName = string.Empty }, commandType: CommandType.StoredProcedure).ToList();
            var novels = Connection.Query<Novel>("SELECT * FROM Novels").ToList();

            foreach (var author in authors)
            {
                author.Novels = novels.Where(novel => novel.AuthorId == author.ID).ToList();
            }

            return authors;
        }
    }
}
