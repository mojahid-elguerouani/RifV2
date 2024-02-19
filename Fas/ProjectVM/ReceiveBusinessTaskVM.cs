using System;
using System.ComponentModel.DataAnnotations;

namespace FasDemo.ProjectModel
{
    public partial class ReceiveBusinessTaskVM
    {
        public int ReceiveBusinessTaskId { get; set; }
        public int ReceiveBusinessId { get; set; }

        [Display(Name = "المهمة")]
        public string TaskName { get; set; }

        [Display(Name = "الترتيب")]
        public int? TaskOrder { get; set; }

        public int? TaskId { get; set; }
        [Display(Name = "مسنده الى")]
        public string ReceiveBusinessAssignToId { get; set; }


        [Display(Name = "نشطة")]
        public bool? IsActive { get; set; }

        [Display(Name = "الحالة")]
        public int? StatusId { get; set; }

        [Display(Name = "الاعتماد")]
        public bool? IsApproved { get; set; }

        [Display(Name = "التوقيع")]
        public bool? IsSigned { get; set; }

        [Display(Name = "تعتمد بـ")]
        public string ApprovedById { get; set; }

        [Display(Name = "المهمة تابعة")]
        public int? TaskParentId { get; set; }

        [Display(Name = "نسبة الانجاز")]
        public int? Compleation { get; set; }

        //FROM PROJECTS TABLE

        [Display(Name = "اسم المشروع")]
        public string ProjectName { get; set; }

        [Display(Name = "كود المشروع")]
        public string ProjectCode { get; set; }

        //FROM CONTRACTORS TABLE

        [Display(Name = "اسم المقاول")]
        public string ContractorName { get; set; }

        [Display(Name = "كود المقاول")]
        public string ContractorCode { get; set; }

        //FROM RECEIVEBUSINESS TABLE

        [Display(Name = "تاريخ الطلب")]
        public DateTime ReceiveBusinessDate { get; set; } = DateTime.Now;


        [Display(Name = "مدني")]
        public bool? IsCivil { get; set; } = false;

        [Display(Name = "معماري")]
        public bool? IsArchitectural { get; set; } = false;

        [Display(Name = "ميكانيكا")]
        public bool? IsMechanics { get; set; } = false;

        [Display(Name = "كهرباء")]
        public bool? IsElectricity { get; set; } = false;

        [Display(Name = "زراعي")]
        public bool? IsAgricultural { get; set; } = false;

        [Display(Name = "أخرى")]
        public bool? IsOthers { get; set; } = false;

        [Display(Name = "تخصص اخر")]
        public string OtherSpecialization { get; set; }

        [Display(Name = "رقم التسلسل")]
        public int? SerialNumber { get; set; }

        [Display(Name = "رقم المراجعة")]
        public int? ReviewNumber { get; set; }


        public string ProfilePicture { get; set; }
        public string TaskUserName { get; set; }


        //public int ProgreesType { get; set; }

        //[Display(Name = "الوقت المتبقي")]
        //public int? Remaintime { get; set; }


    }



}
