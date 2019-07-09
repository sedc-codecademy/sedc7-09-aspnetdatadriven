using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SEDC.AuthorsApp.Services;
using SEDC.AuthorsApp.WebUI.Models;

namespace SEDC.AuthorsApp.WebUI.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View(new NameViewModel());
        }
        [HttpPost]
        public IActionResult Index(NameViewModel model)
        {
            if(model.Option == "Author")
            {
                return RedirectToAction("All", "Author", new { query = model.NameFragment });
            } else if(model.Option == "Novel")
            {
                return RedirectToAction("All", "Novel", new { query = model.NameFragment });
            }
            return RedirectToAction("Index");
            
        }
        public IActionResult About()
        {
            return View();
        }
        public IActionResult Contact()
        {
            return View();
        }
    }
}
