using System;
using System.Collections.Generic;
using System.Text;

namespace SEDC.AuthorsApp.Services
{
    public interface INovelService
    {
        List<NovelDTO> GetAllNovels(string query);
        NovelDTO GetNovelById(int id);
    }
}
