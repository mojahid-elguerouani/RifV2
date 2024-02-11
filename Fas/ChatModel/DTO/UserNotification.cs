using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Domain.Entity.DTO
{
    public class UserNotificationDTO
    {
        [Key]
        public int NotificationID { get; set; }
        public string ToUserID { get; set; }
        public string FromUserID { get; set; }
        public string FromUser { get; set; }
        public string FromUserImage { get; set; }
        public string NotificationType { get; set; }
        public string Status { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public bool IsActive { get; set; }

    }
}
