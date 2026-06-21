using Assignment.Models.Requests;
using Assignment.Services;

using Microsoft.AspNetCore.Mvc;

namespace Assignment.Controllers
{
    public class BookController : Controller
    {
        private readonly ILibraryApiService _service;

        public BookController(
            ILibraryApiService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult Search()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Search(
            SearchBooksRequest request)
        {
            var result =
                _service.SearchBooks(request);

            return View("SearchResult", result);
        }
    }
}