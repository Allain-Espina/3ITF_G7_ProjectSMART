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

            return View("CG", _dbData.Certified_Grades.Where(cg => cg.ScholarEmailAddress == User.Identity.Name));
        }

        public IActionResult ViewCG(int id)
        {

            Certified_Grades? cgFile = _dbData.Certified_Grades.FirstOrDefault(cg => cg.CG_ID == id);

            if (cgFile != null)
            {

                return View(cgFile);

            }
            return NotFound();

        }

        [HttpGet]
        public IActionResult UploadCG()
        {
            return View();
        }

        [HttpPost]
        public IActionResult UploadCG(Certified_Grades newCGFile)
        {
            if (!ModelState.IsValid)
                return View();

            string folder = "ScholarshipRequirements/CertifiedTrueCopyOfGrades/";
            string serverFolder = Path.Combine(_environment.WebRootPath, folder);
            string uniqueFilename = User.Identity.Name + "_" + newCGFile.CG_File.FileName;
            string filePath = Path.Combine(serverFolder, uniqueFilename);

            //Save the Photo within the specified File Path
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                newCGFile.CG_File.CopyTo(fileStream);
                fileStream.Close();
                fileStream.Dispose();
            }

            int firstYear = DateTime.Now.Year;
            int secondYear = DateTime.Now.Year + 1;
            string year = "A.Y. " + firstYear.ToString() + " - " + secondYear.ToString();

            newCGFile.CG_AcademicYear = year;
            newCGFile.CG_FilePath = folder + uniqueFilename;

            _dbData.Certified_Grades.Add(newCGFile);
            _dbData.SaveChanges();

            return RedirectToAction("CG", _dbData.Certified_Grades);

        }

        [Authorize(Roles = "Scholar")]
        public IActionResult RF()
        {

            return View("RF", _dbData.Registration_Form.Where(rf => rf.ScholarEmailAddress == User.Identity.Name));
        }

        public IActionResult ViewRF(int id)
        {

            Registration_Form? rfFile = _dbData.Registration_Form.FirstOrDefault(rf => rf.RF_ID == id);

            if (rfFile != null)
            {

                return View(rfFile);

            }
            return NotFound();

        }

        [HttpGet]
        public IActionResult UploadRF()
        {
            return View();
        }

        [HttpPost]
        public IActionResult UploadRF(Registration_Form newRFFile)
        {
            if (!ModelState.IsValid)
                return View();

            string folder = "ScholarshipRequirements/RegistrationForm/";
            string serverFolder = Path.Combine(_environment.WebRootPath, folder);
            string uniqueFilename = User.Identity.Name + "_" + newRFFile.RF_File.FileName;
            string filePath = Path.Combine(serverFolder, uniqueFilename);

            //Save the Photo within the specified File Path
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                newRFFile.RF_File.CopyTo(fileStream);
                fileStream.Close();
                fileStream.Dispose();
            }

            int firstYear = DateTime.Now.Year;
            int secondYear = DateTime.Now.Year + 1;
            string year = "A.Y. " + firstYear.ToString() + " - " + secondYear.ToString();

            newRFFile.RF_AcademicYear = year;
            newRFFile.RF_FilePath = folder + uniqueFilename;

            _dbData.Registration_Form.Add(newRFFile);
            _dbData.SaveChanges();

            return RedirectToAction("RF", _dbData.Registration_Form);

        }

        [Authorize(Roles = "Scholar")]
        public IActionResult TR()
        {

            return View("TR", _dbData.Terminal_Report.Where(rf => rf.ScholarEmailAddress == User.Identity.Name));
        }

        public IActionResult ViewTR(int id)
        {

            Terminal_Report? trFile = _dbData.Terminal_Report.FirstOrDefault(tr => tr.TR_ID == id);

            if (trFile != null)
            {

                return View(trFile);

            }
            return NotFound();

        }

        [HttpGet]
        public IActionResult UploadTR()
        {
            return View();
        }

        [HttpPost]
        public IActionResult UploadTR(Terminal_Report newTRFile)
        {
            if (!ModelState.IsValid)
                return View();

            string folder = "ScholarshipRequirements/TerminalReportForm/";
            string serverFolder = Path.Combine(_environment.WebRootPath, folder);
            string uniqueFilename = User.Identity.Name + "_" + newTRFile.TR_File.FileName;
            string filePath = Path.Combine(serverFolder, uniqueFilename);

            //Save the Photo within the specified File Path
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                newTRFile.TR_File.CopyTo(fileStream);
                fileStream.Close();
                fileStream.Dispose();
            }

            int firstYear = DateTime.Now.Year;
            int secondYear = DateTime.Now.Year + 1;
            string year = "A.Y. " + firstYear.ToString() + " - " + secondYear.ToString();

            newTRFile.TR_AcademicYear = year;
            newTRFile.TR_FilePath = folder + uniqueFilename;

            _dbData.Terminal_Report.Add(newTRFile);
            _dbData.SaveChanges();

            return RedirectToAction("TR", _dbData.Terminal_Report);

        }

        [Authorize(Roles = "Scholar")]
        public IActionResult GL()
        {

            return View("GL", _dbData.Gratitude_Letter.Where(rf => rf.ScholarEmailAddress == User.Identity.Name));
        }

        public IActionResult ViewGL(int id)
        {

            Gratitude_Letter? glFile = _dbData.Gratitude_Letter.FirstOrDefault(gl => gl.GL_ID == id);

            if (glFile != null)
            {

                return View(glFile);

            }
            return NotFound();

        }

        [HttpGet]
        public IActionResult UploadGL()
        {
            return View();
        }

        [HttpPost]
        public IActionResult UploadGL(Gratitude_Letter newGLFile)
        {
            if (!ModelState.IsValid)
                return View();

            string folder = "ScholarshipRequirements/GratitudeLetter/";
            string serverFolder = Path.Combine(_environment.WebRootPath, folder);
            string uniqueFilename = User.Identity.Name + "_" + newGLFile.GL_File.FileName;
            string filePath = Path.Combine(serverFolder, uniqueFilename);

            //Save the Photo within the specified File Path
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                newGLFile.GL_File.CopyTo(fileStream);
                fileStream.Close();
                fileStream.Dispose();
            }

            int firstYear = DateTime.Now.Year;
            int secondYear = DateTime.Now.Year + 1;
            string year = "A.Y. " + firstYear.ToString() + " - " + secondYear.ToString();

            newGLFile.GL_AcademicYear = year;
            newGLFile.GL_FilePath = folder + uniqueFilename;

            _dbData.Gratitude_Letter.Add(newGLFile);
            _dbData.SaveChanges();

            return RedirectToAction("GL", _dbData.Gratitude_Letter);

        }

    }

}