using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjectSmart.Data;
using ProjectSmart.Models;
using ProjectSmart.ViewModels;
using System.Threading.Tasks;

namespace ProjectSmart.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<User> _signInManager;
        private readonly Microsoft.AspNetCore.Identity.UserManager<User> _userManager;
        private readonly AppDbContext _dbData;

        public AccountController(SignInManager<User> signInManager, Microsoft.AspNetCore.Identity.UserManager<User> userManager, AppDbContext dbData)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _dbData = dbData;
        }

        [HttpGet]
        public IActionResult ScholarLogin()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ScholarLogin(ScholarLoginModel scholarLoginInfo)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _signInManager.PasswordSignInAsync(scholarLoginInfo.Email, scholarLoginInfo.Password, false, false);

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(scholarLoginInfo);
        }

        [Authorize(Roles = "Scholar")]
        public IActionResult ScholarAccountSettings()
        {

            return View("ScholarAccountSettings", _dbData.Scholars.Where(sa => sa.ScholarEmail == User.Identity.GetUserName()));

        }

        [Authorize(Roles = "Scholar")]
        public IActionResult ScholarViewDetails(int id)
        {

            ScholarUsers? scholar = _dbData.Scholars.FirstOrDefault(sa => sa.ScholarId == id);

            if (scholar != null)
            {

                return View(scholar);

            }
            return NotFound();

        }

        [Authorize(Roles = "Scholar")]
        [HttpGet]
        public IActionResult ScholarUpdateDetails(int id)
        {
            ScholarUsers? scholar = _dbData.Scholars.FirstOrDefault(sa => sa.ScholarId == id);

            if (scholar != null)
                return View(scholar);

            return NotFound();
        }

        [Authorize(Roles = "Scholar")]
        [HttpPost]
        public IActionResult ScholarUpdateDetails(ScholarUsers scholarChanges)
        {
            ScholarUsers? scholar = _dbData.Scholars.FirstOrDefault(sa => sa.ScholarId == scholarChanges.ScholarId);

            if (!ModelState.IsValid)
                return View();

            if (ModelState.IsValid)
            {

                if (scholarChanges != null)
                {
                    scholar.ScholarFirstName = scholarChanges.ScholarFirstName;
                    scholar.ScholarMiddleName = scholarChanges.ScholarMiddleName;
                    scholar.ScholarLastName = scholarChanges.ScholarLastName;
                    scholar.ScholarContactNumber = scholarChanges.ScholarContactNumber;
                    scholar.ScholarUniversity = scholarChanges.ScholarUniversity;
                    scholar.ScholarAddress1 = scholarChanges.ScholarAddress1;
                    scholar.ScholarAddress2 = scholarChanges.ScholarAddress2;
                    scholar.ScholarCity = scholarChanges.ScholarCity;
                    scholar.ScholarRegion = scholarChanges.ScholarRegion;
                    _dbData.SaveChanges();
                }

            }

            return View("ScholarAccountSettings", _dbData.Scholars.Where(sa => sa.ScholarEmail == User.Identity.Name));

        }

        [Authorize(Roles = "Scholar")]
        [Authorize]
        [HttpGet]
        public IActionResult ScholarUpdatePassword()
        {
            return View();
        }

        [Authorize(Roles = "Scholar")]
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ScholarUpdatePassword(ChangePWModel passwordChanges)
        {
            User? user = _dbData.Users.FirstOrDefault(u => u.Email == passwordChanges.Email);


            if (!ModelState.IsValid)
                return View();

            if (ModelState.IsValid)
            {

                if (passwordChanges != null)
                {

                    if (user.Email == passwordChanges.Email)
                    {
                        if (passwordChanges.NewPassword == passwordChanges.ConfirmNewPassword)
                        {
                            var result = await _userManager.ChangePasswordAsync(user, passwordChanges.CurrentPassword, passwordChanges.NewPassword);


                            if (result.Succeeded)
                            {
                                return View("ScholarAccountSettings", _dbData.Scholars.Where(sa => sa.ScholarEmail == User.Identity.Name));
                            }
                            else
                            {
                                foreach (var error in result.Errors)
                                {
                                    ModelState.AddModelError("", error.Description);
                                }
                            }

                        }
                    }

                }
            }

            return View("ScholarAccountSettings", _dbData.Scholars.Where(sa => sa.ScholarEmail == User.Identity.Name));

        }

        [HttpGet]
        public IActionResult AdminLogin()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AdminLogin(AdminLoginModel adminLoginInfo)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _signInManager.PasswordSignInAsync(adminLoginInfo.Email, adminLoginInfo.Password, false, false);

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(adminLoginInfo);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult AdminAccountSettings()
        {

            return View("AdminAccountSettings", _dbData.Admins.Where(aa => aa.AdminEmail == User.Identity.GetUserName()));

        }

        [Authorize(Roles = "Admin")]
        public IActionResult AdminViewDetails(int id)
        {

            AdminUsers? admin = _dbData.Admins.FirstOrDefault(aa => aa.AdminId == id);

            if (admin != null)
            {

                return View(admin);

            }
            return NotFound();

        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult AdminUpdateDetails(int id)
        {
            AdminUsers? admin = _dbData.Admins.FirstOrDefault(aa => aa.AdminId == id);

            if (admin != null)
                return View(admin);

            return NotFound();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult AdminUpdateDetails(AdminUsers adminChanges)
        {
            AdminUsers? admin = _dbData.Admins.FirstOrDefault(aa => aa.AdminId == adminChanges.AdminId);

            if (!ModelState.IsValid)
                return View();

            if (ModelState.IsValid)
            {

                if (adminChanges != null)
                {
                    admin.AdminFirstName = adminChanges.AdminFirstName;
                    admin.AdminMiddleName = adminChanges.AdminMiddleName;
                    admin.AdminLastName = adminChanges.AdminLastName;
                    admin.AdminContactNumber = adminChanges.AdminContactNumber;
                    admin.AdminAddress1 = adminChanges.AdminAddress1;
                    admin.AdminAddress2 = adminChanges.AdminAddress2;
                    admin.AdminCity = adminChanges.AdminCity;
                    admin.AdminRegion = adminChanges.AdminRegion;
                    admin.AdminPosition = adminChanges.AdminPosition;
                    admin.AdminBranch = adminChanges.AdminBranch;
                    _dbData.SaveChanges();
                }

            }

            return View("AdminAccountSettings", _dbData.Admins.Where(aa => aa.AdminEmail == User.Identity.Name));

        }

        [Authorize(Roles = "Admin")]
        [Authorize]
        [HttpGet]
        public IActionResult AdminUpdatePassword()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AdminUpdatePassword(ChangePWModel passwordChanges)
        {
            User? user = _dbData.Users.FirstOrDefault(u => u.Email == passwordChanges.Email);


            if (!ModelState.IsValid)
                return View();

            if (ModelState.IsValid)
            {

                if (passwordChanges != null)
                {

                    if (user.Email == passwordChanges.Email)
                    {
                        if (passwordChanges.NewPassword == passwordChanges.ConfirmNewPassword)
                        {
                            var result = await _userManager.ChangePasswordAsync(user, passwordChanges.CurrentPassword, passwordChanges.NewPassword);


                            if (result.Succeeded)
                            {
                                return View("AdminAccountSettings", _dbData.Admins.Where(sa => sa.AdminEmail == User.Identity.Name));
                            }
                            else
                            {
                                foreach (var error in result.Errors)
                                {
                                    ModelState.AddModelError("", error.Description);
                                }
                            }

                        }
                    }

                }
            }

            return View("AdminAccountSettings", _dbData.Admins.Where(sa => sa.AdminEmail == User.Identity.Name));

        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("ScholarLogin", "Account");
        }


    }
}
