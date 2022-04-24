using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeManagement.Entities
{
    public class News : DateProperty
    {
        public string Id { get; set; }
        public string EmployeeId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Images { get; set; }
    }
}