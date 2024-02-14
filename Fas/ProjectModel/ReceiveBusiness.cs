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

        [Display(Name = "المشروع")]
        public int? ProjectId { get; set; }
        public Project Project { get; set; }

        [Display(Name = "التخصص")]
        public string Specialization { get; set; }

        //التخصصات

        [Required(ErrorMessage = "مدني؟")]
        public bool IsCivil { get; set; } = false;

        [Required(ErrorMessage = "معماري؟")]
        public bool IsArchitectural { get; set; } = false;

        [Required(ErrorMessage = "ميكانيكا؟")]
        public bool IsMechanics { get; set; } = false;

        [Required(ErrorMessage = "كهرباء؟")]
        public bool IsElectricity { get; set; } = false;

        [Required(ErrorMessage = "زراعي؟")]
        public bool IsAgricultural { get; set; } = false;

        [Required(ErrorMessage = "أخرى؟")]
        public bool IsOthers { get; set; } = false;


        [Display(Name = "رقم العمل")]
        public int WorkId { get; set; }


        [Display(Name = "توقيع المقاول")]
        public string ContractorSignature { get; set; }

        //حقل معتمد من قبل
        [Display(Name = "حالة التسليم")]
        public int? StatusId { get; set; }

        //حقل معتمد من قبل
        [Required(ErrorMessage = "النمودج مطلوب")]
        [Display(Name = "النموذج")]
        public string ReceiveBusinessSchedualTempletId { get; set; }
        public virtual ReceiveBusinessSchedualTemplet ReceiveBusinessSchedualTemplet { get; set; }


        public List<ReceiveBusinessTask> ReceiveBusinessTasks { get; set; } = new List<ReceiveBusinessTask>();


    }
}
