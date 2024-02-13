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

        [Display(Name = "تاريخ الطلب")]
        public DateTime ReceiveBusinessDate { get; set; } = DateTime.Now;

        //[Display(Name = "اسم البرنامج")]
        //public string ProgramName { get; set; }

        //[Display(Name = "اسم المشروع")]
        //public string ProjectName { get; set; }

        [Display(Name = "المشروع")]
        public int? ProjectId { get; set; }
        public Project Project { get; set; }


        //[Display(Name = "استشاري ادارة المشاريع")]
        //public string ConsultantProjectManagementId { get; set; }

        //[Display(Name = "استشاري الاشراف")]
        //public string SupervisionConsultantId { get; set; }

        //[Display(Name = "المقاول")]
        //public string ContractorId { get; set; }

        [Display(Name = "التخصص")]
        public string Specialization { get; set; }


        [Display(Name = "رقم العمل")]
        public int WorkId { get; set; }

        [Display(Name = "توقيع المقاول")]
        public string ContractorSignature { get; set; }

        //[Display(Name = "المهندس المسؤول")]
        //public string ResponsibleEngineerId { get; set; }

        //[Display(Name = "توقيع المهندس المسؤول")]
        //public string ResponsibleEngineerSignature { get; set; }

        //[Display(Name = "المدير المسؤول")]
        //public string ResponsibleBossId { get; set; }

        //[Display(Name = "توقيع المدير المسؤول")]
        //public string ResponsibleBossSignature { get; set; }


        //[Display(Name = "ملاحظات الاستشاري")]
        //public string ConsultantDescription { get; set; }

        //حقل معتمد من قبل
        [Display(Name = "حالة التسليم")]
        public int? StatusId { get; set; }

        //[Display(Name = "اعتماد الاستشاري")]
        //public int ConsultantApprovalId { get; set; }

        //[Display(Name = "استلام الاستشاري")]
        //public int ReceivingTheConsultantId { get; set; }

        //[Display(Name = "استلام المقاول بعد الفحص")]
        //public int ReceivingTheContractorIdAfterCheck { get; set; }

        //حقل معتمد من قبل
        [Required(ErrorMessage = "النمودج مطلوب")]
        [Display(Name = "النموذج")]
        public string ReceiveBusinessSchedualTempletId { get; set; }
        public virtual ReceiveBusinessSchedualTemplet ReceiveBusinessSchedualTemplet { get; set; }


        public List<ReceiveBusinessTask> ReceiveBusinessTasks { get; set; } = new List<ReceiveBusinessTask>();


    }
}
