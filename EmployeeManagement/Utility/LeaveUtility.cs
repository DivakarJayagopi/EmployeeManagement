using EmployeeManagement.Entities;
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

        public bool UpdateLeaveRequest(string Id,string Status, string EmployeeId, string Reason)
        {
            bool status = false;
            try
            {
                var EmployeeInfo = _dbContext.LeaveRequest.FirstOrDefault(item => item.Id == Id && item.EmployeeId == EmployeeId);
                if (EmployeeInfo != null && !string.IsNullOrEmpty(EmployeeInfo.Id))
                {
                    EmployeeInfo.Status = Status;
                    EmployeeInfo.Reason = Reason;
                    int count = _dbContext.SaveChanges();
                    status = count > -1;
                }
            }
            catch (Exception)
            {

            }
            return status;
        }
    }
}