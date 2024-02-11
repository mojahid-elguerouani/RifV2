using FasDemo.Models;
using System.ComponentModel.DataAnnotations;

namespace FasDemo.ProjectModel
{
    public class BillType : Base
    {
        public string BillTypeId { get; set; }
        [Required]
        [Display(Name = "اسم البند")]
        public string Name { get; set; }
        [Display(Name = "وصف البند")]
        public string Description { get; set; }
    }
}
