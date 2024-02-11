using FasDemo.Data;
using FasDemo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProjectManagment.Models;
using System.Linq;

namespace FasDemo.Controllers
{
    [Authorize(Roles = Services.App.Pages.Agenda.RoleName)]
    public class AgendaController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly Services.Security.ICommon _security;
        private readonly Services.App.ICommon _app;
        private readonly SignInManager<ApplicationUser> _signInManager;
        JsonSerializerSettings _jsonSetting = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };

        //dependency injection through constructor, to directly access services
        public AgendaController(
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
            //ViewData["CustomerUser"] = _app.GetCustomerSelectList();
            //ViewData["ProjectSchedualTemplet"] = _app.GetProjectSchedualTempletSelectList();
            ViewData["Employee"] = _app.GetEmployeeSelectList();
            //ViewData["ProjectApprovedBy"] = _app.GetSystemUserSelectList();
        }

        public IActionResult Index()
        {
            return View();
        }

        #region calender
        [HttpGet]
        public JsonResult GetEvents()
        {
            var currenntUser = IdentityId();// _userManager.GetUserAsync(User);
            var events = _context.Events.Where(a => a.UserId == currenntUser).ToList();


            //var events = _context.Events.ToList();
            //return Ok(events);
            return Json(events);
        }


        [HttpPost]
        public JsonResult SaveEvent(Event e)
        {
            var status = false;
            var currenntUser = IdentityId();// _userManager.GetUserAsync(User);
            if (e.EventID > 0)
            {
                //Update the event
                var v = _context.Events.Where(a => a.EventID == e.EventID).FirstOrDefault();
                if (v != null)
                {
                    v.Subject = e.Subject;
                    v.Start = e.Start;
                    v.End = e.End;
                    v.Description = e.Description;
                    v.IsFullDay = e.IsFullDay;
                    v.ThemeColor = e.ThemeColor;
                    v.UserId = currenntUser;

                }
            }
            else
            {
                e.UserId = currenntUser;
                _context.Events.Add(e);
            }

            _context.SaveChanges();
            status = true;


            return Json(status);
        }


        [HttpPost]
        public JsonResult DeleteEvent(int eventID)
        {
            var status = false;

            var v = _context.Events.Where(a => a.EventID == eventID).FirstOrDefault();
            if (v != null)
            {
                _context.Events.Remove(v);
                _context.SaveChanges();
                status = true;
            }

            return Json(status);
        }
        #endregion


        public string IdentityId()
        {
            var a = User.Identities.ToArray();
            var b = a[0].Claims.ToArray();
            var userID = b[0].Value;//  Context.User.Identity.Name;
            return userID;
        }
    }
}
