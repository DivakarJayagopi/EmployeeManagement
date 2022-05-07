using EmployeeManagement.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmployeeManagement.Controllers
{
    public class EmployeeController : Controller
    {
        private EmployeeDbContext _dbContext = new EmployeeDbContext();
        // GET: Employee
        public ActionResult AddEmployee()
        {
            if (Session["IsAdmin"] == null)
                return RedirectToAction("Login", "Account");
            return View();
        }

        public ActionResult ListEmployee()
        {
            if (Session["IsAdmin"] == null)
                return RedirectToAction("Login", "Account");

            var AllEmployees = _dbContext.Employees.ToList();
            ViewBag.Employees = AllEmployees;
            return View();
        }
    }
}