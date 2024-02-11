namespace ProjectManagment.Models
{
    using FasDemo.Models;
    using Microsoft.AspNetCore.Mvc.ViewEngines;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    public partial class ReceiveBusiness : Base
    {
        public int ReceiveBusinessId { get; set; }

        [Display(Name = "رقم التسلسل")]
        public int SerialNumber { get; set; }

        [Display(Name = "رقم المراجعة")]
        public int ReviewNumber { get; set; }

        [Display(Name = "نوع طلب الاعتماد")]
        public int TypeOfAccreditationRequest  { get; set; }

        [Display(Name = "التخصص")]
        public int Specialization { get; set; }

        [Display(Name = "المشروع")]
        public int? ProjectId { get; set; }
        public Project Project { get; set; }



        [Display(Name = "تاريخ الطلب")]
        public DateTime ReceiveBusinessDate { get; set; } = DateTime.Now;

        [Display(Name = "حالة الطلب")]
        public int? StatusId { get; set; }

        [Required(ErrorMessage = "النمودج مطلوب")]
        [Display(Name = "النموذج")]
        public string ReceiveBusinessSchedualTempletId { get; set; }
        public virtual ReceiveBusinessSchedualTemplet ReceiveBusinessSchedualTemplet { get; set; }

        public List<ReceiveBusinessTask> ReceiveBusinessTasks { get; set; } = new List<ReceiveBusinessTask>();
    }
}
