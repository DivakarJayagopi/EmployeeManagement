using EmployeeManagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeManagement.Utility
{
    public class NewsUtility
    {
        private EmployeeDbContext _dbContext = new EmployeeDbContext();
        public bool AddNews(News news, List<string> EmployeeIds)
        {
            bool status = false;
            try
            {
                _dbContext.News.Add(news);
                int count = _dbContext.SaveChanges();
                status = count > 0;
                if (status)
                {
                    if(EmployeeIds != null && EmployeeIds.Count > 0)
                    {
                        List<Employees> employees = new List<Employees>();
                        if(EmployeeIds.FirstOrDefault().ToString().ToLower() == "all")
                        {
                            employees = _dbContext.Employees.ToList();
                        }
                        else
                        {
                            employees = _dbContext.Employees.Where(x => EmployeeIds.Contains(x.Id)).ToList();
                        }
                        foreach(var Employee in employees)
                        {
                            NewsConnector newsConnector = new NewsConnector
                            {
                                Id = Guid.NewGuid().ToString(),
                                EmployeeId = Employee.Id,
                                NewsId = news.Id,
                                CreatedDate = DateTime.Now,
                                ModifiedDate = DateTime.Now
                            };
                            _dbContext.NewsConnector.Add(newsConnector);
                            count = _dbContext.SaveChanges();
                            status = count > 0;
                        }
                    }
                }
            }
            catch (Exception)
            {

            }
            return status;
        }

        public bool DeleteNewsById(string Id)
        {
            bool status = false;
            try
            {
                var NewsList = _dbContext.News.Where(x => x.Id == Id).ToList();
                _dbContext.News.RemoveRange(NewsList);
                int count = _dbContext.SaveChanges();
                status = count > 0;
                if (status)
                {
                    var NewsConnectorslist = _dbContext.NewsConnector.Where(x=>x.NewsId == Id).ToList();
                    _dbContext.NewsConnector.RemoveRange(NewsConnectorslist);
                    count = _dbContext.SaveChanges();
                    status = count > 0;
                }
            }
            catch (Exception)
            {

            }
            return status;
        }

        public PaySlip GetNewsById(string EmployeeId, string SelectedMonth)
        {
            PaySlip paySlip = new PaySlip();
            try
            {
                DateTime _selectedMonth = DateTime.Parse(SelectedMonth);
                var PaySlipInfo = _dbContext.PaySlip.Where(x => x.PaidMonth.Month == _selectedMonth.Month && x.EmployeeId == EmployeeId).FirstOrDefault();
                if (PaySlipInfo != null && !string.IsNullOrEmpty(PaySlipInfo.Id))
                {
                    paySlip = PaySlipInfo;
                }
            }
            catch (Exception)
            {

            }
            return paySlip;
        }
    }
}