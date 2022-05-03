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

            var CurrentDate = DateTime.Now.ToString("yyyy-MM");
            ViewBag.CurrentDate = CurrentDate;
            return View();
        }

        public ActionResult ViewPendingRequest()
        {
            if (Session["IsAdmin"] == null)
                return RedirectToAction("Login", "Account");

            var IsAdmin = (int)Session["IsAdmin"];
            var EmployeeId = Session["EmployeeId"].ToString();
            List<LeaveRequest> LeaveRequestList = new List<LeaveRequest>();

            var Listitems = (from lr in _dbContext.LeaveRequest
                             join emp in _dbContext.Employees on lr.EmployeeId equals emp.Id
                             where lr.Status.ToLower() == "pending"
                             && (IsAdmin == 0 ? lr.EmployeeId == EmployeeId : true)
                             select new Object.LeaveRequestInfo
                             {
                                 Id = lr.Id,
                                 EmployeeId = emp.Id,
                                 Name = emp.Name,
                                 Team = emp.Team,
                                 Title = lr.Title,
                                 Description = lr.Description,
                                 FromDate = lr.FromDate,
                                 ToDate = lr.ToDate,
                                 Comments = lr.Comments
                             }).ToList();
            
            ViewBag.LeaveRequest = Listitems;
            return View();
        }
    }
}