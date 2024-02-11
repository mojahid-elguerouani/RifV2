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


            var projectList = await _context.Projects.Where(a => a.StatusId == 1)
                                    .OrderBy(a => a.StartDate)
                                    .ToListAsync();

            return View(projectList);
        }
}
}
