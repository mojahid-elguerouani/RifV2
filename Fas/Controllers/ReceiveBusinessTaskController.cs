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
using Newtonsoft.Json;
using Microsoft.AspNetCore.SignalR;
using FasDemo.Hubs;
using ChatApp.Models;
using ChatApp.Domain.Entity;
using FasDemo.SurveyModel.ViewModel;
using FasDemo.SurveyModel;


namespace FasDemo.Controllers
{
    public class ReceiveBusinessTaskController : Controller
    {

        JsonSerializerSettings _jsonSetting = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };
        private IHostingEnvironment Environment;
        private readonly IHubContext<ChatHub> _hubContext;

        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly Services.Security.ICommon _security;
        private readonly Services.App.ICommon _app;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public ReceiveBusinessTaskController(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            Services.Security.ICommon security,
            Services.App.ICommon app,
            SignInManager<ApplicationUser> signInManager,
            IHostingEnvironment _environment,
            IHubContext<ChatHub> hubContext
            )
        {
            _context = context;
            _userManager = userManager;
            _security = security;
            _app = app;
            _signInManager = signInManager;
            Environment = _environment;
            _hubContext = hubContext;
        }

        private void FillDropdownListWithData()
        {
            ViewData["SystemUser"] = _app.GetSystemUserSelectList();
            ViewData["ContractorUser"] = _app.GetContractorSelectList();
            ViewData["EmployeeUser"] = _app.GetEmployeeUserSelectList();
        }


        public async Task<IActionResult> Index()
        {

            ApplicationUser applicationUser = await _userManager.GetUserAsync(User);

            string tempid = applicationUser.Id;

            SqlParameter[] parameters1 = {
                 new SqlParameter("@cmdType", "GetUserTasks"),
                 new SqlParameter("@AssignTo", tempid)
            };

            var objs = await _context.Query<ReceiveBusinessTaskVM>().FromSql("sp_DisplayReceiveBusinessTasks @cmdType, @AssignTo", parameters1).ToListAsync();

            return View(objs);

        }

        //display project create edit form
        [HttpGet]
        public IActionResult Form(int id)
        {
            //create new
            if (id == null)
            {
                //dropdownlist 
                FillDropdownListWithData();

                ReceiveBusiness newObj = new ReceiveBusiness();
                return View(newObj);
            }

            //edit object
            ReceiveBusinessTask editObj = new ReceiveBusinessTask();
            editObj = _context.ReceiveBusinessTasks.Include(a => a.ReceiveBusinessTaskLogs).ThenInclude(a => a.ReceiveBusinessTaskLogImages)
                .Where(x => x.ReceiveBusinessTaskId == id).Include(x => x.ReceiveBusiness).ThenInclude(p => p.Project).FirstOrDefault();

            if (editObj == null)
            {
                return NotFound();
            }

            //dropdownlist 
            FillDropdownListWithData();

            ViewBag.ProjectName = editObj.ReceiveBusiness.Project.ProjectName;
            ViewBag.ReceiveBusinessSerialNumber = editObj.ReceiveBusiness.SerialNumber;
            ViewBag.ReceiveBusinessReviewNumber = editObj.ReceiveBusiness.ReviewNumber;

            
            ViewBag.ReceiveBusinessTaskName = editObj.TaskName;

            

            return View(editObj);

        }

