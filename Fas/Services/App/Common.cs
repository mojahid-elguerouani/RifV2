using FasDemo.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FasDemo.ViewModels;
using FasDemo.Models;
using ChatApp.Domain.Entity;
using ChatApp.Domain.Entity.DTO;
using System.Data.SqlClient;
using FasDemo.ProjectModel;

namespace FasDemo.Services.App
{
    public class Common : ICommon
    {
        private readonly ApplicationDbContext _context;

        public Common(
            ApplicationDbContext context
            )
        {
            _context = context;
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
            string FirstName = "مقاول";
            var existing = _context.Employee.Where(a => a.SystemUserId == userId).FirstOrDefault();
            FirstName = existing != null ?
                                    _context.Employee.Where(a => a.SystemUserId == userId).FirstOrDefault()?.FirstName + " " + _context.Employee.Where(a => a.SystemUserId == userId).FirstOrDefault()?.LastName :
                                    _context.Contractors.Where(a => a.ContractorUserId == userId).FirstOrDefault()?.ContractorName;
            return FirstName;
        }

        public IEnumerable<SelectListItem> GetEmployeeSelectList()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list = _context.Employee.AsNoTracking()
                .OrderBy(x => x.EmployeeId)//.Where(x => x.EmployeeIDNumber == null)
                .Select(x => new SelectListItem
                {
                    Value = x.EmployeeId,
                    Text = x.FirstName + " " + x.LastName
                    //Value = x.EmployeeId,
                    //Text = x.EmployeeIDNumber + " " + x.FirstName + " " + x.LastName
                }).ToList();
            SelectListItem blankOption = new SelectListItem()
            {
                Value = "",
                Text = ""
            };
            list.Insert(0, blankOption);
            return new SelectList(list, "Value", "Text");
        }
        public IEnumerable<SelectListItem> GetDesignationSelectList()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list = _context.Designation.AsNoTracking()
                .OrderBy(x => x.CreatedAtUtc)
                .Select(x => new SelectListItem
                {
                    Value = x.DesignationId,
                    Text = x.Name
                }).ToList();
            SelectListItem blankOption = new SelectListItem()
            {
                Value = "",
                Text = ""
            };
            list.Insert(0, blankOption);
            return new SelectList(list, "Value", "Text");
        }
        public IEnumerable<SelectListItem> GetDepartmentSelectList()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list = _context.Department.AsNoTracking()
                .OrderBy(x => x.CreatedAtUtc)
                .Select(x => new SelectListItem
                {
                    Value = x.DepartmentId,
                    Text = x.Name
                }).ToList();
            SelectListItem blankOption = new SelectListItem()
            {
                Value = "",
                Text = ""
            };
            list.Insert(0, blankOption);
            return new SelectList(list, "Value", "Text");
        }
        public IEnumerable<SelectListItem> GetSystemUserSelectList()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list = _context.ApplicationUser.AsNoTracking()
                .OrderBy(x => x.Email)
                .Select(x => new SelectListItem
                {
                    Value = x.Id,
                    Text = x.Email
                }).ToList();
            SelectListItem blankOption = new SelectListItem()
            {
                Value = "",
                Text = ""
            };
            list.Insert(0, blankOption);
            return new SelectList(list, "Value", "Text");
        }
        public IEnumerable<SelectListItem> GetEmployeeUserSelectList()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list = _context.Employee.AsNoTracking()
                .OrderBy(x => x.FirstName)
                .Select(x => new SelectListItem
                {
                    Value = x.SystemUserId,
                    Text = x.FirstName + x.LastName
                }).ToList();
            SelectListItem blankOption = new SelectListItem()
            {
                Value = "",
                Text = ""
            };
            list.Insert(0, blankOption);
            return new SelectList(list, "Value", "Text");
        }
        public IEnumerable<SelectListItem> GetContractorSelectList()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list = _context.Contractors.AsNoTracking()
                .OrderByDescending(x => x.UpdatedAtUtc)
                .Select(x => new SelectListItem
                {
                    Value = x.ContractorId,
                    Text = x.ContractorName
                }).ToList();
            SelectListItem blankOption = new SelectListItem()
            {
                Value = "",
                Text = ""
            };
            list.Insert(0, blankOption);
            return new SelectList(list, "Value", "Text");
        }

        

        public IEnumerable<SelectListItem> GetProjectCodeSelectList()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list = _context.Projects.AsNoTracking()
                .OrderBy(x => x.CreatedAtUtc)
                .Select(x => new SelectListItem
                {
                    Value = x.ProjectId.ToString(),
                    Text = x.ProjectCode
                }).ToList();
            SelectListItem blankOption = new SelectListItem()
            {
                Value = "",
                Text = ""
            };
            list.Insert(0, blankOption);
            return new SelectList(list, "Value", "Text");
        }

        public IEnumerable<SelectListItem> GetSectorSelectList()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            List<Listitems> all = new List<Listitems>();

            all = new List<Listitems>() {
                new Listitems(){ Id = "1", Name="البن"},
                new Listitems(){ Id = "2", Name="العسل"},
                new Listitems(){ Id = "3", Name="الفواكه"},
                new Listitems(){ Id = "4", Name="الورود"}
            };

            list = all
                .OrderBy(x => x.Id)
                .Select(x => new SelectListItem
                {
                    Value = x.Id,
                    Text = x.Name
                }).ToList();
            return new SelectList(list, "Value", "Text");
        }
        
        public IEnumerable<SelectListItem> GetRegionSelectList()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            List<Listitems> all = new List<Listitems>();

            all = new List<Listitems>() {
                new Listitems(){ Id = "1",  Name="الرياض"},
                new Listitems(){ Id = "2",  Name="مكة المكرمة"},
                new Listitems(){ Id = "3",  Name="المدينة المنورة"},
                new Listitems(){ Id = "4",  Name="المنطقة الشرقية"},
                new Listitems(){ Id = "5",  Name="القصيم"},
                new Listitems(){ Id = "6",  Name="عسير"},
                new Listitems(){ Id = "7",  Name="حائل"},
                new Listitems(){ Id = "8",  Name="تبوك"},
                new Listitems(){ Id = "9",  Name="الباحة"},
                new Listitems(){ Id = "10", Name="الحدود الشمالية"},
                new Listitems(){ Id = "11", Name="الجوف"},
                new Listitems(){ Id = "12", Name="جازان"},
                new Listitems(){ Id = "13", Name="نجران"}
            };

            list = all
                .OrderBy(x => x.Id)
                .Select(x => new SelectListItem
                {
                    Value = x.Id,
                    Text = x.Name
                }).ToList();
            return new SelectList(list, "Value", "Text");
        }

        public IEnumerable<SelectListItem> GetAllUserSelectList()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list = _context.Employee.AsNoTracking()
                .OrderBy(x => x.FirstName)
                .Select(x => new SelectListItem
                {
                    Value = x.SystemUserId,
                    Text = x.FirstName + x.LastName
                }).ToList();

            List<SelectListItem> list1 = new List<SelectListItem>();
            list1 = _context.Contractors.AsNoTracking()
                .OrderByDescending(x => x.UpdatedAtUtc)
                .Select(x => new SelectListItem
                {
                    Value = x.ContractorUserId,
                    Text = x.ContractorName
                }).ToList();
            SelectListItem blankOption = new SelectListItem()
            {
                Value = "",
                Text = ""
            };

            list.AddRange(list1);
            list.Insert(0, blankOption);
            return new SelectList(list, "Value", "Text");
        }
        public IEnumerable<SelectListItem> GetGenderSelectList()
        {
            return new SelectList(GenderList.GetAll());
        }
        public IEnumerable<SelectListItem> GetMaritalStatusSelectList()
        {
            return new SelectList(MaritalStatusList.GetAll());
        }
        public IEnumerable<SelectListItem> GetProjectStatusTypeSelectList()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            List<Listitems> all = new List<Listitems>();

            all = new List<Listitems>() {
                new Listitems(){ Id = "1", Name="نشط"},
                new Listitems(){ Id = "2", Name="منتهي"},
                new Listitems(){ Id = "3", Name="متوقف"},
                new Listitems(){ Id = "4", Name="ملغي"}
            };

            list = all
                .OrderBy(x => x.Id)
                .Select(x => new SelectListItem
                {
                    Value = x.Id,
                    Text = x.Name
                }).ToList();
            return new SelectList(list, "Value", "Text");
        }

    

        //public IEnumerable<SelectListItem> GetSupervisionConsultantSelectList()
        //{
        //    List<SelectListItem> list = new List<SelectListItem>();
        //    list = _context.SupervisionConsultants.AsNoTracking()
        //        .OrderBy(x => x.CreatedAtUtc)
        //        .Select(x => new SelectListItem
        //        {
        //            Value = x.SupervisionConsultantId,
        //            Text = x.SupervisionConsultantName
        //        }).ToList();
        //    SelectListItem blankOption = new SelectListItem()
        //    {
        //        Value = "",
        //        Text = ""
        //    };
        //    list.Insert(0, blankOption);
        //    return new SelectList(list, "Value", "Text");
        //}

        //public IEnumerable<SelectListItem> GetProjectManagementConsultantSelectList()
        //{
        //    List<SelectListItem> list = new List<SelectListItem>();
        //    list = _context.ProjectManagementConsultants.AsNoTracking()
        //        .OrderBy(x => x.CreatedAtUtc)
        //        .Select(x => new SelectListItem
        //        {
        //            Value = x.ProjectManagementConsultantId,
        //            Text = x.ProjectManagementConsultantName
        //        }).ToList();
        //    SelectListItem blankOption = new SelectListItem()
        //    {
        //        Value = "",
        //        Text = ""
        //    };
        //    list.Insert(0, blankOption);
        //    return new SelectList(list, "Value", "Text");
        //}

        public IEnumerable<SelectListItem> GetTicketTypeSelectList()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list = _context.TicketType.AsNoTracking()
                .OrderBy(x => x.CreatedAtUtc)
                .Select(x => new SelectListItem
                {
                    Value = x.TicketTypeId,
                    Text = x.Name
                }).ToList();
            SelectListItem blankOption = new SelectListItem()
            {
                Value = "",
                Text = ""
            };
            list.Insert(0, blankOption);
            return new SelectList(list, "Value", "Text");
        }
        public IEnumerable<SelectListItem> GetTicketSelectList()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list = _context.Ticket.AsNoTracking()
                .OrderBy(x => x.CreatedAtUtc)
                .Select(x => new SelectListItem
                {
                    Value = x.TicketId,
                    Text = x.TicketName
                }).ToList();
            SelectListItem blankOption = new SelectListItem()
            {
                Value = "",
                Text = ""
            };
            list.Insert(0, blankOption);
            return new SelectList(list, "Value", "Text");
        }
        public IEnumerable<SelectListItem> GetTicketSelectListByEmployeeId(string employeeId)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list = _context.Ticket.Where(x => x.OnBehalfId.Equals(employeeId)).AsNoTracking()
                .OrderBy(x => x.CreatedAtUtc)
                .Select(x => new SelectListItem
                {
                    Value = x.TicketId,
                    Text = x.TicketName
                }).ToList();
            SelectListItem blankOption = new SelectListItem()
            {
                Value = "",
                Text = ""
            };
            list.Insert(0, blankOption);
            return new SelectList(list, "Value", "Text");
        }
        public IEnumerable<SelectListItem> GetTodoTypeSelectList()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list = _context.TodoType.AsNoTracking()
                .OrderBy(x => x.CreatedAtUtc)
                .Select(x => new SelectListItem
                {
                    Value = x.TodoTypeId,
                    Text = x.Name
                }).ToList();
            SelectListItem blankOption = new SelectListItem()
            {
                Value = "",
                Text = ""
            };
            list.Insert(0, blankOption);
            return new SelectList(list, "Value", "Text");
        }

        // ReceiveBusiness Select Lists

        public IEnumerable<SelectListItem> GetReceiveBusinessStatusTypeSelectList()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            List<Listitems> all = new List<Listitems>();

            all = new List<Listitems>() {
                new Listitems(){ Id = "1", Name="نشط"},
                new Listitems(){ Id = "2", Name="مقبول"},
                new Listitems(){ Id = "3", Name="مقبول مع وجود ملاحظات"},
                new Listitems(){ Id = "4", Name="يعاد تقديمه"},
                new Listitems(){ Id = "5", Name="مرفوض"}
            };

            list = all
                .OrderBy(x => x.Id)
                .Select(x => new SelectListItem
                {
                    Value = x.Id,
                    Text = x.Name
                }).ToList();
            return new SelectList(list, "Value", "Text");
        }

        public IEnumerable<SelectListItem> GetReceiveBusinessSchedualTempletSelectList()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list = _context.ReceiveBusinessSchedualTemplets.AsNoTracking()
                .OrderBy(x => x.CreatedAtUtc)
                .Select(x => new SelectListItem
                {
                    Value = x.ReceiveBusinessSchedualTempletId,
                    Text = x.ReceiveBusinessSchedualTempletName
                }).ToList();
            SelectListItem blankOption = new SelectListItem()
            {
                Value = "",
                Text = ""
            };
            list.Insert(0, blankOption);
            return new SelectList(list, "Value", "Text");
        }

        public IEnumerable<SelectListItem> GetReceiveBusinessSpecializationSelectList()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            List<Listitems> all = new List<Listitems>();

            all = new List<Listitems>() {
                new Listitems(){ Id = "1", Name="مدني"},
                new Listitems(){ Id = "2", Name="معماري"},
                new Listitems(){ Id = "3", Name="ميكانيكا"},
                new Listitems(){ Id = "4", Name="كهرباء"},
                new Listitems(){ Id = "5", Name="زراعي"},
                new Listitems(){ Id = "6", Name="أخرى"}
            };

            list = all
                .OrderBy(x => x.Id)
                .Select(x => new SelectListItem
                {
                    Value = x.Id,
                    Text = x.Name
                }).ToList();
            return new SelectList(list, "Value", "Text");
        }

        

        public IEnumerable<SelectListItem> GetReceiveBusinessSchedualParentSelectList(string _ReceiveBusinessSchedualTempletId)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list = _context.ReceiveBusinessScheduals.Where(x => x.ReceiveBusinessSchedualTempletId == _ReceiveBusinessSchedualTempletId)
                .OrderBy(x => x.CreatedAtUtc)
                .Select(x => new SelectListItem
                {
                    Value = x.ReceiveBusinessSchedualId.ToString(),
                    Text = x.TaskName
                }).ToList();
            SelectListItem blankOption = new SelectListItem()
            {
                Value = "",
                Text = ""
            };
            list.Insert(0, blankOption);
            return new SelectList(list, "Value", "Text");
        }

        public IEnumerable<SelectListItem> GetProjectSelectList()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list = _context.Projects.AsNoTracking()
                .OrderBy(x => x.CreatedAtUtc)
                .Select(x => new SelectListItem
                {
                    Value = x.ProjectId.ToString(),
                    Text = x.ProjectName
                }).ToList();
            SelectListItem blankOption = new SelectListItem()
            {
                Value = "",
                Text = ""
            };
            list.Insert(0, blankOption);
            return new SelectList(list, "Value", "Text");
        }


        public List<UserNotificationDTO> GetUserNotificationList(string _userId)
        {
            var list1 = _context.UserNotifications.AsNoTracking().Where(x => x.ToUserID == _userId)
                  .OrderByDescending(x => x.CreatedOn).ToList();


            List<UserNotificationDTO> userNotificationDTOs = new List<UserNotificationDTO>();


            foreach (UserNotification userNotification in list1)
            {
                UserNotificationDTO userNotificationDTO = new UserNotificationDTO
                {
                    FromUser = userFirstName(userNotification.FromUserID),
                    FromUserImage = userImage(userNotification.FromUserID),
                    NotificationType = userNotification.NotificationType,
                    CreatedOn = userNotification.CreatedOn,
                };
                userNotificationDTOs.Add(userNotificationDTO);
            }


            return userNotificationDTOs;
        }
        public List<ProjectTaskVM> GetEmployeeTaskList(string _userId)
        {
            List<ProjectTaskVM> EmployeeTaskList = new List<ProjectTaskVM>();
            SqlParameter[] parameters1 = {
                    new SqlParameter("@cmdType", "GetUserTasks"),
                    new SqlParameter("@AssignTo", _userId)
                };
            EmployeeTaskList = _context.Query<ProjectTaskVM>().FromSql("sp_ProjectTasks @cmdType, @AssignTo", parameters1).ToList();
            return EmployeeTaskList;
        }

        public TodoSummary GetTodoSummaryByPeriod(string period)
        {
            try
            {
                TodoSummary result = new TodoSummary();

                decimal done = _context.Todo
                    .Where(x => x.StartDate.ToString("yyyy-MM").Equals(period)
                        && x.IsDone.Equals(true))
                    .Count();

                decimal notDone = _context.Todo
                    .Where(x => x.StartDate.ToString("yyyy-MM").Equals(period)
                        && x.IsDone.Equals(false))
                    .Count();

                decimal total = done + notDone;

                result.Done = done.ToString();
                result.DonePercentage = total != 0m ? ((done / total) * 100m).ToString("##") : "0";
                result.NotDone = notDone.ToString();
                result.NotDonePercentage = total != 0m ? ((notDone / total) * 100m).ToString("##") : "0";

                decimal oneDay = _context.Todo
                    .Where(x => x.StartDate.ToString("yyyy-MM").Equals(period)
                        && (x.EndDate.Date - x.StartDate.Date).Days + 1 == 1)
                    .Count();

                decimal moreThanOne = _context.Todo
                    .Where(x => x.StartDate.ToString("yyyy-MM").Equals(period)
                        && (x.EndDate.Date - x.StartDate.Date).Days + 1 > 1)
                    .Count();

                decimal total2 = oneDay + moreThanOne;

                result.OneDay = oneDay.ToString();
                result.OneDayPercentage = total2 != 0m ? ((oneDay / total2) * 100m).ToString("##") : "0";
                result.MoreThanOne = moreThanOne.ToString();
                result.MoreThanOnePercentage = total2 != 0m ? ((moreThanOne / total2) * 100m).ToString("##") : "0";

                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public EmployeeSummary GetEmployeeSummary()
        {
            try
            {
                EmployeeSummary result = new EmployeeSummary();

                List<Employee> employees = new List<Employee>();
                employees = _context.Employee.ToList();

                int male = 0;
                int female = 0;
                int mosLess = 0;
                int mosMore = 0;
                int totalMaleFemale = 0;
                int totalMosLessMosMore = 0;

                male = employees.Where(x => x.Gender.Equals("Male")).Count();
                female = employees.Where(x => x.Gender.Equals("Female")).Count();
                mosLess = employees.Where(x => DateTime.Now.Date.Subtract(x.JoiningDate.Date).TotalDays / (365.25 / 12) < 6).Count();
                mosMore = employees.Where(x => DateTime.Now.Date.Subtract(x.JoiningDate.Date).TotalDays / (365.25 / 12) >= 6).Count();

                totalMaleFemale = male + female;
                totalMosLessMosMore = mosLess + mosMore;

                result.Male = male.ToString();
                result.Female = female.ToString();
                result.MalePercentage = totalMaleFemale == 0 ? "0" : (male * 100.0 / totalMaleFemale * 1.0).ToString("##");
                result.FemalePercentage = totalMaleFemale == 0 ? "0" : (female * 100.0 / totalMaleFemale * 1.0).ToString("##");
                result.MosLess = mosLess.ToString();
                result.MosMore = mosMore.ToString();
                result.MosLessPercentage = totalMosLessMosMore == 0 ? "0" : (mosLess * 100.0 / totalMosLessMosMore * 1.0).ToString("##");
                result.MosMorePercentage = totalMosLessMosMore == 0 ? "0" : (mosMore * 100.0 / totalMosLessMosMore * 1.0).ToString("##");

                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public TicketSummary GetTicketSummaryByPeriod(string period)
        {
            try
            {
                TicketSummary result = new TicketSummary();

                List<Ticket> tickets = new List<Ticket>();
                tickets = _context.Ticket
                    .Where(x => x.SubmitDate.ToString("yyyy-MM").Equals(period))
                    .ToList();

                int solve = 0;
                int notSolve = 0;
                int recurring = 0;
                int notRecurring = 0;
                int totalSolveNotSolve = 0;
                int totalRecurringNotRecurring = 0;

                solve = tickets.Where(x => x.IsSolve.Equals(true)).Count();
                notSolve = tickets.Where(x => x.IsSolve.Equals(false)).Count();
                recurring = tickets.Where(x => !String.IsNullOrEmpty(x.ParentTicketThreadId)).Count();
                notRecurring = tickets.Where(x => String.IsNullOrEmpty(x.ParentTicketThreadId)).Count();

                totalSolveNotSolve = solve + notSolve;
                totalRecurringNotRecurring = recurring + notRecurring;

                result.Solve = solve.ToString();
                result.NotSolve = notSolve.ToString();
                result.SolvePercentage = totalSolveNotSolve == 0 ? "0" : (solve * 100.0 / totalSolveNotSolve * 1.0).ToString("##");
                result.NotSolvePercentage = totalSolveNotSolve == 0 ? "0" : (notSolve * 100.0 / totalSolveNotSolve * 1.0).ToString("##");
                result.Recurring = recurring.ToString();
                result.NotRecurring = notRecurring.ToString();
                result.RecurringPercentage = totalRecurringNotRecurring == 0 ? "0" : (recurring * 100.0 / totalRecurringNotRecurring * 1.0).ToString("##");
                result.NotRecurringPercentage = totalRecurringNotRecurring == 0 ? "0" : (notRecurring * 100.0 / totalRecurringNotRecurring * 1.0).ToString("##");

                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ChartDoughnut GetTodoDoughnutByPeriod(string period)
        {
            try
            {
                ChartDoughnut result = new ChartDoughnut();
                List<string> labels = new List<string>();
                List<string> colors = new List<string>();
                List<int> datas = new List<int>();
                int index = 0;
                foreach (var item in _context.TodoType.ToList())
                {
                    labels.Add(item.Name);

                    colors.Add(ColorList.GetAllRGBA()[index]);
                    index++;

                    datas.Add(_context.Todo
                        .Where(x => x.StartDate.ToString("yyyy-MM").Equals(period)
                            && x.TodoTypeId.Equals(item.TodoTypeId))
                        .Count());
                }

                result.Labels = labels.ToArray();
                result.Colors = colors.ToArray();
                result.Data = datas.ToArray();

                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ChartDoughnut GetTicketDoughnutByPeriodByEmployeeId(string period, string employeeId)
        {
            try
            {
                ChartDoughnut result = new ChartDoughnut();
                List<string> labels = new List<string>();
                List<string> colors = new List<string>();
                List<int> datas = new List<int>();
                int index = 0;
                foreach (var item in _context.TicketType.ToList())
                {
                    labels.Add(item.Name);

                    colors.Add(ColorList.GetAllRGBA()[index]);
                    index++;

                    datas.Add(_context.Ticket
                        .Where(x => x.SubmitDate.ToString("yyyy-MM").Equals(period)
                            && x.TicketTypeId.Equals(item.TicketTypeId)
                            && x.OnBehalfId.Equals(employeeId))
                        .Count());
                }

                result.Labels = labels.ToArray();
                result.Colors = colors.ToArray();
                result.Data = datas.ToArray();

                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ChartDoughnut GetTicketDoughnutByPeriod(string period)
        {
            try
            {
                ChartDoughnut result = new ChartDoughnut();
                List<string> labels = new List<string>();
                List<string> colors = new List<string>();
                List<int> datas = new List<int>();
                int index = 0;
                foreach (var item in _context.TicketType.ToList())
                {
                    labels.Add(item.Name);

                    colors.Add(ColorList.GetAllRGBA()[index]);
                    index++;

                    datas.Add(_context.Ticket
                        .Where(x => x.SubmitDate.ToString("yyyy-MM").Equals(period)
                            && x.TicketTypeId.Equals(item.TicketTypeId))
                        .Count());
                }

                result.Labels = labels.ToArray();
                result.Colors = colors.ToArray();
                result.Data = datas.ToArray();

                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public ChartDoughnut GetEmployeeDoughnut()
        {
            try
            {
                ChartDoughnut result = new ChartDoughnut();
                List<string> labels = new List<string>();
                List<string> colors = new List<string>();
                List<int> datas = new List<int>();
                int index = 0;
                foreach (var item in _context.Department.ToList())
                {
                    labels.Add(item.Name);

                    colors.Add(ColorList.GetAllRGBA()[index]);
                    index++;

                    datas.Add(_context.Employee
                        .Where(x => x.DepartmentId.Equals(item.DepartmentId))
                        .Count());
                }

                result.Labels = labels.ToArray();
                result.Colors = colors.ToArray();
                result.Data = datas.ToArray();

                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public ChartDoughnut GetProjectsDoughnut()
        {
            try
            {
                ChartDoughnut result = new ChartDoughnut();
                List<string> labels = new List<string>();
                List<string> colors = new List<string>();
                List<int> datas = new List<int>();
                int index = 0;
                foreach (var item in GetProjectStatusTypeSelectList())
                {
                    labels.Add(item.Text);

                    colors.Add(ColorList.GetAllRGBA()[index]);
                    index++;

                    datas.Add(_context.Projects
                        .Where(x => x.StatusId.ToString().Equals(item.Value))
                        .Count());
                }

                result.Labels = labels.ToArray();
                result.Colors = colors.ToArray();
                result.Data = datas.ToArray();

                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        //public ChartDoughnut GetProjectDoughnut(int projectId)
        //{
        //    try
        //    {
        //        ChartDoughnut result = new ChartDoughnut();
        //        List<string> labels = new List<string>();
        //        List<string> colors = new List<string>();
        //        List<int> datas = new List<int>();
        //        int index = 0;

        //        var project = _context.ProjectTasks
        //      .Where(ctx => ctx.ProjectId == projectId)
        //      .ToList();


        //        decimal _ProjectTasksSum = decimal.Parse(string.Format("{0:0.00}", project.Sum(d => d.Compleation * d.Duration) / 100.00));
        //        decimal _ProjectTasksSum100 = decimal.Parse(string.Format("{0:0.00}", project.Sum(d => 100.00 * d.Duration) / 100.00));
        //        decimal _ProjectTasksRatio = (_ProjectTasksSum / _ProjectTasksSum100) * 100;



        //        int convertedInt = decimal.ToInt32(_ProjectTasksSum);

        //        labels.Add("الانجاز");

        //        colors.Add(ColorList.GetAllRGBA()[index]);
        //        datas.Add(convertedInt);
        //        index++;
        //        labels.Add("المتبقي");

        //        colors.Add(ColorList.GetAllRGBA()[index]);
        //        datas.Add(100 - convertedInt);
        //        //index++;




        //        result.Labels = labels.ToArray();
        //        result.Colors = colors.ToArray();
        //        result.Data = datas.ToArray();

        //        return result;
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}




    }
}
