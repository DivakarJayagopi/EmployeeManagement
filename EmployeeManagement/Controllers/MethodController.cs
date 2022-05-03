using EmployeeManagement.Entities;
using EmployeeManagement.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmployeeManagement.Controllers
{
    public class MethodController : Controller
    {
        EmployeeUtility employeeUtility = new EmployeeUtility();
        LeaveUtility leaveUtility = new LeaveUtility();
        PaySlipUtility paySlipUtility = new PaySlipUtility();
        NewsUtility newsUtility = new NewsUtility();
        // GET: Method
        public ActionResult ValidateUserLogin(string Email, string Password)
        {
            Dictionary<string, object> returnObject = new Dictionary<string, object>();
            try
            {
                var EmployeeInfo = employeeUtility.ValidateLogin(Email, Password);
                if(EmployeeInfo != null && !string.IsNullOrEmpty(EmployeeInfo.Id))
                {
                    Session["EmployeeId"] = EmployeeInfo.Id;
                    Session["Name"] = EmployeeInfo.Name;
                    Session["Email"] = EmployeeInfo.Email;
                    Session["IsAdmin"] = EmployeeInfo.IsAdmin;

                    returnObject.Add("status", "success");
                }
                else
                {
                    returnObject.Add("errorMessage", "Invalid credentials");
                    returnObject.Add("status", "fail");
                }
            }
            catch (Exception exe)
            {
                returnObject.Add("errorMessage", exe.Message);
            }
            return Json(new { message = returnObject }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddEmployee(string Name,string Email, long Number,string Password, string dob,string doj, string Address, string Team, string Role, int IsAdmin)
        {
            Dictionary<string, object> returnObject = new Dictionary<string, object>();
            
            try
            {
                DateTime DOB = DateTime.Parse(dob);
                DateTime DOJ = DateTime.Parse(doj);
                Employees employee = new Employees
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = Name,
                    Email = Email,
                    MobileNumber = Number,
                    Password = Password,
                    DOB = DOB,
                    DOJ = DOJ,
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
                returnObject.Add("errorMessage", exe.Message);
            }
            return Json(new { message = returnObject }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpdateEmployee(string Id,string Name, string Email, long Number, string dob, string doj, string Address, string Team, string Role, int IsAdmin)
        {
            Dictionary<string, object> returnObject = new Dictionary<string, object>();
            try
            {
                DateTime DOB = DateTime.Parse(dob);
                DateTime DOJ = DateTime.Parse(doj);
                Employees employee = new Employees
                {
                    Id = Id,
                    Name = Name,
                    Email = Email,
                    MobileNumber = Number,
                    DOB = DOB,
                    DOJ = DOJ,
                    Address = Address,
                    Team = Team,
                    Role = Role,
                    IsAdmin = IsAdmin,
                    ModifiedDate = DateTime.Now
                };
                bool status = employeeUtility.UpdateEmployee(employee);
                if (status)
                {
                    var EmployeeInfo = employeeUtility.GetEmployeeById(Id);
                    if(EmployeeInfo != null && !string.IsNullOrEmpty(EmployeeInfo.Id))
                    {

                        returnObject.Add("EmployeeInfo", EmployeeInfo);
                        returnObject.Add("DateObject_dob", EmployeeInfo.DOB.ToString("yyyy-MM-dd"));
                        returnObject.Add("DateObject_doj", EmployeeInfo.DOJ.ToString("yyyy-MM-dd"));
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
                returnObject.Add("errorMessage", exe.Message);
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
                    returnObject.Add("DateObject_dob", EmployeeInfo.DOB.ToString("yyyy-MM-dd"));
                    returnObject.Add("DateObject_doj", EmployeeInfo.DOJ.ToString("yyyy-MM-dd"));
                }
                else
                {
                    returnObject.Add("status", "fail");
                }
            }
            catch (Exception exe)
            {
                returnObject.Add("errorMessage", exe.Message);
                returnObject.Add("status", "fail");
            }
            return Json(new { message = returnObject }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddLeaveRequest(string Type, string Title, string FromDate, string ToDate, string Description, string Status)
        {
            Dictionary<string, object> returnObject = new Dictionary<string, object>();
            try
            {
                DateTime _fromDate = DateTime.Parse(FromDate);
                DateTime _toDate = DateTime.Parse(ToDate);
                var EmployeeId = Session["EmployeeId"].ToString();
                LeaveRequest leaveRequest = new LeaveRequest
                {
                    Id = Guid.NewGuid().ToString(),
                    Type = Type,
                    Title = Title,
                    FromDate = _fromDate,
                    ToDate = _toDate,
                    Description = Description,
                    Status = Status,
                    EmployeeId = EmployeeId,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now
                };
                bool status = leaveUtility.AddLeaveRequest(leaveRequest);
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
                returnObject.Add("errorMessage", exe.Message);
            }
            return Json(new { message = returnObject }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpdateLeaveRequest(string Id,string Status,string EmployeeId,string Reason)
        {
            Dictionary<string, object> returnObject = new Dictionary<string, object>();
            try
            {
                bool status = leaveUtility.UpdateLeaveRequest(Id,Status,EmployeeId, Reason);
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
                returnObject.Add("errorMessage", exe.Message);
            }
            return Json(new { message = returnObject }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetLeaveRquestByDate(string SelectedDate)
        {
            Dictionary<string, object> returnObject = new Dictionary<string, object>();
            try
            {
                string EmployeeId = string.Empty;
                var LeaveRequestList = leaveUtility.GetLeaveRquestByDate(SelectedDate, EmployeeId);
                if (LeaveRequestList != null)
                {
                    returnObject.Add("status", "success");
                    returnObject.Add("LeaveRequestList", LeaveRequestList);
                }
                else
                {
                    returnObject.Add("status", "fail");
                }
            }
            catch (Exception exe)
            {
                returnObject.Add("status", "fail");
                returnObject.Add("errorMessage", exe.Message);
            }
            return Json(new { message = returnObject }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddpaySlip(string EmployeeId, long Basic, long DA, long HRA, long MedicalAllowances, long ConveyanceCharges, long SpecialAllowances, long IncomeTax, long EducationalCess, long LOP, string PaidMonth)
        {
            Dictionary<string, object> returnObject = new Dictionary<string, object>();
            try
            {
                DateTime _paidMonth = DateTime.Parse(PaidMonth);
                PaySlip paySlip = new PaySlip
                {
                    Id = Guid.NewGuid().ToString(),
                    EmployeeId = EmployeeId,
                    Basic = Basic,
                    DA = DA,
                    HRA = HRA,
                    MedicalAllowances = MedicalAllowances,
                    ConveyanceCharges = ConveyanceCharges,
                    SpecialAllowances = SpecialAllowances,
                    IncomeTax = IncomeTax,
                    EducationalCess = EducationalCess,
                    LOP = LOP,
                    PaidMonth = _paidMonth,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now
                };
                bool status = paySlipUtility.AddPaySlip(paySlip);
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
                returnObject.Add("errorMessage", exe.Message);
            }
            return Json(new { message = returnObject }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetpaySlipByEmployeeId(string EmployeeId, string SelectedMonth)
        {
            Dictionary<string, object> returnObject = new Dictionary<string, object>();
            try
            {
                if (string.IsNullOrEmpty(EmployeeId))
                {
                    EmployeeId = Session["EmployeeId"].ToString();
                }
                var PaySlipInfo = paySlipUtility.GetPaySlipByEmployeeId(EmployeeId,SelectedMonth);
                if (PaySlipInfo != null)
                {
                    returnObject.Add("status", "success");
                    returnObject.Add("PaySlipInfo", PaySlipInfo);
                    var EmployeeInfo = employeeUtility.GetEmployeeById(EmployeeId);
                    if (EmployeeInfo != null)
                    {
                        returnObject.Add("EmployeeInfo", EmployeeInfo);
                        returnObject.Add("EmployeeDateOfJoining", EmployeeInfo.DOJ.ToString("dd-MM-yyyy"));
                    }
                }
                else
                {
                    returnObject.Add("status", "fail");
                }
            }
            catch (Exception exe)
            {
                returnObject.Add("status", "fail");
                returnObject.Add("errorMessage", exe.Message);
            }
            return Json(new { message = returnObject }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult AddNews(string Title, string Description, string image, List<string> EmployeeIds)
        {
            Dictionary<string, object> returnObject = new Dictionary<string, object>();
            try
            {
                var imagePath = "";
                if (!string.IsNullOrEmpty(image))
                {
                    imagePath = SaveImage(image); 
                }
                News news = new News
                {
                    Id = Guid.NewGuid().ToString(),
                    Title = Title,
                    Description = Description,
                    Images = imagePath,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now
                };
                var status = newsUtility.AddNews(news, EmployeeIds);
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
                returnObject.Add("errorMessage", exe.Message);
            }
            return Json(new { message = returnObject }, JsonRequestBehavior.AllowGet);
        }

        public string SaveImage(string Image)
        {
            string imagePath = "";
            string folder = System.Web.HttpContext.Current.Server.MapPath("~/") + "App_Assests\\Images";
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
            var Base64Type = Image.Split(',')[0].Split('/')[1].Split(';')[0];
            Image = Image.Split(',')[1];
            string ImageGuid = Guid.NewGuid().ToString();
            string filePath = folder + "\\" + ImageGuid + "." + Base64Type;
            var imageBytes = Convert.FromBase64String(Image);
            System.IO.File.WriteAllBytes(filePath, imageBytes);
            imagePath = "/App_Assests/Images" + "/" + ImageGuid + "." + Base64Type;
            return imagePath;
        }

        public ActionResult DeleteNewsById(string Id)
        {
            Dictionary<string, object> returnObject = new Dictionary<string, object>();
            try
            {
                bool status = newsUtility.DeleteNewsById(Id);
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