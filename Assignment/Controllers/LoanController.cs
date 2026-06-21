using Assignment.Models.Requests;
using Assignment.Services;
using Microsoft.AspNetCore.Mvc;

namespace Assignment.Controllers
{
    public class LoanController : Controller
    {
        private readonly ILibraryApiService _service;

        public LoanController(
            ILibraryApiService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult Checkout()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Checkout(
            CheckoutBookRequest request)
        {
            var result =
                _service.CheckoutBook(request);

            return View("LoanResult", result);
        }

        [HttpGet]
        public IActionResult Return()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Return(
            ReturnBookRequest request)
        {
            var result =
                _service.ReturnBook(request);

            return View("ReturnResult", result);
        }
    }
}