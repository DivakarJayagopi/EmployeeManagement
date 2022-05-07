using EmployeeManagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmployeeManagement.Controllers
{
    public class DashboardController : Controller
    {
        private EmployeeDbContext _dbContext = new EmployeeDbContext();
        // GET: Dashboard
        public ActionResult Index()
        {
            if (Session["IsAdmin"] == null)
                return RedirectToAction("Login", "Account");
            var CurrentDate = DateTime.Now;

            if (Session["IsAdmin"].ToString() == "1")
            {
                var TotalEmployee = _dbContext.Employees.ToList();
                int TotalEmployeeCount = TotalEmployee.Count;
                var TodayBirthday = _dbContext.Employees.Where(x => x.DOB.Month == CurrentDate.Month && x.DOB.Day == CurrentDate.Day).ToList();
                int TodayBirthdayCount = TodayBirthday.Count;
                var TodayWorkAniversay = _dbContext.Employees.Where(x => x.DOJ.Month == CurrentDate.Month && x.DOJ.Day == CurrentDate.Day).ToList();
                int TodayWorkAniversayCount = TodayWorkAniversay.Count;
                int TodayLeavesCount = 0;
                List<string> LeaveEmployeeIds = new List<string>();
                List<Employees> LeaveEmployees = new List<Employees>();
                foreach (var Employee in TotalEmployee)
                {
                    var LeaveRequestList = _dbContext.LeaveRequest.Where(x => ((x.FromDate.Month == CurrentDate.Month && x.FromDate.Day == CurrentDate.Day && x.FromDate.Year == CurrentDate.Year) || (x.ToDate.Month == CurrentDate.Month && x.ToDate.Day == CurrentDate.Day && x.ToDate.Year == CurrentDate.Year) && x.Status.ToLower() == "accepted") && x.EmployeeId == Employee.Id).ToList();
                    if(LeaveRequestList != null && LeaveRequestList.Count > 0)
                    {
                        foreach(var LeaveRequest in LeaveRequestList)
                        {
                            var dates = new List<DateTime>();

                            for (var dt = LeaveRequest.FromDate; dt <= LeaveRequest.ToDate; dt = dt.AddDays(1))
                            {
                                dates.Add(dt);
                            }
                            bool IsExist = dates.Any(x => x.Day == CurrentDate.Day && x.Month == CurrentDate.Month && x.Year == CurrentDate.Year);
                            if (IsExist)
                            {
                                if (!LeaveEmployeeIds.Contains(LeaveRequest.EmployeeId))
                                {
                                    TodayLeavesCount++;
                                    LeaveEmployeeIds.Add(LeaveRequest.EmployeeId);
                                }
                            }
                        }
                    }
                }
                LeaveEmployees = _dbContext.Employees.Where(x => LeaveEmployeeIds.Contains(x.Id)).ToList();
                ViewBag.TotalEmployeeCount = TotalEmployeeCount;

                ViewBag.TodayBirthdayCount = TodayBirthdayCount;
                ViewBag.TodayBirthday = TodayBirthday;

                ViewBag.TodayLeavesCount = TodayLeavesCount;
                ViewBag.LeaveEmployees = LeaveEmployees;

                ViewBag.TodayWorkAniversayCount = TodayWorkAniversayCount;
                ViewBag.TodayWorkAniversay = TodayWorkAniversay;

                ViewBag.RemainingCount = TotalEmployeeCount - TodayBirthdayCount - TodayLeavesCount - TodayWorkAniversayCount;
            }
            else
            {
                string EmployeeId = Session["EmployeeId"].ToString();

                var TodayBirthday = _dbContext.Employees.Where(x => x.DOB.Month == CurrentDate.Month && x.DOB.Day == CurrentDate.Day).ToList();
                int TodayBirthdayCount = TodayBirthday.Count;
                var TodayWorkAniversay = _dbContext.Employees.Where(x => x.DOJ.Month == CurrentDate.Month && x.DOJ.Day == CurrentDate.Day).ToList();
                int TodayWorkAniversayCount = TodayWorkAniversay.Count;
                var TotalLeaveTaken = _dbContext.LeaveRequest.Where(x => ((x.FromDate.Year == CurrentDate.Year) || (x.ToDate.Year == CurrentDate.Year)) && x.EmployeeId == EmployeeId && x.Status.ToLower() == "accepted").ToList();
                int TotalCount = 0;
                foreach (var LeaveTaken in TotalLeaveTaken)
                {
                    var dates = new List<DateTime>();

                    for (var dt = LeaveTaken.FromDate; dt <= LeaveTaken.ToDate; dt = dt.AddDays(1))
                    {
                        bool IsExist = dates.Any(x => x.Day == CurrentDate.Day && x.Month == CurrentDate.Month && x.Year == CurrentDate.Year);
                        if (!IsExist)
                        {
                            TotalCount++;
                            dates.Add(dt);
                        }
                        
                    }
                }

                int TotalLeaveTakenCount = TotalLeaveTaken.Count;

                ViewBag.TodayBirthdayCount = TodayBirthdayCount;
                ViewBag.TodayBirthday = TodayBirthday;

                ViewBag.TodayWorkAniversayCount = TodayWorkAniversayCount;
                ViewBag.TodayWorkAniversay = TodayWorkAniversay;

                ViewBag.TotalLeaveTakenCount = TotalCount;
                ViewBag.TotalLeaveAvailableCount = 12 - TotalCount;
            }
            return View();
        }
    }
}