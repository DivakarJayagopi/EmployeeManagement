using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeManagement.Object
{
    public class LeaveRequestInfo
    {
        public string Id { get; set; }
        public string EmployeeId { get; set; }
        public int EmployeeCode { get; set; }
        public string Name { get; set; }
        public string Team { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string FromDateString { get; set; }
        public string ToDateString { get; set; }
        public string Status { get; set; }
        public string Comments { get; set; }
    }
}