using EmployeeManagement.Entities;
using EmployeeManagement.Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeManagement.Utility
{
    public class ChatUtility
    {
        private EmployeeDbContext _dbContext = new EmployeeDbContext();
        public bool AddChat(Chats chats)
        {
            bool status = false;
            try
            {
                _dbContext.Chats.Add(chats);
                int count = _dbContext.SaveChanges();
                status = count > 0;
            }
            catch (Exception ex)
            {

            }
            return status;
        }

        public List<ChatInfo> GetChatById(string FromId, string ToId)
        {
            List<ChatInfo> chats = new List<ChatInfo>();
            try
            {
                var ChatsInfo = (from cht in _dbContext.Chats
                                 join emp in _dbContext.Employees on cht.FromId equals emp.Id
                                 where (cht.FromId == FromId && cht.ToId == ToId) || 
                                 (cht.FromId == ToId && cht.ToId == FromId)
                                 select new ChatInfo
                                 {
                                     Id = cht.Id,
                                     ToId = emp.Id,
                                     FromId = cht.FromId,
                                     ChatDate = cht.CreatedDate,
                                     Message = cht.Message,
                                     EmployeeCode = emp.EmployeeCode,
                                     EmployeeName = emp.Name,
                                     EmployeeProfileImage = emp.ProfileImage
                                 }).ToList();
                if (ChatsInfo != null && ChatsInfo.Count > 0)
                {
                    ChatsInfo = ChatsInfo.Select(x =>
                    {
                        x.ChatDateString = x.ChatDate.ToString("dd-MMM-yy") + " [ " + x.ChatDate.ToString("H:mm") + " ]";
                        return x;
                    }).ToList();
                    chats = ChatsInfo;
                }
            }
            catch (Exception)
            {

            }
            return chats;
        }
    }
}