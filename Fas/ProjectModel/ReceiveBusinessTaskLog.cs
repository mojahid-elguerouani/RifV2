namespace ProjectManagment.Models
{
    using FasDemo.Models;
    using FasDemo.ProjectModel;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    public partial class ReceiveBusinessTaskLog
    {
        public int ReceiveBusinessTaskLogId { get; set; }

        [Display(Name = "الملاحظه")]
        public string ReceiveBusinessTaskComment { get; set; }
        [Display(Name = "التاريخ")]
        public DateTime? CreatedOn { get; set; }

        public int ReceiveBusinessTaskId { get; set; }
        public virtual ReceiveBusinessTask ReceiveBusinessTask { get; set; }


        public string ReceiveBusinessUserId { get; set; }
        public ApplicationUser ReceiveBusinessUser { get; set; }

        public List<ReceiveBusinessTaskLogImage> ReceiveBusinessTaskLogImages { get; set; } = new List<ReceiveBusinessTaskLogImage>();
    }
}
