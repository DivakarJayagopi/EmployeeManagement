using EmployeeManagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmployeeManagement.Controllers
{
    public class PaySlipController : Controller
    {
        private EmployeeDbContext _dbContext = new EmployeeDbContext();
        // GET: PaySlip
        public ActionResult AddPaySlip()
        {
            if (Session["IsAdmin"] == null)
                return RedirectToAction("Login", "Account");

            var EmployeesList = _dbContext.Employees.ToList();
            ViewBag.EmployeesList = EmployeesList;
            var CurrentDate = DateTime.Now.ToString("yyyy-MM");
            ViewBag.CurrentDate = CurrentDate;
            return View();
        }

        public ActionResult ViewSlip()
        {
            if (Session["IsAdmin"] == null)
                return RedirectToAction("Login", "Account");

            if(Session["IsAdmin"].ToString() == "1")
            {
                var EmployeesList = _dbContext.Employees.ToList();
                ViewBag.EmployeesList = EmployeesList;
                var CurrentDate = DateTime.Now.ToString("yyyy-MM");
                ViewBag.CurrentDate = CurrentDate;
                ViewBag.MinimumDate = "1900-01";
            }
            else
            {
                string EmployeeId = Session["EmployeeId"].ToString();
                var EmployeeInfo = _dbContext.Employees.Where(x=>x.Id == EmployeeId).FirstOrDefault();
                if(EmployeeInfo != null && !string.IsNullOrEmpty(EmployeeInfo.Id))
                {
                    var MinimumDate = EmployeeInfo.DOJ.ToString("yyyy-MM");
                    ViewBag.MinimumDate = MinimumDate;
                }
            }

            
            return View();
        }
    }
}