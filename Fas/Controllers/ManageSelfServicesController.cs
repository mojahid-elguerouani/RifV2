using Fas.Services.Security;
using FasDemo.Data;
using FasDemo.SurveyModel.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using System.Linq;
using System.Security.Claims;

namespace Fas.Controllers
{
    public class ManageSelfServicesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _config;
        public ManageSelfServicesController(ApplicationDbContext context,IConfiguration config)
        {
            _context = context;
            _config = config;
        }
        public IActionResult Index(string message)
        {
            if(message != null)
            {
                ViewBag.message=message;
            }
            return View();
        }


        [HttpPost]
        public IActionResult CreateNewPass(string pass,string confirmPass)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            string message = "";
            if(pass.Length<=10)
            {
                if(pass==confirmPass)
                {
                    var user = _context.Employee.Where(x=>x.SystemUserId==userId).FirstOrDefault();
                    var key = _config.GetSection("KeyForEncrypt").Value;
                    string passAfterEncrypt = AesOperation.EncryptString(key, pass);
                    user.Signature = passAfterEncrypt;
                    _context.Employee.Update(user);
                    _context.SaveChanges();
                    message = "تم انشاء كلمة مرور بنجاح";
                }
                else
                {
                    message = "يوجد خطأ في تأكيد كلمة المرور";
                }
            }
            else
            {
                message = "الرجاء ادخال كلمة مرور اقل من 10 احرف";
            }

            return RedirectToAction("Index", "ManageSelfServices", new { message = message });
        }


        [HttpPost]
        public IActionResult EditPass(string oldPass,string newPass,string confirmNewPass)
        {
            string message = "";
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user = _context.Employee.Where(x => x.SystemUserId == userId).FirstOrDefault();
            var userPass = user.Signature;
            var key = _config.GetSection("KeyForEncrypt").Value;
            string passAfterEncrypt = AesOperation.EncryptString(key, oldPass);
            if(userPass==passAfterEncrypt)
            {
                if(newPass.Length<=10)
                {
                    if (newPass == confirmNewPass)
                    {
                        user.Signature = AesOperation.EncryptString(key, newPass);
                        _context.Employee.Update(user);
                        _context.SaveChanges();
                        message = "تم تعديل كلمة المرور بنجاح";
                    }
                    else
                    {
                        message = "يوجد خطأ في تأكيد كلمة المرور";

                    }
                }
                else
                {
                    message = "يرجى ادخال كلمة مرور اقل من 10 احرف";
                }
            }
            else
            {
                message = "كلمة المرور التي ادخلتها غير صحيحة";
            }

            return RedirectToAction("Index", "ManageSelfServices", new { message = message });
        }

    }
}
