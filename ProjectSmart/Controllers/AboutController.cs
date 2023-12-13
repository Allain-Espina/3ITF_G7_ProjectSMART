using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ProjectSmart.Controllers
{
    public class AboutController : Controller
    {
        //[Authorize(Roles = "Scholar")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
