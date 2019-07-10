using SEDC.AuthorsApp.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using SEDC.AuthorsApp.Domain;
using System.Linq;

namespace SEDC.AuthorsApp.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        public AuthorService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public int CreateAuthorAndNovel(AuthorDTO author, NovelDTO novel)
        {
            Novel novelDomain = _mapper.Map<NovelDTO, Novel>(novel);
            Author authorDomain = _mapper.Map<AuthorDTO, Author>(author);
            int id = _uow.Authors.Insert(authorDomain);
            novelDomain.AuthorID = id;
            _uow.Novels.Insert(novelDomain);
            _uow.SaveChanges();

            return id;
        }

        public List<AuthorDTO> GetAllAuthors(string query)
        {
            return _mapper.Map<List<Author>, List<AuthorDTO>>(_uow.Authors.GetAll(query).ToList());
        }
        public AuthorDTO GetAuthorById(int id)
        {
            return _mapper.Map<Author, AuthorDTO>(_uow.Authors.GetById(id));
        }
    }
}
