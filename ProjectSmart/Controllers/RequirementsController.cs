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

        [Authorize(Roles = "Scholar")]
        public IActionResult ViewCG(int id)
        {

            Certified_Grades? cgFile = _dbData.Certified_Grades.FirstOrDefault(cg => cg.CG_ID == id);

            if (cgFile != null)
            {

                return View(cgFile);

            }
            return NotFound();

        }

        [Authorize(Roles = "Scholar")]
        [HttpGet]
        public IActionResult UploadCG()
        {
            return View();
        }

        [Authorize(Roles = "Scholar")]
        [HttpPost]
        public IActionResult UploadCG(Certified_Grades newCGFile)
        {
            if (!ModelState.IsValid)
                return View();

            string folder = "ScholarshipRequirements/CertifiedTrueCopyOfGrades/";
            string serverFolder = Path.Combine(_environment.WebRootPath, folder);
            string uniqueFilename = User.Identity.Name + "_" + newCGFile.CG_File.FileName;
            string filePath = Path.Combine(serverFolder, uniqueFilename);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                newCGFile.CG_File.CopyTo(fileStream);
                fileStream.Close();
                fileStream.Dispose();
            }

            int firstYear = DateTime.Now.Year;
            int secondYear = DateTime.Now.Year + 1;
            string year = "A.Y. " + firstYear.ToString() + " - " + secondYear.ToString();

            newCGFile.CG_FileName = uniqueFilename;
            newCGFile.CG_AcademicYear = year;
            newCGFile.CG_FilePath = folder + uniqueFilename;

            _dbData.Certified_Grades.Add(newCGFile);
            _dbData.SaveChanges();

            return RedirectToAction("CG", _dbData.Certified_Grades);

        }

        [Authorize(Roles = "Scholar")]
        [HttpGet]
        public IActionResult UpdateCG(int id)
        {
            Certified_Grades? cg = _dbData.Certified_Grades.FirstOrDefault(cg => cg.CG_ID == id);

            if (cg != null)
                return View(cg);

            return NotFound();
        }

        [Authorize(Roles = "Scholar")]
        [HttpPost]
        public IActionResult UpdateCG(Certified_Grades updatedCG)
        {
            Certified_Grades? cg = _dbData.Certified_Grades.FirstOrDefault(cg => cg.CG_ID == updatedCG.CG_ID);

            if (!ModelState.IsValid)
                return View();

            if (updatedCG != null)
            {

                string folder = "ScholarshipRequirements/CertifiedTrueCopyOfGrades/";
                string serverFolder = Path.Combine(_environment.WebRootPath, folder);
                string uniqueFilename = User.Identity.Name + "_" + updatedCG.CG_File.FileName;
                string filePath = Path.Combine(serverFolder, uniqueFilename);

                string cgUrl = cg.CG_FilePath;
                string oldPath = Path.Combine(_environment.WebRootPath, cgUrl);
                if (System.IO.File.Exists(oldPath))
                {
                    System.IO.File.Delete(oldPath);
                }

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {

                    updatedCG.CG_File.CopyTo(fileStream);
                    fileStream.Close();
                    fileStream.Dispose();

                }

                cg.CG_Term = updatedCG.CG_Term;
                cg.CG_FilePath = folder + uniqueFilename;

                _dbData.SaveChanges();

            }
            return RedirectToAction("CG", _dbData.Certified_Grades);

        }

        [Authorize(Roles = "Admin")]
        public IActionResult AdminCG()
        {

            return View("AdminCG", _dbData.Certified_Grades);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult AdminCheckCG(int id)
        {

            Certified_Grades? cgFile = _dbData.Certified_Grades.FirstOrDefault(cg => cg.CG_ID == id);

            if (cgFile != null)
            {

                return View(cgFile);

            }
            return NotFound();

        }

        [Authorize(Roles = "Admin")]
        public IActionResult AdminUpdateCGStatus(int id)
        {

            Certified_Grades? cg = _dbData.Certified_Grades.FirstOrDefault(cg => cg.CG_ID == id);

            if (cg != null)
                return View(cg);

            return NotFound();

        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult AdminUpdateCGStatus(Certified_Grades cgUpdated)
        {

            Certified_Grades? cg = _dbData.Certified_Grades.FirstOrDefault(cg => cg.CG_ID == cgUpdated.CG_ID);

            cg.CG_Status = cgUpdated.CG_Status;
            _dbData.SaveChanges();

            return RedirectToAction("AdminCG", _dbData.Certified_Grades);

        }

        [Authorize(Roles = "Scholar")]
        public IActionResult RF()
        {

            return View("RF", _dbData.Registration_Form.Where(rf => rf.ScholarEmailAddress == User.Identity.Name));
        }

        [Authorize(Roles = "Scholar")]
        public IActionResult ViewRF(int id)
        {

            Registration_Form? rfFile = _dbData.Registration_Form.FirstOrDefault(rf => rf.RF_ID == id);

            if (rfFile != null)
            {

                return View(rfFile);

            }
            return NotFound();

        }

        [Authorize(Roles = "Scholar")]
        [HttpGet]
        public IActionResult UploadRF()
        {
            return View();
        }

        [Authorize(Roles = "Scholar")]
        [HttpPost]
        public IActionResult UploadRF(Registration_Form newRFFile)
        {
            if (!ModelState.IsValid)
                return View();

            string folder = "ScholarshipRequirements/RegistrationForm/";
            string serverFolder = Path.Combine(_environment.WebRootPath, folder);
            string uniqueFilename = User.Identity.Name + "_" + newRFFile.RF_File.FileName;
            string filePath = Path.Combine(serverFolder, uniqueFilename);

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
        [HttpGet]
        public IActionResult UpdateRF(int id)
        {
            Registration_Form? rf = _dbData.Registration_Form.FirstOrDefault(rf => rf.RF_ID == id);

            if (rf != null)
                return View(rf);

            return NotFound();
        }

        [Authorize(Roles = "Scholar")]
        [HttpPost]
        public IActionResult UpdateRF(Registration_Form updatedRF)
        {
            Registration_Form? rf = _dbData.Registration_Form.FirstOrDefault(rf => rf.RF_ID == updatedRF.RF_ID);

            if (!ModelState.IsValid)
                return View();

            if (updatedRF != null)
            {

                string folder = "ScholarshipRequirements/RegistrationForm/";
                string serverFolder = Path.Combine(_environment.WebRootPath, folder);
                string uniqueFilename = User.Identity.Name + "_" + updatedRF.RF_File.FileName;
                string filePath = Path.Combine(serverFolder, uniqueFilename);

                string rfUrl = rf.RF_FilePath;
                string oldPath = Path.Combine(_environment.WebRootPath, rfUrl);
                if (System.IO.File.Exists(oldPath))
                {
                    System.IO.File.Delete(oldPath);
                }

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {

                    updatedRF.RF_File.CopyTo(fileStream);
                    fileStream.Close();
                    fileStream.Dispose();

                }

                rf.RF_Term = updatedRF.RF_Term;
                rf.RF_FilePath = folder + uniqueFilename;

                _dbData.SaveChanges();

            }
            return RedirectToAction("RF", _dbData.Registration_Form);

        }

        [Authorize(Roles = "Admin")]
        public IActionResult AdminRF()
        {

            return View("AdminRF", _dbData.Registration_Form);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult AdminCheckRF(int id)
        {

            Registration_Form? rfFile = _dbData.Registration_Form.FirstOrDefault(rf => rf.RF_ID == id);

            if (rfFile != null)
            {

                return View(rfFile);

            }
            return NotFound();

        }

        [Authorize(Roles = "Admin")]
        public IActionResult AdminUpdateRFStatus(int id)
        {

            Registration_Form? rf = _dbData.Registration_Form.FirstOrDefault(rf => rf.RF_ID == id);

            if (rf != null)
                return View(rf);

            return NotFound();

        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult AdminUpdateRFStatus(Registration_Form rfUpdated)
        {

            Registration_Form? rf = _dbData.Registration_Form.FirstOrDefault(rf => rf.RF_ID == rfUpdated.RF_ID);

            rf.RF_Status = rfUpdated.RF_Status;
            _dbData.SaveChanges();

            return RedirectToAction("AdminRF", _dbData.Registration_Form);

        }

        [Authorize(Roles = "Scholar")]
        public IActionResult TR()
        {

            return View("TR", _dbData.Terminal_Report.Where(rf => rf.ScholarEmailAddress == User.Identity.Name));
        }

        [Authorize(Roles = "Scholar")]
        public IActionResult ViewTR(int id)
        {

            Terminal_Report? trFile = _dbData.Terminal_Report.FirstOrDefault(tr => tr.TR_ID == id);

            if (trFile != null)
            {

                return View(trFile);

            }
            return NotFound();

        }

        [Authorize(Roles = "Scholar")]
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
        [HttpGet]
        public IActionResult UpdateTR(int id)
        {
            Terminal_Report? tr = _dbData.Terminal_Report.FirstOrDefault(tr => tr.TR_ID == id);

            if (tr != null)
                return View(tr);

            return NotFound();
        }

        [Authorize(Roles = "Scholar")]
        [HttpPost]
        public IActionResult UpdateTR(Terminal_Report updatedTR)
        {
            Terminal_Report? tr = _dbData.Terminal_Report.FirstOrDefault(tr => tr.TR_ID == updatedTR.TR_ID);

            if (!ModelState.IsValid)
                return View();

            if (updatedTR != null)
            {

                string folder = "ScholarshipRequirements/TerminalReportForm/";
                string serverFolder = Path.Combine(_environment.WebRootPath, folder);
                string uniqueFilename = User.Identity.Name + "_" + updatedTR.TR_File.FileName;
                string filePath = Path.Combine(serverFolder, uniqueFilename);

                string trUrl = tr.TR_FilePath;
                string oldPath = Path.Combine(_environment.WebRootPath, trUrl);
                if (System.IO.File.Exists(oldPath))
                {
                    System.IO.File.Delete(oldPath);
                }

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {

                    updatedTR.TR_File.CopyTo(fileStream);
                    fileStream.Close();
                    fileStream.Dispose();

                }

                tr.TR_Term = updatedTR.TR_Term;
                tr.TR_FilePath = folder + uniqueFilename;

                _dbData.SaveChanges();

            }
            return RedirectToAction("TR", _dbData.Terminal_Report);

        }

        [Authorize(Roles = "Admin")]
        public IActionResult AdminTR()
        {

            return View("AdminTR", _dbData.Terminal_Report);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult AdminCheckTR(int id)
        {

            Terminal_Report? trFile = _dbData.Terminal_Report.FirstOrDefault(tr => tr.TR_ID == id);

            if (trFile != null)
            {

                return View(trFile);

            }
            return NotFound();

        }

        [Authorize(Roles = "Admin")]
        public IActionResult AdminUpdateTRStatus(int id)
        {

            Terminal_Report? tr = _dbData.Terminal_Report.FirstOrDefault(tr => tr.TR_ID == id);

            if (tr != null)
                return View(tr);

            return NotFound();

        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult AdminUpdateTRStatus(Terminal_Report trUpdated)
        {

            Terminal_Report? tr = _dbData.Terminal_Report.FirstOrDefault(tr => tr.TR_ID == trUpdated.TR_ID);

            tr.TR_Status = trUpdated.TR_Status;
            _dbData.SaveChanges();

            return RedirectToAction("AdminTR", _dbData.Terminal_Report);

        }

        [Authorize(Roles = "Scholar")]
        public IActionResult GL()
        {

            return View("GL", _dbData.Gratitude_Letter.Where(rf => rf.ScholarEmailAddress == User.Identity.Name));
        }

        [Authorize(Roles = "Scholar")]
        public IActionResult ViewGL(int id)
        {

            Gratitude_Letter? glFile = _dbData.Gratitude_Letter.FirstOrDefault(gl => gl.GL_ID == id);

            if (glFile != null)
            {

                return View(glFile);

            }
            return NotFound();

        }

        [Authorize(Roles = "Scholar")]
        [HttpGet]
        public IActionResult UploadGL()
        {
            return View();
        }

        [Authorize(Roles = "Scholar")]
        [HttpPost]
        public IActionResult UploadGL(Gratitude_Letter newGLFile)
        {
            if (!ModelState.IsValid)
                return View();

            string folder = "ScholarshipRequirements/GratitudeLetter/";
            string serverFolder = Path.Combine(_environment.WebRootPath, folder);
            string uniqueFilename = User.Identity.Name + "_" + newGLFile.GL_File.FileName;
            string filePath = Path.Combine(serverFolder, uniqueFilename);

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

        [Authorize(Roles = "Scholar")]
        [HttpGet]
        public IActionResult UpdateGL(int id)
        {
            Gratitude_Letter? gl = _dbData.Gratitude_Letter.FirstOrDefault(gl => gl.GL_ID == id);

            if (gl != null)
                return View(gl);

            return NotFound();
        }

        [Authorize(Roles = "Scholar")]
        [HttpPost]
        public IActionResult UpdateGL(Gratitude_Letter updatedGL)
        {
            Gratitude_Letter? gl = _dbData.Gratitude_Letter.FirstOrDefault(gl => gl.GL_ID == updatedGL.GL_ID);

            if (!ModelState.IsValid)
                return View();

            if (updatedGL != null)
            {

                string folder = "ScholarshipRequirements/GratitudeLetter/";
                string serverFolder = Path.Combine(_environment.WebRootPath, folder);
                string uniqueFilename = User.Identity.Name + "_" + updatedGL.GL_File.FileName;
                string filePath = Path.Combine(serverFolder, uniqueFilename);

                string glUrl = gl.GL_FilePath;
                string oldPath = Path.Combine(_environment.WebRootPath, glUrl);
                if (System.IO.File.Exists(oldPath))
                {
                    System.IO.File.Delete(oldPath);
                }

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {

                    updatedGL.GL_File.CopyTo(fileStream);
                    fileStream.Close();
                    fileStream.Dispose();

                }

                gl.GL_Term = updatedGL.GL_Term;
                gl.GL_FilePath = folder + uniqueFilename;

                _dbData.SaveChanges();

            }
            return RedirectToAction("GL", _dbData.Gratitude_Letter);

        }

        [Authorize(Roles = "Admin")]
        public IActionResult AdminGL()
        {

            return View("AdminGL", _dbData.Gratitude_Letter);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult AdminCheckGL(int id)
        {

            Gratitude_Letter? glFile = _dbData.Gratitude_Letter.FirstOrDefault(gl => gl.GL_ID == id);

            if (glFile != null)
            {

                return View(glFile);

            }
            return NotFound();

        }

        [Authorize(Roles = "Admin")]
        public IActionResult AdminUpdateGLStatus(int id)
        {

            Gratitude_Letter? gl = _dbData.Gratitude_Letter.FirstOrDefault(gl => gl.GL_ID == id);

            if (gl != null)
                return View(gl);

            return NotFound();

        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult AdminUpdateGLStatus(Gratitude_Letter glUpdated)
        {

            Gratitude_Letter? gl = _dbData.Gratitude_Letter.FirstOrDefault(gl => gl.GL_ID == glUpdated.GL_ID);

            gl.GL_Status = glUpdated.GL_Status;
            _dbData.SaveChanges();

            return RedirectToAction("AdminGL", _dbData.Gratitude_Letter);

        }

    }

}