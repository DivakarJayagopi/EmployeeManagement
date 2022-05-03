using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeManagement.Entities
{
    public class PaySlip : DateProperty
    {
        public string Id { get; set; }
        public string EmployeeId { get; set; }
        public long Basic { get; set; }
        public long DA { get; set; }
        public long HRA { get; set; }
        public long MedicalAllowances { get; set; }
        public long ConveyanceCharges { get; set; }
        public long SpecialAllowances { get; set; }
        public long IncomeTax { get; set; }
        public long EducationalCess { get; set; }
        public long LOP { get; set; }
        public DateTime PaidMonth { get; set; }
    }
}