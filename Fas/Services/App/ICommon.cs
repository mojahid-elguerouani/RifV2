using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FasDemo.ViewModels;
using ProjectManagment.Models;
using ChatApp.Domain.Entity;
using ChatApp.Domain.Entity.DTO;
using FasDemo.ProjectModel;

namespace FasDemo.Services.App
{
    public interface ICommon
    {
        string userImage(string userId);
        string userFirstName(string userId);

        IEnumerable<SelectListItem> GetEmployeeSelectList();

        IEnumerable<SelectListItem> GetDesignationSelectList();

        IEnumerable<SelectListItem> GetDepartmentSelectList();

        IEnumerable<SelectListItem> GetSystemUserSelectList();
        IEnumerable<SelectListItem> GetEmployeeUserSelectList();
        IEnumerable<SelectListItem> GetContractorSelectList(); 

        IEnumerable<SelectListItem> GetAllUserSelectList();
        IEnumerable<SelectListItem> GetGenderSelectList();

        IEnumerable<SelectListItem> GetMaritalStatusSelectList();
        IEnumerable<SelectListItem> GetProjectStatusTypeSelectList();
        IEnumerable<SelectListItem> GetProjectSelectList();

        IEnumerable<SelectListItem> GetTicketTypeSelectList();

        IEnumerable<SelectListItem> GetTicketSelectList();

        IEnumerable<SelectListItem> GetTicketSelectListByEmployeeId(string employeeId);

        IEnumerable<SelectListItem> GetTodoTypeSelectList();

        // Start ReceiveBusiness Select Lists

        IEnumerable<SelectListItem> GetReceiveBusinessStatusTypeSelectList();
        IEnumerable<SelectListItem> GetReceiveBusinessSchedualTempletSelectList();
        IEnumerable<SelectListItem> GetReceiveBusinessSchedualParentSelectList(string _ReceiveBusinessSchedualTempletId);
        IEnumerable<SelectListItem> GetReceiveBusinessSpecializationSelectList();
        IEnumerable<SelectListItem> GetProgramSelectList();

        //IEnumerable<SelectListItem> GetSupervisionConsultantSelectList();
        //IEnumerable<SelectListItem> GetProjectManagementConsultantSelectList();



        // End ReceiveBusiness Select Lists

        List<UserNotificationDTO> GetUserNotificationList(string _userId);
        List<ProjectTaskVM> GetEmployeeTaskList(string _userId);
        TodoSummary GetTodoSummaryByPeriod(string period);

        EmployeeSummary GetEmployeeSummary();

        TicketSummary GetTicketSummaryByPeriod(string period);


        ChartDoughnut GetTicketDoughnutByPeriodByEmployeeId(string period, string employeeId);

        ChartDoughnut GetTodoDoughnutByPeriod(string period);


        ChartDoughnut GetTicketDoughnutByPeriod(string period);

        ChartDoughnut GetEmployeeDoughnut();
        ChartDoughnut GetProjectsDoughnut();
        //ChartDoughnut GetProjectDoughnut(int projectId);

    }

}
