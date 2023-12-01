using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ProjectSmart.Data;
using ProjectSmart.Models;
using ProjectSmart.ViewModels;

namespace ProjectSmart.Controllers
{
    public class ConnectController : Controller
    {

        private readonly AppDbContext _dbData;
        public ConnectController(AppDbContext dbData)
        {
            _dbData = dbData;
        }

        [Authorize(Roles = "Scholar")]
        public IActionResult ViewConnect()
        {
            return View(_dbData.Connects);
        }
        [HttpGet]
        public IActionResult AddConnect()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddConnect(Connect newConnect)
        {
            if (!ModelState.IsValid)
            {
                return View();
                _dbData.Connects.Add(newConnect);
                return RedirectToAction("ViewConnect");
            }
            _dbData.Connects.Add(newConnect);
            _dbData.SaveChanges();
            return RedirectToAction("ViewConnect");
        }
    }
}