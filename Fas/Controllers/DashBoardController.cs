using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FasDemo.Data;
using FasDemo.Models;
using FasDemo.ProjectModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectManagment.Models;
using Microsoft.AspNetCore.Http;
using FasDemo.ProjectModel.DTO;
using Microsoft.AspNetCore.Hosting;
using FasDemo.ViewModels;
using Newtonsoft.Json;
using Fas.ProjectVM;
using Newtonsoft.Json.Linq;

namespace FasDemo.Controllers
{
    [Authorize(Roles = Services.App.Pages.DashBoard.RoleName)]
    public class DashBoardController : Controller
    {
        JsonSerializerSettings _jsonSetting = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };

        private IHostingEnvironment Environment;

        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly Services.Security.ICommon _security;
        private readonly Services.App.ICommon _app;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public DashBoardController(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            Services.Security.ICommon security,
            Services.App.ICommon app,
            SignInManager<ApplicationUser> signInManager,
            IHostingEnvironment _environment
            )
        {
            _context = context;
            _userManager = userManager;
            _security = security;
            _app = app;
            _signInManager = signInManager;
            Environment = _environment;
        }

        //fill viewdata as dropdownlist datasource for Project form
        private void FillDropdownListWithData()
        {
            ViewData["SystemUser"] = _app.GetSystemUserSelectList();
            ViewData["ContractorUser"] = _app.GetContractorSelectList();
            ViewData["EmployeeUser"] = _app.GetEmployeeUserSelectList();
        }

        public async Task<IActionResult> Index()
        {
            var projectList = await _context.Projects.Where(a => a.StatusId == 1).OrderBy(a => a.StartDate).ToListAsync();

            return View(projectList);
        }

        [HttpGet]
        public IActionResult Details(string region)
        {

            var objs = _context.Projects.Where(x => x.Region == region).Include(a => a.Contractor)
               .AsNoTracking()
               //.Include(x => x.ProjectProgram)
               .Include(x => x.SupervisionConsultant)
               .Include(x => x.ProjectManagementConsultant)
               .OrderByDescending(x => x.CreatedAtUtc).ToList();


            return View(objs);
        }


        public List<MyArray> getAllArea()
        {
            var list = new List<MyArray>();
            var projectInDb = _context.Projects.GroupBy(x => x.Region).ToList();
            foreach (var item in projectInDb)
            {
                var project = new MyArray();
                var smalProject = new CountryWithProjects();
                if (item.Key == "الرياض")
                {
                    project.CountrySymbol = "SA01";
                    smalProject.CountryName = item.Key;
                    smalProject.ProjectNumber = item.Count();
                    project.CountryWithProjects = smalProject;
                    list.Add(project);
                }
                else if (item.Key == "مكة المكرمة")
                {
                    project.CountrySymbol = "SA02";
                    smalProject.CountryName = item.Key;
                    smalProject.ProjectNumber = item.Count();
                    project.CountryWithProjects = smalProject;
                    list.Add(project);
                }
                else if (item.Key == "المدينة المنورة")
                {
                    project.CountrySymbol = "SA03";
                    smalProject.CountryName = item.Key;
                    smalProject.ProjectNumber = item.Count();
                    project.CountryWithProjects = smalProject;
                    list.Add(project);
                }
                else if (item.Key == "المنطقة الشرقية")
                {
                    project.CountrySymbol = "SA04";
                    smalProject.CountryName = item.Key;
                    smalProject.ProjectNumber = item.Count();
                    project.CountryWithProjects = smalProject;
                    list.Add(project);
                }
                else if (item.Key == "القصيم")
                {
                    project.CountrySymbol = "SA05";
                    smalProject.CountryName = item.Key;
                    smalProject.ProjectNumber = item.Count();
                    project.CountryWithProjects = smalProject;
                    list.Add(project);
                }
                else if (item.Key == "حائل")
                {
                    project.CountrySymbol = "SA06";
                    smalProject.CountryName = item.Key;
                    smalProject.ProjectNumber = item.Count();
                    project.CountryWithProjects = smalProject;
                    list.Add(project);
                }
                else if (item.Key == "تبوك")
                {
                    project.CountrySymbol = "SA07";
                    smalProject.CountryName = item.Key;
                    smalProject.ProjectNumber = item.Count();
                    project.CountryWithProjects = smalProject;
                    list.Add(project);
                }
                else if (item.Key == "الحدود الشمالية")
                {
                    project.CountrySymbol = "SA08";
                    smalProject.CountryName = item.Key;
                    smalProject.ProjectNumber = item.Count();
                    project.CountryWithProjects = smalProject;
                    list.Add(project);
                }
                else if (item.Key == "جازان")
                {
                    project.CountrySymbol = "SA09";
                    smalProject.CountryName = item.Key;
                    smalProject.ProjectNumber = item.Count();
                    project.CountryWithProjects = smalProject;
                    list.Add(project);
                }
                else if (item.Key == "نجران")
                {
                    project.CountrySymbol = "SA10";
                    smalProject.CountryName = item.Key;
                    smalProject.ProjectNumber = item.Count();
                    project.CountryWithProjects = smalProject;
                    list.Add(project);
                }
                else if (item.Key == "الباحة")
                {
                    project.CountrySymbol = "SA11";
                    smalProject.CountryName = item.Key;
                    smalProject.ProjectNumber = item.Count();
                    project.CountryWithProjects = smalProject;
                    list.Add(project);
                }
                else if (item.Key == "الجوف")
                {
                    project.CountrySymbol = "SA12";
                    smalProject.CountryName = item.Key;
                    smalProject.ProjectNumber = item.Count();
                    project.CountryWithProjects = smalProject;
                    list.Add(project);
                }
                else if (item.Key == "عسير")
                {
                    project.CountrySymbol = "SA14";
                    smalProject.CountryName = item.Key;
                    smalProject.ProjectNumber = item.Count();
                    project.CountryWithProjects = smalProject;
                    list.Add(project);
                }

            }
            return list;
        }
    }
}
