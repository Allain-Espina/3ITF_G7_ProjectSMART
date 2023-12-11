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

    }

}
