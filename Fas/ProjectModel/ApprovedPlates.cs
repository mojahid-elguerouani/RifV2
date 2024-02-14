﻿using System.ComponentModel.DataAnnotations;

namespace Fas.ProjectModel
{
    public class ApprovedPlates
    {
        [Key]
        public int WorkId { get; set; }
        [Display(Name = "البيان")]
        public string Statement { get; set; }
        [Display(Name = "ملاحظات")]
        public string Comments { get; set; }
    }

}
