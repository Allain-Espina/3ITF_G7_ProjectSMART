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
        public IActionResult UploadCG(RequirementsModel newCGFile)
        {
            if (!ModelState.IsValid)
                return View();

            Certified_Grades? cg = _dbData.Certified_Grades.FirstOrDefault(cg => cg.ScholarEmailAddress == newCGFile.ScholarEmailAddress);

            string folder = "ScholarshipRequirements/CertifiedTrueCopyOfGrades/";
            string serverFolder = Path.Combine(_environment.WebRootPath, folder);
            string uniqueFilename = User.Identity.Name + "_" + newCGFile.File.FileName;
            string filePath = Path.Combine(serverFolder, uniqueFilename);

            //Save the Photo within the specified File Path
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                newCGFile.File.CopyTo(fileStream);
                fileStream.Close();
                fileStream.Dispose();
            }

            int firstYear = DateTime.Now.Year;
            int secondYear = DateTime.Now.Year + 1;
            string year = "A.Y. " + firstYear.ToString() + " - " + secondYear.ToString();

            cg.ScholarEmailAddress = newCGFile.ScholarEmailAddress;
            cg.CG_Term = newCGFile.Term;
            cg.CG_AcademicYear = year;
            cg.CG_FileName = uniqueFilename;
            cg.CG_FilePath = folder + uniqueFilename;
            cg.CG_DateUploaded = newCGFile.DateUploaded;
            cg.CG_Status = newCGFile.Status;

            _dbData.Certified_Grades.Add(cg);
            _dbData.SaveChanges();

            return RedirectToAction("CG", _dbData.Certified_Grades);

        }

        [HttpGet]
        public IActionResult UpdateCG(int id)
        {
            Certified_Grades? cg = _dbData.Certified_Grades.FirstOrDefault(cg => cg.CG_ID == id);

            if (cg != null)
                return View(cg);

            return NotFound();
        }

        [HttpPost]
        public IActionResult UpdateCG(RequirementsModel updatedCG)
        {
            Certified_Grades? cg = _dbData.Certified_Grades.FirstOrDefault(cg => cg.ScholarEmailAddress == updatedCG.ScholarEmailAddress);

            if (!ModelState.IsValid)
                return View();

            if (updatedCG != null)
            {

                string folder = "ScholarshipRequirements/CertifiedTrueCopyOfGrades/";
                string serverFolder = Path.Combine(_environment.WebRootPath, folder);
                string uniqueFilename = User.Identity.Name + "_" + updatedCG.File.FileName;
                string filePath = Path.Combine(serverFolder, uniqueFilename);

                string cgUrl = cg.CG_FilePath;
                string oldPath = Path.Combine(_environment.WebRootPath, cgUrl);
                if (System.IO.File.Exists(oldPath))
                {
                    System.IO.File.Delete(oldPath);
                }

                //Save the Photo within the specified File Path
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {

                    updatedCG.File.CopyTo(fileStream);
                    fileStream.Close();
                    fileStream.Dispose();

                }

                cg.CG_Term = updatedCG.Term;
                cg.CG_FilePath = folder + uniqueFilename;

                _dbData.SaveChanges();

            }
            return RedirectToAction("CG", _dbData.Certified_Grades);

        }

        public IActionResult AdminCG()
        {

            return View("AdminCG", _dbData.Certified_Grades);
        }

        [HttpGet]
        public IActionResult AdminViewCG(int id)
        {

            Certified_Grades? cg = _dbData.Certified_Grades.FirstOrDefault(cg => cg.CG_ID == id);

            if (cg != null)
            {

                return View(cg);

            }
            return NotFound();

        }

        [HttpGet]
        public IActionResult AdminUpdateCGStatus(int id)
        {
            Certified_Grades? cgUpdatedStatus = _dbData.Certified_Grades.FirstOrDefault(cg => cg.CG_ID == id);

            if (cgUpdatedStatus != null)
                return View(cgUpdatedStatus);

            return NotFound();
        }

        [HttpPost]
        public IActionResult AdminUpdateCGStatus(Certified_Grades updatedCG)
        {
            Certified_Grades? cg = _dbData.Certified_Grades.FirstOrDefault(cg => cg.CG_ID == updatedCG.CG_ID);

            if (!ModelState.IsValid)
                return View();

            if (updatedCG != null)
            {

                cg.CG_Status = updatedCG.CG_Status;

                _dbData.SaveChanges();

            }
            return RedirectToAction("AdminCG", _dbData.Certified_Grades);

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
        public IActionResult UploadRF(RequirementsModel newRFFile)
        {
            if (!ModelState.IsValid)
                return View();

            Registration_Form? rf = _dbData.Registration_Form.FirstOrDefault(rf => rf.ScholarEmailAddress == newRFFile.ScholarEmailAddress);

            string folder = "ScholarshipRequirements/RegistrationForm/";
            string serverFolder = Path.Combine(_environment.WebRootPath, folder);
            string uniqueFilename = User.Identity.Name + "_" + newRFFile.File.FileName;
            string filePath = Path.Combine(serverFolder, uniqueFilename);

            //Save the Photo within the specified File Path
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                newRFFile.File.CopyTo(fileStream);
                fileStream.Close();
                fileStream.Dispose();
            }

            int firstYear = DateTime.Now.Year;
            int secondYear = DateTime.Now.Year + 1;
            string year = "A.Y. " + firstYear.ToString() + " - " + secondYear.ToString();

            rf.ScholarEmailAddress = newRFFile.ScholarEmailAddress;
            rf.RF_Term = newRFFile.Term;
            rf.RF_AcademicYear = year;
            rf.RF_FileName = uniqueFilename;
            rf.RF_FilePath = folder + uniqueFilename;
            rf.RF_DateUploaded = newRFFile.DateUploaded;
            rf.RF_Status = newRFFile.Status;

            _dbData.Registration_Form.Add(rf);
            _dbData.SaveChanges();

            return RedirectToAction("RF", _dbData.Registration_Form);

        }

        [HttpGet]
        public IActionResult UpdateRF(int id)
        {
            Registration_Form? rf = _dbData.Registration_Form.FirstOrDefault(rf => rf.RF_ID == id);

            if (rf != null)
                return View(rf);

            return NotFound();
        }

        [HttpPost]
        public IActionResult UpdateRF(RequirementsModel updatedRF)
        {
            Registration_Form? rf = _dbData.Registration_Form.FirstOrDefault(rf => rf.ScholarEmailAddress == updatedRF.ScholarEmailAddress);

            if (!ModelState.IsValid)
                return View();

            if (updatedRF != null)
            {

                string folder = "ScholarshipRequirements/RegistrationForm/";
                string serverFolder = Path.Combine(_environment.WebRootPath, folder);
                string uniqueFilename = User.Identity.Name + "_" + updatedRF.File.FileName;
                string filePath = Path.Combine(serverFolder, uniqueFilename);

                string cgUrl = rf.RF_FilePath;
                string oldPath = Path.Combine(_environment.WebRootPath, cgUrl);
                if (System.IO.File.Exists(oldPath))
                {
                    System.IO.File.Delete(oldPath);
                }

                //Save the Photo within the specified File Path
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {

                    updatedRF.File.CopyTo(fileStream);
                    fileStream.Close();
                    fileStream.Dispose();

                }

                rf.RF_Term = updatedRF.Term;
                rf.RF_FilePath = folder + uniqueFilename;

                _dbData.SaveChanges();

            }
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
        public IActionResult UploadTR(RequirementsModel newTRFile)
        {
            if (!ModelState.IsValid)
                return View();

            Terminal_Report? tr = _dbData.Terminal_Report.FirstOrDefault(tr => tr.ScholarEmailAddress == newTRFile.ScholarEmailAddress);

            string folder = "ScholarshipRequirements/TerminalReportForm/";
            string serverFolder = Path.Combine(_environment.WebRootPath, folder);
            string uniqueFilename = User.Identity.Name + "_" + newTRFile.File.FileName;
            string filePath = Path.Combine(serverFolder, uniqueFilename);

            //Save the Photo within the specified File Path
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                newTRFile.File.CopyTo(fileStream);
                fileStream.Close();
                fileStream.Dispose();
            }

            int firstYear = DateTime.Now.Year;
            int secondYear = DateTime.Now.Year + 1;
            string year = "A.Y. " + firstYear.ToString() + " - " + secondYear.ToString();

            tr.ScholarEmailAddress = newTRFile.ScholarEmailAddress;
            tr.TR_Term = newTRFile.Term;
            tr.TR_AcademicYear = year;
            tr.TR_FileName = uniqueFilename;
            tr.TR_FilePath = folder + uniqueFilename;
            tr.TR_DateUploaded = newTRFile.DateUploaded;
            tr.TR_Status = newTRFile.Status;

            _dbData.Terminal_Report.Add(tr);
            _dbData.SaveChanges();

            return RedirectToAction("CGTR", _dbData.Terminal_Report);

        }

        [HttpGet]
        public IActionResult UpdateTR(int id)
        {
            Terminal_Report? tr = _dbData.Terminal_Report.FirstOrDefault(tr => tr.TR_ID == id);

            if (tr != null)
                return View(tr);

            return NotFound();
        }

        [HttpPost]
        public IActionResult UpdateTR(RequirementsModel updatedTR)
        {
            Terminal_Report? tr = _dbData.Terminal_Report.FirstOrDefault(tr => tr.ScholarEmailAddress == updatedTR.ScholarEmailAddress);

            if (!ModelState.IsValid)
                return View();

            if (updatedTR != null)
            {

                string folder = "ScholarshipRequirements/TerminalReportForm/";
                string serverFolder = Path.Combine(_environment.WebRootPath, folder);
                string uniqueFilename = User.Identity.Name + "_" + updatedTR.File.FileName;
                string filePath = Path.Combine(serverFolder, uniqueFilename);

                string cgUrl = tr.TR_FilePath;
                string oldPath = Path.Combine(_environment.WebRootPath, cgUrl);
                if (System.IO.File.Exists(oldPath))
                {
                    System.IO.File.Delete(oldPath);
                }

                //Save the Photo within the specified File Path
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {

                    updatedTR.File.CopyTo(fileStream);
                    fileStream.Close();
                    fileStream.Dispose();

                }

                tr.TR_Term = updatedTR.Term;
                tr.TR_FilePath = folder + uniqueFilename;

                _dbData.SaveChanges();

            }
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
        public IActionResult UploadGL(RequirementsModel newGLFile)
        {
            if (!ModelState.IsValid)
                return View();

            Gratitude_Letter? gl = _dbData.Gratitude_Letter.FirstOrDefault(gl => gl.ScholarEmailAddress == newGLFile.ScholarEmailAddress);

            string folder = "ScholarshipRequirements/GratitudeLetter/";
            string serverFolder = Path.Combine(_environment.WebRootPath, folder);
            string uniqueFilename = User.Identity.Name + "_" + newGLFile.File.FileName;
            string filePath = Path.Combine(serverFolder, uniqueFilename);

            //Save the Photo within the specified File Path
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                newGLFile.File.CopyTo(fileStream);
                fileStream.Close();
                fileStream.Dispose();
            }

            int firstYear = DateTime.Now.Year;
            int secondYear = DateTime.Now.Year + 1;
            string year = "A.Y. " + firstYear.ToString() + " - " + secondYear.ToString();

            gl.ScholarEmailAddress = newGLFile.ScholarEmailAddress;
            gl.GL_Term = newGLFile.Term;
            gl.GL_AcademicYear = year;
            gl.GL_FileName = uniqueFilename;
            gl.GL_FilePath = folder + uniqueFilename;
            gl.GL_DateUploaded = newGLFile.DateUploaded;
            gl.GL_Status = newGLFile.Status;

            _dbData.Gratitude_Letter.Add(gl);
            _dbData.SaveChanges();

            return RedirectToAction("GL", _dbData.Gratitude_Letter);

        }

        [HttpGet]
        public IActionResult UpdateGL(int id)
        {
            Gratitude_Letter? gl = _dbData.Gratitude_Letter.FirstOrDefault(gl => gl.GL_ID == id);

            if (gl != null)
                return View(gl);

            return NotFound();
        }

        [HttpPost]
        public IActionResult UpdateGL(RequirementsModel updatedGL)
        {
            Gratitude_Letter? gl = _dbData.Gratitude_Letter.FirstOrDefault(gl => gl.ScholarEmailAddress == updatedGL.ScholarEmailAddress);

            if (!ModelState.IsValid)
                return View();

            if (updatedGL != null)
            {

                string folder = "ScholarshipRequirements/GratitudeLetter/";
                string serverFolder = Path.Combine(_environment.WebRootPath, folder);
                string uniqueFilename = User.Identity.Name + "_" + updatedGL.File.FileName;
                string filePath = Path.Combine(serverFolder, uniqueFilename);

                string cgUrl = gl.GL_FilePath;
                string oldPath = Path.Combine(_environment.WebRootPath, cgUrl);
                if (System.IO.File.Exists(oldPath))
                {
                    System.IO.File.Delete(oldPath);
                }

                //Save the Photo within the specified File Path
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {

                    updatedGL.File.CopyTo(fileStream);
                    fileStream.Close();
                    fileStream.Dispose();

                }

                gl.GL_Term = updatedGL.Term;
                gl.GL_FilePath = folder + uniqueFilename;

                _dbData.SaveChanges();

            }
            return RedirectToAction("GL", _dbData.Gratitude_Letter);

        }

    }

}