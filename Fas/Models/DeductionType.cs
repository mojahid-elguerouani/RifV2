using System.ComponentModel.DataAnnotations;

namespace FasDemo.Models
{
    public class DeductionType : Base
    {
        public string DeductionTypeId { get; set; }
        [Required]
        [Display(Name = "انواع الحسميات")]
        public string Name { get; set; }
        [Display(Name = "وصف الحسميات")]
        public string Description { get; set; }
    }
}
