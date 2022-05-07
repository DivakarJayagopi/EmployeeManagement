using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeManagement.Object
{
    public class ChatInfo
    {
        public string Id { get; set; }
        public string FromId { get; set; }
        public string ToId { get; set; }
        public int EmployeeCode { get; set; }
        public string EmployeeProfileImage { get; set; }
        public string EmployeeName { get; set; }
        public string Message { get; set; }
        public DateTime ChatDate { get; set; }
        public string ChatDateString { get; set; }
    }
}