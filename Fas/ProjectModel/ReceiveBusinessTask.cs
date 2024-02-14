namespace ProjectManagment.Models
{
    using FasDemo.Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    public partial class ReceiveBusinessTask : Base
    {
        public int ReceiveBusinessTaskId { get; set; }

        [Display(Name = "المهمة")]
        public string TaskName { get; set; }
        [Display(Name = "الترتيب")]
        public int? TaskOrder { get; set; }

        public int TaskId { get; set; }

        [Display(Name = "مسندة الى")]
        public string ReceiveBusinessAssignToId { get; set; }
        public ApplicationUser ReceiveBusinessAssignTo { get; set; }

        [Display(Name = "البريد الالكتروني الدي سيتلقى الاشعار")]
        public string toEmail { get; set; }

        [Display(Name = "نشطة")]
        public bool? IsActive { get; set; }

        [Display(Name = "الحالة")]
        public int? StatusId { get; set; }

        [Display(Name = "الاعتماد")]
        public bool? IsApproved { get; set; }

        [Display(Name = "تعتمد بـ")]
        public string ApprovedById { get; set; }
        public ApplicationUser ApprovedBy { get; set; }

        [Display(Name = "المهمة تابعة")]
        public int? TaskParentId { get; set; }

        [Display(Name = "نسبة الانجاز")]
        [Range(0, 100, ErrorMessage = "فضلاً ادخل قيمة بين  1  و  100")]
        public int? Compleation { get; set; }

        public DateTime? ApproveDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        [Display(Name = "الطلب")]
        public int ReceiveBusinessId { get; set; }
        public virtual ReceiveBusiness ReceiveBusiness { get; set; }

        public List<ReceiveBusinessTaskLog> ReceiveBusinessTaskLogs { get; set; } = new List<ReceiveBusinessTaskLog>();

        public ReceiveBusinessTask TaskParent { get; set; }
        public ICollection<ReceiveBusinessTask> Children { get; set; }

        public int? GroupId { get; set; }
    }
}
