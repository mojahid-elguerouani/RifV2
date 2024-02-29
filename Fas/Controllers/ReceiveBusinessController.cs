using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using FasDemo.Data;
using FasDemo.Models;
using FasDemo.ProjectModel;
using FasDemo.SurveyModel.ViewModel;
using FasDemo.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ProjectManagment.Models;
using static FasDemo.Services.App.Pages;
using Employee = FasDemo.Models.Employee;
using Project = ProjectManagment.Models.Project;
using ReceiveBusiness = ProjectManagment.Models.ReceiveBusiness;
using ReceiveBusinessSchedualTemplet = ProjectManagment.Models.ReceiveBusinessSchedualTemplet;
using ReceiveBusinessTask = ProjectManagment.Models.ReceiveBusinessTask;
using Newtonsoft.Json;
namespace FasDemo.Controllers
{
    public class ReceiveBusinessController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly Services.Security.ICommon _security;
        private readonly Services.App.ICommon _app;
        private readonly SignInManager<ApplicationUser> _signInManager;
        JsonSerializerSettings _jsonSetting = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };

        //dependency injection through constructor, to directly access services
        public ReceiveBusinessController(
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

        private void FillDropdownListWithData()
        {
            ViewData["SystemUser"] = _app.GetSystemUserSelectList();


            ViewData["AllUserSelect"] = _app.GetAllUserSelectList();

            ViewData["ContractorUser"] = _app.GetContractorSelectList();
            ViewData["ReceiveBusinessSchedualTemplet"] = _app.GetReceiveBusinessSchedualTempletSelectList();
            ViewData["Employee"] = _app.GetEmployeeUserSelectList();
            ViewData["Status"] = _app.GetReceiveBusinessStatusTypeSelectList();
            ViewData["Specialization"] = _app.GetReceiveBusinessSpecializationSelectList();
            ViewData["Project"] = _app.GetProjectSelectList();

            ViewData["ProjectCode"] = _app.GetProjectCodeSelectList();
            ViewData["Sector"] = _app.GetSectorSelectList();
            ViewData["Region"] = _app.GetRegionSelectList();

        }


        public IActionResult Index()
        {
            var objs = _context.ReceiveBusiness.Include(a => a.ReceiveBusinessTasks)
               .AsNoTracking()
               .Include(x => x.Project).ThenInclude(x=>x.Contractor)
               //.Include(x => x.ProjectUser)
               .OrderByDescending(x => x.CreatedAtUtc).ToList();
            return View(objs);
        }

        public IActionResult FullCalendar()
        {
            return View();
        }

        public IActionResult Form(int? id)
        {
            //create new
            if (id == null)
            {
                //dropdownlist 
                FillDropdownListWithData();
                ReceiveBusiness newObj = new ReceiveBusiness();
                var RecordCount = _context.ReceiveBusiness.Count();
                if (RecordCount != 0)
                {
                    newObj.SerialNumber = 1;
                    newObj.ReviewNumber = 0;
                }

                return View(newObj);
            }

            //edit object
            ReceiveBusiness editObj = new ReceiveBusiness(); 
            editObj = _context.ReceiveBusiness.Where(x => x.ReceiveBusinessId == id).FirstOrDefault(); 

            if (editObj == null)
            {
                return NotFound();
            }

            //dropdownlist  
            FillDropdownListWithData(); 

            return View(editObj);

        }

        //post submitted receivebusiness data. if ReceiveBusinessId is null then create new, otherwise edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SubmitForm([Bind(
            "ReceiveBusinessId",
            "SerialNumber",
            "ReviewNumber",
            "ReceiveBusinessDate",
            "ProjectId",
            "IsCivil",
            "IsArchitectural",
            "IsMechanics",
            "IsElectricity",
            "IsAgricultural",
            "IsOthers",
            "OtherSpecialization",
            "BuildingStatement",
            "BuildingComments",
            "WorkToBeExaminedStatement",
            "WorkToBeExaminedComments",
            "FloorStatement",
            "FloorComments",
            "RequiredExaminationDateStatement",
            "RequiredExaminationDateComments",
            "ApprovedPlatesStatement",
            "ApprovedPlatesComments",
            "StatusId",
            "ReceiveBusinessSchedualTempletId"
            )]ReceiveBusiness receivebusiness)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    TempData[StaticString.StatusMessage] = " خطأ : حالة النموذج غير صالحة.";
                    return RedirectToAction(nameof(Form), new { id = receivebusiness.ReceiveBusinessId });
                }

                //create new
                if (receivebusiness.ReceiveBusinessId == 0)
                {
                    var receivebusinessSchedualCount = await _context.ReceiveBusinessScheduals.Where(a => a.ReceiveBusinessSchedualTempletId == receivebusiness.ReceiveBusinessSchedualTempletId).CountAsync();
                    if (receivebusinessSchedualCount <= 0)
                    {
                        TempData[StaticString.StatusMessage] = " خطأ : النموذج المخطار لا يحتوي على أي مهام يجب ادخال مهمة واحدة على الاقل للمشروع :. ";
                        return RedirectToAction(nameof(Form));
                    }


                    ReceiveBusiness newReceiveBusiness = new ReceiveBusiness();

                    newReceiveBusiness.ReceiveBusinessId = receivebusiness.ReceiveBusinessId;
                    newReceiveBusiness.SerialNumber = receivebusiness.SerialNumber;
                    newReceiveBusiness.ReviewNumber = receivebusiness.ReviewNumber;
                    newReceiveBusiness.ReceiveBusinessDate = receivebusiness.ReceiveBusinessDate;

                    newReceiveBusiness.IsCivil = receivebusiness.IsCivil;
                    newReceiveBusiness.IsArchitectural = receivebusiness.IsArchitectural;
                    newReceiveBusiness.IsMechanics = receivebusiness.IsMechanics;
                    newReceiveBusiness.IsElectricity = receivebusiness.IsElectricity;
                    newReceiveBusiness.IsAgricultural = receivebusiness.IsAgricultural;
                    newReceiveBusiness.IsOthers = receivebusiness.IsOthers;
                    newReceiveBusiness.OtherSpecialization = receivebusiness.OtherSpecialization;

                    newReceiveBusiness.StatusId = receivebusiness.StatusId;

                    newReceiveBusiness.ReceiveBusinessSchedualTempletId = receivebusiness.ReceiveBusinessSchedualTempletId;

                    newReceiveBusiness.ProjectId = receivebusiness.ProjectId;


                    newReceiveBusiness.BuildingStatement = receivebusiness.BuildingStatement;
                    newReceiveBusiness.BuildingComments = receivebusiness.BuildingComments;
                    newReceiveBusiness.WorkToBeExaminedStatement = receivebusiness.WorkToBeExaminedStatement;
                    newReceiveBusiness.WorkToBeExaminedComments = receivebusiness.WorkToBeExaminedComments;
                    newReceiveBusiness.FloorStatement = receivebusiness.FloorStatement;
                    newReceiveBusiness.FloorComments  = receivebusiness.FloorComments;
                    newReceiveBusiness.RequiredExaminationDateStatement = receivebusiness.RequiredExaminationDateStatement;
                    newReceiveBusiness.RequiredExaminationDateComments = receivebusiness.RequiredExaminationDateComments;
                    newReceiveBusiness.ApprovedPlatesStatement = receivebusiness.ApprovedPlatesStatement;
                    newReceiveBusiness.ApprovedPlatesComments = receivebusiness.ApprovedPlatesComments;




                    newReceiveBusiness.CreatedBy = await _userManager.GetUserAsync(User);
                    newReceiveBusiness.CreatedAtUtc = DateTime.UtcNow;
                    newReceiveBusiness.UpdatedBy = newReceiveBusiness.CreatedBy;
                    newReceiveBusiness.UpdatedAtUtc = newReceiveBusiness.CreatedAtUtc;

                    _context.ReceiveBusiness.Add(newReceiveBusiness);
                    _context.SaveChanges();

                    //dropdownlist 
                    FillDropdownListWithData();


                    SqlParameter[] parameters = {
                        new SqlParameter("@ReceiveBusinessSchedualTempletId", receivebusiness.ReceiveBusinessSchedualTempletId),
                        new SqlParameter("@ReceiveBusinessId",  newReceiveBusiness.ReceiveBusinessId),
                        new SqlParameter("@CreatedBy",  newReceiveBusiness.CreatedById),
                        new SqlParameter("@CreatedAtUtc",  newReceiveBusiness.CreatedAtUtc),
                    };

                    var xdata = _context.Database.ExecuteSqlCommand("CreateReceiveBusiness @ReceiveBusinessSchedualTempletId, @ReceiveBusinessId, @CreatedBy ,@CreatedAtUtc", parameters);

                    TempData[StaticString.StatusMessage] = "تم انشاء الطلب بنجاح.";
                  
                    return RedirectToAction(nameof(Form), new { id = newReceiveBusiness.ReceiveBusinessId });
                }

                //edit existing
                ReceiveBusiness editreceivebusiness = new ReceiveBusiness();
                editreceivebusiness = _context.ReceiveBusiness.Where(x => x.ProjectId.Equals(receivebusiness.ReceiveBusinessId)).FirstOrDefault();

                if (editreceivebusiness != null)
                {

                    //editreceivebusiness.ReceiveBusinessId = receivebusiness.ReceiveBusinessId;

                    editreceivebusiness.SerialNumber = receivebusiness.SerialNumber;
                    editreceivebusiness.ReviewNumber = editreceivebusiness.ReviewNumber;
                    editreceivebusiness.ReceiveBusinessDate = receivebusiness.ReceiveBusinessDate;

                    editreceivebusiness.IsCivil = receivebusiness.IsCivil;
                    editreceivebusiness.IsArchitectural = receivebusiness.IsArchitectural;
                    editreceivebusiness.IsMechanics = receivebusiness.IsMechanics;
                    editreceivebusiness.IsElectricity = receivebusiness.IsElectricity;
                    editreceivebusiness.IsAgricultural = receivebusiness.IsAgricultural;
                    editreceivebusiness.IsOthers = receivebusiness.IsOthers;
                    editreceivebusiness.OtherSpecialization = receivebusiness.OtherSpecialization;


                    editreceivebusiness.StatusId = receivebusiness.StatusId;
                    editreceivebusiness.ReceiveBusinessSchedualTempletId = receivebusiness.ReceiveBusinessSchedualTempletId;
                    editreceivebusiness.ProjectId = receivebusiness.ProjectId;

                    editreceivebusiness.BuildingStatement = receivebusiness.BuildingStatement;
                    editreceivebusiness.BuildingComments = receivebusiness.BuildingComments;
                    editreceivebusiness.WorkToBeExaminedStatement = receivebusiness.WorkToBeExaminedStatement;
                    editreceivebusiness.WorkToBeExaminedComments = receivebusiness.WorkToBeExaminedComments;
                    editreceivebusiness.FloorStatement = receivebusiness.FloorStatement;
                    editreceivebusiness.FloorComments = receivebusiness.FloorComments;
                    editreceivebusiness.RequiredExaminationDateStatement = receivebusiness.RequiredExaminationDateStatement;
                    editreceivebusiness.RequiredExaminationDateComments = receivebusiness.RequiredExaminationDateComments;
                    editreceivebusiness.ApprovedPlatesStatement = receivebusiness.ApprovedPlatesStatement;
                    editreceivebusiness.ApprovedPlatesComments = receivebusiness.ApprovedPlatesComments;

                    editreceivebusiness.UpdatedBy = await _userManager.GetUserAsync(User);
                    editreceivebusiness.UpdatedAtUtc = DateTime.UtcNow;


                    _context.Update(editreceivebusiness);
                    _context.SaveChanges();

                    //dropdownlist 
                    FillDropdownListWithData();

                    TempData[StaticString.StatusMessage] = "تم تعديل بيانات الطلب بنجاح.";
                    return RedirectToAction(nameof(Form), new { id = editreceivebusiness.ReceiveBusinessId });
                }
                else
                {
                    return NotFound();
                }


            }
            catch (Exception ex)
            {

                TempData[StaticString.StatusMessage] = " خطأ : " + ex.Message;
                return RedirectToAction(nameof(Form), new { id = receivebusiness.ReceiveBusinessId });
            }
        }


        //display project item for deletion
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var del = _context.ReceiveBusiness
                .AsNoTracking()
                .Include(x => x.Project).ThenInclude(x => x.Contractor)
                .Include(x => x.ReceiveBusinessTasks)
                .Where(x => x.ReceiveBusinessId == id).FirstOrDefault();

            //dropdownlist 
            FillDropdownListWithData();

            return View(del);
        }

        //delete submitted project if found, otherwise 404
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SubmitDelete([Bind("ReceiveBusinessId")] ReceiveBusiness receivebusiness)
        {
            try
            {
                var del = _context.ReceiveBusiness.Where(x => x.ReceiveBusinessId == receivebusiness.ReceiveBusinessId).FirstOrDefault();
                if (del == null)
                {
                    return NotFound();
                }

                _context.ReceiveBusiness.Remove(del);
                _context.SaveChanges();

                TempData[StaticString.StatusMessage] = "تم حذف الطلب بنجاح.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData[StaticString.StatusMessage] = " خطأ : " + ex.Message;
                return RedirectToAction(nameof(Delete), new { id = receivebusiness.ReceiveBusinessId });
            }
        }



        #region ReceiveBusinessScheduals
        public IActionResult ReceiveBusinessSchedualTempletIndex()
        {
            var objs = _context.ReceiveBusinessSchedualTemplets.OrderByDescending(x => x.CreatedAtUtc).ToList();
            return View(objs);
        }

        //display ReceiveBusinessSchedualTemplet create edit form
        [HttpGet]
        public IActionResult ReceiveBusinessSchedualTempletForm(string id)
        {
            FillDropdownListWithData();
            //create new
            if (id == null)
            {
                ReceiveBusinessSchedualTemplet newObj = new ReceiveBusinessSchedualTemplet();
                return View(newObj);
            }

            //edit ProjectSchedualTemplet
            ReceiveBusinessSchedualTemplet obj = new ReceiveBusinessSchedualTemplet();
            obj = _context.ReceiveBusinessSchedualTemplets
                .Include(x => x.ReceiveBusinessScheduals)
                .ThenInclude(x => x.ReceiveBusinessApprovedBy)
                .Include(z => z.ReceiveBusinessScheduals)
                .ThenInclude(x => x.ReceiveBusinessAssignTo)
                .Where(x => x.ReceiveBusinessSchedualTempletId.Equals(id))
                .FirstOrDefault();

            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        //post submitted ReceiveBusinessSchedualTemplet data. if id is null then create new, otherwise edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SubmitReceiveBusinessSchedualTempletForm([Bind("ReceiveBusinessSchedualTempletId", "ReceiveBusinessSchedualTempletName")] ReceiveBusinessSchedualTemplet ReceiveBusinessSchedualTemplet)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    TempData[StaticString.StatusMessage] = " خطأ : حالة النموذج غير صالحة.";
                    return RedirectToAction(nameof(ReceiveBusinessSchedualTempletForm), new { id = ReceiveBusinessSchedualTemplet.ReceiveBusinessSchedualTempletId ?? "" });
                }

                //create new
                if (ReceiveBusinessSchedualTemplet.ReceiveBusinessSchedualTempletId == null)
                {
                    if (await _context.ReceiveBusinessSchedualTemplets.AnyAsync(x => x.ReceiveBusinessSchedualTempletName.Equals(ReceiveBusinessSchedualTemplet.ReceiveBusinessSchedualTempletName)))
                    {
                        TempData[StaticString.StatusMessage] = " خطأ : " + ReceiveBusinessSchedualTemplet.ReceiveBusinessSchedualTempletName + " موجود بالفعل";
                        return RedirectToAction(nameof(ReceiveBusinessSchedualTempletForm), new { id = ReceiveBusinessSchedualTemplet.ReceiveBusinessSchedualTempletId ?? "" });
                    }

                    ReceiveBusinessSchedualTemplet newObj = new ReceiveBusinessSchedualTemplet();
                    newObj.ReceiveBusinessSchedualTempletId = Guid.NewGuid().ToString();
                    newObj.ReceiveBusinessSchedualTempletName = ReceiveBusinessSchedualTemplet.ReceiveBusinessSchedualTempletName;
                    newObj.CreatedBy = await _userManager.GetUserAsync(User);
                    newObj.CreatedAtUtc = DateTime.UtcNow;
                    newObj.UpdatedBy = newObj.CreatedBy;
                    newObj.UpdatedAtUtc = newObj.CreatedAtUtc;

                    _context.ReceiveBusinessSchedualTemplets.Add(newObj);
                    _context.SaveChanges();

                    TempData[StaticString.StatusMessage] = "تم انشاء سجل جديد بنجاح.";
                    return RedirectToAction(nameof(ReceiveBusinessSchedualTempletForm), new { id = newObj.ReceiveBusinessSchedualTempletId ?? "" });
                }

                //edit existing
                ReceiveBusinessSchedualTemplet editObj = new ReceiveBusinessSchedualTemplet();
                ReceiveBusinessSchedualTemplet existObj = new ReceiveBusinessSchedualTemplet();
                editObj = await _context.ReceiveBusinessSchedualTemplets.Where(x => x.ReceiveBusinessSchedualTempletId.Equals(ReceiveBusinessSchedualTemplet.ReceiveBusinessSchedualTempletId)).FirstOrDefaultAsync();
                existObj = await _context.ReceiveBusinessSchedualTemplets.Where(x => x.ReceiveBusinessSchedualTempletName.Equals(ReceiveBusinessSchedualTemplet.ReceiveBusinessSchedualTempletName)).FirstOrDefaultAsync();

                if (existObj != null)
                {
                    if (editObj.ReceiveBusinessSchedualTempletId != existObj.ReceiveBusinessSchedualTempletId)
                    {
                        TempData[StaticString.StatusMessage] = " خطأ : " + ReceiveBusinessSchedualTemplet.ReceiveBusinessSchedualTempletName + " موجود بالفعل";
                        return RedirectToAction(nameof(ReceiveBusinessSchedualTempletForm), new { id = ReceiveBusinessSchedualTemplet.ReceiveBusinessSchedualTempletId ?? "" });
                    }

                }

                editObj.ReceiveBusinessSchedualTempletName = ReceiveBusinessSchedualTemplet.ReceiveBusinessSchedualTempletName;
                editObj.UpdatedBy = await _userManager.GetUserAsync(User);
                editObj.UpdatedAtUtc = DateTime.UtcNow;

                _context.Update(editObj);
                _context.SaveChanges();

                TempData[StaticString.StatusMessage] = "تم تعديل السجل بنجاح.";
                return RedirectToAction(nameof(ReceiveBusinessSchedualTempletForm), new { id = ReceiveBusinessSchedualTemplet.ReceiveBusinessSchedualTempletId ?? "" });
            }
            catch (Exception ex)
            {

                TempData[StaticString.StatusMessage] = " خطأ : " + ex.Message;
                return RedirectToAction(nameof(ReceiveBusinessSchedualTempletForm), new { id = ReceiveBusinessSchedualTemplet.ReceiveBusinessSchedualTempletId ?? "" });
            }
        }


        //display item for deletion
        [HttpGet]
        public async Task<IActionResult> ReceiveBusinessSchedualTempletDelete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var obj = await _context.ReceiveBusinessSchedualTemplets.Where(x => x.ReceiveBusinessSchedualTempletId.Equals(id)).FirstOrDefaultAsync();
            return View(obj);
        }

        //delete submitted item if found, otherwise 404
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SubmitReceiveBusinessSchedualTempletDelete([Bind("ReceiveBusinessSchedualTempletId")] ReceiveBusinessSchedualTemplet ReceiveBusinessSchedualTemplet)
        {
            try
            {
                var deleteObj = await _context.ReceiveBusinessSchedualTemplets.Where(x => x.ReceiveBusinessSchedualTempletId.Equals(ReceiveBusinessSchedualTemplet.ReceiveBusinessSchedualTempletId)).FirstOrDefaultAsync();
                if (deleteObj == null)
                {
                    return NotFound();
                }

                //todo: cek existing ke Project

                _context.ReceiveBusinessSchedualTemplets.Remove(deleteObj);
                _context.SaveChanges();

                TempData[StaticString.StatusMessage] = "تم حذف العنصر بنجاح .";
                return RedirectToAction(nameof(ReceiveBusinessSchedualTempletIndex));
            }
            catch (Exception ex)
            {

                TempData[StaticString.StatusMessage] = " خطأ : " + ex.Message;
                return RedirectToAction(nameof(ReceiveBusinessSchedualTempletDelete), new { id = ReceiveBusinessSchedualTemplet.ReceiveBusinessSchedualTempletId ?? "" });
            }
        }

        //display ReceiveBusinessSchedual create edit form
        [HttpGet]
        public IActionResult ReceiveBusinessSchedualForm(int? id, string header)
        {
            //dropdownlist type
            FillDropdownListWithData();
            ViewData["ReceiveBusinessSchedualParent"] = _app.GetReceiveBusinessSchedualParentSelectList(header);
            //create new
            if (id == null)
            {
                ReceiveBusinessSchedual newObj = new ReceiveBusinessSchedual();
                newObj.ReceiveBusinessSchedualTempletId = header;
                return View(newObj);
            }

            //edit ProjectSchedual
            ReceiveBusinessSchedual obj = new ReceiveBusinessSchedual();
            obj = _context.ReceiveBusinessScheduals.Where(x => x.ReceiveBusinessSchedualId == id).FirstOrDefault();

            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        //post submitted ProjectSchedual data. if id is null then create new, otherwise edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SubmitReceiveBusinessSchedualForm([Bind(
             "ReceiveBusinessSchedualId"
            ,"ReceiveBusinessSchedualTempletId"
            ,"TaskName"
            ,"TaskOrder"
            ,"ReceiveBusinessAssignToId"
            ,"ReceiveBusinessSchedualParentId"
            ,"toEmail"
            ,"ReceiveBusinessApprovedById")]
        ReceiveBusinessSchedual ReceiveBusinessSchedual)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    TempData[StaticString.StatusMessage] = " خطأ : حالة النموذج غير صالحة.";
                    return RedirectToAction(nameof(ReceiveBusinessSchedualForm), new { id = ReceiveBusinessSchedual.ReceiveBusinessSchedualId.ToString() ?? "", header = ReceiveBusinessSchedual.ReceiveBusinessSchedualTempletId });
                }

                //dropdownlist type
                FillDropdownListWithData();
                ViewData["ReceiveBusinessSchedualParent"] = _app.GetReceiveBusinessSchedualParentSelectList(ReceiveBusinessSchedual.ReceiveBusinessSchedualTempletId);
                //create new
                if (ReceiveBusinessSchedual.ReceiveBusinessSchedualId == 0)
                {


                    ReceiveBusinessSchedual newObj = new ReceiveBusinessSchedual();
                    //newObj.ProjectSchedualId = Guid.NewGuid().ToString();
                    newObj.ReceiveBusinessSchedualTempletId = ReceiveBusinessSchedual.ReceiveBusinessSchedualTempletId;
                    newObj.TaskName = ReceiveBusinessSchedual.TaskName;
                    newObj.TaskOrder = ReceiveBusinessSchedual.TaskOrder;
                    newObj.ReceiveBusinessAssignToId = ReceiveBusinessSchedual.ReceiveBusinessAssignToId;
                    newObj.ReceiveBusinessSchedualParentId = ReceiveBusinessSchedual.ReceiveBusinessSchedualParentId;
                    newObj.toEmail = ReceiveBusinessSchedual.toEmail;
                    newObj.ReceiveBusinessApprovedById = ReceiveBusinessSchedual.ReceiveBusinessApprovedById;
                    newObj.CreatedBy = await _userManager.GetUserAsync(User);
                    newObj.CreatedAtUtc = DateTime.UtcNow;
                    newObj.UpdatedBy = newObj.CreatedBy;
                    newObj.UpdatedAtUtc = newObj.CreatedAtUtc;

                    _context.ReceiveBusinessScheduals.Add(newObj);
                    _context.SaveChanges();

                    TempData[StaticString.StatusMessage] = "تم انشاء سجل جديد بنجاح.";
                    return RedirectToAction(nameof(ReceiveBusinessSchedualForm), new { id = newObj.ReceiveBusinessSchedualId.ToString() ?? "0", header = newObj.ReceiveBusinessSchedualTempletId });
                }

                //edit existing
                ReceiveBusinessSchedual editObj = new ReceiveBusinessSchedual();
                ReceiveBusinessSchedual existObj = new ReceiveBusinessSchedual();
                editObj = await _context.ReceiveBusinessScheduals.Where(x => x.ReceiveBusinessSchedualId.Equals(ReceiveBusinessSchedual.ReceiveBusinessSchedualId)).FirstOrDefaultAsync();

                editObj.ReceiveBusinessSchedualTempletId = ReceiveBusinessSchedual.ReceiveBusinessSchedualTempletId;
                editObj.TaskName = ReceiveBusinessSchedual.TaskName;
                editObj.TaskOrder = ReceiveBusinessSchedual.TaskOrder;
                editObj.ReceiveBusinessAssignToId = ReceiveBusinessSchedual.ReceiveBusinessAssignToId;
                editObj.ReceiveBusinessSchedualParentId = ReceiveBusinessSchedual.ReceiveBusinessSchedualParentId;
                editObj.toEmail = ReceiveBusinessSchedual.toEmail;
                editObj.ReceiveBusinessApprovedById = ReceiveBusinessSchedual.ReceiveBusinessApprovedById;
                

                editObj.UpdatedBy = await _userManager.GetUserAsync(User);
                editObj.UpdatedAtUtc = DateTime.UtcNow;

                _context.Update(editObj);
                _context.SaveChanges();

                TempData[StaticString.StatusMessage] = "تم تعديل السجل بنجاح.";
                return RedirectToAction(nameof(ReceiveBusinessSchedualForm), new { id = ReceiveBusinessSchedual.ReceiveBusinessSchedualId.ToString() ?? "", header = ReceiveBusinessSchedual.ReceiveBusinessSchedualId });
            }
            catch (Exception ex)
            {

                TempData[StaticString.StatusMessage] = " خطأ : " + ex.Message;
                return RedirectToAction(nameof(ReceiveBusinessSchedualForm), new { id = ReceiveBusinessSchedual.ReceiveBusinessSchedualId.ToString() ?? "", header = ReceiveBusinessSchedual.ReceiveBusinessSchedualId });
            }
        }

        //display item for deletion
        [HttpGet]
        public async Task<IActionResult> ReceiveBusinessSchedualDelete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            //dropdownlist type
            FillDropdownListWithData();
            var obj = await _context.ReceiveBusinessScheduals.Where(x => x.ReceiveBusinessSchedualId.Equals(id)).FirstOrDefaultAsync();
            return View(obj);
        }

        //delete submitted item if found, otherwise 404
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SubmitReceiveBusinessSchedualDelete([Bind("ReceiveBusinessSchedualId")] ReceiveBusinessSchedual ReceiveBusinessSchedual)
        {
            try
            {
                var deleteObj = await _context.ReceiveBusinessScheduals.Where(x => x.ReceiveBusinessSchedualId.Equals(ReceiveBusinessSchedual.ReceiveBusinessSchedualId)).FirstOrDefaultAsync();
                if (deleteObj == null)
                {
                    return NotFound();
                }

                //todo: cek existing ke ..


                _context.ReceiveBusinessScheduals.Remove(deleteObj);
                _context.SaveChanges();

                TempData[StaticString.StatusMessage] = "تم حذف العنصر بنجاح .";
                return RedirectToAction(nameof(ReceiveBusinessSchedualTempletForm), new { id = deleteObj.ReceiveBusinessSchedualTempletId });
            }
            catch (Exception ex)
            {

                TempData[StaticString.StatusMessage] = " خطأ : " + ex.Message;
                return RedirectToAction(nameof(ReceiveBusinessSchedualDelete), new { id = ReceiveBusinessSchedual.ReceiveBusinessSchedualId.ToString() ?? "", header = ReceiveBusinessSchedual.ReceiveBusinessSchedualTempletId });
            }
        }

        //Copy item for (ReceiveBusiness Schedual Templet
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CopyReceiveBusinessSchedualTemplet([Bind("ReceiveBusinessSchedualTempletId")] ReceiveBusinessSchedual ReceiveBusinessSchedual, string ReceiveBusinessSchedualTempletIdTo)
        {

            try
            {
                var deleteObj = await _context.ReceiveBusinessScheduals
                                    .Where(x => x.ReceiveBusinessSchedualTempletId.Equals(ReceiveBusinessSchedual.ReceiveBusinessSchedualTempletId)).FirstOrDefaultAsync();
                if (deleteObj == null)
                {
                    return NotFound();
                }

                //todo: cek existing ke ..

                SqlParameter[] parameters1 = {
                    new SqlParameter("@ReceiveBusinessSchedualTempletIdTo", ReceiveBusinessSchedualTempletIdTo),
                    new SqlParameter("@ReceiveBusinessSchedualTempletIdfrom", ReceiveBusinessSchedual.ReceiveBusinessSchedualTempletId)//applicationUser.Id
                };

                //var result = await context.SomeModels.FromSql("SQL_SCRIPT").ToListAsync();
                var objs1 = _context.Database.ExecuteSqlCommand("CopyReceiveBusinessSchedualTemplet  @ReceiveBusinessSchedualTempletIdTo, @ReceiveBusinessSchedualTempletIdfrom", parameters1);



                TempData[StaticString.StatusMessage] = " تم نسخ " + objs1 + " عنصر بنجاح .";
                return RedirectToAction(nameof(ReceiveBusinessSchedualTempletForm), new { id = ReceiveBusinessSchedualTempletIdTo });
            }
            catch (Exception ex)
            {

                TempData[StaticString.StatusMessage] = " خطأ : " + ex.Message;
                return RedirectToAction(nameof(ReceiveBusinessSchedualTempletForm), new { id = ReceiveBusinessSchedualTempletIdTo });
            }
        }


        #endregion


        [HttpGet]
        public async Task<IActionResult> LoginOnBehalf(string id)
        {
            Employee employee = new Employee();
            employee = _context.Employee.Where(x => x.SystemUserId.Equals(id)).FirstOrDefault();
            if (employee == null)
            {
                return NotFound();
            }

            ApplicationUser appUser = new ApplicationUser();
            appUser = await _userManager.FindByIdAsync(employee.SystemUserId);

            if (appUser == null)
            {
                return NotFound();
            }

            //attempt to sign in
            await _signInManager.SignInAsync(appUser, false);

            await _signInManager.RefreshSignInAsync(appUser);

            return RedirectToAction("Index", "Todo", new { period = DateTime.Now.ToString("yyyy-MM") });
        }


        // GET: ReceiveBusiness/Edit/5
        public async Task<ActionResult> EditReceiveBusiness(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ReceiveBusiness receivebusiness = await _context.ReceiveBusiness.FindAsync(id);
            if (receivebusiness == null)
            {
                return NotFound();
            }
            //Session["ReceiveBusinessId"] = receivebusiness.receivebusinessId;
            var listItems = _context.ReceiveBusinessTasks.Where(a => a.ReceiveBusinessId == id).Select(u => new SelectListItem { Value = u.TaskId.ToString(), Text = u.TaskName }).ToList();
            listItems.Add(new SelectListItem() { Value = null, Text = "أختر المهمة التابعة" });
            ViewBag.TaskParentId = new SelectList(listItems, "Value", "Text");
            // ViewBag.users = _app.GetSystemUserSelectList();
            ViewBag.users = _app.GetAllUserSelectList();
            ViewBag.ReceiveBusinessId = id;
            ViewBag.receivebusinesstasks = _context.ReceiveBusinessTasks.Include(p => p.ReceiveBusiness).Include(p => p.ReceiveBusinessAssignTo).Where(x => x.ReceiveBusinessId == id).ToList();//.OrderBy(p=>p.TaskOrder);
            return View(receivebusiness);
        }

        public ActionResult ReassignEditProject(int? ReceiveBusinessId, string ReceiveBusinessAssignfromId, string ReceiveBusinessAssignToId)
        {
            List<ReceiveBusinessTask> receiveBusinessTasks = _context.ReceiveBusinessTasks.Where(a => a.ReceiveBusinessId == ReceiveBusinessId && a.ReceiveBusinessAssignToId == ReceiveBusinessAssignfromId).ToList();



            receiveBusinessTasks.Select(c =>
            {
                c.ReceiveBusinessAssignToId = ReceiveBusinessAssignToId;
                c.ApprovedById = ReceiveBusinessAssignToId;
                c.UpdatedAtUtc = DateTime.Now;
                ; return c;
            }).ToList();

            _context.SaveChanges();

            TempData[StaticString.StatusMessage] = "تم تعديل بيانات المشروع بنجاح.";
            return RedirectToAction(nameof(EditReceiveBusiness), new { id = ReceiveBusinessId });
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Index(List<ReceiveBusinessTask> ReceiveBusinessTasks)
        {
            int ReceiveBusinessId = 0;
            string applicationUserId = _userManager.GetUserId(User);
            foreach (ReceiveBusinessTask cust in ReceiveBusinessTasks)
            {
                ReceiveBusinessTask existing = _context.ReceiveBusinessTasks.Find(cust.ReceiveBusinessTaskId);
                existing.TaskName = cust.TaskName;
                existing.TaskOrder = cust.TaskOrder;
                //existing.StarDate = cust.StarDate;
                //existing.EndDate = cust.EndDate ?? ((DateTime)cust.StarDate).AddDays((double)cust.Duration);
                //existing.Duration = cust.Duration;
                existing.Compleation = cust.Compleation;
                existing.ReceiveBusinessAssignToId = cust.ReceiveBusinessAssignToId;
                existing.UpdateDate = DateTime.Now;
                if (cust.Compleation == 100)
                {
                    existing.ApprovedById = applicationUserId;
                    existing.ApproveDate = DateTime.Now;
                    existing.IsApproved = true;
                }
                else
                {
                    existing.IsApproved = false;
                }

                existing.TaskParentId = cust.TaskParentId;
                ReceiveBusinessId = cust.ReceiveBusinessId;
            }
            ViewBag.Msg = "تم حفظ البيانات";
            _context.SaveChanges();
            TempData[StaticString.StatusMessage] = "تم تعديل بيانات المشروع بنجاح.";
            return RedirectToAction(nameof(EditReceiveBusiness), new { id = ReceiveBusinessId });
        }


        public List<Project> GetProjectByType(string type)
        {
            var result = _context.Projects.Where(x => x.Sector == type).ToList();
            return result;
        }


        public List<ReceiveBusiness> GetMaxReviewAndSerialNumber(string sqlStetment)
        {
            var newList = _context.ReceiveBusiness.FromSql(sqlStetment).ToList();
            return newList;
        }

    }
}
