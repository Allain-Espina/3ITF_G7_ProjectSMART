using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ProjectSmart.Data;
using ProjectSmart.Models;
using ProjectSmart.ViewModels;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace ProjectSmart.Controllers
{
    public class RequirementsController : Controller
    {
        private readonly AppDbContext _dbData;
        private readonly IWebHostEnvironment _environment;

        public RequirementsController(AppDbContext dbData, IWebHostEnvironment environment)
        {
            _dbData = dbData;
            _environment = environment;
        }

        [Authorize(Roles = "Scholar")]
        public IActionResult CG()
        {

            return View("CG", _dbData.Certified_Grades.Where(tor => tor.ScholarEmailAddress == User.Identity.Name));
        }

        [HttpGet]
        public IActionResult UploadCG()
        {
            int firstYear = DateTime.Now.Year;
            int secondYear = DateTime.Now.Year + 1;
            string year = "A.Y. " + firstYear.ToString() + " - " + secondYear.ToString();

            string uploadDate = DateTime.Now.Date.ToString();

            ViewBag.UploadDate = uploadDate;

            ViewBag.Year = year;
            return View();
        }

        [HttpPost]
        public IActionResult UploadCG(Certified_Grades newCGFile)
        {
            if (!ModelState.IsValid)
                return View();

                string folder = "ScholarshipRequirements/TranscriptOfRecords/";
                string serverFolder = Path.Combine(_environment.WebRootPath, folder);
                string uniqueFilename = Guid.NewGuid().ToString() + "_" + newCGFile.CG_File.FileName;
                string filePath = Path.Combine(serverFolder, uniqueFilename);


                //Save the Photo within the specified File Path
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    newCGFile.CG_File.CopyTo(fileStream);
                }

                newCGFile.CG_FilePath = folder + uniqueFilename;

                _dbData.Certified_Grades.Add(newCGFile);
                _dbData.SaveChanges();

                return View("CG", _dbData.Certified_Grades);

        }

    }

}