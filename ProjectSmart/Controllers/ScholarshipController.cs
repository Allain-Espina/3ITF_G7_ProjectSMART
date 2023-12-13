using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ProjectSmart.Controllers
{
    public class ScholarshipController : Controller
    {
<<<<<<< HEAD
        //[Authorize(Roles = "Scholar")]
=======
>>>>>>> 87262e40ac3647f5dc596a25960f7aaafbe5f2de
        public IActionResult Index()
        {
            return View();
        }
    }
}
