namespace ProjectManagment.Models
{
    using FasDemo.Models;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class ProjectProgram : Base
    {
        public string ProjectProgramId { get; set; }

        [StringLength(50)]
        [Display(Name = "اسم البرنامج")]
        [Required(ErrorMessage = "فضلاُ أدخل اسم البرنامج")]
        public string ProjectProgramName { get; set; }

        public List<Project> Projects { get; set; } = new List<Project>();
    }
}
