using System;
using System.ComponentModel.DataAnnotations;

namespace FasDemo.Models
{
    public class Base
    {
        public string CreatedById { get; set; }
        [Display(Name = "انشأ بواسطة")]
        public ApplicationUser CreatedBy { get; set; }
        [Display(Name = "أنشئت في")]
        public DateTime CreatedAtUtc { get; set; }
        public string UpdatedById { get; set; }
        [Display(Name = "تم التحديث بواسطة")]
        public ApplicationUser UpdatedBy { get; set; }
        [Display(Name = "تم التحديث في")]
        public DateTime UpdatedAtUtc { get; set; }
    }
}
