using System.ComponentModel.DataAnnotations;

namespace FasDemo.Models
{
    //division of a larger organization into parts with specific responsibility
    public class Department : Base
    {
        public string DepartmentId { get; set; }
        [Required]
        [Display(Name = "الادارة")]
        public string Name { get; set; }
        [Display(Name = "وصف الادارة")]
        public string Description { get; set; }
    }
}
