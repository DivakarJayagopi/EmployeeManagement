using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeManagement.Entities
{
    public class LeaveRequest : DateProperty
    {
        public string Id { get; set; }
        public string EmployeeId { get; set; }
        public DateTime LeaveDate { get; set; }
        public string Reason { get; set; }
    }
}