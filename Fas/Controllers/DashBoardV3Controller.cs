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
using FasDemo.ProjectVM;
using FasDemo.Services.App;

namespace FasDemo.Controllers
{
    [Authorize(Roles = Services.App.Pages.Employee.RoleName)]
    public class DashBoardV3Controller : Controller
    {

        JsonSerializerSettings _jsonSetting = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };

        private IHostingEnvironment Environment;

        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly Services.Security.ICommon _security;
        private readonly Services.App.ICommon _app;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public DashBoardV3Controller(
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
        public async Task<IActionResult> Index(string period)
        {
            TaskSummary taskSummary = new TaskSummary();
            //ApplicationUser applicationUser = await _userManager.GetUserAsync(User);
            string tempid = "ff767992-00da-4e84-979f-1488c3835ddb";
            SqlParameter[] parameters1 = {
                    new SqlParameter("@cmdType", "GetProjectRatio"),
                    new SqlParameter("@ProjectAssignToId", tempid ),//applicationUser.Id
                     new SqlParameter("@date", period )//applicationUser.Id
                };

            //var result = await context.SomeModels.FromSql("SQL_SCRIPT").ToListAsync();
            var objs = await _context.Query<ProjectRatioVM>()
                                    .FromSql("ProjectEmployeeBonus @cmdType, @ProjectAssignToId, @date", parameters1)
                                    .ToListAsync();

            ViewData["ChartDoughnut"] = GetProjectEmployeeBonus(objs);



            return View(objs);
        }


        public ChartDoughnut GetProjectEmployeeBonus(List<ProjectRatioVM> projectRatioVMs)
        {
            try
            {
                ChartDoughnut result = new ChartDoughnut();
                List<string> labels = new List<string>();
                List<string> colors = new List<string>();
                List<int> datas = new List<int>();
                int index = 0;
                var usersGroupedByCountry = projectRatioVMs.GroupBy(user => user.ProjectAssignToId);
                foreach (var item in usersGroupedByCountry.ToList())
                {
                    labels.Add(_context.Employee.Where(a => a.SystemUserId == item.Key).FirstOrDefault()?.FirstName);

                    colors.Add(ColorList.GetAllRGBA()[index]);
                    index++;
                    int? count = projectRatioVMs.Where(x => x.ProjectAssignToId==item.Key)?.Count();

                    datas.Add(count ?? 0);
                }

                result.Labels = labels.ToArray();
                result.Colors = colors.ToArray();
                result.Data = datas.ToArray();

                return result;
            }
            catch (Exception ex)
            {

                throw;
            }
        }


    }
}