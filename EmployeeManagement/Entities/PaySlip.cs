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
        public int Basic { get; set; }
        public int DA { get; set; }
        public int HRA { get; set; }
        public int MedicalAllowances { get; set; }
        public int ConveyanceCharges { get; set; }
        public int SpecialAllowances { get; set; }
        public int IncomeTax { get; set; }
        public int EducationalCess { get; set; }
        public int LOP { get; set; }
    }
}