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

        //تخصص اخر

        [Display(Name = "أكتب تخصص اخر")]
        public string OtherSpecialization { get; set; }


        [Display(Name = "بيان المبنى")]
        public string BuildingStatement { get; set; }

        [Display(Name = "ملاحظات المبنى")]
        public string BuildingComments { get; set; }

        [Display(Name = "بيان العمل المطلوب فحصه")]
        public string WorkToBeExaminedStatement { get; set; }

        [Display(Name = "ملاحظات العمل المطلوب فحصه")]
        public string WorkToBeExaminedComments { get; set; }

        [Display(Name = "بيان الدور")]
        public string FloorStatement { get; set; }

        [Display(Name = "ملاحظات الدور")]
        public string FloorComments { get; set; }

        [Display(Name = "بيان تاريخ الفحص المطلوب")]
        public string RequiredExaminationDateStatement { get; set; }

        [Display(Name = "ملاحظات تاريخ الفحص المطلوب")]
        public string RequiredExaminationDateComments { get; set; }


        [Display(Name = "بيان اللوحات المعتمدة")]
        public string ApprovedPlatesStatement { get; set; }

        [Display(Name = "ملاحظات اللوحات المعتمدة")]
        public string ApprovedPlatesComments { get; set; }

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
