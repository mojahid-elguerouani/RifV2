using FasDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Domain.Entity
{
    public class FriendRequests
    {
        public ApplicationUser UserInfo { get; set; }
        public string RequestStatus { get; set; }
        public string RequestorUserID { get; set; }
        public string EndUserID { get; set; }
    }
}
