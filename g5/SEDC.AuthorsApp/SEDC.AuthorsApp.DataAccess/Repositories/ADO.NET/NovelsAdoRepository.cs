using SEDC.AuthorsApp.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace SEDC.AuthorsApp.DataAccess
{
    public class NovelsAdoRepository : IRepository<Novel>
    {
        private IUnitOfWork _uow;
        public NovelsAdoRepository(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public IList<Novel> GetAll(string nameFragment)
        {
            SqlCommand cmd = (SqlCommand)_uow.CreateCommand();
            cmd.CommandText = @"SELECT * FROM Novels as n 
                                JOIN Authors as a ON n.AuthorID = a.ID 
                                WHERE Title like '%'+ @novelName +'%'";
            cmd.Parameters.AddWithValue("@novelName", nameFragment);

            SqlDataReader dr = cmd.ExecuteReader();
            List<Novel> novels = new List<Novel>();
            while (dr.Read())
            {
                Novel novel = new Novel();
                Author author = new Author();
                author.ID = dr.GetFieldValue<int>(4);
                author.Name = (string)dr["Name"];
                author.DateOfBirth = (DateTime)dr["DateOfBirth"];
                author.DateOfDeath = dr.IsDBNull(7)
                    ? (DateTime?)null
                    : (DateTime)dr["DateOfDeath"];
                novel.ID = dr.GetFieldValue<int>(0);
                novel.Title = (string)dr["Title"];
                novel.IsRead = (bool)dr["IsRead"];
                novel.AuthorID = (int)dr["AuthorID"];
                novel.Author = author;
                novels.Add(novel);
            }

            return novels;
        }

        public Novel GetById(int id)
        {
            SqlCommand cmd = (SqlCommand)_uow.CreateCommand();
            cmd.CommandText = @"SELECT * FROM Novels WHERE id = @ID";
            cmd.Parameters.AddWithValue("@ID", id);

            SqlDataReader dr = cmd.ExecuteReader();
            Novel novel = new Novel();
            while (dr.Read())
            {
                novel.ID = dr.GetFieldValue<int>(0);
                novel.Title = (string)dr["Title"];
                novel.IsRead = (bool)dr["IsRead"];
                novel.AuthorID = (int)dr["AuthorID"];
            }

            return novel;
        }

        public int Insert(Novel entity)
        {
            SqlCommand cmd = (SqlCommand)_uow.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "createNovel";
            cmd.Parameters.AddWithValue("@Title", entity.Title);
            cmd.Parameters.AddWithValue("@AuthorID", entity.AuthorID);
            cmd.Parameters.AddWithValue("@IsRead", entity.IsRead);
            SqlParameter outputParam = cmd.Parameters.Add("@ID", SqlDbType.Int);
            outputParam.Direction = ParameterDirection.Output;

            cmd.ExecuteNonQuery();

            return (int)outputParam.Value;
        }
    }
}
