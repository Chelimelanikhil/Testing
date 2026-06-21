using Assignment.Helpers;
using Assignment.Repositories;
using Assignment.Services;
using Microsoft.AspNetCore.Mvc;

namespace Assignment.Controllers
{
    public class DemoController : Controller
    {
        private readonly ILibraryApiService _service;
        private readonly ILibraryRepository _repository;

        public DemoController(
            ILibraryApiService service,
            ILibraryRepository repository)
        {
            _service = service;
            _repository = repository;
        }

        public IActionResult Run()
        {
            DemoRunner.RunApiDemo(
                _service,
                _repository);

            return Content("Demo Executed");
        }
    }
}
