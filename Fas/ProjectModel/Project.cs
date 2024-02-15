namespace ProjectManagment.Models
{
    using FasDemo.Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    public partial class Project : Base
    {
        public int ProjectId { get; set; }

        [StringLength(50)]
        [Display(Name = "اسم المشروع")]
        public string ProjectName { get; set; }

        [StringLength(50)]
        [Display(Name = "كود المشروع")]
        [Required(ErrorMessage = "فضلاُ أدخل كود المشروع")]
        public string ProjectCode { get; set; }

        [Display(Name = "وصف المشروع")]
        public string ProjectDescription { get; set; }

        [Column(TypeName = "date")]
        [DataType(DataType.Date)]
        [Display(Name = "تاريخ البداية")]
        public DateTime? StartDate { get; set; }

        [DataType(DataType.Date)]
        [Column(TypeName = "date")]
        [Display(Name = "تارخ النهاية")]
        public DateTime? EndDate { get; set; }

        [Display(Name = "الميزانية التقديرية")]
        public int? EstimatedBudget { get; set; }

        [Display(Name = "الميزانية التعاقدية")]
        public int? ContractualBudget { get; set; }

        [Required]
        [Display(Name = "القطاع")]
        public string Sector { get; set; }

        [Required]
        [Display(Name = "المنطقة")]
        public string Region { get; set; }

        [Required]
        [Display(Name = "المقاول")]
        public string ContractorId { get; set; }
        public virtual Contractor Contractor { get; set; }

        [Required]
        [Display(Name = "استشاري الإشراف")]
        public string SupervisionConsultantId { get; set; }
        public virtual Employee SupervisionConsultant { get; set; }

        [Required]
        [Display(Name = "استشاري إدارة المشاريع")]
        public string ProjectManagementConsultantId { get; set; }
        public virtual Employee ProjectManagementConsultant { get; set; }

        [Display(Name = "حالةالمشروع")]
        public int? StatusId { get; set; }

        public List<ReceiveBusiness> ReceiveBusiness { get; set; } = new List<ReceiveBusiness>();
    }
}
