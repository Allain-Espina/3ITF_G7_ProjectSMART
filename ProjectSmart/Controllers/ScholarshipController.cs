using Microsoft.AspNetCore.Mvc;

namespace ProjectSmart.Controllers
{
    public class ScholarshipController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
