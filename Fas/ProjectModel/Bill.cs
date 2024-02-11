using FasDemo.Models;
using Microsoft.VisualBasic;
using ProjectManagment.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FasDemo.ProjectModel
{
    public class Bill : Base
    {
        public string BillId { get; set; }
        [Required]
        [Display(Name = "نوع البند")]
        public string BillTypeId { get; set; }

        public BillType BillType { get; set; }

        //[Required]
        [Display(Name = "وصف الفاتوة")]
        public string Description { get; set; }
        [Display(Name = "مبلغ الفاتوة")]
        public int Quantity { get; set; }
        [Required]
        [Display(Name = "تاريخ الفاتورة")]
        public DateTimeOffset BillDate { get; set; }
        [Required]
        [Display(Name = "تاريخ الاستحقاق")]
        public DateTimeOffset DueDate { get; set; }
        //public Customer CustomerBill { get; set; }
        //[Required]
        //[Display(Name = "العميل")]
        //public string CustomerBillId { get; set; }

        public Project ProjectBill { get; set; }
        [Required]
        [Display(Name = "المشروع")]
        public int ProjectBillId { get; set; }


    }
}
