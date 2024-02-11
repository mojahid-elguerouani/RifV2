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

        public string AssignToId { get; set; }
        public int? ApprovedBy { get; set; }

        public int? TaskParentId { get; set; }

        public int? Duration { get; set; }

        public int? Compleation { get; set; }

        public DateTime? ApproveDate { get; set; }

        public DateTime? UpdateDate { get; set; }
        [DisplayFormat(DataFormatString = "{0: MM/dd/yyyy}")]
        public DateTime? StarDateActual { get; set; }
        [DisplayFormat(DataFormatString = "{0: MM/dd/yyyy}")]
        public DateTime? EndDateActual { get; set; }

        public string ProfilePicture { get; set; }
        public virtual ICollection<ReceiveBusinessTaskLog> ReceiveBusinessTaskLog { get; set; }

        public decimal ReceiveBusinessTasksSum { get; set; }
        public decimal ReceiveBusinessTasksSum100 { get; set; }
        public decimal ReceiveBusinessTasksRatio { get; set; }
    }
}
