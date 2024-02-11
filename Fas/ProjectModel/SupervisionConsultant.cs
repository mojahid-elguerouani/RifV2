namespace ProjectManagment.Models
{
    using FasDemo.Models;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class SupervisionConsultant : Base
    {
        public string SupervisionConsultantId { get; set; }

        [StringLength(50)]
        [Display(Name = "اسم استشاري إدارة الإشراف")]
        [Required(ErrorMessage = "فضلاُ أدخل اسم استشاري إدارة الإشراف")]
        public string SupervisionConsultantName { get; set; }

        public List<Project> Projects { get; set; } = new List<Project>();
    }
}
