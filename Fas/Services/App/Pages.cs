using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FasDemo.Services.App
{
    //static class for app pages common information
    public static partial class Pages
    {
        public static class Todo
        {
            public const string ControllerName = "Todo";
            public const string RoleName = "Todo";
            public const string UrlDefault = "/Todo/Index";
            public const string NavigationName = "مهام خاصة";
        }

        public static class Membership
        {
            public const string ControllerName = "Membership";
            public const string RoleName = "Membership";
            public const string UrlDefault = "/Membership/Index";
            public const string NavigationName = "الصلاحيات";
        }

        public static class Role
        {
            public const string ControllerName = "Role";
            public const string RoleName = "Role";
            public const string UrlDefault = "/Role/Index";
            public const string NavigationName = "الشاشات";
        }

        //Nice HRM Lite

        public static class SelfService
        {
            public const string ControllerName = "SelfService";
            public const string RoleName = "SelfService";
            public const string UrlDefault = "/SelfService/Index";
            public const string NavigationName = "الخدمات الذاتية";
        }

        public static class Employee
        {
            public const string ControllerName = "Employee";
            public const string RoleName = "Employee";
            public const string UrlDefault = "/Employee/Index";
            public const string NavigationName = "الموظفون";
        }
        public static class Ticket
        {
            public const string ControllerName = "Ticket";
            public const string RoleName = "Ticket";
            public const string UrlDefault = "/Ticket/Index";
            public const string NavigationName = "تذكرة/ شكوى";
        }

        public static class Settings
        {
            public const string ControllerName = "Settings";
            public const string RoleName = "Settings";
            public const string UrlDefault = "/Settings/Index";
            public const string NavigationName = "الاعدادات";
        }
        public static class Home
        {
            public const string ControllerName = "Home";
            public const string RoleName = "Home";
            public const string UrlDefault = "/Home/Index";
            public const string NavigationName = "المحادثات";
        }
        public static class Project
        {
            public const string ControllerName = "Project";
            public const string RoleName = "Project";
            public const string UrlDefault = "/Project/Index";
            public const string NavigationName = "المشاريع";
        }

        public static class Agenda
        {
            public const string ControllerName = "Agenda";
            public const string RoleName = "Agenda";
            public const string UrlDefault = "/Agenda/Index";
            public const string NavigationName = "أجندة المهام";
        }
        public static class DashBoard
        {
            public const string ControllerName = "DashBoard";
            public const string RoleName = "DashBoard";
            public const string UrlDefault = "/DashBoard/Index";
            public const string NavigationName = "لوحة التحكم";
        }
        public static class DashBoardV2
        {
            public const string ControllerName = "DashBoardV2";
            public const string RoleName = "DashBoardV2";
            public const string UrlDefault = "/DashBoardV2/Index";
            public const string NavigationName = "لوحة التحكم ملخص";
        }
        public static class DashBoardV3
        {
            public const string ControllerName = "DashBoardV3";
            public const string RoleName = "DashBoardV3";
            public const string UrlDefault = "/DashBoardV3/Index";
            public const string NavigationName = "لوحة حالة الانجاز";
        }
        public static class DashBoardV4
        {
            public const string ControllerName = "DashBoardV4";
            public const string RoleName = "DashBoardV4";
            public const string UrlDefault = "/DashBoardV4/Index";
            public const string NavigationName = "حالة المهام";
        }

        public static class Contractors
        {
            public const string ControllerName = "Contractors";
            public const string RoleName = "Contractors";
            public const string UrlDefault = "/Contractors/Index";
            public const string NavigationName = "المقاولون";
        }

        public static class ReceiveBusiness
        {
            public const string ControllerName = "ReceiveBusiness";
            public const string RoleName = "ReceiveBusiness";
            public const string UrlDefault = "/ReceiveBusiness/Index";
            public const string NavigationName = "طلب استلام أعمال";
        }

        public static class ReceiveBusinessSchedualTemplet
        {
            public const string ControllerName = "ReceiveBusinessSchedualTemplet";
            public const string RoleName = "ReceiveBusinessSchedualTemplet";
            public const string UrlDefault = "/ReceiveBusinessSchedualTemplet/Index";
            public const string NavigationName = "نماذج استلام الأعمال";
        }


        public static class ReceiveBusinessTask
        {
            public const string ControllerName = "ReceiveBusinessTask";
            public const string RoleName = "ReceiveBusinessTask";
            public const string UrlDefault = "/ReceiveBusinessTask/Index";
            public const string NavigationName = "اعتمادات استلام الأعمال";
        }


    }
}
