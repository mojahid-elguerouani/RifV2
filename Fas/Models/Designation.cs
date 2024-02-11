using System.ComponentModel.DataAnnotations;

namespace FasDemo.Models
{
    // official position of an employee
    public class Designation : Base
    {
        public string DesignationId { get; set; }
        [Required]
        [Display(Name = "اسم التعيين")]
        public string Name { get; set; }
        [Display(Name = "وصف التعيين")]
        public string Description { get; set; }
    }
}
