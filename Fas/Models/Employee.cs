using System;
using System.ComponentModel.DataAnnotations;

namespace FasDemo.Models
{
    public class Employee : Base
    {
        public string EmployeeId { get; set; }

        //basic info

        [Required]
        [Display(Name = "الاسم الاول")]
        public string FirstName { get; set; }
        [Display(Name = "الأسم الاخير")]
        public string LastName { get; set; }
        [Required]
        [Display(Name = "الجنس")]
        public string Gender { get; set; }
        [Required]
        [Display(Name = "تاريخ الميلاد")]
        public DateTime DateOfBirth { get; set; }
        [Required]
        [Display(Name = "محل الميلاد")]
        public string PlaceOfBirth { get; set; }
        [Display(Name = "الحالة الاجتماعية")]
        public string MaritalStatus { get; set; }
        [Required]
        [Display(Name = "البريد الالكتروني")]
        public string Email { get; set; }
        [Required]
        [Display(Name = "الجوال")]
        public string Phone { get; set; }
        [Required]
        [Display(Name = "العنوان الاول")]
        public string Address1 { get; set; }
        [Display(Name = "العنوان الثاني")]
        public string Address2 { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        [Display(Name = "المنطقة")]
        public string StateProvince { get; set; }
        [Display(Name = "رمز المنطقة / رمز بريدي")]
        public string ZipCode { get; set; }
        [Display(Name = "البلد")]
        public string Country { get; set; }
        [Display(Name = "الصورة")]
        public string ProfilePicture { get; set; }


        //staff info

        [Display(Name = "رقم الموظف")]
        public string EmployeeIDNumber { get; set; }
        [Required]
        [Display(Name = "المنصب")]
        public string DesignationId { get; set; }
        public Designation Designation { get; set; }
        [Required]
        [Display(Name = "الادارة")]
        public string DepartmentId { get; set; }
        public Department Department { get; set; }
        [Required]
        [Display(Name = "تاريخ الالتحاق")]
        public DateTime JoiningDate { get; set; }
        [Display(Name = "تاريخ ترك العمل")]
        public DateTime? LeavingDate { get; set; }
        [Display(Name = "المدير")]
        public string SupervisorId { get; set; }
        public Employee Supervisor { get; set; }


        //salary info

        [Required]
        [Display(Name = "الراتب الاساسي")]
        public Decimal BasicSalary { get; set; }
        [Required]
        [Display(Name = "مغادرات غير مدفوعة الاجر")]
        public Decimal UnpaidLeavePerDay { get; set; }
        [Display(Name = "نموذج البدلات")]
        public string BenefitTemplateId { get; set; }
        public BenefitTemplate BenefitTemplate { get; set; }


        //bank account info

        [Required]
        [Display(Name = "عنوان الحساب")]
        public string AccountTitle { get; set; }
        [Required]
        [Display(Name = "سم البنك")]
        public string BankName { get; set; }
        [Required]
        [Display(Name = "رقم الحساب")]
        public string AccountNumber { get; set; }
        [Display(Name = "سويفت كود / ايبان")]
        public string SwiftCode { get; set; }

        //system user account
        [Display(Name = "المستخدم")]
        public string SystemUserId { get; set; }       
        public ApplicationUser SystemUser { get; set; }
    }
}
