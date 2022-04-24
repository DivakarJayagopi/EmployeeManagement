using EmployeeManagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmployeeManagement.Controllers
{
    public class MethodController : Controller
    {
        private EmployeeDbContext _dbContext = new EmployeeDbContext();
        //public MethodController(EmployeeDbContext dbContext)
        //{
        //    _dbContext = dbContext;
        //}
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
    }
}