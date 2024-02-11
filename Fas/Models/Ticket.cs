using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FasDemo.Models
{
    //ticket entity
    public class Ticket : Base
    {
        public string TicketId { get; set; }

        [Required]
        [Display(Name = "اسم التذكرة")]
        public string TicketName { get; set; }

        [Required]
        [Display(Name = "وصف")]
        public string Description { get; set; }

        [Display(Name = "تم حلها?")]
        public bool IsSolve { get; set; }
        
        [Display(Name = "ملاحظة الحل")]
        public string SolutionNote { get; set; }

        public TicketType TicketType { get; set; }

        [Required]
        [Display(Name = "نوع التذكرة")]
        public string TicketTypeId { get; set; }

        [Required]
        [Display(Name = "تاريخ الطلب")]
        public DateTimeOffset SubmitDate { get; set; }

        public Employee OnBehalf { get; set; }

        [Required]
        [Display(Name = "نيابة عن")]
        public string OnBehalfId { get; set; }
        
        public Employee Agent { get; set; }

       
        [Display(Name = "الوكيل")]
        public string AgentId { get; set; }

        public Ticket ParentTicketThread { get; set; }

        [Display(Name = "موضوع تذكرة السابقة")]
        public string ParentTicketThreadId { get; set; }
    }
}
