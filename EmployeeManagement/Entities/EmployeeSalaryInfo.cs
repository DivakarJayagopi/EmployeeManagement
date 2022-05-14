using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeManagement.Entities
{
    public class EmployeeSalaryInfo : DateProperty
    {
        public string Id { get; set; }
        public string EmployeeId { get; set; }
        public long Basic { get; set; }
        public long DA { get; set; }
        public long HRA { get; set; }
        public long MedicalAllowances { get; set; }
        public long ConveyanceCharges { get; set; }
        public long SpecialAllowances { get; set; }
    }
}