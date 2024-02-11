using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FasDemo.Data;
using FasDemo.Models;
using FasDemo.ProjectModel;
using FasDemo.ProjectModel.DTO;
using FasDemo.SurveyModel;
using FasDemo.SurveyModel.ViewModel;
using FasDemo.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ProjectManagment.Models;

namespace FasDemo.Controllers
{
    [Authorize(Roles = Services.App.Pages.Employee.RoleName)]
    public class ContractorsController : Controller
    {
        JsonSerializerSettings _jsonSetting = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };

        private IHostingEnvironment Environment;

        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly Services.Security.ICommon _security;
        private readonly Services.App.ICommon _app;
        private readonly SignInManager<ApplicationUser> _signInManager;

        //dependency injection through constructor, to directly access services
        public ContractorsController(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            Services.Security.ICommon security,
            Services.App.ICommon app,
            SignInManager<ApplicationUser> signInManager
            )
        {
            _context = context;
            _userManager = userManager;
            _security = security;
            _app = app;
            _signInManager = signInManager;
        }

        //fill viewdata as dropdownlist datasource for Project form
        private void FillDropdownListWithData()
        {
            ViewData["SystemUser"] = _app.GetSystemUserSelectList();
            ViewData["ContractorUser"] = _app.GetContractorSelectList();
            ViewData["Employee"] = _app.GetEmployeeSelectList();
            ViewData["Status"] = _app.GetProjectStatusTypeSelectList();
        }
        #region Customer
        public IActionResult Index()
        {
            var objs = _context.Contractors.OrderByDescending(x => x.CreatedAtUtc).ToList();
            return View(objs);
        }

        //display Customer create edit form
        [HttpGet]
        public IActionResult Form(string id)
        {
            //create new
            if (id == null)
            {
                //dropdownlist 
                FillDropdownListWithData();
                Contractor newObj = new Contractor();
                return View(newObj);
            }

            //edit Customer
            Contractor obj = new Contractor();
            obj = _context.Contractors.Where(x => x.ContractorId.Equals(id)).FirstOrDefault();

            if (obj == null)
            {
                return NotFound();
            }
            //dropdownlist 
            FillDropdownListWithData();
            return View(obj);
        }

