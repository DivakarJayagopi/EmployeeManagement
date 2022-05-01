using EmployeeManagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmployeeManagement.Controllers
{
    public class LeaveController : Controller
    {
        private EmployeeDbContext _dbContext = new EmployeeDbContext();
        // GET: Leave
        public ActionResult AddLeaveRequest()
        {
            if (Session["IsAdmin"] == null)
                return RedirectToAction("Login", "Account");
            var CurrentDate = DateTime.Now.ToString("yyyy-MM-dd");
            ViewBag.CurrentDate = CurrentDate;
            return View();
        }

        public ActionResult ViewLeaveRequest()
        {
            if (Session["IsAdmin"] == null)
                return RedirectToAction("Login", "Account");

            var LeaveRequest = _dbContext.LeaveRequest.ToList();
            ViewBag.LeaveRequest = LeaveRequest;
            return View();
        }

        public ActionResult ViewPendingRequest()
        {
            if (Session["IsAdmin"] == null)
                return RedirectToAction("Login", "Account");

            var LeaveRequest = _dbContext.LeaveRequest.Where(x=>x.Status.ToLower() == "pending").ToList();
            ViewBag.LeaveRequest = LeaveRequest;
            return View();
        }
    }
}