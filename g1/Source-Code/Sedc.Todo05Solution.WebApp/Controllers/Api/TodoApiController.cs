using Microsoft.AspNetCore.Mvc;
using Sedc.Todo03Solution.Entities;
using Sedc.Todo03Solution.Repositories.Interfaces;
using System.Linq;

namespace Sedc.Todo05Solution.WebApp.Controllers.Api
{
    [Route("/api")]
    public class TodoApiController: Controller
    {
        private readonly IGenericRepository<Todo> todosRepo;

        public TodoApiController(IGenericRepository<Todo> todosRepo)
        {
            this.todosRepo = todosRepo;
        }

        [HttpGet("todos")]
        public IActionResult GetTodos()
        {
            var results = todosRepo.GetAll().ToList();
            return new JsonResult(results);
        }
    }
}
