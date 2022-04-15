using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmployeeManagement.Controllers
{
    public class PaySlipController : Controller
    {
        // GET: PaySlip
        public ActionResult AddPaySlip()
        {
            if (Session["IsAdmin"] == null)
                return RedirectToAction("Login", "Account");
            return View();
        }

        public ActionResult ViewSlip()
        {
            if (Session["IsAdmin"] == null)
                return RedirectToAction("Login", "Account");
            return View();
        }
    }
}