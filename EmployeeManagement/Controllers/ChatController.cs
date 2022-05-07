using EmployeeManagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmployeeManagement.Controllers
{
    public class ChatController : Controller
    {
        private EmployeeDbContext _dbContext = new EmployeeDbContext();
        // GET: Chat
        public ActionResult Index()
        {
            if (Session["IsAdmin"] == null)
                return RedirectToAction("Login", "Account");

            var EmployeeId = Session["EmployeeId"].ToString();
            var AllEmployees = _dbContext.Employees.Where(x=>x.Id != EmployeeId).ToList();
            ViewBag.Employees = AllEmployees;
            return View();
        }
    }
}