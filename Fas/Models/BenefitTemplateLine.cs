using System.ComponentModel.DataAnnotations;

namespace FasDemo.Models
{
    public class BenefitTemplateLine : Base
    {
        public string BenefitTemplateLineId { get; set; }
        [Required]
        [Display(Name = "نموذج المزايا")]
        public string BenefitTemplateId { get; set; }
        public BenefitTemplate BenefitTemplate { get; set; }
        [Required]
        [Display(Name = "الوصف")]
        public string Description { get; set; }

        //allowances
        public AllowanceType AllowanceType { get; set; }
        [Display(Name = "البدل")]
        public string AllowanceTypeId { get; set; }

        //deductions (should be minus)
        public DeductionType DeductionType { get; set; }
        [Display(Name = "المستقطع")]
        public string DeductionTypeId { get; set; }

        //amount of money
        [Required]
        [Display(Name = "المبلغ المستحق")]
        public decimal Amount { get; set; }
    }
}
