using SEDC.AuthorsApp.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace SEDC.AuthorsApp.DataAccess
{
    public class AuthorsAdoRepository : IRepository<Author>
    {
        private IUnitOfWork _uow;

        public AuthorsAdoRepository(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public IList<Author> GetAll(string nameFragment = "")
        {
            SqlCommand cmd = (SqlCommand)_uow.CreateCommand();
            cmd.CommandText = @"SELECT * FROM Authors 
                                WHERE Name like '%'+ @authorName +'%'";
            cmd.Parameters.AddWithValue("@authorName", nameFragment);

            SqlDataReader dr = cmd.ExecuteReader();
            List<Author> authors = new List<Author>();
            while (dr.Read())
            {
                Author author = new Author();

                author.ID = dr.GetFieldValue<int>(0);
                author.Name = (string)dr["Name"];
                author.DateOfBirth = (DateTime)dr["DateOfBirth"];
                author.DateOfDeath = dr.IsDBNull(3)
                    ? (DateTime?)null
                    : (DateTime)dr["DateOfDeath"];
                author.Novels = new List<Novel>();
                authors.Add(author);
            }

            return authors;
        }
        public Author GetById(int id)
        {
            SqlCommand cmd = (SqlCommand)_uow.CreateCommand();
            cmd.CommandText = @"SELECT * FROM Authors as a LEFT JOIN Novels as n ON a.ID = n.AuthorId WHERE a.ID = @ID";
            cmd.Parameters.AddWithValue("@ID", id);

            SqlDataReader dr = cmd.ExecuteReader();
            Novel novel = new Novel();
            Author author = new Author();
            while (dr.Read())
            {
                author.ID = dr.GetFieldValue<int>(0);
                author.Name = (string)dr["Name"];
                author.DateOfBirth = (DateTime)dr["DateOfBirth"];
                author.DateOfDeath = dr.IsDBNull(3)
                    ? (DateTime?)null
                    : (DateTime)dr["DateOfDeath"];
                author.Novels = new List<Novel>();
                if (!dr.IsDBNull(4))
                {
                    novel.ID = dr.GetFieldValue<int>(4);
                    novel.Title = (string)dr["Title"];
                    novel.IsRead = (bool)dr["IsRead"];
                    novel.AuthorID = (int)dr["AuthorID"];

                    author.Novels.Add(novel);
                }
            }
            return author;
        }

        public int Insert(Author entity)
        {
            SqlCommand cmd = (SqlCommand)_uow.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "createAuthor";
            cmd.Parameters.AddWithValue("@AuthorName", entity.Name);
            cmd.Parameters.AddWithValue("@DateOfBirth", entity.DateOfBirth);
            cmd.Parameters.AddWithValue("@DateOfDeath", entity.DateOfDeath);
            SqlParameter outputParam = cmd.Parameters.Add("@ID", SqlDbType.Int);
            outputParam.Direction = ParameterDirection.Output;

            cmd.ExecuteNonQuery();

            return (int)outputParam.Value;
        }
    }
}
