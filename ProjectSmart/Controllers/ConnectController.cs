using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProjectSmart.Data;
using ProjectSmart.Models;
using ProjectSmart.ViewModels;
using System.Diagnostics;

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
            var connects = _dbData.Connects.Include(c => c.Replies).ToList();
            return View(connects);
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
        [HttpGet]
        public IActionResult AddReply(int connectId)
        {
            Connect connect = _dbData.Connects.Find(connectId);
            if (connect == null)
            {
                return NotFound();
            } 
                Reply reply = new Reply(connectId);
                return View(reply);  
        }

        [HttpPost]
        public IActionResult AddReply(Reply newReply)
        {
            Debug.WriteLine("Adding new reply: {0}", newReply.ToString());
/*            if (!ModelState.IsValid)
            {
                TempData["message"] = "Not a Valid Reply.";
                return RedirectToAction("ViewConnect");
            }
*/
            try
            {
                _dbData.Replies.Add(newReply);
                int rowsAffected = _dbData.SaveChanges();

                if (rowsAffected > 0)
                {
                    TempData["message"] = "Reply added successfully.";
                    return RedirectToAction("ViewConnect");
                }
                else
                {
                    TempData["message"] = "Error adding reply. Please try again.";
                    return RedirectToAction("ViewConnect");
                }
            }
            catch (DbUpdateException ex)
            {
                TempData["message"] = "Error adding reply. Please try again.";
                return RedirectToAction("ViewConnect");
            }
        }
    }
}