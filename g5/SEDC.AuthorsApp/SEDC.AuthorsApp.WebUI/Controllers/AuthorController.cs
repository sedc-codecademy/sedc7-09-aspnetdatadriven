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
    public class AuthorController : Controller
    {
        private readonly IAuthorService _authorService;
        private readonly IMapper _mapper;
        public AuthorController(IAuthorService authorService, IMapper mapper)
        {
            _authorService = authorService;
            _mapper = mapper;
        }
        public IActionResult All(string query = "")
        {
            List<AuthorViewModel> model = _mapper.Map<List<AuthorViewModel>>(_authorService.GetAllAuthors(query));
            return View(model);
        }
        public IActionResult Details(int id)
        {
            AuthorViewModel model = _mapper.Map<AuthorViewModel>(_authorService.GetAuthorById(id));
            return View(model);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View(new CreateAuthorViewModel());
        }
        [HttpPost]
        public IActionResult Create(CreateAuthorViewModel model)
        {
            AuthorDTO author = _mapper.Map<AuthorDTO>(model.Author);
            NovelDTO novel = _mapper.Map<NovelDTO>(model.Novel);
            _authorService.CreateAuthorAndNovel(author, novel);
            return View();
        }
    }
}