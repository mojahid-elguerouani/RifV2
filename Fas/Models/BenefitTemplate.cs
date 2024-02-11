using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FasDemo.Models
{
    public class BenefitTemplate : Base
    {
        public string BenefitTemplateId { get; set; }
        [Required]
        [Display(Name = "اسم نموذج المزايا")]
        public string Name { get; set; }
        [Display(Name = "وصف نموذج المزايا")]
        public string Description { get; set; }

        //lines
        public List<BenefitTemplateLine> Lines { get; set; } = new List<BenefitTemplateLine>();
    }
}
