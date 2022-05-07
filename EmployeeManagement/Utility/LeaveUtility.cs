using EmployeeManagement.Entities;
using EmployeeManagement.Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeManagement.Utility
{
    public class LeaveUtility
    {
        private EmployeeDbContext _dbContext = new EmployeeDbContext();

        public bool AddLeaveRequest(LeaveRequest leaveRequest)
        {
            bool status = false;
            try
            {
                _dbContext.LeaveRequest.Add(leaveRequest);
                int count = _dbContext.SaveChanges();
                status = count > 0;
            }
            catch (Exception)
            {

            }
            return status;
        }

        public bool UpdateLeaveRequest(string Id, string Status, string EmployeeId, string Reason)
        {
            bool status = false;
            try
            {
                var EmployeeInfo = _dbContext.LeaveRequest.Where(item => item.Id == Id && item.EmployeeId == EmployeeId).FirstOrDefault();
                if (EmployeeInfo != null && !string.IsNullOrEmpty(EmployeeInfo.Id))
                {
                    EmployeeInfo.Status = Status;
                    EmployeeInfo.Comments = Reason;
                    EmployeeInfo.ModifiedDate = DateTime.Now;
                    int count = _dbContext.SaveChanges();
                    status = count > -1;
                }
            }
            catch (Exception)
            {

            }
            return status;
        }

        public List<LeaveRequestInfo> GetLeaveRquestByDate(string Date, string EmployeeId)
        {
            List<LeaveRequestInfo> leaveRequestInfo = new List<LeaveRequestInfo>();
            try
            {
                DateTime _selectedDate = DateTime.Parse(Date);
                var Listitems = (from lr in _dbContext.LeaveRequest
                                 join emp in _dbContext.Employees on lr.EmployeeId equals emp.Id
                                 where 
                                 lr.FromDate.Month == _selectedDate.Month &&
                                 lr.FromDate.Year == _selectedDate.Year &&
                                 ((!string.IsNullOrEmpty(EmployeeId)) ? lr.EmployeeId == EmployeeId : true)
                                 select new LeaveRequestInfo
                                 {
                                     Id = lr.Id,
                                     EmployeeId = emp.Id,
                                     EmployeeCode = emp.EmployeeCode,
                                     Name = emp.Name,
                                     Team = emp.Team,
                                     Title = lr.Title,
                                     Description = lr.Description,
                                     FromDate = lr.FromDate,
                                     ToDate = lr.ToDate,
                                     //FromDateString = lr.FromDate.ToString("dd-MM-yyyy"),
                                     //ToDateString = lr.ToDate.ToString("dd-MM-yyyy"),
                                     Status = lr.Status,
                                     Comments = lr.Comments
                                 }).ToList();
                if (Listitems != null && Listitems.Count > 0)
                {
                    Listitems = Listitems.Select(x =>
                    {
                        x.FromDateString = x.FromDate.ToString("dd-MM-yyyy");
                        x.ToDateString = x.ToDate.ToString("dd-MM-yyyy");
                        return x;
                    }).ToList();
                    leaveRequestInfo = Listitems;
                }
            }
            catch (Exception)
            {

            }
            return leaveRequestInfo;
        }
    }
}