using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SEDC.AuthorsApp.Services;
using SEDC.AuthorsApp.WebUI.Models;

namespace SEDC.AuthorsApp.WebUI.Controllers
{
    public class NovelController : Controller
    {
        private readonly INovelService _novelService;
        private readonly IMapper _mapper;
        public NovelController(INovelService novelService, IMapper mapper)
        {
            _novelService = novelService;
            _mapper = mapper;
        }
       
        public IActionResult All(string query)
        {
            List<NovelViewModel> model = _mapper.Map<List<NovelViewModel>>(_novelService.GetAllNovels(query));
            return View(model);
        }
        public IActionResult Details(int id)
        {
            NovelViewModel model = _mapper.Map<NovelViewModel>(_novelService.GetNovelById(id));
            return View(model);
        }
    }
}