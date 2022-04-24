using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeManagement.Entities
{
    public class Chats : DateProperty
    {
        public string Id { get; set; }
        public string FromId { get; set; }
        public string ToId { get; set; }
        public string Message { get; set; }       
    }
}