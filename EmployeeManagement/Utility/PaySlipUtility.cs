using EmployeeManagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeManagement.Utility
{
    public class PaySlipUtility
    {
        private EmployeeDbContext _dbContext = new EmployeeDbContext();

        public bool AddPaySlip(PaySlip paySlip)
        {
            bool status = false;
            try
            {
                _dbContext.PaySlip.Add(paySlip);
                int count = _dbContext.SaveChanges();
                status = count > 0;
            }
            catch (Exception)
            {

            }
            return status;
        }

        public PaySlip GetPaySlipByEmployeeId(string EmployeeId, string SelectedMonth)
        {
            PaySlip paySlip = new PaySlip();
            try
            {
                DateTime _selectedMonth = DateTime.Parse(SelectedMonth);
                var PaySlipInfo = _dbContext.PaySlip.Where(x => x.PaidMonth.Month == _selectedMonth.Month && x.EmployeeId == EmployeeId).OrderByDescending(x=>x.CreatedDate).FirstOrDefault();
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