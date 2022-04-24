using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmployeeManagement.Controllers
{
    public class EmployeeController : Controller
    {
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
            return View();
        }
    }
}