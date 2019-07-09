using Dapper;
using SEDC.Dapper.Class5.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace SEDC.Dapper.Class5.DataAccess
{
    public class NovelsRepository : BaseRepository, INovelsRepository
    {
        public NovelsRepository(IDbConnection connection)
            : base(connection)
        {

        }

        public IList<Novel> GetAll()
        {
            List<Novel> novels = new List<Novel>();
            using (var multi = Connection.QueryMultiple("SELECT * FROM Novels; SELECT * FROM Nominations"))
            {
                novels = multi.Read<Novel>().ToList();
                foreach (var novel in novels)
                {
                    novel.Nominations.Add(multi.Read<Nomination>().Where(x => x.BookID == novel.ID).Single());
                }
            }
            return novels;
        }
    }
}
