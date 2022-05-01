using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeManagement.Entities
{
    public class Employees : DateProperty
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public long MobileNumber { get; set; }
        public DateTime DOB { get; set; }
        public string Address { get; set; }
        public string Team { get; set; }
        public string Role { get; set; }
        public int IsAdmin { get; set; }
    }
}