        //post submitted project data. if projectId is null then create new, otherwise edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SubmitForm([Bind(
            "ReceiveBusinessTaskId,ReceiveBusinessId,TaskName,TaskOrder,TaskId,AssignTo,IsActive,StatusId,IsApproved,ApprovedBy,TaskParentId,Compleation,ReceiveBusinessAssignToId")] ReceiveBusinessTask receivebusinessTask,
            string ReceiveBusinessTaskComment, string service_type, IFormFile[] files)
        {
            try
            {
                ApplicationUser currentser = await _userManager.GetUserAsync(User);
                if (!ModelState.IsValid)
                {
                    TempData[StaticString.StatusMessage] = " خطأ : حالة النموذج غير صالحة.";
                    return RedirectToAction(nameof(Form), new { id = receivebusinessTask.ReceiveBusinessTaskId.ToString() ?? "" });
                }

                //edit existing
                ReceiveBusinessTask _receivebusinessTask = _context.ReceiveBusinessTasks.Find(receivebusinessTask.ReceiveBusinessTaskId);

                ReceiveBusinessTask _receivebusinessTask2 = _context.ReceiveBusinessTasks.Include(p => p.ReceiveBusiness).ThenInclude(pi => pi.Project).FirstOrDefault(p => p.ReceiveBusinessTaskId == receivebusinessTask.ReceiveBusinessTaskId);

                if (_receivebusinessTask != null)
                {

                    if (service_type == "blue")
                    {
                        if (receivebusinessTask.Compleation == 100 && _receivebusinessTask.ApprovedById == currentser.Id)
                        {
                            _receivebusinessTask.IsApproved = true;
                            _receivebusinessTask.ApproveDate = DateTime.Now;
                            //_receivebusinessTask.EndDateActual = DateTime.Now;
                            List<ReceiveBusinessTask> __receivebusinessTask = _context.ReceiveBusinessTasks.Where(a => a.TaskParentId == _receivebusinessTask.TaskId
                                                                  && a.ReceiveBusinessId == _receivebusinessTask.ReceiveBusinessId).ToList();
                            if (__receivebusinessTask != null && __receivebusinessTask.Count > 0)
                            {
                                foreach (ReceiveBusinessTask item in __receivebusinessTask)
                                {
                                    //item.StartDateActual = DateTime.Now;
                                    //item.EndDateActual = DateTime.Now.AddDays((double)item.Duration);

                                    string porjectName = _context.Projects.Where(a => a.ProjectId == _receivebusinessTask2.ReceiveBusiness.Project.ProjectId).FirstOrDefault().ProjectName;
                                    string notificationType = receivebusinessTask.TaskName + "==>" + porjectName + "==>" + "تم اضافة مهمة جديده";
                                    string toUserID = item.ReceiveBusinessAssignToId;

                                    int notificationID = SaveUserNotification(notificationType, currentser.Id, toUserID);

                                    var connectionId = _context.OnlineUsers.Where(m => m.UserID == toUserID && m.IsActive == true && m.IsOnline == true).Select(m => m.ConnectionID).ToList();
                                    if (connectionId != null && connectionId.Count() > 0)
                                    {
                                        var userInfo = GetUserModel(currentser.Id);
                                        int notificationCounts = GetUserNotificationCounts(toUserID);
                                        await _hubContext.Clients.Clients(connectionId).SendAsync("ReceiveNotification", notificationType, userInfo, 2, notificationCounts);
                                    }
                                }
                                await _context.SaveChangesAsync();
                            }

                        }
                        _receivebusinessTask.Compleation = _receivebusinessTask.Compleation;
                    }
                    if (service_type == "red")
                    {
                        _receivebusinessTask.ReceiveBusinessAssignToId = _receivebusinessTask.ReceiveBusinessAssignToId;
                    }
                    _receivebusinessTask.UpdateDate = DateTime.Now;
                    if (service_type == "green")
                    {
                        ReceiveBusinessTask __receivebusinessTask = new ReceiveBusinessTask
                        {
                            ReceiveBusinessAssignToId = receivebusinessTask.ReceiveBusinessAssignToId,
                            ApprovedById = currentser.Id,
                            //StarDate = ReceiveBusinessTask.StarDate,
                            //EndDate = ReceiveBusinessTask.StarDate.Value.AddDays(receivebusinessTask.Duration ?? 1),
                            TaskName = "طلب تعديلات",
                            Compleation = 0,
                            ReceiveBusinessId = _receivebusinessTask.ReceiveBusinessId,
                            TaskId = 999,
                            IsActive = true,
                            //Duration = ReceiveBusinessTask.Duration,
                            StatusId = 2,
                            TaskOrder = _receivebusinessTask.TaskOrder,
                            IsApproved = false,
                            CreatedById = currentser.Id,
                            CreatedAtUtc = DateTime.Now,
                            UpdatedById = currentser.Id,
                            UpdatedAtUtc = DateTime.Now,

                        };
                        _context.ReceiveBusinessTasks.Add(__receivebusinessTask);
                    }

                    await _context.SaveChangesAsync();



                }



                ReceiveBusinessTaskLog _ReceiveBusinessTaskLog = new ReceiveBusinessTaskLog
                {
                    //ReceiveBusinessTaskLogId = Guid.NewGuid().ToString(),
                    ReceiveBusinessTaskId = receivebusinessTask.ReceiveBusinessTaskId,
                    ReceiveBusinessUserId = currentser.Id,
                    ReceiveBusinessTaskComment = ReceiveBusinessTaskComment == "" ? "تم تعديل النسبة الى " + receivebusinessTask.Compleation + "%" : ReceiveBusinessTaskComment,
                    CreatedOn = DateTime.Now,
                };

                _context.ReceiveBusinessTaskLog.Add(_ReceiveBusinessTaskLog);
                await _context.SaveChangesAsync();
                // تم نقله الى drag drop

                string wwwPath = this.Environment.WebRootPath;
                //string contentPath = this.Environment.ContentRootPath;

                string contentPath = "/UploadedFiles/ReceiveBusinessTasks/" + _receivebusinessTask2.ReceiveBusiness.ReceiveBusinessId.ToString() + "/";

                string strMappath = Path.Combine(this.Environment.WebRootPath, "UploadedFiles/ReceiveBusinessTasks/" + _receivebusinessTask2.ReceiveBusiness.ReceiveBusinessId.ToString() + "/");

                if (!Directory.Exists(strMappath))
                {
                    Directory.CreateDirectory(strMappath);
                }
                List<string> uploadedFiles = new List<string>();
                string UploadStatus = string.Empty; ;
                string Message = string.Empty;
                // Verify that the user selected a file
                foreach (var file in files)
                {
                    //Checking file is available to save.  
                    if (file != null)
                    {

                        var InputFileName = Path.GetFileName(file.FileName);
                        var ServerSavePath = Path.Combine(strMappath + InputFileName);
                        //Save file to server folder  
                        //file.SaveAs(ServerSavePath);
                        using (var stream = new FileStream(ServerSavePath, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }
                        uploadedFiles.Add(InputFileName);
                        Message += string.Format("<b>{0}</b> تم التحميل.<br />", InputFileName);

                        //assigning file uploaded status to ViewBag for showing message to user.  
                        UploadStatus = files.Count().ToString() + " ملفات تم رفعها بنجاح.";
                        ReceiveBusinessTaskLogImage _ReceiveBusinessTaskLogImage = new ReceiveBusinessTaskLogImage
                        {
                            //ReceiveBusinessTaskLogImageId = Guid.NewGuid().ToString(),
                            ReceiveBusinessTaskLogId = _ReceiveBusinessTaskLog.ReceiveBusinessTaskLogId,
                            FileName = InputFileName,
                            ImageType = file.ContentType,
                            CreatedAtUtc = DateTime.Now,
                            ReceiveBusinessTaskLogImageUrl = contentPath + InputFileName // ServerSavePath
                        };
                        _context.ReceiveBusinessTaskLogImages.Add(_ReceiveBusinessTaskLogImage);
                        await _context.SaveChangesAsync();
                    }
                }
                Message += string.Format("<b>{0}</b> تم تعديل المهمة بنجاح. ", UploadStatus);

                TempData[StaticString.StatusMessage] = Message;
                TempData[StaticString.StatusMessage] = "تم تعديل المهمة بنجاح.";
                return RedirectToAction(nameof(Form), new { id = receivebusinessTask.ReceiveBusinessTaskId.ToString() });
            }
            catch (Exception ex)
            {

                TempData[StaticString.StatusMessage] = " خطأ : " + ex.Message;
                return RedirectToAction(nameof(Form), new { id = receivebusinessTask.ReceiveBusinessId });
            }
        }


        //display project create edit form
        #region ReceiveBusinessTaskDetails
        //display project create edit form
        [HttpGet]
        public async Task<IActionResult> ReceiveBusinessTaskDetails(int? id)
        {
            {
                //create new
                if (id == null)
                {
                    return NotFound();
                }
                //edit object
                var _project = await _context.Projects.Where(x => x.ProjectId == id).FirstOrDefaultAsync();
                ViewData["EndDate"] = _project.EndDate;
                ViewData["ProjectName"] = _project.ProjectName;
                ViewData["Projectid"] = _project.ProjectId;

                ViewData["ProjectComment"] = await _context.ProjectComment.Include(x => x.CommentImages)
                                                    .Where(x => x.ProjectId == id && x.CommentToId == null).ToListAsync();
                ViewData["ProjectCommentClient"] = await _context.ProjectComment.Include(x => x.CommentImages)
                                                    .Where(x => x.ProjectId == id && x.CommentToId != null).ToListAsync();


                var ReceiveBusinessTasksList = await _context.ReceiveBusinessTasks
                                            .Where(a => a.ReceiveBusinessId == id)
                                            .Include(ctx => ctx.ReceiveBusinessAssignTo)
                                            //.Include(ctx => ctx.ReceiveBusinessTaskLogs)
                                            .OrderBy(a => a.TaskOrder)
                                            .ToListAsync();
                List<ReceiveBusinessTaskDto> editObj = new List<ReceiveBusinessTaskDto>();
                foreach (var item in ReceiveBusinessTasksList)
                {
                    try
                    {
                        var proj = new ReceiveBusinessTaskDto
                        {
                            ReceiveBusinessTaskId = item.ReceiveBusinessTaskId,
                            TaskName = item.TaskName,
                            //StarDate = item.StarDate,
                            //StarDateActual = item.StartDateActual,
                            //EndDate = item.EndDate,
                            //EndDateActual = item.EndDateActual,
                            AssignToId = item.ReceiveBusinessAssignToId,
                            //AssignTo = item.ProjectAssignTo,
                            //Firstname = _context.Employee.Where(a => a.SystemUserId == x.ProjectAssignToId).FirstOrDefault().FirstName,
                            Compleation = item.Compleation,
                            ReceiveBusinessId = item.ReceiveBusinessId,
                            TaskId = item.TaskId,
                            //Duration = item.Duration,
                            IsApproved = item.IsApproved,
                            //ReceiveBusinessTasksSum = (decimal)item.Compleation
                            Firstname = userFirstName(item.ReceiveBusinessAssignToId),
                            ProfilePicture = userImage(item.ReceiveBusinessAssignToId),
                            ReceiveBusinessTaskLog = _context.ReceiveBusinessTaskLog.AsNoTracking()
                                                .Include(x => x.ReceiveBusinessUser)
                                                .Include(x => x.ReceiveBusinessTaskLogImages)
                                                .Where(x => x.ReceiveBusinessTaskId == item.ReceiveBusinessTaskId)
                                                .Select(x => new ReceiveBusinessTaskLog
                                                {
                                                    ReceiveBusinessTaskComment = x.ReceiveBusinessTaskComment,
                                                    CreatedOn = x.CreatedOn,
                                                    ReceiveBusinessTaskLogImages = x.ReceiveBusinessTaskLogImages,
                                                    ReceiveBusinessUser = x.ReceiveBusinessUser
                                                }).ToList()
                        };

                        editObj.Add(proj);
                    }
                    catch (Exception ex)
                    {


                    }
                };


                var _ReceiveBusinessTasksSum = editObj.Sum(d => d.Compleation * d.Duration);
                var _ReceiveBusinessTasksSum1 = editObj.Sum(d => 100.00 * d.Duration);

                var _ReceiveBusinessTasksSum2 = (_ReceiveBusinessTasksSum / _ReceiveBusinessTasksSum1) * 100;

                if (editObj == null)
                {
                    return NotFound();
                }

                ViewData["currenttask"] = editObj.Where(a => a.Compleation < 100).FirstOrDefault();
                ViewData["Team"] = editObj.GroupBy(s => s.AssignToId).Select(g => g.FirstOrDefault()).ToList();



                List<Datum> Data = new List<Datum>();
                foreach (var item in ReceiveBusinessTasksList)
                {
                    Datum datum = new Datum
                    {
                        Name = item.TaskName,
                        Completed = new Completed { Amount = item.Compleation.ToString(), Fill = "#180" },
                        Dependency = item.TaskParentId.ToString(),
                        //Start = item.StarDate,
                        //End = item.EndDate == null ? item.StarDate.Value.AddDays(1) : item.EndDate,
                        Owner = item.ReceiveBusinessAssignToId == null ? item.ReceiveBusinessAssignToId.ToString() : item.ApprovedById,
                        Id = item.TaskId.ToString(),
                        //Milestone = item.Duration == 0 ? true : false
                    };
                    Data.Add(datum);
                }


                SeriesValues seriesValues = new SeriesValues
                {
                    Name = "new one",
                    Data = Data
                };
                string xyz = JsonConvert.SerializeObject(seriesValues, _jsonSetting);



                ViewBag.DataPoints = JsonConvert.SerializeObject(seriesValues, _jsonSetting);
                ViewBag.DataPoints = JsonConvert.SerializeObject(Data, _jsonSetting);


                SqlParameter[] parameters1 = {
                new SqlParameter("@cmdType", "GetTaskstatus"),
                 new SqlParameter("@ProjectId", id)//applicationUser.Id
            };


                var objs = await _context.Query<TaskstatusVM>().FromSql("ManageTasks @cmdType, @ProjectId", parameters1).ToListAsync();

                ViewBag.Taskstatus = JsonConvert.SerializeObject(objs, _jsonSetting);

                string ResponseId = id.ToString();

                List<Response> responses = _context.Responses.Where(q => q.ResponseId == ResponseId)
               .ToList();

                List<QuestionVM> questions = null;

                if (responses.Count > 0)
                {
                    var questionVMs = _context.Questions.ToList();
                    questions = _context.Questions.Where(q => q.SurveyId == responses.FirstOrDefault().SurveyId)
                       .Select(q => new QuestionVM
                       {
                           QuestionId = q.QuestionId,
                           QuestionText = q.QuestionBody,
                           QuestionType = q.AnswerType,
                           Choices = responses.Where(c => c.QuestionId == q.QuestionId)
                                .Select(c => new ChoiceVM
                                {
                                    ChoiceID = c.ChoiceId,
                                    ChoiceText = c.ChoiceText
                                }).ToList()

                       }).ToList();

                    ViewData["survey"] = questions;
                }
                else
                {
                    QuizVM quizSelected = _context.Surveys.Where(q => q.Id == "07636B1A-790C-4C96-A506-B8CEDA98CE97").Select(q => new QuizVM
                    {
                        QuizID = q.Id,
                        QuizName = q.Title,

                    }).FirstOrDefault();

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
                    ViewData["Newsurvey"] = questions;
                }


                return View(editObj);

            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SubmitReceiveBusinessTaskDetails(string ReceiveBusinessTaskComment, int projectId, IFormFile[] files)
        {
            ApplicationUser applicationUser = await _userManager.GetUserAsync(User);
            ProjectComment projectComment = new ProjectComment
            {
                Comment = ReceiveBusinessTaskComment,
                CreatedById = applicationUser.Id,
                CreatedAtUtc = DateTime.Now,
                UpdatedById = applicationUser.Id,
                UpdatedAtUtc = DateTime.Now,
                CommentFromId = applicationUser.Id,
                ProjectId = projectId
            };

            _context.ProjectComment.Add(projectComment);
            await _context.SaveChangesAsync();

            string wwwPath = this.Environment.WebRootPath;
            string contentPath = "/UploadedFiles/ReceiveBusinessTasks/" + projectId.ToString() + "/CommentImage/";

            string strMappath = Path.Combine(this.Environment.WebRootPath, "UploadedFiles/ReceiveBusinessTasks/" + projectId.ToString() + "/CommentImage/");

            if (!Directory.Exists(strMappath))
            {
                Directory.CreateDirectory(strMappath);
            }
            List<string> uploadedFiles = new List<string>();
            string UploadStatus = string.Empty; ;
            string Message = string.Empty;
            // Verify that the user selected a file
            foreach (var file in files)
            {
                //Checking file is available to save.  
                if (file != null)
                {

                    var InputFileName = Path.GetFileName(file.FileName);
                    var ServerSavePath = Path.Combine(strMappath + InputFileName);
                    using (var stream = new FileStream(ServerSavePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    uploadedFiles.Add(InputFileName);
                    Message += string.Format("<b>{0}</b> تم التحميل.<br />", InputFileName);

                    //assigning file uploaded status to ViewBag for showing message to user.  
                    UploadStatus = files.Count().ToString() + " ملفات تم رفعها بنجاح.";
                    CommentImage commentImage = new CommentImage
                    {
                        ProjectCommentId = projectComment.ProjectCommentId,
                        FileName = InputFileName,
                        ImageType = file.ContentType,
                        CommentImageUrl = contentPath + InputFileName // ServerSavePath
                    };
                    _context.CommentImage.Add(commentImage);
                    await _context.SaveChangesAsync();
                }
            }
            Message += string.Format("<b>{0}</b> تم تعديل المهمة بنجاح. ", UploadStatus);

            TempData[StaticString.StatusMessage] = Message;
            //TempData[StaticString.StatusMessage] = "تم تعديل المهمة بنجاح.";
            return RedirectToAction("ReceiveBusinessTaskDetails", "ReceiveBusinessTask", new { id = projectId.ToString() });
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SubmitClientReceiveBusinessTaskDetails(string ReceiveBusinessTaskComment, int projectId, IFormFile[] files)
        {
            ApplicationUser applicationUser = await _userManager.GetUserAsync(User);
            string contractorId = _context.Projects
                    .Where(a => a.ProjectId == projectId)
                    .Include(a => a.Contractor).FirstOrDefault().Contractor.ContractorUserId;
            ProjectComment projectComment = new ProjectComment
            {
                Comment = ReceiveBusinessTaskComment,
                CreatedById = applicationUser.Id,
                CreatedAtUtc = DateTime.Now,
                UpdatedById = applicationUser.Id,
                UpdatedAtUtc = DateTime.Now,
                CommentFromId = applicationUser.Id,
                CommentToId = contractorId,
                ProjectId = projectId
            };

            _context.ProjectComment.Add(projectComment);
            await _context.SaveChangesAsync();

            string wwwPath = this.Environment.WebRootPath;
            string contentPath = "/UploadedFiles/ReceiveBusinessTasks/" + projectId.ToString() + "/CommentImage/";

            string strMappath = Path.Combine(this.Environment.WebRootPath, "UploadedFiles/ReceiveBusinessTasks/" + projectId.ToString() + "/CommentImage/");

            if (!Directory.Exists(strMappath))
            {
                Directory.CreateDirectory(strMappath);
            }
            List<string> uploadedFiles = new List<string>();
            string UploadStatus = string.Empty; ;
            string Message = string.Empty;
            // Verify that the user selected a file
            foreach (var file in files)
            {
                //Checking file is available to save.  
                if (file != null)
                {

                    var InputFileName = Path.GetFileName(file.FileName);
                    var ServerSavePath = Path.Combine(strMappath + InputFileName);
                    using (var stream = new FileStream(ServerSavePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    uploadedFiles.Add(InputFileName);
                    Message += string.Format("<b>{0}</b> تم التحميل.<br />", InputFileName);

                    //assigning file uploaded status to ViewBag for showing message to user.  
                    UploadStatus = files.Count().ToString() + " ملفات تم رفعها بنجاح.";
                    CommentImage commentImage = new CommentImage
                    {
                        ProjectCommentId = projectComment.ProjectCommentId,
                        FileName = InputFileName,
                        ImageType = file.ContentType,
                        CommentImageUrl = contentPath + InputFileName // ServerSavePath
                    };
                    _context.CommentImage.Add(commentImage);
                    await _context.SaveChangesAsync();
                }
            }
            Message += string.Format("<b>{0}</b> تم تعديل المهمة بنجاح. ", UploadStatus);

            TempData[StaticString.StatusMessage] = Message;
            //TempData[StaticString.StatusMessage] = "تم تعديل المهمة بنجاح.";
            return RedirectToAction("ReceiveBusinessTaskDetails", "ReceiveBusinessTask", new { id = projectId.ToString() });
        }



        [HttpPost]
        public ActionResult Download(int? id, string Url)
        {
            string fullName = Path.Combine(this.Environment.WebRootPath, Url);
            byte[] fileBytes = GetFile(fullName);
            return File(
                fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, Url);


        }
        byte[] GetFile(string s)
        {
            System.IO.FileStream fs = System.IO.File.OpenRead(s);
            byte[] data = new byte[fs.Length];
            int br = fs.Read(data, 0, data.Length);
            if (br != fs.Length)
                throw new System.IO.IOException(s);
            return data;
        }
        public FileResult Download(string imagename, string Url)
        {
            string filePath = this.Environment.WebRootPath + Url.Replace("../../", "");


            byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);

            string fileName = imagename;
            //return PhysicalFile(fullName, fileName);


            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }
        #endregion
        [HttpGet]
        public async Task<IActionResult> LoginOnBehalf(string id)
        {
            ApplicationUser appUser = new ApplicationUser();
            string[] variables = id.Split('&');
            id = variables[0];
            string ReceiveBusinessTaskId = variables[1];
            appUser = await _userManager.FindByIdAsync(id);

            if (appUser == null)
            {
                return NotFound();
            }

            //attempt to sign in
            await _signInManager.SignInAsync(appUser, false);

            await _signInManager.RefreshSignInAsync(appUser);

            return RedirectToAction("Form", "ReceiveBusinessTask", new { id = ReceiveBusinessTaskId });
        }

        public string userImage(string userId)
        {

            string ProfilePicture = "/assets/images/user/avatar-2.jpg";
            var existing = _context.Employee.Where(a => a.SystemUserId == userId).FirstOrDefault();
            ProfilePicture = existing != null ?
                                   "/" + _context.Employee.Where(a => a.SystemUserId == userId).FirstOrDefault().ProfilePicture :
                                    "/assets/images/user/avatar-2.jpg";

            return ProfilePicture;
        }
        public string userFirstName(string userId)
        {
            string FirstName = "عميل";
            var existing = _context.Employee.Where(a => a.SystemUserId == userId).FirstOrDefault();
            FirstName = existing != null ?
                                    _context.Employee.Where(a => a.SystemUserId == userId).FirstOrDefault().FirstName + " " + _context.Employee.Where(a => a.SystemUserId == userId).FirstOrDefault().LastName :
                                    _context.Contractors.Where(a => a.ContractorUserId == userId).FirstOrDefault().ContractorName;
            return FirstName;
        }
        public UserModel GetUserModel(string id, ApplicationUser objentity = null, string friendRequestStatus = "", bool isRequestReceived = false)
        {
            var user = new ApplicationUser();
            if (objentity != null)
            {
                user = objentity;
            }
            else
            {

                user = _context.ApplicationUser.Where(m => m.Id == id).FirstOrDefault();
            }
            UserModel objmodel = new UserModel();
            if (user != null)
            {
                objmodel.IsRequestReceived = isRequestReceived;
                objmodel.FriendRequestStatus = friendRequestStatus;
                objmodel.UserID = user.Id;
                objmodel.Name = userFirstName(user.Id);
                objmodel.ProfilePicture = userImage(user.Id);

            }
            return objmodel;
        }
        public int SaveUserNotification(string notificationType, string fromUserID, string toUserID)
        {
            UserNotification notification = new UserNotification();
            notification.CreatedOn = System.DateTime.Now;
            notification.IsActive = true;
            notification.NotificationType = notificationType;
            notification.FromUserID = fromUserID;
            notification.Status = "New";
            notification.UpdatedOn = System.DateTime.Now;
            notification.ToUserID = toUserID;
            _context.UserNotifications.Add(notification);
            _context.SaveChanges();
            return notification.NotificationID;
        }
        public int GetUserNotificationCounts(string toUserID)
        {
            int count = _context.UserNotifications.Where(m => m.Status == "New" && m.ToUserID == toUserID && m.IsActive == true).Count();
            return count;
        }

        [HttpPost]
        public async Task<JsonResult> AJAXPost(int ReceiveBusinessTaskId)
        {
            ApplicationUser currentser = await _userManager.GetUserAsync(User);
            //edit existing
            try
            {


                ReceiveBusinessTask _ReceiveBusinessTask = _context.ReceiveBusinessTasks.Find(ReceiveBusinessTaskId);
                _ReceiveBusinessTask.IsApproved = true;
                _ReceiveBusinessTask.ApproveDate = DateTime.Now;
                //_ReceiveBusinessTask.EndDateActual = DateTime.Now;
                _ReceiveBusinessTask.Compleation = 100;
                _context.SaveChanges();
                List<ReceiveBusinessTask> __ReceiveBusinessTask = _context.ReceiveBusinessTasks.Where(a => a.TaskParentId == _ReceiveBusinessTask.TaskId
                                                      && a.ReceiveBusinessId == _ReceiveBusinessTask.ReceiveBusinessId).ToList();
                if (__ReceiveBusinessTask != null && __ReceiveBusinessTask.Count > 0)
                {
                    foreach (ReceiveBusinessTask item in __ReceiveBusinessTask)
                    {
                        //item.StartDateActual = DateTime.Now;
                        //item.EndDateActual = DateTime.Now.AddDays((double)item.Duration);


                        string porjectName = _context.Projects.Where(a => a.ProjectId == _ReceiveBusinessTask.ReceiveBusinessId).FirstOrDefault().ProjectName;
                        string notificationType = _ReceiveBusinessTask.TaskName + "==>" + porjectName + "==>" + "تم اضافة مهمة جديده";
                        string toUserID = item.ReceiveBusinessAssignToId;

                        int notificationID = SaveUserNotification(notificationType, currentser.Id, toUserID);

                        var connectionId = _context.OnlineUsers.Where(m => m.UserID == toUserID && m.IsActive == true && m.IsOnline == true).Select(m => m.ConnectionID).ToList();
                        if (connectionId != null && connectionId.Count() > 0)
                        {
                            var userInfo = GetUserModel(currentser.Id);
                            int notificationCounts = GetUserNotificationCounts(toUserID);
                            await _hubContext.Clients.Clients(connectionId).SendAsync("ReceiveNotification", notificationType, userInfo, 2, notificationCounts);
                        }
                    }
                    await _context.SaveChangesAsync();
                    TempData[StaticString.StatusMessage] = " تم انهاء المهمة ." + _ReceiveBusinessTask.TaskName;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            //save it in database
            return Json("Index");
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Upload(int ReceiveBusinessTaskId, ICollection<IFormFile> files)

        {
            ApplicationUser currentser = await _userManager.GetUserAsync(User);
            ReceiveBusinessTask _ReceiveBusinessTask = _context.ReceiveBusinessTasks.Find(ReceiveBusinessTaskId);
            ReceiveBusinessTaskLog _ReceiveBusinessTaskLog = new ReceiveBusinessTaskLog
            {
                //ReceiveBusinessTaskLogId = Guid.NewGuid().ToString(),
                ReceiveBusinessTaskId = ReceiveBusinessTaskId,
                ReceiveBusinessUserId = currentser.Id,
                ReceiveBusinessTaskComment = "تم أضافة ملفات النسبة الى ",
                CreatedOn = DateTime.Now,
            };

            _context.ReceiveBusinessTaskLog.Add(_ReceiveBusinessTaskLog);
            await _context.SaveChangesAsync();


            string wwwPath = this.Environment.WebRootPath;
            //string contentPath = this.Environment.ContentRootPath;

            string contentPath = "/UploadedFiles/ReceiveBusinessTasks/" + _ReceiveBusinessTask.ReceiveBusinessId.ToString() + "/";

            string strMappath = Path.Combine(this.Environment.WebRootPath, "UploadedFiles/ReceiveBusinessTasks/" + _ReceiveBusinessTask.ReceiveBusinessId.ToString() + "/");

            if (!Directory.Exists(strMappath))
            {
                Directory.CreateDirectory(strMappath);
            }
            List<string> uploadedFiles = new List<string>();
            string UploadStatus = string.Empty; ;
            string Message = string.Empty;
            // Verify that the user selected a file
            foreach (var file in files)
            {
                //Checking file is available to save.  
                if (file != null)
                {

                    var InputFileName = Path.GetFileName(file.FileName);
                    var ServerSavePath = Path.Combine(strMappath + InputFileName);
                    //Save file to server folder  
                    //file.SaveAs(ServerSavePath);
                    using (var stream = new FileStream(ServerSavePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    uploadedFiles.Add(InputFileName);
                    Message += string.Format("<b>{0}</b> تم التحميل.<br />", InputFileName);

                    //assigning file uploaded status to ViewBag for showing message to user.  
                    UploadStatus = files.Count().ToString() + " ملفات تم رفعها بنجاح.";
                    ReceiveBusinessTaskLogImage _ReceiveBusinessTaskLogImage = new ReceiveBusinessTaskLogImage
                    {
                        //ReceiveBusinessTaskLogImageId = Guid.NewGuid().ToString(),
                        ReceiveBusinessTaskLogId = _ReceiveBusinessTaskLog.ReceiveBusinessTaskLogId,
                        FileName = InputFileName,
                        ImageType = file.ContentType,
                        CreatedAtUtc = DateTime.Now,
                        ReceiveBusinessTaskLogImageUrl = contentPath + InputFileName // ServerSavePath
                    };
                    _context.ReceiveBusinessTaskLogImages.Add(_ReceiveBusinessTaskLogImage);
                    await _context.SaveChangesAsync();
                }
            }


            return RedirectToAction("Index");

        }


    }
}
