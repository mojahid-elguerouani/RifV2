using System.ComponentModel.DataAnnotations;

namespace FasDemo.Models
{
    //type of allowance
    public class AllowanceType : Base
    {
        public string AllowanceTypeId { get; set; }
        [Required]
        [Display(Name = "نوع البدل")]
        public string Name { get; set; }
        [Display(Name = "وصف البدل")]
        public string Description { get; set; }
    }
}
