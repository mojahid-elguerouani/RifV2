namespace ProjectManagment.Models
{
    using FasDemo.Models;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    public partial class ReceiveBusinessSchedualTemplet : Base
    {
        public string ReceiveBusinessSchedualTempletId { get; set; }

        [StringLength(250)]
        [Display(Name = "أسم النموذج")]
        public string ReceiveBusinessSchedualTempletName { get; set; }

        public List<ReceiveBusinessSchedual> ReceiveBusinessScheduals { get; set; } = new List<ReceiveBusinessSchedual>();
        public List<ReceiveBusiness> ReceiveBusiness { get; set; } = new List<ReceiveBusiness>();
    }
}
