using FasDemo.Models;
using ProjectManagment.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FasDemo.ProjectModel.DTO
{
    public class ReceiveBusinessTaskDto
    {
        public int ReceiveBusinessTaskId { get; set; }

        public int ReceiveBusinessId { get; set; }

        public string TaskName { get; set; }

        public int? TaskOrder { get; set; }

        public int? TaskId { get; set; }

        public string Firstname { get; set; }
        public ApplicationUser AssignTo { get; set; }


        public bool? IsActive { get; set; }

        public int? StatusId { get; set; }

        public bool? IsApproved { get; set; }

        public bool? IsSigned { get; set; }

        public string AssignToId { get; set; }
        public int? ApprovedBy { get; set; }

        public int? TaskParentId { get; set; }

        public int? Duration { get; set; }

        public int? Compleation { get; set; }

        public DateTime? ApproveDate { get; set; }

        public DateTime? UpdateDate { get; set; }


        //FROM RECEIVEBUSINESS TABLE

        [Display(Name = "تاريخ الطلب")]
        public DateTime ReceiveBusinessDate { get; set; } = DateTime.Now;


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



        //[DisplayFormat(DataFormatString = "{0: MM/dd/yyyy}")]
        //public DateTime? StarDateActual { get; set; }
        //[DisplayFormat(DataFormatString = "{0: MM/dd/yyyy}")]
        //public DateTime? EndDateActual { get; set; }

        public string ProfilePicture { get; set; }
        public virtual ICollection<ReceiveBusinessTaskLog> ReceiveBusinessTaskLog { get; set; }

        //public decimal ReceiveBusinessTasksSum { get; set; }
        //public decimal ReceiveBusinessTasksSum100 { get; set; }
        //public decimal ReceiveBusinessTasksRatio { get; set; }
    }
}
