namespace ProjectManagment.Models
{
    using FasDemo.Models;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Contractor : Base
    {
        public string ContractorId { get; set; }

        [StringLength(50)]
        [Display(Name = "اسم المقاول")]
        [Required(ErrorMessage = "فضلاُ أدخل اسم العميل")]
        public string ContractorName { get; set; }

        [StringLength(50)]
        [Display(Name = "كود المقاول")]
        [Required(ErrorMessage = "فضلاُ أدخل كود المقاول")]
        public string ContractorCode { get; set; }

        [StringLength(50)]
        [Display(Name = "ملفات المقاول")]
        public string LogoUrl { get; set; }

        [StringLength(50)]
        [Display(Name = "البريد الالكتروني")]
        public string Email { get; set; }

        [StringLength(50)]
        [Required]
        [Display(Name = "رقم الجوال")]
        public string PhoneNumber { get; set; }

        [StringLength(50)]
        [Display(Name = "اسم المستخدم")]
        public string UserName { get; set; }
        [StringLength(50)]
        [Display(Name = "كلمة المرور")]
        public string Password { get; set; }

        [Display(Name = "اسم المقاول")]
        public string ContractorUserId { get; set; }
        public ApplicationUser ContractorUser { get; set; }

        public List<ContractorImage> ContractorImages { get; set; } = new List<ContractorImage>();
        public List<Project> ContractorProjects { get; set; } = new List<Project>();


    }
}
