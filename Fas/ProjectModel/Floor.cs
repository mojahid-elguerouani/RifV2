namespace ProjectManagment.Models
{
    using FasDemo.Models;
    using Microsoft.AspNetCore.Mvc.ViewEngines;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    public partial class Floor
    {
        [Key]
        public int WorkId { get; set; }
        [Display(Name = "البيان")]
        public string Statement { get; set; }
        [Display(Name = "ملاحظات")]
        public string Comments { get; set; }

        public List<ReceiveBusiness> ReceiveBusiness { get; set; } = new List<ReceiveBusiness>();
    }

}
