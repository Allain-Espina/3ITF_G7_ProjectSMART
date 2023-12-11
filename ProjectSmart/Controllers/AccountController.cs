using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectSmart.Data;
using ProjectSmart.Models;
using ProjectSmart.ViewModels;
using System.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ProjectSmart.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<User> _signInManager;
        private readonly Microsoft.AspNetCore.Identity.UserManager<User> _userManager;
        private readonly AppDbContext _dbData;

        public AccountController(SignInManager<User> SignInManager, Microsoft.AspNetCore.Identity.UserManager<User> UserManager, AppDbContext dbData)
        {
            _signInManager = SignInManager;
            _userManager = UserManager;
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

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("ScholarLogin", "Account");
        }

        [HttpGet]
        public IActionResult ScholarRegister()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ScholarRegister(ScholarRegisterModel scholarCredentials)
        {
            if (!ModelState.IsValid)
                return View();

            if (ModelState.IsValid)
            {

                User newScholar = new User();

                newScholar.UserName = scholarCredentials.Email;
                newScholar.FirstName = scholarCredentials.FirstName;
                newScholar.MiddleName = scholarCredentials.MiddleName;
                newScholar.LastName = scholarCredentials.LastName;
                newScholar.Email = scholarCredentials.Email;
                newScholar.PhoneNumber = scholarCredentials.Phone;

                var result = await _userManager.CreateAsync(newScholar, scholarCredentials.Password);

                if (result.Succeeded)
                {

                    ScholarUsers newScholarInfo = new ScholarUsers();

                    newScholarInfo.ScholarFirstName = scholarCredentials.FirstName;
                    newScholarInfo.ScholarMiddleName = scholarCredentials.MiddleName;
                    newScholarInfo.ScholarLastName = scholarCredentials.LastName;
                    newScholarInfo.ScholarEmail = scholarCredentials.Email;
                    newScholarInfo.ScholarContactNumber = scholarCredentials.Phone;
                    newScholarInfo.ScholarDateOfBirth = scholarCredentials.DateOfBirth;
                    newScholarInfo.ScholarUniversity = scholarCredentials.University;
                    newScholarInfo.ScholarAddress1 = scholarCredentials.Address1;
                    newScholarInfo.ScholarAddress2 = scholarCredentials.Address2;
                    newScholarInfo.ScholarCity = scholarCredentials.City;
                    newScholarInfo.ScholarRegion = scholarCredentials.Region;
                    _dbData.Scholars.Add(newScholarInfo);
                    _dbData.SaveChanges();

                    result = await _userManager.AddToRoleAsync(newScholar, "Scholar");
                    return RedirectToAction("ScholarLogin", "Account");

                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
                return View(scholarCredentials);

            }

            return View(scholarCredentials);

        }

        [HttpGet]
        public IActionResult AdminRegister()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AdminRegister(AdminRegisterModel adminCredentials)
        {
            if (!ModelState.IsValid)
                return View();

            if (ModelState.IsValid)
            {

                User newAdmin = new User();

                newAdmin.UserName = adminCredentials.Email;
                newAdmin.FirstName = adminCredentials.FirstName;
                newAdmin.MiddleName = adminCredentials.MiddleName;
                newAdmin.LastName = adminCredentials.LastName;
                newAdmin.Email = adminCredentials.Email;
                newAdmin.PhoneNumber = adminCredentials.Phone;

                var result = await _userManager.CreateAsync(newAdmin, adminCredentials.Password);

                if (result.Succeeded)
                {

                    AdminUsers newAdminInfo = new AdminUsers();

                    newAdminInfo.AdminFirstName = adminCredentials.FirstName;
                    newAdminInfo.AdminMiddleName = adminCredentials.MiddleName;
                    newAdminInfo.AdminLastName = adminCredentials.LastName;
                    newAdminInfo.AdminEmail = adminCredentials.Email;
                    newAdminInfo.AdminContactNumber = adminCredentials.Phone;
                    newAdminInfo.AdminDateOfBirth = adminCredentials.DateOfBirth;
                    newAdminInfo.AdminAddress1 = adminCredentials.Address1;
                    newAdminInfo.AdminAddress2 = adminCredentials.Address2;
                    newAdminInfo.AdminCity = adminCredentials.City;
                    newAdminInfo.AdminRegion = adminCredentials.Region;
                    newAdminInfo.AdminPosition = adminCredentials.Position;
                    newAdminInfo.AdminBranch = adminCredentials.Branch;
                    _dbData.Admins.Add(newAdminInfo);
                    _dbData.SaveChanges();

                    result = await _userManager.AddToRoleAsync(newAdmin, "Admin");
                    return RedirectToAction("AdminLogin", "Account");

                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
                return View(adminCredentials);

            }

            return View(adminCredentials);

        }

    }
}