        //post submitted Customer data. if id is null then create new, otherwise edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SubmitForm([Bind("ContractorId", "ContractorName", "ContractorCode", "Email", "PhoneNumber", "ContractorUserId")] Contractor Contractor)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    TempData[StaticString.StatusMessage] = " خطأ : حالة النموذج غير صالحة.";
                    return RedirectToAction(nameof(Form), new { id = Contractor.ContractorId ?? "" });
                }

                //create new
                if (Contractor.ContractorId == null)
                {
                    if (await _context.Contractors.AnyAsync(x => x.ContractorName.Equals(Contractor.ContractorName)))
                    {
                        TempData[StaticString.StatusMessage] = " خطأ : " + Contractor.ContractorName + " موجود مسبقا";
                        return RedirectToAction(nameof(Form), new { id = Contractor.ContractorId ?? "" });
                    }

                    Contractor newObj = new Contractor();
                    newObj.ContractorId = Guid.NewGuid().ToString();
                    newObj.ContractorName = Contractor.ContractorName;
                    newObj.ContractorCode = Contractor.ContractorCode;
                    newObj.Email = Contractor.Email;
                    newObj.PhoneNumber = Contractor.PhoneNumber;
                    newObj.ContractorUserId = Contractor.ContractorUserId;
                    newObj.CreatedBy = await _userManager.GetUserAsync(User);
                    newObj.CreatedAtUtc = DateTime.UtcNow;
                    newObj.UpdatedBy = newObj.CreatedBy;
                    newObj.UpdatedAtUtc = newObj.CreatedAtUtc;

                    _context.Contractors.Add(newObj);
                    _context.SaveChanges();

                    TempData[StaticString.StatusMessage] = "تم انشاء سجل جديد بنجاح.";
                    return RedirectToAction(nameof(Form), new { id = newObj.ContractorId ?? "" });
                }

                //edit existing
                Contractor editObj = new Contractor();
                Contractor existObj = new Contractor();
                editObj = await _context.Contractors.Where(x => x.ContractorId.Equals(Contractor.ContractorId)).FirstOrDefaultAsync();
                existObj = await _context.Contractors.Where(x => x.ContractorName.Equals(Contractor.ContractorName)).FirstOrDefaultAsync();

                if (existObj != null && editObj.ContractorId != existObj.ContractorId)
                {
                    TempData[StaticString.StatusMessage] = " خطأ : " + Contractor.ContractorName + " موجود مسبقا";
                    return RedirectToAction(nameof(Form), new { id = Contractor.ContractorId ?? "" });
                }

                editObj.ContractorName = Contractor.ContractorName;
                editObj.ContractorCode = Contractor.ContractorCode;
                editObj.Email = Contractor.Email;
                editObj.PhoneNumber = Contractor.PhoneNumber;
                editObj.ContractorUserId = Contractor.ContractorUserId;
                editObj.UpdatedBy = await _userManager.GetUserAsync(User);
                editObj.UpdatedAtUtc = DateTime.UtcNow;

                _context.Update(editObj);
                _context.SaveChanges();

                TempData[StaticString.StatusMessage] = "تم تعديل السجل بنجاح.";
                return RedirectToAction(nameof(Form), new { id = Contractor.ContractorId ?? "" });
            }
            catch (Exception ex)
            {

                TempData[StaticString.StatusMessage] = " خطأ : " + ex.Message;
                return RedirectToAction(nameof(Form), new { id = Contractor.ContractorId ?? "" });
            }
        }


        //display item for deletion
        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var obj = await _context.Contractors.Where(x => x.ContractorId.Equals(id)).FirstOrDefaultAsync();
            return View(obj);
        }

        //delete submitted item if found, otherwise 404
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SubmitDelete([Bind("ContractorId")] Contractor Customer)
        {
            try
            {
                var deleteObj = await _context.Contractors.Where(x => x.ContractorId.Equals(Customer.ContractorId)).FirstOrDefaultAsync();
                if (deleteObj == null)
                {
                    return NotFound();
                }

                //cek existing ke Project
                Project objCheck = new Project();
                objCheck = await _context.Projects
                    .Where(x => x.ContractorId.Equals(deleteObj.ContractorId))
                    .FirstOrDefaultAsync();

                if (objCheck != null)
                {
                    TempData[StaticString.StatusMessage] = " خطأ : استخدمت بالفعل في الاجراء.";
                    return RedirectToAction(nameof(Delete), new { id = Customer.ContractorId ?? "" });
                }

                _context.Contractors.Remove(deleteObj);
                _context.SaveChanges();

                TempData[StaticString.StatusMessage] = "تم حذف العنصر بنجاح .";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                TempData[StaticString.StatusMessage] = " خطأ : " + ex.Message;
                return RedirectToAction(nameof(Delete), new { id = Customer.ContractorId ?? "" });
            }
        }

        #endregion

        private string statusname(int? statusid)
        {
            string status = "";
            switch (statusid)
            {
                case 1: status = "نشط"; break;
                case 2: status = "منتهي"; break;
                case 3: status = "متوقف"; break;
                case 4: status = "ملغي"; break;
                default:
                    break;
            }
            return status;

        }

        public async Task<string> userImage(string userId)
        {

            string ProfilePicture = "/assets/images/user/avatar-2.jpg";
            var existing = await _context.Employee.Where(a => a.SystemUserId == userId).FirstOrDefaultAsync();
            ProfilePicture = existing != null ?
                                   "/" + existing.ProfilePicture :
                                    "/assets/images/user/avatar-2.jpg";

            return ProfilePicture;
        }
        public string userFirstName(string userId)
        {
            string FirstName = "مقاول";
            var existing = _context.Employee.Where(a => a.SystemUserId == userId).FirstOrDefault();
            FirstName = existing != null ?
                                    _context.Employee.Where(a => a.SystemUserId == userId).FirstOrDefault().FirstName :
                                    _context.Contractors.Where(a => a.ContractorUserId == userId).FirstOrDefault().ContractorName;
            return FirstName;
        }

        #region survey

        [HttpGet]
        public ActionResult QuizTest()
        {
            QuizVM quizSelected = _context.Surveys.Where(q => q.Id == "07636B1A-790C-4C96-A506-B8CEDA98CE97").Select(q => new QuizVM
            {
                QuizID = q.Id,
                QuizName = q.Title,

            }).FirstOrDefault();

            List<QuestionVM> questions = null;

            if (quizSelected != null)
            {
                var questionVMs = _context.Questions.ToList();
                questions = _context.Questions.Where(q => q.SurveyId == quizSelected.QuizID)
                   .Select(q => new QuestionVM
                   {
                       QuestionId = q.QuestionId,
                       QuestionText = q.QuestionBody,
                       QuestionType = q.AnswerType,
                       Choices = q.Choices.Select(c => new ChoiceVM
                       {
                           ChoiceID = c.ChoiceId,
                           ChoiceText = c.ChoiceText
                       }).ToList()

                   }).ToList();


            }

            return View(questions);
        }

        [HttpPost]
        public ActionResult QuizTest(List<QuizAnswersVM> resultquiz)
        {
            List<QuizAnswersVM> finalResultQuiz = new List<QuizAnswersVM>();

            foreach (QuizAnswersVM answser in resultquiz)
            {
                Response newObj = new Response();
                newObj.ResponseId = "1";
                newObj.SurveyId = "";
                newObj.QuestionId = answser.QuestionId;
                newObj.ChoiceText = answser.AnswerQ;
                //newObj.ChoiceId = answser.ChoiceId;
                //newObj.CreatedBy = await _userManager.GetUserAsync(User);
                newObj.CreatedOn = DateTime.UtcNow;

                _context.Responses.Add(newObj);
                _context.SaveChanges();
            }

            return Json(new { result = finalResultQuiz });
        }



        [HttpGet]
        public ActionResult QuizTestResponce()
        {
            List<Response> quizSelected = _context.Responses.Where(q => q.ResponseId == "c878a29c-6686-472d-a311-940cc997cba0")
                .ToList();

            List<QuestionVM> questions = null;

            if (quizSelected != null)
            {
                var questionVMs = _context.Questions.ToList();
                questions = _context.Questions.Where(q => q.SurveyId == quizSelected.FirstOrDefault().SurveyId)
                   .Select(q => new QuestionVM
                   {
                       QuestionId = q.QuestionId,
                       QuestionText = q.QuestionBody,
                       QuestionType = q.AnswerType,
                       Choices = quizSelected.Where(c => c.QuestionId == q.QuestionId)
                            .Select(c => new ChoiceVM
                            {
                                ChoiceID = c.ChoiceId,
                                ChoiceText = c.ChoiceText
                            }).ToList()

                   }).ToList();


            }

            return View(questions);
        }
        #endregion

        

    }
}
