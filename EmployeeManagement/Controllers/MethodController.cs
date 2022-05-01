using EmployeeManagement.Entities;
using EmployeeManagement.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmployeeManagement.Controllers
{
    public class MethodController : Controller
    {
        EmployeeUtility employeeUtility = new EmployeeUtility();
        LeaveUtility LeaveUtility = new LeaveUtility();
        // GET: Method
        public ActionResult ValidateUserLogin(string IsAdmin)
        {
            Dictionary<string, object> returnObject = new Dictionary<string, object>();
            try
            {
                if(IsAdmin.ToLower() == "true")
                {
                    Session["Name"] = "Super Admin";
                    Session["Email"] = "admin@gmail.com";
                    Session["IsAdmin"] = "1";
                }
                else
                {
                    Session["Name"] = "Kavitha";
                    Session["Email"] = "kavitha@gmail.com";
                    Session["IsAdmin"] = "0";
                }
                returnObject.Add("status", "success");
            }
            catch (Exception exe)
            {

            }
            return Json(new { message = returnObject }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddEmployee(string Name,string Email, long Number, string dob, string Address, string Team, string Role, int IsAdmin)
        {
            Dictionary<string, object> returnObject = new Dictionary<string, object>();
            
            try
            {
                DateTime DOB = DateTime.Parse(dob);
                Employees employee = new Employees
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = Name,
                    Email = Email,
                    MobileNumber = Number,
                    DOB = DOB,
                    Address = Address,
                    Team = Team,
                    Role = Role,
                    IsAdmin = IsAdmin,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now
                };
                bool status = employeeUtility.AddEmployee(employee);
                if (status)
                {
                    returnObject.Add("status", "success");
                }
                else
                {
                    returnObject.Add("status", "fail");
                }
            }
            catch (Exception exe)
            {
                returnObject.Add("status", "fail");
            }
            return Json(new { message = returnObject }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpdateEmployee(string Id,string Name, string Email, long Number, string dob, string Address, string Team, string Role, int IsAdmin)
        {
            Dictionary<string, object> returnObject = new Dictionary<string, object>();
            try
            {
                DateTime DOB = DateTime.Parse(dob);
                Employees employee = new Employees
                {
                    Id = Id,
                    Name = Name,
                    Email = Email,
                    MobileNumber = Number,
                    DOB = DOB,
                    Address = Address,
                    Team = Team,
                    Role = Role,
                    IsAdmin = IsAdmin,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now
                };
                bool status = employeeUtility.UpdateEmployee(employee);
                if (status)
                {
                    var EmployeeInfo = employeeUtility.GetEmployeeById(Id);
                    if(EmployeeInfo != null && !string.IsNullOrEmpty(EmployeeInfo.Id))
                    {
                        returnObject.Add("EmployeeInfo", EmployeeInfo);
                        returnObject.Add("DateObject", EmployeeInfo.DOB.ToString("yyyy-MM-dd"));
                    }
                    returnObject.Add("status", "success");
                }
                else
                {
                    returnObject.Add("status", "fail");
                }
            }
            catch (Exception exe)
            {
                returnObject.Add("status", "fail");
            }
            return Json(new { message = returnObject }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteEmployee(string Id)
        {
            Dictionary<string, object> returnObject = new Dictionary<string, object>();
            try
            {
                bool status = employeeUtility.DeleteEmployee(Id);
                if (status)
                {
                    returnObject.Add("status", "success");
                }
                else
                {
                    returnObject.Add("status", "fail");
                }
            }
            catch (Exception exe)
            {
                returnObject.Add("status", "fail");
            }
            return Json(new { message = returnObject }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetEmployeeById(string Id)
        {
            Dictionary<string, object> returnObject = new Dictionary<string, object>();
            try
            {
                Employees EmployeeInfo = employeeUtility.GetEmployeeById(Id);
                if (EmployeeInfo != null && !string.IsNullOrEmpty(EmployeeInfo.Id))
                {
                    returnObject.Add("status", "success");
                    returnObject.Add("EmployeeInfo", EmployeeInfo);
                    returnObject.Add("DateObject", EmployeeInfo.DOB.ToString("yyyy-MM-dd"));
                }
                else
                {
                    returnObject.Add("status", "fail");
                }
            }
            catch (Exception exe)
            {
                returnObject.Add("status", "fail");
            }
            return Json(new { message = returnObject }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddLeaveRequest(string Type, string Title, string FromDate, string ToDate, string Reason, string Status)
        {
            Dictionary<string, object> returnObject = new Dictionary<string, object>();
            try
            {
                DateTime _fromDate = DateTime.Parse(FromDate);
                DateTime _toDate = DateTime.Parse(ToDate);
                var EmployeeId = "";// Session["Id"].ToString();
                LeaveRequest leaveRequest = new LeaveRequest
                {
                    Id = Guid.NewGuid().ToString(),
                    Type = Type,
                    Title = Title,
                    FromDate = _fromDate,
                    ToDate = _toDate,
                    Reason = Reason,
                    Status = Status,
                    EmployeeId = EmployeeId,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now
                };
                bool status = LeaveUtility.AddLeaveRequest(leaveRequest);
                if (status)
                {
                    returnObject.Add("status", "success");
                }
                else
                {
                    returnObject.Add("status", "fail");
                }
            }
            catch (Exception exe)
            {
                returnObject.Add("status", "fail");
            }
            return Json(new { message = returnObject }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpdateLeaveRequest(string Id,string Status,string EmployeeId,string Reason)
        {
            Dictionary<string, object> returnObject = new Dictionary<string, object>();
            try
            {
                bool status = LeaveUtility.UpdateLeaveRequest(Id,Status,EmployeeId, Reason);
                if (status)
                {
                    returnObject.Add("status", "success");
                }
                else
                {
                    returnObject.Add("status", "fail");
                }
            }
            catch (Exception exe)
            {
                returnObject.Add("status", "fail");
            }
            return Json(new { message = returnObject }, JsonRequestBehavior.AllowGet);
        }
    }
}