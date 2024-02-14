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


        [Display(Name = "العمل المطلوب فحصه")]
        public int WorkToBeExaminedId { get; set; }
        public virtual WorkToBeExamined WorkToBeExamined { get; set; }

        [Display(Name = "المبنى")]
        public int BuildingId { get; set; }
        public virtual Building Buildings { get; set; }

        [Display(Name = "الدور")]
        public int FloorId { get; set; }
        public virtual Floor Floor { get; set; }

        [Display(Name = "تاريخ الفحص المطلوب")]
        public int RequiredExaminationDateId { get; set; }
        public virtual RequiredExaminationDate RequiredExaminationDate { get; set; }

        [Display(Name = "اللوحات المعتمدة")]
        public int ApprovedPlatesId { get; set; }
        public virtual ApprovedPlates ApprovedPlates { get; set; }


        [Display(Name = "التوقيع")]
        public string Signature { get; set; }

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
