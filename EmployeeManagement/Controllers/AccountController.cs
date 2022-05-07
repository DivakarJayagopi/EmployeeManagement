using EmployeeManagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmployeeManagement.Controllers
{
    public class AccountController : Controller
    {
        private EmployeeDbContext _dbContext = new EmployeeDbContext();
        // GET: Account
        public ActionResult Login()
        {
            Session.Abandon();
            return View();
        }

        public ActionResult Profile()
        {
            if (Session["IsAdmin"] == null)
                return RedirectToAction("Login", "Account");

            string EmployeeId = Session["EmployeeId"].ToString();
            var EmployeeInfo = _dbContext.Employees.Where(x => x.Id == EmployeeId).FirstOrDefault();
            if (EmployeeInfo != null && !string.IsNullOrEmpty(EmployeeInfo.Id))
            {
                ViewBag.EmployeeInfo = EmployeeInfo;
            }
            return View();
        }

        public ActionResult ChangePassword()
        {
            if (Session["IsAdmin"] == null)
                return RedirectToAction("Login", "Account");

            string EmployeeId = Session["EmployeeId"].ToString();
            var EmployeeInfo = _dbContext.Employees.Where(x => x.Id == EmployeeId).FirstOrDefault();
            if (EmployeeInfo != null && !string.IsNullOrEmpty(EmployeeInfo.Id))
            {
                ViewBag.Password = EmployeeInfo.Password;
            }
            return View();
        }

        public ActionResult ForgotPassword()
        {
            return View();
        }
    }
}