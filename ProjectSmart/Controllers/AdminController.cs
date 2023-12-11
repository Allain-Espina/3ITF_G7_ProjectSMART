using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjectSmart.Data;
using ProjectSmart.Models;
using ProjectSmart.ViewModels;

namespace ProjectSmart.Controllers
{
    public class AdminController : Controller
    {
        private readonly Microsoft.AspNetCore.Identity.UserManager<User> _userManager;
        private readonly AppDbContext _dbData;

        public AdminController(Microsoft.AspNetCore.Identity.UserManager<User> UserManager, AppDbContext dbData)
        {
            _userManager = UserManager;
            _dbData = dbData;
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
                newScholar.Role = "Scholar";

                var result = await _userManager.CreateAsync(newScholar, scholarCredentials.Password);

                if (result.Succeeded)
                {

                    ScholarUsers newScholarInfo = new ScholarUsers();

                    newScholarInfo.ScholarFirstName = scholarCredentials.FirstName;
                    newScholarInfo.ScholarMiddleName = scholarCredentials.MiddleName;
                    newScholarInfo.ScholarLastName = scholarCredentials.LastName;
                    newScholarInfo.ScholarEmail = scholarCredentials.Email;
                    newScholarInfo.ScholarRole = "Scholar";
                    newScholarInfo.ScholarContactNumber = scholarCredentials.Phone;
                    newScholarInfo.ScholarDateOfBirth = scholarCredentials.DateOfBirth.ToString("MM/dd/yyyy");
                    newScholarInfo.ScholarUniversity = scholarCredentials.University;
                    newScholarInfo.ScholarAddress1 = scholarCredentials.Address1;
                    newScholarInfo.ScholarAddress2 = scholarCredentials.Address2;
                    newScholarInfo.ScholarCity = scholarCredentials.City;
                    newScholarInfo.ScholarRegion = scholarCredentials.Region;
                    _dbData.Scholars.Add(newScholarInfo);
                    _dbData.SaveChanges();

                    result = await _userManager.AddToRoleAsync(newScholar, "Scholar");
                    return View("ScholarAccounts", _dbData.Scholars.Where(sa => sa.ScholarRole == "Scholar"));

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

        public IActionResult ScholarAccounts()
        {
            return View("ScholarAccounts", _dbData.Scholars.Where(sa => sa.ScholarRole == "Scholar"));
        }

        public IActionResult ViewScholarDetails(int id)
        {

            ScholarUsers? scholar = _dbData.Scholars.FirstOrDefault(sa => sa.ScholarId == id);

            if (scholar != null)
            {

                return View(scholar);

            }
            return NotFound();

        }

        [HttpGet]
        public IActionResult UpdateScholarDetails(int id)
        {
            ScholarUsers? scholar = _dbData.Scholars.FirstOrDefault(sa => sa.ScholarId == id);

            if (scholar != null)
                return View(scholar);

            return NotFound();
        }

        [HttpPost]
        public IActionResult UpdateScholarDetails(ScholarUsers scholarChanges)
        {
            ScholarUsers? scholar = _dbData.Scholars.FirstOrDefault(sa => sa.ScholarId == scholarChanges.ScholarId);

            if (scholarChanges != null)
            {
                scholar.ScholarEmail = scholarChanges.ScholarEmail;
                scholar.ScholarFirstName = scholarChanges.ScholarFirstName;
                scholar.ScholarMiddleName = scholarChanges.ScholarMiddleName;
                scholar.ScholarLastName = scholarChanges.ScholarLastName;
                scholar.ScholarContactNumber = scholarChanges.ScholarContactNumber;
                scholar.ScholarDateOfBirth = scholarChanges.ScholarDateOfBirth;
                scholar.ScholarUniversity = scholarChanges.ScholarUniversity;
                scholar.ScholarAddress1 = scholarChanges.ScholarAddress1;
                scholar.ScholarAddress2 = scholarChanges.ScholarAddress2;
                scholar.ScholarCity = scholarChanges.ScholarCity;
                scholar.ScholarRegion = scholarChanges.ScholarRegion;
                _dbData.SaveChanges();
            }
            return View("ScholarAccounts", _dbData.Scholars.Where(sa => sa.ScholarRole == "Scholar"));

        }

        [HttpGet]
        public IActionResult ScholarDeactivate(int id)
        {
            ScholarUsers? scholar = _dbData.Scholars.FirstOrDefault(sa => sa.ScholarId == id);

            if (scholar != null)
                return View(scholar);

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> ScholarDeactivate(ScholarUsers deactivateScholar)
        {
            User? scholar = _dbData.Users.FirstOrDefault(su => su.Email == deactivateScholar.ScholarEmail);
            ScholarUsers? scholarUser = _dbData.Scholars.FirstOrDefault(sa => sa.ScholarId == deactivateScholar.ScholarId);

            if (scholar != null)
            {
                scholar.Role = "Inactive";
                scholarUser.ScholarRole = "Inactive";
            }

            var oldRole = await _userManager.RemoveFromRoleAsync(scholar, "Scholar");
            var newRole = await _userManager.AddToRoleAsync(scholar, "Inactive");

            return View("ScholarAccounts", _dbData.Scholars.Where(sa => sa.ScholarRole == "Scholar"));
        }

        public IActionResult InactiveScholars()
        {
            return View("InactiveScholars", _dbData.Scholars.Where(sa => sa.ScholarRole == "Inactive"));
        }

        [HttpGet]
        public IActionResult ScholarReactivate(int id)
        {
            ScholarUsers? scholar = _dbData.Scholars.FirstOrDefault(sa => sa.ScholarId == id);

            if (scholar != null)
                return View(scholar);

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> ScholarReactivate(ScholarUsers reactivateScholar)
        {
            User? scholar = _dbData.Users.FirstOrDefault(su => su.Email == reactivateScholar.ScholarEmail);
            ScholarUsers? scholarUser = _dbData.Scholars.FirstOrDefault(sa => sa.ScholarId == reactivateScholar.ScholarId);

            if (scholar != null)
            {
                scholar.Role = "Scholar";
                scholarUser.ScholarRole = "Scholar";
            }

            var oldRole = await _userManager.RemoveFromRoleAsync(scholar, "Inactive");
            var newRole = await _userManager.AddToRoleAsync(scholar, "Scholar");

            return View("InactiveScholars", _dbData.Scholars.Where(sa => sa.ScholarRole == "Inactive"));
        }

        /*-----------*/

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
                newAdmin.Role = "Admin";

                var result = await _userManager.CreateAsync(newAdmin, adminCredentials.Password);

                if (result.Succeeded)
                {

                    AdminUsers newAdminInfo = new AdminUsers();

                    newAdminInfo.AdminFirstName = adminCredentials.FirstName;
                    newAdminInfo.AdminMiddleName = adminCredentials.MiddleName;
                    newAdminInfo.AdminLastName = adminCredentials.LastName;
                    newAdminInfo.AdminEmail = adminCredentials.Email;
                    newAdminInfo.AdminRole = "Admin";
                    newAdminInfo.AdminContactNumber = adminCredentials.Phone;
                    newAdminInfo.AdminDateOfBirth = adminCredentials.DateOfBirth.ToString("MM/dd/yyyy");
                    newAdminInfo.AdminAddress1 = adminCredentials.Address1;
                    newAdminInfo.AdminAddress2 = adminCredentials.Address2;
                    newAdminInfo.AdminCity = adminCredentials.City;
                    newAdminInfo.AdminRegion = adminCredentials.Region;
                    newAdminInfo.AdminPosition = adminCredentials.Position;
                    newAdminInfo.AdminBranch = adminCredentials.Branch;
                    _dbData.Admins.Add(newAdminInfo);
                    _dbData.SaveChanges();

                    result = await _userManager.AddToRoleAsync(newAdmin, "Admin");
                    return View("AdminAccounts", _dbData.Admins.Where(sa => sa.AdminRole == "Admin"));

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

        public IActionResult AdminAccounts()
        {
            return View("AdminAccounts", _dbData.Admins.Where(aa => aa.AdminRole == "Admin"));
        }

        public IActionResult ViewAdminDetails(int id)
        {

            AdminUsers? admin = _dbData.Admins.FirstOrDefault(aa => aa.AdminId == id);

            if (admin != null)
            {

                return View(admin);

            }
            return NotFound();

        }

        [HttpGet]
        public IActionResult UpdateAdminDetails(int id)
        {
            AdminUsers? admin = _dbData.Admins.FirstOrDefault(aa => aa.AdminId == id);

            if (admin != null)
                return View(admin);

            return NotFound();
        }

        [HttpPost]
        public IActionResult UpdateAdminDetails(AdminUsers adminChanges)
        {
            AdminUsers? admin = _dbData.Admins.FirstOrDefault(aa => aa.AdminId == adminChanges.AdminId);

            if (adminChanges != null)
            {
                admin.AdminEmail = adminChanges.AdminEmail;
                admin.AdminFirstName = adminChanges.AdminFirstName;
                admin.AdminMiddleName = adminChanges.AdminMiddleName;
                admin.AdminLastName = adminChanges.AdminLastName;
                admin.AdminContactNumber = adminChanges.AdminContactNumber;
                admin.AdminDateOfBirth = adminChanges.AdminDateOfBirth;
                admin.AdminAddress1 = adminChanges.AdminAddress1;
                admin.AdminAddress2 = adminChanges.AdminAddress2;
                admin.AdminCity = adminChanges.AdminCity;
                admin.AdminRegion = adminChanges.AdminRegion;
                admin.AdminPosition = adminChanges.AdminPosition;
                admin.AdminBranch = adminChanges.AdminBranch;
                _dbData.SaveChanges();
            }
            return View("AdminAccounts", _dbData.Admins.Where(aa => aa.AdminRole == "Admin"));

        }

        [HttpGet]
        public IActionResult AdminDeactivate(int id)
        {
            AdminUsers? admin = _dbData.Admins.FirstOrDefault(aa => aa.AdminId == id);

            if (admin != null)
                return View(admin);

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> AdminDeactivate(AdminUsers deactivateAdmin)
        {
            User? admin = _dbData.Users.FirstOrDefault(au => au.Email == deactivateAdmin.AdminEmail);
            AdminUsers? adminUser = _dbData.Admins.FirstOrDefault(aa => aa.AdminId == deactivateAdmin.AdminId);

            if (admin != null)
            {
                admin.Role = "Inactive";
                adminUser.AdminRole = "Inactive";
            }

            var oldRole = await _userManager.RemoveFromRoleAsync(admin, "Admin");
            var newRole = await _userManager.AddToRoleAsync(admin, "Inactive");

            return View("AdminAccounts", _dbData.Admins.Where(aa => aa.AdminRole == "Admin"));
        }

        public IActionResult InactiveAdmins()
        {
            return View("InactiveAdmins", _dbData.Admins.Where(aa => aa.AdminRole == "Inactive"));
        }

        [HttpGet]
        public IActionResult AdminReactivate(int id)
        {
            AdminUsers? admin = _dbData.Admins.FirstOrDefault(aa => aa.AdminId == id);

            if (admin != null)
                return View(admin);

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> AdminReactivate(AdminUsers reactivateAdmin)
        {
            User? admin = _dbData.Users.FirstOrDefault(au => au.Email == reactivateAdmin.AdminEmail);
            AdminUsers? adminUser = _dbData.Admins.FirstOrDefault(aa => aa.AdminId == reactivateAdmin.AdminId);

            if (admin != null)
            {
                admin.Role = "Admin";
                adminUser.AdminRole = "Admin";
            }

            var oldRole = await _userManager.RemoveFromRoleAsync(admin, "Inactive");
            var newRole = await _userManager.AddToRoleAsync(admin, "Admin");

            return View("InactiveAdmins", _dbData.Admins.Where(aa => aa.AdminRole == "Inactive"));
        }

    }
}
