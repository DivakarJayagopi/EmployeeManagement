using EmployeeManagement.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;

namespace EmployeeManagement.Utility
{
    public class Common
    {
        private EmployeeDbContext _dbContext = new EmployeeDbContext();
        public bool SendEmail(string Email)
        {
            bool Status = false;
            try
            {
                var EmployeeInfo = _dbContext.Employees.Where(x => x.Email == Email).FirstOrDefault();
                if (EmployeeInfo != null && !string.IsNullOrEmpty(EmployeeInfo.Id))
                {
                    string to = Email; //To address    
                    string from = "divakarjayagopi@gmail.com"; //From address    
                    MailMessage message = new MailMessage(from, to);

                    string mailbody = "Hi, <i>" + EmployeeInfo.Name + "</i><br /><br /> Here is the password you have been using for <b>Kavitha Technologies</b> : <b><i>" + EmployeeInfo.Password + "</i></b> <br><br> Kinds Regards, <br/> Admin";
                    message.Subject = "Hi, " + EmployeeInfo.Name;
                    message.Body = mailbody;
                    message.BodyEncoding = Encoding.UTF8;
                    message.IsBodyHtml = true;
                    SmtpClient client = new SmtpClient("smtp.gmail.com", 587); //Gmail smtp    
                    System.Net.NetworkCredential basicCredential1 = new
                    System.Net.NetworkCredential("eamiltesting007@gmail.com", "Welcome@123");
                    client.EnableSsl = true;
                    client.UseDefaultCredentials = false;
                    client.Credentials = basicCredential1;
                    client.Send(message);
                    Status = true;
                }
            }
            catch (Exception ex) { 
            
            }
            return Status;
        }
    }
}