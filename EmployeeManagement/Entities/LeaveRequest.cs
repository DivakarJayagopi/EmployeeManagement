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
        public string Type { get; set; }
        public string Title { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string Reason { get; set; }
        public string Status { get; set; }
    }
}