using System;
using System.ComponentModel.DataAnnotations;

namespace ProjectManagment.Models
{
    public class Event
    {
        public int EventID { get; set; }

      
        [StringLength(100)]
        public string Subject { get; set; }

         
        [StringLength(200)]
        public string Description { get; set; }

        public DateTime Start { get; set; }

        public DateTime? End { get; set; }

        [StringLength(10)]
        public string ThemeColor { get; set; }

        public bool IsFullDay { get; set; }
        public string UserId { get; set; }
    }
}