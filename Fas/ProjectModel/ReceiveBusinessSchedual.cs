namespace ProjectManagment.Models
{
    using FasDemo.Models;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    public class ReceiveBusinessSchedual : Base
    {
        public int ReceiveBusinessSchedualId { get; set; }

        [Display(Name = "النموذج")]
        public string ReceiveBusinessSchedualTempletId { get; set; }
        public virtual ReceiveBusinessSchedualTemplet ReceiveBusinessSchedualTemplet { get; set; }

        [Display(Name = "المهمة")]
        public string TaskName { get; set; }
        [Display(Name = "الترتيب")]
        public int? TaskOrder { get; set; }

        [Display(Name = "مسندة الى")]
        public string ReceiveBusinessAssignToId { get; set; }
        public ApplicationUser ReceiveBusinessAssignTo { get; set; }

        [Display(Name = "البريد الالكتروني الدي سيتلقى الاشعار")]
        public string toEmail { get; set; }


        [Display(Name = "تعتمد على")]
        public int? ReceiveBusinessSchedualParentId { get; set; }
        public ReceiveBusinessSchedual ReceiveBusinessSchedualParent { get; set; }

        [Display(Name = "تعتمد من")]
        public string ReceiveBusinessApprovedById { get; set; }
        public ApplicationUser ReceiveBusinessApprovedBy { get; set; }
    }
}
