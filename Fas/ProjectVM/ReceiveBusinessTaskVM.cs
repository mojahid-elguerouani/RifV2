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

        [Display(Name = "تعتمد بـ")]
        public string ApprovedById { get; set; }

        [Display(Name = "المهمة تابعة")]
        public int? TaskParentId { get; set; }

        [Display(Name = "نسبة الانجاز")]
        public int? Compleation { get; set; }

        //FROM PROJECTS TABLE

        [Display(Name = "المشروع")]
        public string ProjectName { get; set; }

        //FROM CONTRACTORS TABLE

        [Display(Name = "المقاول")]
        public string ContractorName { get; set; }

        //FROM RECEIVEBUSINESS TABLE

        [Display(Name = "تاريخ الطلب")]
        public DateTime ReceiveBusinessDate { get; set; } = DateTime.Now;

        [Display(Name = "رقم التسلسل")]
        public int SerialNumber { get; set; }

        [Display(Name = "رقم المراجعة")]
        public int ReviewNumber { get; set; }

        [Display(Name = "نوع طلب الاعتماد")]
        public int TypeOfAccreditationRequest { get; set; }

        [Display(Name = "التخصص")]
        public int Specialization { get; set; }


        public string ProfilePicture { get; set; }
        public string TaskUserName { get; set; }


        //public int ProgreesType { get; set; }

        //[Display(Name = "الوقت المتبقي")]
        //public int? Remaintime { get; set; }


    }



}
