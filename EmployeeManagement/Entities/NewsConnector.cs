using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeManagement.Entities
{
    public class NewsConnector : DateProperty
    {
        public string Id { get; set; }
        public string NewsId { get; set; }
        public string EmployeeId { get; set; }
    }
}