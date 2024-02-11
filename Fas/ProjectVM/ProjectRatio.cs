using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FasDemo.ProjectVM
{
    public partial class ProjectRatioVM
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? TotalTime { get; set; }
        public int? Duration { get; set; }
        public int? actual { get; set; }
        public int? CurrentCompleation { get; set; }
        public int ProjectId { get; set; }
        public int GroupId { get; set; }
        public string ProjectName { get; set; }
        public string GroupName { get; set; }
        public string ProjectAssignToId { get; set; }
        public decimal? Ratio { get; set; }

    }
}
