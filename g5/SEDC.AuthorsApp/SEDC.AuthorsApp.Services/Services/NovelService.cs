using AutoMapper;
using SEDC.AuthorsApp.DataAccess;
using SEDC.AuthorsApp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SEDC.AuthorsApp.Services
{
    public class NovelService : INovelService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        public NovelService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public List<NovelDTO> GetAllNovels(string query)
        {
            return _mapper.Map<List<Novel>, List<NovelDTO>>(_uow.Novels.GetAll(query).ToList());
        }

        public NovelDTO GetNovelById(int id)
        {
            return _mapper.Map<Novel, NovelDTO>(_uow.Novels.GetById(id));
        }
    }
}
