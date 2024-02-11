using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Domain.Entity
{
    public class FriendMapping
    {
        [Key]
        public int FriendMappingID { get; set; }
        public string RequestorUserID { get; set; }
        public string EndUserID { get; set; }
        public string RequestStatus { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public bool IsActive { get; set; }
    }
}
