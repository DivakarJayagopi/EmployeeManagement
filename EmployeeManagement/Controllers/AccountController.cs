using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmployeeManagement.Controllers
{
    public class AccountController : Controller
    {
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
            return View();
        }

        public ActionResult ChangePassword()
        {
            if (Session["IsAdmin"] == null)
                return RedirectToAction("Login", "Account");
            return View();
        }
    }
}