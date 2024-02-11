using FasDemo.Models;
using ProjectManagment.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FasDemo.ProjectModel.DTO
{
    public class DashBoerdV2DTO
    {
        public int ProjectId { get; set; }
        public int GroupId { get; set; }
        public string ProjectName { get; set; }
        public string GroupName { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; } 
        public int? totaltime { get; set; }
        public int? Duration { get; set; }
        public int? actual { get; set; }
        public int? CurrentCompleation { get; set; }
    }

    public class DashBoerdVM
    { 

        public int ProjectId { get; set; }

        public string ProjectName { get; set; } 
        public string ProjectTasksRatio { get; set; } 
        public virtual ICollection<DashBoerdV2DTO> DashBoerdV2DTOs   { get; set; } 
    }

}
