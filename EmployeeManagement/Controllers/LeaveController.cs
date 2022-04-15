using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmployeeManagement.Controllers
{
    public class LeaveController : Controller
    {
        // GET: Leave
        public ActionResult AddLeaveRequest()
        {
            if (Session["IsAdmin"] == null)
                return RedirectToAction("Login", "Account");
            return View();
        }

        public ActionResult ViewLeaveRequest()
        {
            if (Session["IsAdmin"] == null)
                return RedirectToAction("Login", "Account");
            return View();
        }
    }
}