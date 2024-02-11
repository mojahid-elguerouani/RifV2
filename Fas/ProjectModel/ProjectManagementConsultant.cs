namespace ProjectManagment.Models
{
    using FasDemo.Models;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class ProjectManagementConsultant : Base
    {
        public string ProjectManagementConsultantId { get; set; }

        [StringLength(50)]
        [Display(Name = "اسم استشاري إدارة المشاريع")]
        [Required(ErrorMessage = "فضلاُ أدخل اسم استشاري إدارة المشاريع")]
        public string ProjectManagementConsultantName { get; set; }

        public List<Project> Projects { get; set; } = new List<Project>();
    }
}
