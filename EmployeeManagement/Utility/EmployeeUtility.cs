using EmployeeManagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeManagement.Utility
{
    public class EmployeeUtility
    {
        private EmployeeDbContext _dbContext = new EmployeeDbContext();

        public bool AddEmployee(Employees employee)
        {
            bool status = false;
            try
            {
                _dbContext.Employees.Add(employee);
                int count = _dbContext.SaveChanges();
                status = count > 0;
            }
            catch (Exception)
            {

            }
            return status;
        }

        public bool UpdateEmployee(Employees employee)
        {
            bool status = false;
            try
            {
                var EmployeeInfo = _dbContext.Employees.FirstOrDefault(item => item.Id == employee.Id);
                if (EmployeeInfo != null && !string.IsNullOrEmpty(EmployeeInfo.Id))
                {
                    EmployeeInfo.Name = employee.Name;
                    EmployeeInfo.Email = employee.Email;
                    EmployeeInfo.MobileNumber = employee.MobileNumber;
                    EmployeeInfo.DOB = employee.DOB;
                    EmployeeInfo.Address = employee.Address;
                    EmployeeInfo.Team = employee.Team;
                    EmployeeInfo.Role = employee.Role;
                    EmployeeInfo.IsAdmin = employee.IsAdmin;
                    int count = _dbContext.SaveChanges();
                    status = count > -1;
                }
            }
            catch (Exception)
            {

            }
            return status;
        }

        public bool DeleteEmployee(string Id)
        {
            bool status = false;
            try
            {
                var EmployeeInfo = _dbContext.Employees.FirstOrDefault(item => item.Id == Id);
                if (EmployeeInfo != null && !string.IsNullOrEmpty(EmployeeInfo.Id))
                {
                    _dbContext.Employees.Remove(EmployeeInfo);
                    int count = _dbContext.SaveChanges();
                    status = count > 0;
                    if (status)
                    {
                        var NewsConnectorslist = _dbContext.NewsConnector.Where(x => x.EmployeeId == Id).ToList();
                        _dbContext.NewsConnector.RemoveRange(NewsConnectorslist);
                        count = _dbContext.SaveChanges();
                        status = count > 0;
                        if (status)
                        {
                            var LeaveRequestlist = _dbContext.LeaveRequest.Where(x => x.EmployeeId == Id).ToList();
                            _dbContext.LeaveRequest.RemoveRange(LeaveRequestlist);
                            count = _dbContext.SaveChanges();
                            status = count > 0;
                            if (status)
                            {
                                var PaySliplist = _dbContext.PaySlip.Where(x => x.EmployeeId == Id).ToList();
                                _dbContext.PaySlip.RemoveRange(PaySliplist);
                                count = _dbContext.SaveChanges();
                                status = count > 0;

                            }
                        }
                    }
                }
            }
            catch (Exception)
            {

            }
            return status;
        }

        public Employees GetEmployeeById(string Id)
        {
            Employees Employee = new Employees();
            try
            {
                var EmployeeInfo = _dbContext.Employees.FirstOrDefault(item => item.Id == Id);
                if (EmployeeInfo != null && !string.IsNullOrEmpty(EmployeeInfo.Id))
                {
                    Employee = EmployeeInfo;
                }
            }
            catch (Exception)
            {

            }
            return Employee;
        }

        public Employees ValidateLogin(string Email, string Password)
        {
            Employees Employee = new Employees();
            try
            {
                var EmployeeInfo = _dbContext.Employees.FirstOrDefault(item => item.Email == Email && item.Password == Password);
                if (EmployeeInfo != null && !string.IsNullOrEmpty(EmployeeInfo.Id))
                {
                    Employee = EmployeeInfo;
                }
            }
            catch (Exception)
            {

            }
            return Employee;
        }
    }
}