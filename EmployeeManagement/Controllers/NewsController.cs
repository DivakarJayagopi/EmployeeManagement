using EmployeeManagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmployeeManagement.Controllers
{
    public class NewsController : Controller
    {
        private EmployeeDbContext _dbContext = new EmployeeDbContext();
        // GET: New
        public ActionResult AddNews()
        {
            if (Session["IsAdmin"] == null)
                return RedirectToAction("Login", "Account");

            var EmployeesList = _dbContext.Employees.ToList();
            ViewBag.EmployeesList = EmployeesList;
            return View();
        }

        public ActionResult ListNews()
        {
            if (Session["IsAdmin"] == null)
                return RedirectToAction("Login", "Account");

            List<News> NewsList = new List<News>();
            if(Session["IsAdmin"].ToString() == "1")
            {
                NewsList = _dbContext.News.ToList();
            }
            else
            {
                string EmployeeId = Session["EmployeeId"].ToString();
                NewsList = (from ns in _dbContext.News
                            join nc in _dbContext.NewsConnector on ns.Id equals nc.NewsId
                            where nc.EmployeeId == EmployeeId
                            select ns).ToList();
            }
            ViewBag.NewsList = NewsList;
            return View();
        }
    }
}