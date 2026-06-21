using Assignment.Models.Requests;
using Assignment.Services;

using Microsoft.AspNetCore.Mvc;

namespace Assignment.Controllers
{
    public class MemberController : Controller
    {
        private readonly ILibraryApiService _service;

        public MemberController(
            ILibraryApiService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(
            RegisterMemberRequest request)
        {
            if (!ModelState.IsValid)
                return View(request);

            var result =
                _service.RegisterMember(request);

            return View("MemberResult", result);
        }
    }
}