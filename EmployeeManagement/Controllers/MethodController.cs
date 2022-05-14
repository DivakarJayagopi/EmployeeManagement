using EmployeeManagement.Entities;
using EmployeeManagement.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
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
        ChatUtility chatUtility = new ChatUtility();
        EmployeeSalaryInfoUtility employeeSalaryInfoUtility = new EmployeeSalaryInfoUtility();
        // GET: Method
        public ActionResult ValidateUserLogin(string Email, string Password)
        {
            Dictionary<string, object> returnObject = new Dictionary<string, object>();
            try
            {
                var EmployeeInfo = employeeUtility.ValidateLogin(Email, Password);
                if(EmployeeInfo != null && !string.IsNullOrEmpty(EmployeeInfo.Id))
                {
                    Thread.Sleep(2000);
                    Session["EmployeeId"] = EmployeeInfo.Id;
                    Session["Name"] = EmployeeInfo.Name;
                    Session["ProfileImage"] = EmployeeInfo.ProfileImage;
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

        public ActionResult AddEmployee(string Name,string Email, long Number,string Password, string dob,string doj, string Address, string Team, string Role, int IsAdmin, EmployeeSalaryInfo employeeSalaryInfo)
        {
            Dictionary<string, object> returnObject = new Dictionary<string, object>();
            
            try
            {
                string EmployeeId = Guid.NewGuid().ToString();
                DateTime DOB = DateTime.Parse(dob);
                DateTime DOJ = DateTime.Parse(doj);
                Employees employee = new Employees
                {
                    Id = EmployeeId,
                    Name = Name,
                    ProfileImage = "/assets/img/avatar/avatar-1.png",
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
                    employeeSalaryInfo.Id = Guid.NewGuid().ToString();
                    employeeSalaryInfo.EmployeeId = EmployeeId;
                    employeeSalaryInfo.CreatedDate = DateTime.Now;
                    employeeSalaryInfo.ModifiedDate = DateTime.Now;

                    status = employeeSalaryInfoUtility.AddEmployeeSalaryInfo(employeeSalaryInfo);
                    if (status)
                    {
                        returnObject.Add("status", "success");
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

        public ActionResult UpdateEmployeeSalaryInfo(EmployeeSalaryInfo employeeSalaryInfo)
        {
            Dictionary<string, object> returnObject = new Dictionary<string, object>();
            try
            {
                bool status = employeeSalaryInfoUtility.UpdateEmployeeSalaryInfo(employeeSalaryInfo);
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

        public ActionResult GetEmployeeSalaryInfoByEmployeeId(string EmployeeId)
        {
            Dictionary<string, object> returnObject = new Dictionary<string, object>();
            try
            {
                var SalaryInfo = employeeSalaryInfoUtility.GetEmployeeSalaryInfoByEmployeeId(EmployeeId);
                if (SalaryInfo != null && !string.IsNullOrEmpty(SalaryInfo.EmployeeId))
                {
                    returnObject.Add("SalaryInfo", SalaryInfo);
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

        public ActionResult UpdateEmployeeInfo(string Name, long Number, string Email, string Address,string ProfileImage)
        {
            Dictionary<string, object> returnObject = new Dictionary<string, object>();
            try
            {
                string EmployeeId = Session["EmployeeId"].ToString();
                if (string.IsNullOrEmpty(ProfileImage))
                {
                    var EmployeeInfo = employeeUtility.GetEmployeeById(EmployeeId);
                    if (EmployeeInfo != null && !string.IsNullOrEmpty(EmployeeInfo.Id))
                    {
                        ProfileImage = EmployeeInfo.ProfileImage;
                    }
                }
                else
                {
                    ProfileImage = SaveImage(ProfileImage);
                }
                Employees employee = new Employees
                {
                    Id = EmployeeId,
                    Name = Name,
                    ProfileImage = ProfileImage,
                    Email = Email,
                    MobileNumber = Number,
                    Address = Address,
                    ModifiedDate = DateTime.Now
                };
                bool status = employeeUtility.UpdateEmployeeInfo(employee);
                if (status)
                {
                    var EmployeeInfo = employeeUtility.GetEmployeeById(EmployeeId);
                    if(EmployeeInfo != null && !string.IsNullOrEmpty(EmployeeInfo.Id))
                    {
                        Session["Name"] = EmployeeInfo.Name;
                        Session["ProfileImage"] = EmployeeInfo.ProfileImage;
                        returnObject.Add("EmployeeInfo", EmployeeInfo);
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

        public ActionResult GetExistingEmployeeInfo()
        {
            Dictionary<string, object> returnObject = new Dictionary<string, object>();
            try
            {
                var EmployeesList = employeeUtility.GetAllEmployees();
                if (EmployeesList != null)
                {
                    var EmailList = EmployeesList.Select(x => x.Email).ToList();
                    var MoblieNumberList = EmployeesList.Select(x => x.MobileNumber.ToString()).ToList();
                    returnObject.Add("status", "success");
                    returnObject.Add("EmailList", EmailList);
                    returnObject.Add("MoblieNumberList", MoblieNumberList);
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

        public ActionResult ForgotPassword(string Email)
        {
            Dictionary<string, object> returnObject = new Dictionary<string, object>();
            try
            {
                var EmployeeInfo = employeeUtility.GetEmployeeByEmail(Email);
                if (EmployeeInfo != null && !string.IsNullOrEmpty(EmployeeInfo.Id))
                {
                    Common commonClass = new Common();
                    var status = commonClass.SendEmail(Email);
                    if (status)
                    {
                        returnObject.Add("status", "success");
                    }
                    else
                    {
                        returnObject.Add("errorMessage", "Error on sending Email to " + Email);
                        returnObject.Add("status", "fail");
                    }
                }
                else
                {
                    returnObject.Add("errorMessage", "Email id Not found");
                    returnObject.Add("status", "fail");
                }
            }
            catch (Exception exe)
            {
                returnObject.Add("status", "fail");
            }
            return Json(new { message = returnObject }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpdatePassword(string Password)
        {
            Dictionary<string, object> returnObject = new Dictionary<string, object>();
            try
            {
                string EmployeeId = Session["EmployeeId"].ToString();
                bool status = employeeUtility.UpdatePassword(EmployeeId, Password);
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

        public ActionResult AddChat(string Message, string ToId)
        {
            Dictionary<string, object> returnObject = new Dictionary<string, object>();
            try
            {
                string FromId = Session["EmployeeId"].ToString();
                Chats chats = new Chats
                {
                    Id = Guid.NewGuid().ToString(),
                    Message = Message,
                    FromId = FromId,
                    ToId = ToId,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now
                };
                bool status = chatUtility.AddChat(chats);
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

        public ActionResult GetChatById(string ToId)
        {
            Dictionary<string, object> returnObject = new Dictionary<string, object>();
            try
            {
                string FromId = Session["EmployeeId"].ToString();

                var ChatsInfo = chatUtility.GetChatById(FromId, ToId);
                if (ChatsInfo != null)
                {
                    returnObject.Add("ChatsInfo", ChatsInfo);
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
    }
}