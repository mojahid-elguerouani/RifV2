using System;
using System.ComponentModel.DataAnnotations;

namespace FasDemo.ProjectModel
{
    public partial class ProjectTaskVM
    {
        public int ProjectTaskId { get; set; }

        public int ProjectId { get; set; }
        [Display(Name = "المهمة")]
        public string TaskName { get; set; }
        [Display(Name = "الترتيب")]
        public int? TaskOrder { get; set; }

        public int? TaskId { get; set; }
        [Display(Name = "مسنده الى")]
        public string ProjectAssignToId { get; set; }
        //[Display(Name = "تاريخ البداية")]
        //[DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
        //public DateTime? StarDate { get; set; }
        //[Display(Name = "تاريخ النهاية")]
        //[DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
        //public DateTime? EndDate { get; set; }
        [Display(Name = "تاريخ البداية")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime? StartDateActual { get; set; }
        [Display(Name = "تاريخ النهاية")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime? EndDateActual { get; set; }

        [Display(Name = "نشطة")]
        public bool? IsActive { get; set; }
        [Display(Name = "الحالة")]
        public int? StatusId { get; set; }
        [Display(Name = "الاعتماد")]
        public bool? IsApproved { get; set; }
        [Display(Name = "تعتمد بـ")]
        public string ApprovedById { get; set; }
        [Display(Name = "المهمة تابعة")]
        public int? TaskParentId { get; set; }
        [Display(Name = "المدة")]
        public int? Duration { get; set; }
        [Display(Name = "نسبة الانجاز")]
        public int? Compleation { get; set; }
        [Display(Name = "المشروع")]
        public string ProjectName { get; set; }
        [Display(Name = "العميل")]

        public string  CustomerName { get; set; }
        public int ProgreesType { get; set; }
        [Display(Name = "الوقت المتبقي")]
        public int? Remaintime { get; set; } 

        public string ProfilePicture { get; set; }
        public string TaskUserName { get; set; }

    }



}
