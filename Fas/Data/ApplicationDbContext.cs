using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using ChatApp.Domain.Entity;
using FasDemo.Models;
using FasDemo.ProjectModel;
using FasDemo.ProjectModel.DTO;
using FasDemo.ProjectVM;
using FasDemo.SurveyModel;
using FasDemo.ViewModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProjectManagment.Models;

namespace FasDemo.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        //custom entity, override identity user with new column
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        //custom entity, for simple todo app
        public DbSet<Todo> Todo { get; set; }
        //custom entity, for log
        public DbSet<Log> Log { get; set; }


        #region HRM Chat

        public DbSet<OnlineUser> OnlineUsers { get; set; }
        public DbSet<FriendMapping> FriendMappings { get; set; }
        public DbSet<UserNotification> UserNotifications { get; set; }
        public DbSet<ChatMessage> ChatMessages { get; set; }
        public DbSet<UserImage> UserImages { get; set; }
        #endregion
        #region HRM

        public DbSet<Designation> Designation { get; set; }
        public DbSet<Department> Department { get; set; }
        public DbSet<Ticket> Ticket { get; set; }
        public DbSet<TicketType> TicketType { get; set; }
        public DbSet<TodoType> TodoType { get; set; }
        public DbSet<Employee> Employee { get; set; }

        #endregion

        #region Projects
        public DbSet<Attachment> Attachments { get; set; }
        public DbSet<Contractor> Contractors { get; set; }
        //public DbSet<BillType> BillTypes { get; set; }
        //public DbSet<Bill> Bills { get; set; }
        public DbSet<ContractorImage> ContractorImages { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectProgram> ProjectPrograms { get; set; }
        public DbSet<SupervisionConsultant> SupervisionConsultants { get; set; }
        public DbSet<ProjectManagementConsultant> ProjectManagementConsultants { get; set; }

        public DbSet<ProjectComment> ProjectComment { get; set; }
        public DbSet<CommentImage> CommentImage { get; set; }

        // ReceiveBusiness

        public DbSet<ReceiveBusiness> ReceiveBusiness { get; set; }
        public DbSet<ReceiveBusinessSchedual> ReceiveBusinessScheduals { get; set; }
        public DbSet<ReceiveBusinessSchedualTemplet> ReceiveBusinessSchedualTemplets { get; set; }
        public DbSet<ReceiveBusinessTask> ReceiveBusinessTasks { get; set; }
        public DbSet<ReceiveBusinessTaskLog> ReceiveBusinessTaskLog { get; set; }
        public DbSet<ReceiveBusinessTaskLogImage> ReceiveBusinessTaskLogImages { get; set; }

        public DbSet<ReceiveBusinessComment> ReceiveBusinessComment { get; set; }
        public DbSet<ReceiveBusinessCommentImage> ReceiveBusinessCommentImages { get; set; }



        #endregion

        #region Surey
        public DbSet<Survey> Surveys { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Choice> Choices { get; set; }
        public DbSet<Response> Responses { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            modelBuilder.Query<ProjectTaskVM>();
            modelBuilder.Query<TaskstatusVM>(); 
            modelBuilder.Query<DashBoerdV2DTO>();
            modelBuilder.Query<ProjectRatioVM>();
            modelBuilder.Query<WeeklyRep>();


            //To Solve this problem : No type was specified for the decimal column

            foreach (var property in modelBuilder.Model.GetEntityTypes()
                         .SelectMany(t => t.GetProperties())
                         .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
            {
                property.Relational().ColumnType = "decimal(18,2)";
            }
      

        }


    }
}
