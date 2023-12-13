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
        private readonly UserManager<User> _userManager;
        private readonly AppDbContext _dbData;

        public AccountController(SignInManager<User> signInManager, UserManager<User> userManager, AppDbContext dbData)
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

        [HttpGet]
        public IActionResult ScholarRegister()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ScholarRegister(ScholarRegisterModel scholarRegisterInfo)
        {
            if (!ModelState.IsValid)
                return View();

            var user = new User
            {
                UserName = scholarRegisterInfo.Email,
                Email = scholarRegisterInfo.Email,
                FirstName = scholarRegisterInfo.FirstName,
                MiddleName = scholarRegisterInfo.MiddleName,
                LastName = scholarRegisterInfo.LastName,
                Role = "Scholar"
            };

            var result = await _userManager.CreateAsync(user, scholarRegisterInfo.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToAction("Index", "Home");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(scholarRegisterInfo);
        }

        [HttpGet]
        public IActionResult AdminRegister()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AdminRegister(AdminRegisterModel adminRegisterInfo)
        {
            if (!ModelState.IsValid)
                return View();

            var user = new User
            {
                UserName = adminRegisterInfo.Email,
                Email = adminRegisterInfo.Email,
                FirstName = adminRegisterInfo.FirstName,
                MiddleName = adminRegisterInfo.MiddleName,
                LastName = adminRegisterInfo.LastName,
                Role = "Admin"
            };

            var result = await _userManager.CreateAsync(user, adminRegisterInfo.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Admin");
                await _signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToAction("Index", "Home");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(adminRegisterInfo);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("ScholarLogin", "Account");
        }


    }
}
