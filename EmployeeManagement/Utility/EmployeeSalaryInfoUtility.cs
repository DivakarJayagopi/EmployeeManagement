using EmployeeManagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeManagement.Utility
{
    public class EmployeeSalaryInfoUtility
    {
        private EmployeeDbContext _dbContext = new EmployeeDbContext();

        public bool AddEmployeeSalaryInfo(EmployeeSalaryInfo employeeSalaryInfo)
        {
            bool status = false;
            try
            {
                _dbContext.EmployeeSalaryInfo.Add(employeeSalaryInfo);
                int count = _dbContext.SaveChanges();
                status = count > 0;
            }
            catch (Exception ex)
            {

            }
            return status;
        }

        public bool UpdateEmployeeSalaryInfo(EmployeeSalaryInfo employeeSalaryInfo)
        {
            bool status = false;
            try
            {
                var EmployeeSalary = _dbContext.EmployeeSalaryInfo.FirstOrDefault(item => item.EmployeeId == employeeSalaryInfo.EmployeeId);
                if (EmployeeSalary != null && !string.IsNullOrEmpty(EmployeeSalary.Id))
                {
                    EmployeeSalary.Basic = employeeSalaryInfo.Basic;
                    EmployeeSalary.DA = employeeSalaryInfo.DA;
                    EmployeeSalary.HRA = employeeSalaryInfo.HRA;
                    EmployeeSalary.MedicalAllowances = employeeSalaryInfo.MedicalAllowances;
                    EmployeeSalary.ConveyanceCharges = employeeSalaryInfo.ConveyanceCharges;
                    EmployeeSalary.SpecialAllowances = employeeSalaryInfo.SpecialAllowances;
                    EmployeeSalary.ModifiedDate = DateTime.Now;
                    int count = _dbContext.SaveChanges();
                    status = count > -1;
                }
            }
            catch (Exception)
            {

            }
            return status;
        }

        public EmployeeSalaryInfo GetEmployeeSalaryInfoByEmployeeId(string EmployeeId)
        {
            EmployeeSalaryInfo _employeeSalaryInfo = new  EmployeeSalaryInfo();
            try
            {
                var EmployeeSalary = _dbContext.EmployeeSalaryInfo.FirstOrDefault(item => item.EmployeeId == EmployeeId);
                if (EmployeeSalary != null && !string.IsNullOrEmpty(EmployeeSalary.Id))
                {
                    _employeeSalaryInfo = EmployeeSalary;
                }
            }
            catch (Exception)
            {

            }
            return _employeeSalaryInfo;
        }
    }
}