using System;
using System.Collections.Generic;
using System.Text;

namespace SEDC.AuthorsApp.Services
{
    public interface IAuthorService
    {
        List<AuthorDTO> GetAllAuthors(string query);
        AuthorDTO GetAuthorById(int id);
        int CreateAuthorAndNovel(AuthorDTO author, NovelDTO novel);
    }
}
