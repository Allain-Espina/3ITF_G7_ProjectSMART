using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjectSmart.Data;
using ProjectSmart.Models;
using ProjectSmart.ViewModels;
using System.Threading.Tasks;

namespace ProjectSmart.Controllers
{
    [Authorize] // Apply authorization to make sure only authenticated users can access these actions
    public class SettingsController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public SettingsController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public async Task<IActionResult> AdminUpdateAccount()
        {
            // Retrieve the current user
            User adminUser = await _userManager.GetUserAsync(User);

            // Map the user data to the view model
            AdminUpdateAccountModel viewModel = new AdminUpdateAccountModel
            {
                AdminFirstName = adminUser.FirstName,
                AdminMiddleName = adminUser.MiddleName,
                AdminLastName = adminUser.LastName,
                AdminEmail = adminUser.Email,
                //AdminContactNumber = adminUser.ContactNumber,

                // Add other properties as needed
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AdminUpdateAccount(AdminUpdateAccountModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            // Retrieve the current user
            User adminUser = await _userManager.GetUserAsync(User);

            // Update the user properties
            adminUser.FirstName = model.AdminFirstName;
            adminUser.MiddleName = model.AdminMiddleName;
            adminUser.LastName = model.AdminLastName;
            adminUser.Email = model.AdminEmail;

            //added
            //adminUser.ContactNumber = model.AdminContactNumber;
            //adminUser.DateOfBirth = model.AdminDateOfBirth;
            //adminUser.Address1= model.AdminAddress1;
            //adminUser.Address2 = model.AdminAddress2;
            //adminUser.City = model.AdminCity;
            //adminUser.Region = model.AdminRegion;
            ////not sure since idk if pwede palitan or i-update to??
            //adminUser.Position = model.AdminPosition;
            //adminUser.Branch = model.AdminBranch;

            // Update the user in the database
            await _userManager.UpdateAsync(adminUser);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult AdminUpdatePassword() => View();

        [HttpPost]
        public async Task<IActionResult> AdminUpdatePassword(AdminUpdatePasswordModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            // Retrieve the current user
            User adminUser = await _userManager.GetUserAsync(User);

            // Change the user's password
            var result = await _userManager.ChangePasswordAsync(adminUser, model.CurrentPassword, model.NewPassword);

            if (result.Succeeded)
            {
                // Sign in again to update the authentication cookie
                await _signInManager.SignInAsync(adminUser, isPersistent: false);

                return RedirectToAction("Index", "Home");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View(model);
            }
        }

        [HttpGet]
        public IActionResult ScholarUpdateAccount()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ScholarUpdatePassword() => View();

        [HttpPost]
        public async Task<IActionResult> ScholarUpdatePassword(ScholarUpdatePasswordModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            // Retrieve the current user
            User scholarUser = await _userManager.GetUserAsync(User);

            // Change the user's password
            var result = await _userManager.ChangePasswordAsync(scholarUser, model.CurrentPassword, model.NewPassword);

            if (result.Succeeded)
            {
                // Sign in again to update the authentication cookie
                await _signInManager.SignInAsync(scholarUser, isPersistent: false);

                return RedirectToAction("Index", "Home");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View(model);
            }
        }



    }
}
