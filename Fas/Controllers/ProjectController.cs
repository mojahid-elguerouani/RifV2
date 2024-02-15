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

namespace FasDemo.Controllers
{
    [Authorize(Roles = Services.App.Pages.Project.RoleName)]
    public class ProjectController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly Services.Security.ICommon _security;
        private readonly Services.App.ICommon _app;
        private readonly SignInManager<ApplicationUser> _signInManager;
        JsonSerializerSettings _jsonSetting = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };

        //dependency injection through constructor, to directly access services
        public ProjectController(
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
            ViewData["Employee"] = _app.GetEmployeeUserSelectList();
            ViewData["Status"] = _app.GetProjectStatusTypeSelectList();

            ViewData["SupervisionConsultant"] = _app.GetSupervisionConsultant();
            ViewData["ProjectManagementConsultant"] = _app.GetProjectManagementConsultant();

            ViewData["ProjectCode"] = _app.GetProjectCodeSelectList();
            ViewData["Sector"] = _app.GetSectorSelectList();
            ViewData["Region"] = _app.GetRegionSelectList();

        }
        public IActionResult Index()
        {

            var objs = _context.Projects.Include(a => a.Contractor)
               .AsNoTracking()
               //.Include(x => x.ProjectProgram)
               .Include(x => x.SupervisionConsultant)
               .Include(x => x.ProjectManagementConsultant)
               .OrderByDescending(x => x.CreatedAtUtc).ToList();
            return View(objs);
        }

        public IActionResult FullCalendar()
        {
            return View();
        }

        //display project create edit form
        [HttpGet]
        public IActionResult Form(int? id)
        {
            //create new
            if (id == null)
            {
                //dropdownlist 
                FillDropdownListWithData();

                Project newObj = new Project();
                return View(newObj);
            }

            //edit object
            Project editObj = new Project();
            editObj = _context.Projects.Where(x => x.ProjectId == id).FirstOrDefault();

            if (editObj == null)
            {
                return NotFound();
            }

            //dropdownlist 
            FillDropdownListWithData();

            return View(editObj);

        }


        //post submitted project data. if projectId is null then create new, otherwise edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SubmitForm([Bind(
            "ProjectId",
            "ProjectName",
            "ProjectCode",
            "Sector",
            "Region",
            "ProjectDescription",
            "StartDate",
            "EndDate",
            "EstimatedBudget",
            "ContractualBudget",
            "ContractorId",
            "SupervisionConsultantId",
            "ProjectManagementConsultantId",
            "StatusId"
            )]Project project)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    TempData[StaticString.StatusMessage] = " خطأ : حالة النموذج غير صالحة.";
                    return RedirectToAction(nameof(Form), new { id = project.ProjectId });
                }

                //create new
                if (project.ProjectId == 0)
                {
                    //duplicate project Number ID is not allowed
                    Project empNumber = await _context.Projects.Where(x => x.ProjectName.Equals(project.ProjectName)).FirstOrDefaultAsync();
                    if (empNumber != null && !String.IsNullOrEmpty(project.ProjectName))
                    {
                        TempData[StaticString.StatusMessage] = " خطأ : لا يمكن تكرار اسم المشروع. " + project.ProjectName;
                        return RedirectToAction(nameof(Form), new { id = project.ProjectId });
                    }

                    Project newProject = new Project();


                    newProject.ProjectName = project.ProjectName;
                    newProject.ProjectCode = project.ProjectCode;
                    newProject.StartDate = project.StartDate;
                    newProject.EndDate = project.EndDate;
                    newProject.EstimatedBudget = project.EstimatedBudget;
                    newProject.ContractualBudget = project.ContractualBudget;
                    newProject.ProjectDescription = project.ProjectDescription;
                    newProject.StatusId = project.StatusId;
                    newProject.Sector = project.Sector;
                    newProject.Region = project.Region;
                    newProject.ContractorId = project.ContractorId;
                    newProject.SupervisionConsultantId = project.SupervisionConsultantId;
                    newProject.ProjectManagementConsultantId = project.ProjectManagementConsultantId;


                    newProject.CreatedBy = await _userManager.GetUserAsync(User);
                    newProject.CreatedAtUtc = DateTime.UtcNow;
                    newProject.UpdatedBy = newProject.CreatedBy;
                    newProject.UpdatedAtUtc = newProject.CreatedAtUtc;

                    _context.Projects.Add(newProject);
                    _context.SaveChanges();

                    //dropdownlist 
                    FillDropdownListWithData();

                    TempData[StaticString.StatusMessage] = "تم انشاء المشروع بنجاح.";
                    return RedirectToAction(nameof(Form), new { id = newProject.ProjectId });
                }

                //edit existing
                Project editproject = new Project();
                editproject = _context.Projects.Where(x => x.ProjectId.Equals(project.ProjectId)).FirstOrDefault();

                if (editproject != null)
                {
                    //duplicate project Name  is not allowed
                    Project empNumber = await _context.Projects.Where(x => x.ProjectName.Equals(project.ProjectName)).FirstOrDefaultAsync();

                    editproject.ProjectName = project.ProjectName;
                    editproject.ProjectCode = project.ProjectCode;
                    editproject.StartDate = project.StartDate;
                    editproject.EndDate = project.EndDate;
                    editproject.EstimatedBudget = project.EstimatedBudget;
                    editproject.ContractualBudget = project.ContractualBudget;
                    editproject.ProjectDescription = project.ProjectDescription;
                    editproject.StatusId = project.StatusId;

                    editproject.Sector = project.Sector;
                    editproject.Region = project.Region;

                    editproject.ContractorId = project.ContractorId;
                    editproject.SupervisionConsultantId = project.SupervisionConsultantId;
                    editproject.ProjectManagementConsultantId = project.ProjectManagementConsultantId;

                    editproject.UpdatedBy = await _userManager.GetUserAsync(User);
                    editproject.UpdatedAtUtc = DateTime.UtcNow;


                    _context.Update(editproject);
                    _context.SaveChanges();

                    //dropdownlist 
                    FillDropdownListWithData();

                    TempData[StaticString.StatusMessage] = "تم تعديل بيانات المشروع بنجاح.";
                    return RedirectToAction(nameof(Form), new { id = editproject.ProjectId });
                }
                else
                {
                    return NotFound();
                }


            }
            catch (Exception ex)
            {

                TempData[StaticString.StatusMessage] = " خطأ : " + ex.InnerException.Message;
                return RedirectToAction(nameof(Form), new { id = project.ProjectId });
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

            var del = _context.Projects.Include(a => a.Contractor)
               .AsNoTracking()
               .Include(x => x.SupervisionConsultant)
               .Include(x => x.ProjectManagementConsultant)
               .OrderByDescending(x => x.CreatedAtUtc).ToList();

            //dropdownlist 
            FillDropdownListWithData();

            return View(del);
        }

        //delete submitted project if found, otherwise 404
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SubmitDelete([Bind("ProjectId")] Project project)
        {
            try
            {
                var del = _context.Projects.Where(x => x.ProjectId == project.ProjectId).FirstOrDefault();
                if (del == null)
                {
                    return NotFound();
                }

                _context.Projects.Remove(del);
                _context.SaveChanges();

                TempData[StaticString.StatusMessage] = "تم حذف المشروع بنجاح.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                TempData[StaticString.StatusMessage] = " خطأ : " + ex.Message;
                return RedirectToAction(nameof(Delete), new { id = project.ProjectId });
            }
        }

       
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

            return RedirectToAction("Index", "SelfService", new { period = DateTime.Now.ToString("yyyy-MM") });
        }

    }
}
