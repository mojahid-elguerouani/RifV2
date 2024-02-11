using FasDemo.Data;
using FasDemo.Models;
using FasDemo.Services.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FasDemo.Services.Database
{
    //special service provided for db initialization / data seed
    public class Common : ICommon
    {
        private readonly ApplicationDbContext _context;
        private readonly Security.ICommon _security;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SuperAdminDefaultOptions _superAdminDefaultOptions;
        private readonly App.ICommon _app;

        public Common(ApplicationDbContext context,
            Security.ICommon security,
            RoleManager<IdentityRole> roleManager,
            IOptions<SuperAdminDefaultOptions> superAdminDefaultOptions,
            App.ICommon app,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _security = security;
            _userManager = userManager;
            _roleManager = roleManager;
            _superAdminDefaultOptions = superAdminDefaultOptions.Value;
            _app = app;
        }

        public async Task Initialize()
        {
            try
            {
                _context.Database.EnsureCreated();

                //check for users
                if (_context.ApplicationUser.Any())
                {
                    return; //if user is not empty, DB has been seed
                }

                //init app with super admin user
                await _security.CreateDefaultSuperAdmin();

                //init app with demo data
                await InsertDemoData();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private async Task InsertDemoData()
        {
            try
            {
                //get super admin user
                ApplicationUser superAdmin = new ApplicationUser();
                superAdmin = await _userManager.FindByEmailAsync(_superAdminDefaultOptions.Email);

                //insert ticket type
                TicketType attendanceTicket = new TicketType()
                {
                    Name = "Attendance",
                    Description = ""
                };
                TicketType payrollTicket = new TicketType()
                {
                    Name = "كشف رواتب",
                    Description = ""
                };
                TicketType leaveTicket = new TicketType()
                {
                    Name = "Leave",
                    Description = ""
                };
                TicketType reimburseTicket = new TicketType()
                {
                    Name = "Reimburse",
                    Description = ""
                };
                TicketType miscellaneousTicket = new TicketType()
                {
                    Name = "Miscellaneous",
                    Description = ""
                };

                await _context.TicketType.AddAsync(attendanceTicket);
                await _context.TicketType.AddAsync(payrollTicket);
                await _context.TicketType.AddAsync(leaveTicket);
                await _context.TicketType.AddAsync(reimburseTicket);
                await _context.TicketType.AddAsync(miscellaneousTicket);


                await _context.SaveChangesAsync();

                //insert todo type
                TodoType payrollTodo = new TodoType()
                {
                    Name = "كشف رواتب",
                    Description = ""
                };
                TodoType onboardingTodo = new TodoType()
                {
                    Name = "Onboarding",
                    Description = ""
                };
                TodoType recruitmentTodo = new TodoType()
                {
                    Name = "تعين",
                    Description = ""
                };

                await _context.TodoType.AddAsync(payrollTodo);
                await _context.TodoType.AddAsync(onboardingTodo);
                await _context.TodoType.AddAsync(recruitmentTodo);


                await _context.SaveChangesAsync();

                //insert department
                Department itDepartment = new Department()
                {
                    Name = "IT Department",
                    Description = ""
                };
                Department hrDepartment = new Department()
                {
                    Name = "HR Department",
                    Description = ""
                };
                Department financeDepartment = new Department()
                {
                    Name = "Finance Department",
                    Description = ""
                };
                Department salesDepartment = new Department()
                {
                    Name = "Sales Department",
                    Description = ""
                };
                Department warehouseDepartment = new Department()
                {
                    Name = "Warehouse Department",
                    Description = ""
                };

                await _context.Department.AddAsync(itDepartment);
                await _context.Department.AddAsync(hrDepartment);
                await _context.Department.AddAsync(financeDepartment);
                await _context.Department.AddAsync(salesDepartment);
                await _context.Department.AddAsync(warehouseDepartment);


                await _context.SaveChangesAsync();

                //insert designation
                Designation designationVPIT = new Designation()
                {
                    Name = "VP IT / COO (ExComm)",
                    Description = ""
                };
                Designation designationHeadIT = new Designation()
                {
                    Name = "Head of IT",
                    Description = ""
                };
                Designation designationSeniorManagerIT = new Designation()
                {
                    Name = "IT Senior Manager",
                    Description = ""
                };
                Designation designationManagerIT = new Designation()
                {
                    Name = "IT Manager",
                    Description = ""
                };
                Designation designationStaffIT = new Designation()
                {
                    Name = "IT Staff",
                    Description = ""
                };
                Designation designationVPFinance = new Designation()
                {
                    Name = "VP Finance / CEO (ExComm)",
                    Description = ""
                };
                Designation designationHeadFinance = new Designation()
                {
                    Name = "Head of Finance",
                    Description = ""
                };
                Designation designationSeniorManagerFinance = new Designation()
                {
                    Name = "Finance Senior Manager",
                    Description = ""
                };
                Designation designationManagerFinance = new Designation()
                {
                    Name = "Finance Manager",
                    Description = ""
                };
                Designation designationStaffFinance = new Designation()
                {
                    Name = "Finance Staff",
                    Description = ""
                };
                Designation designationVPHR = new Designation()
                {
                    Name = "VP HR (ExComm)",
                    Description = ""
                };
                Designation designationHeadHR = new Designation()
                {
                    Name = "Head of HR",
                    Description = ""
                };
                Designation designationSeniorManagerHR = new Designation()
                {
                    Name = "HR Senior Manager",
                    Description = ""
                };
                Designation designationManagerHR = new Designation()
                {
                    Name = "HR Manager",
                    Description = ""
                };
                Designation designationStaffHR = new Designation()
                {
                    Name = "HR Staff",
                    Description = ""
                };
                Designation designationVPSales = new Designation()
                {
                    Name = "VP Sales / CMO (ExComm)",
                    Description = ""
                };
                Designation designationHeadSales = new Designation()
                {
                    Name = "Head of Sales",
                    Description = ""
                };
                Designation designationSeniorManagerSales = new Designation()
                {
                    Name = "Sales Senior Manager",
                    Description = ""
                };
                Designation designationManagerSales = new Designation()
                {
                    Name = "Sales Manager",
                    Description = ""
                };
                Designation designationStaffSales = new Designation()
                {
                    Name = "Sales Staff",
                    Description = ""
                };
                Designation designationVPWarehouse = new Designation()
                {
                    Name = "VP Warehouse (ExComm)",
                    Description = ""
                };
                Designation designationHeadWarehouse = new Designation()
                {
                    Name = "Head of Warehouse",
                    Description = ""
                };
                Designation designationSeniorManagerWarehouse = new Designation()
                {
                    Name = "Warehouse Senior Manager",
                    Description = ""
                };
                Designation designationManagerWarehouse = new Designation()
                {
                    Name = "Warehouse Manager",
                    Description = ""
                };
                Designation designationStaffWarehouse = new Designation()
                {
                    Name = "Warehouse Staff",
                    Description = ""
                };

                await _context.Designation.AddAsync(designationVPIT);
                await _context.Designation.AddAsync(designationHeadIT);
                await _context.Designation.AddAsync(designationSeniorManagerIT);
                await _context.Designation.AddAsync(designationManagerIT);
                await _context.Designation.AddAsync(designationStaffIT);

                await _context.Designation.AddAsync(designationVPHR);
                await _context.Designation.AddAsync(designationHeadHR);
                await _context.Designation.AddAsync(designationSeniorManagerHR);
                await _context.Designation.AddAsync(designationManagerHR);
                await _context.Designation.AddAsync(designationStaffHR);

                await _context.Designation.AddAsync(designationVPFinance);
                await _context.Designation.AddAsync(designationHeadFinance);
                await _context.Designation.AddAsync(designationSeniorManagerFinance);
                await _context.Designation.AddAsync(designationManagerFinance);
                await _context.Designation.AddAsync(designationStaffFinance);

                await _context.Designation.AddAsync(designationVPSales);
                await _context.Designation.AddAsync(designationHeadSales);
                await _context.Designation.AddAsync(designationSeniorManagerSales);
                await _context.Designation.AddAsync(designationManagerSales);
                await _context.Designation.AddAsync(designationStaffSales);

                await _context.Designation.AddAsync(designationVPWarehouse);
                await _context.Designation.AddAsync(designationHeadWarehouse);
                await _context.Designation.AddAsync(designationSeniorManagerWarehouse);
                await _context.Designation.AddAsync(designationManagerWarehouse);
                await _context.Designation.AddAsync(designationStaffWarehouse);


                await _context.SaveChangesAsync();

                //random data source: https://www.summet.com/dmsi/html/codesamples/addresses.html

                //insert employee (finance)
                Employee empCeciliaChapman = new Employee()
                {
                    FirstName = "super",
                    LastName = "Admin",
                    Gender = "Male",
                    DateOfBirth = new DateTime(1990, 1, 1),
                    PlaceOfBirth = "الرياض",
                    MaritalStatus = "أعزب",
                    Email = "super@admin.com",
                    Phone = "(966) 543-1234",
                    Address1 = "الرياض.",
                    City = "الرياض",
                    StateProvince = "الرياض",
                    ZipCode = "11111",
                    Country = "السعودية",
                    EmployeeIDNumber = "EMPFI001",
                    Designation = designationVPFinance,
                    Department = financeDepartment,
                    //Supervisor
                    JoiningDate = new DateTime(2017, 1, 1),
                    BasicSalary = 25000m,
                    UnpaidLeavePerDay = 1250m,
                    AccountTitle = "أبو طارق  كشف رواتب",
                    BankName = "Al rajhi",
                    AccountNumber = "1111-2222"
                };
                await _context.Employee.AddAsync(empCeciliaChapman);
                await _context.SaveChangesAsync();

                Employee empIrisWatson = new Employee()
                {
                    FirstName = "أبو طارق",
                    LastName = "",
                    Gender = "Male",
                    DateOfBirth = new DateTime(1990, 1, 1),
                    PlaceOfBirth = "الرياض",
                    MaritalStatus = "أعزب",
                    Email = "Tarik@fas.com",
                    Phone = "(966) 543-1234",
                    Address1 = "الرياض.",
                    City = "الرياض",
                    StateProvince = "الرياض",
                    ZipCode = "11111",
                    Country = "السعودية",
                    EmployeeIDNumber = "EMPFI002",
                    Designation = designationHeadFinance,
                    Department = financeDepartment,
                    Supervisor = empCeciliaChapman,
                    JoiningDate = new DateTime(2017, 1, 2),
                    BasicSalary = 20000m,
                    UnpaidLeavePerDay = 1000m,
                    AccountTitle = "أبو طارق كشف رواتب",
                    BankName = "Al rajhi",
                    AccountNumber = "1111-2222"
                };
                await _context.Employee.AddAsync(empIrisWatson);
                await _context.SaveChangesAsync();

                Employee empCelesteSlater = new Employee()
                {
                    FirstName = "محمد ",
                    LastName = "رضا",
                    Gender = "Male",
                    DateOfBirth = new DateTime(1990, 1, 1),
                    PlaceOfBirth = "الرياض",
                    MaritalStatus = "أعزب",
                    Email = "MReda@fas.com",
                    Phone = "(966) 543-1234",
                    Address1 = "الرياض.",
                    City = "الرياض",
                    StateProvince = "الرياض",
                    ZipCode = "11111",
                    Country = "السعودية",
                    EmployeeIDNumber = "EMPFI003",
                    Designation = designationSeniorManagerFinance,
                    Department = financeDepartment,
                    Supervisor = empIrisWatson,
                    JoiningDate = new DateTime(2017, 1, 3),
                    BasicSalary = 15000m,
                    UnpaidLeavePerDay = 750m,
                    AccountTitle = "رضا كشف رواتب",
                    BankName = "Al rajhi",
                    AccountNumber = "1111-2222"
                };
                await _context.Employee.AddAsync(empCelesteSlater);
                await _context.SaveChangesAsync();

                Employee empTheodoreLowe = new Employee()
                {
                    FirstName = "احمد ",
                    LastName = "بامطرف",
                    Gender = "Male",
                    DateOfBirth = new DateTime(1990, 1, 1),
                    PlaceOfBirth = "الرياض",
                    MaritalStatus = "أعزب",
                    Email = "Ahmed@fas.com",
                    Phone = "(966) 543-1234",
                    Address1 = "الرياض.",
                    City = "الرياض",
                    StateProvince = "الرياض",
                    ZipCode = "11111",
                    Country = "السعودية",
                    EmployeeIDNumber = "EMPFI004",
                    Designation = designationManagerFinance,
                    Department = financeDepartment,
                    Supervisor = empCelesteSlater,
                    JoiningDate = new DateTime(2017, 1, 4),
                    BasicSalary = 10000m,
                    UnpaidLeavePerDay = 500m,
                    AccountTitle = "بامطرف كشف رواتب",
                    BankName = "Al rajhi",
                    AccountNumber = "1111-2222"
                };
                await _context.Employee.AddAsync(empTheodoreLowe);
                await _context.SaveChangesAsync();

                Employee empCalistaWise = new Employee()
                {
                    FirstName = "العميل",
                    LastName = "",
                    Gender = "Male",
                    DateOfBirth = new DateTime(1990, 1, 1),
                    PlaceOfBirth = "الرياض",
                    MaritalStatus = "أعزب",
                    Email = "Customer@fas.com",
                    Phone = "(966) 543-1234",
                    Address1 = "الرياض.",
                    City = "الرياض",
                    StateProvince = "الرياض",
                    ZipCode = "11111",
                    Country = "السعودية",
                    EmployeeIDNumber = "EMPFI005",
                    Designation = designationStaffFinance,
                    Department = financeDepartment,
                    Supervisor = empTheodoreLowe,
                    JoiningDate = new DateTime(2017, 1, 5),
                    BasicSalary = 0m,
                    UnpaidLeavePerDay = 0m,
                    AccountTitle = "  كشف رواتب",
                    BankName = "Al rajhi",
                    AccountNumber = "1111-2222"
                };
                await _context.Employee.AddAsync(empCalistaWise);
                await _context.SaveChangesAsync();


                //insert employee (IT)
                Employee empKylaOlsen = new Employee()
                {
                    FirstName = "محمود ",
                    LastName = "ضاحي",
                    Gender = "Male",
                    DateOfBirth = new DateTime(1990, 1, 1),
                    PlaceOfBirth = "الرياض",
                    MaritalStatus = "أعزب",
                    Email = "Dahi@fas.com",
                    Phone = "(966) 543-1234",
                    Address1 = "الرياض.",
                    City = "الرياض",
                    StateProvince = "الرياض",
                    ZipCode = "11111",
                    Country = "السعودية",
                    EmployeeIDNumber = "EMPIT001",
                    Designation = designationVPIT,
                    Department = itDepartment,
                    Supervisor = empCeciliaChapman,
                    JoiningDate = new DateTime(2017, 2, 1),
                    BasicSalary = 25000m,
                    UnpaidLeavePerDay = 1250m,
                    AccountTitle = "ضاحي كشف رواتب",
                    BankName = "Al rajhi",
                    AccountNumber = "1111-2222"
                };
                await _context.Employee.AddAsync(empKylaOlsen);
                await _context.SaveChangesAsync();

                Employee empForrestRay = new Employee()
                {
                    FirstName = "رامي ",
                    LastName = "كمال",
                    Gender = "Male",
                    DateOfBirth = new DateTime(1990, 1, 1),
                    PlaceOfBirth = "الرياض",
                    MaritalStatus = "أعزب",
                    Email = "Rami@fas.com",
                    Phone = "(966) 543-1234",
                    Address1 = "الرياض.",
                    City = "الرياض",
                    StateProvince = "الرياض",
                    ZipCode = "11111",
                    Country = "السعودية",
                    EmployeeIDNumber = "EMPIT002",
                    Designation = designationHeadIT,
                    Department = itDepartment,
                    Supervisor = empKylaOlsen,
                    JoiningDate = new DateTime(2017, 2, 2),
                    BasicSalary = 20000m,
                    UnpaidLeavePerDay = 1000m,
                    AccountTitle = "رامي كشف رواتب",
                    BankName = "Al rajhi",
                    AccountNumber = "1111-2222"
                };
                await _context.Employee.AddAsync(empForrestRay);
                await _context.SaveChangesAsync();

                Employee empHirokoPotter = new Employee()
                {
                    FirstName = "محمد ",
                    LastName = "مصطفى",
                    Gender = "Male",
                    DateOfBirth = new DateTime(1990, 1, 1),
                    PlaceOfBirth = "الرياض",
                    MaritalStatus = "أعزب",
                    Email = "MMostafa@fas.com",
                    Phone = "(966) 543-1234",
                    Address1 = "الرياض.",
                    City = "الرياض",
                    StateProvince = "الرياض",
                    ZipCode = "11111",
                    Country = "السعودية",
                    EmployeeIDNumber = "EMPIT003",
                    Designation = designationSeniorManagerIT,
                    Department = itDepartment,
                    Supervisor = empForrestRay,
                    JoiningDate = new DateTime(2017, 2, 3),
                    BasicSalary = 15000m,
                    UnpaidLeavePerDay = 750m,
                    AccountTitle = "مصطفى كشف رواتب",
                    BankName = "Al rajhi",
                    AccountNumber = "1111-2222"
                };
                await _context.Employee.AddAsync(empHirokoPotter);
                await _context.SaveChangesAsync();

                Employee empLawrenceMoreno = new Employee()
                {
                    FirstName = "اشرف",
                    LastName = "",
                    Gender = "Male",
                    DateOfBirth = new DateTime(1990, 1, 1),
                    PlaceOfBirth = "الرياض",
                    MaritalStatus = "أعزب",
                    Email = "Ashraf@fas.com",
                    Phone = "(966) 543-1234",
                    Address1 = "الرياض.",
                    City = "الرياض",
                    StateProvince = "الرياض",
                    ZipCode = "11111",
                    Country = "السعودية",
                    EmployeeIDNumber = "EMPIT004",
                    Designation = designationManagerIT,
                    Department = itDepartment,
                    Supervisor = empHirokoPotter,
                    JoiningDate = new DateTime(2017, 2, 4),
                    BasicSalary = 10000m,
                    UnpaidLeavePerDay = 500m,
                    AccountTitle = "اشرف كشف رواتب",
                    BankName = "Al rajhi",
                    AccountNumber = "1111-2222"
                };
                await _context.Employee.AddAsync(empLawrenceMoreno);
                await _context.SaveChangesAsync();

                Employee empAaronHawkins = new Employee()
                {
                    FirstName = "محمد ",
                    LastName = "سامي",
                    Gender = "Male",
                    DateOfBirth = new DateTime(1990, 1, 1),
                    PlaceOfBirth = "الرياض",
                    MaritalStatus = "أعزب",
                    Email = "MSami@fas.com",
                    Phone = "(966) 543-1234",
                    Address1 = "الرياض.",
                    City = "الرياض",
                    StateProvince = "الرياض",
                    ZipCode = "11111",
                    Country = "السعودية",
                    EmployeeIDNumber = "EMPIT005",
                    Designation = designationStaffIT,
                    Department = itDepartment,
                    Supervisor = empLawrenceMoreno,
                    JoiningDate = new DateTime(2017, 2, 5),
                    BasicSalary = 9000m,
                    UnpaidLeavePerDay = 450m,
                    AccountTitle = "سامي كشف رواتب",
                    BankName = "Al rajhi",
                    AccountNumber = "1111-2222"
                };
                await _context.Employee.AddAsync(empAaronHawkins);
                await _context.SaveChangesAsync();


                //insert employee (HR)
                Employee empHedyGreene = new Employee()
                {
                    FirstName = "أحمد ",
                    LastName = "غالب",
                    Gender = "Male",
                    DateOfBirth = new DateTime(1990, 1, 1),
                    PlaceOfBirth = "الرياض",
                    MaritalStatus = "أعزب",
                    Email = "AhmedG@fas.com",
                    Phone = "(966) 543-1234",
                    Address1 = "الرياض.",
                    City = "الرياض",
                    StateProvince = "الرياض",
                    ZipCode = "11111",
                    Country = "السعودية",
                    EmployeeIDNumber = "EMPHR001",
                    Designation = designationVPHR,
                    Department = hrDepartment,
                    Supervisor = empCeciliaChapman,
                    JoiningDate = new DateTime(2017, 3, 1),
                    BasicSalary = 25000m,
                    UnpaidLeavePerDay = 1250m,
                    AccountTitle = "غالب كشف رواتب",
                    BankName = "Al rajhi",
                    AccountNumber = "1111-2222"
                };
                await _context.Employee.AddAsync(empHedyGreene);
                await _context.SaveChangesAsync();

                Employee empMelvinPorter = new Employee()
                {
                    FirstName = "سامر",
                    LastName = "",
                    Gender = "Male",
                    DateOfBirth = new DateTime(1990, 1, 1),
                    PlaceOfBirth = "الرياض",
                    MaritalStatus = "أعزب",
                    Email = "Samer@fas.com",
                    Phone = "(966) 543-1234",
                    Address1 = "الرياض.",
                    City = "الرياض",
                    StateProvince = "الرياض",
                    ZipCode = "11111",
                    Country = "السعودية",
                    EmployeeIDNumber = "EMPHR002",
                    Designation = designationHeadHR,
                    Department = hrDepartment,
                    Supervisor = empHedyGreene,
                    JoiningDate = new DateTime(2017, 3, 2),
                    BasicSalary = 20000m,
                    UnpaidLeavePerDay = 1000m,
                    AccountTitle = "سامر كشف رواتب",
                    BankName = "Al rajhi",
                    AccountNumber = "1111-2222"
                };
                await _context.Employee.AddAsync(empMelvinPorter);
                await _context.SaveChangesAsync();

                Employee empJoanRomero = new Employee()
                {
                    FirstName = "احمد ",
                    LastName = "قاسم",
                    Gender = "Male",
                    DateOfBirth = new DateTime(1990, 1, 1),
                    PlaceOfBirth = "الرياض",
                    MaritalStatus = "أعزب",
                    Email = "AhmedQ@fas.com",
                    Phone = "(966) 543-1234",
                    Address1 = "الرياض.",
                    City = "الرياض",
                    StateProvince = "الرياض",
                    ZipCode = "11111",
                    Country = "السعودية",
                    EmployeeIDNumber = "EMPHR003",
                    Designation = designationSeniorManagerHR,
                    Department = hrDepartment,
                    Supervisor = empMelvinPorter,
                    JoiningDate = new DateTime(2017, 3, 3),
                    BasicSalary = 15000m,
                    UnpaidLeavePerDay = 750m,
                    AccountTitle = "قاسم كشف رواتب",
                    BankName = "Al rajhi",
                    AccountNumber = "1111-2222"
                };
                await _context.Employee.AddAsync(empJoanRomero);
                await _context.SaveChangesAsync();

                Employee empLeilaniBoyer = new Employee()
                {
                    FirstName = "حسن ",
                    LastName = "محمد",
                    Gender = "Male",
                    DateOfBirth = new DateTime(1990, 1, 1),
                    PlaceOfBirth = "الرياض",
                    MaritalStatus = "أعزب",
                    Email = "hasan@fas.com",
                    Phone = "(966) 543-1234",
                    Address1 = "الرياض.",
                    City = "الرياض",
                    StateProvince = "الرياض",
                    ZipCode = "11111",
                    Country = "السعودية",
                    EmployeeIDNumber = "EMPHR004",
                    Designation = designationManagerHR,
                    Department = hrDepartment,
                    Supervisor = empJoanRomero,
                    JoiningDate = new DateTime(2017, 3, 4),
                    BasicSalary = 10000m,
                    UnpaidLeavePerDay = 500m,
                    AccountTitle = "محمد كشف رواتب",
                    BankName = "Al rajhi",
                    AccountNumber = "1111-2222"
                };
                await _context.Employee.AddAsync(empLeilaniBoyer);
                await _context.SaveChangesAsync();

                Employee empColbyBernard = new Employee()
                {
                    FirstName = "شريف",
                    LastName = "",
                    Gender = "Male",
                    DateOfBirth = new DateTime(1990, 1, 1),
                    PlaceOfBirth = "الرياض",
                    MaritalStatus = "أعزب",
                    Email = "Sherif@fas.com",
                    Phone = "(966) 543-1234",
                    Address1 = "الرياض.",
                    City = "الرياض",
                    StateProvince = "الرياض",
                    ZipCode = "11111",
                    Country = "السعودية",
                    EmployeeIDNumber = "EMPHR005",
                    Designation = designationStaffHR,
                    Department = hrDepartment,
                    Supervisor = empLeilaniBoyer,
                    JoiningDate = new DateTime(2017, 3, 5),
                    BasicSalary = 9000m,
                    UnpaidLeavePerDay = 450m,
                    AccountTitle = "شريف كشف رواتب",
                    BankName = "Al rajhi",
                    AccountNumber = "1111-2222"
                };
                await _context.Employee.AddAsync(empColbyBernard);
                await _context.SaveChangesAsync();


                //insert and connect system user with employee
                List<Employee> employees = new List<Employee>();
                employees = _context.Employee.ToList();
                foreach (var item in employees)
                {
                    try
                    {


                        ApplicationUser appUser = new ApplicationUser();

                        appUser.Email = item.Email;
                        appUser.UserName = item.Email;
                        appUser.EmailConfirmed = true;
                        appUser.isSuperAdmin = false;

                        //create system user
                        await _userManager.CreateAsync(appUser, "123456");

                        //connect employee with their system user

                        if (item.Equals(empCeciliaChapman))
                        {
                            //the CEO connect with super admin
                            item.SystemUser = superAdmin;
                        }
                        else
                        {
                            item.SystemUser = appUser;
                        }


                        _context.Employee.Update(item);
                        await _context.SaveChangesAsync();



                        //assign role SelfService to employee
                        //await _userManager.AddToRoleAsync(appUser, Services.App.Pages.SelfService.RoleName);

                        //assign role ProjectTask to employee
                        //await _userManager.AddToRoleAsync(appUser, Services.App.Pages.ProjectTask.RoleName);

                        //assign role chat to employee
                        await _userManager.AddToRoleAsync(appUser, Services.App.Pages.Home.RoleName);

                        //assign role LicenceTask to employee
                        //await _userManager.AddToRoleAsync(appUser, Services.App.Pages.LicenceTask.RoleName);
                    }
                    catch (Exception ex)
                    {

                        //throw;
                    }
                }

                //insert ticket
                List<Employee> agents = new List<Employee>();
                agents = _context.Employee.Where(x => x.Department.Name.Equals("HR Department")).ToList();

                List<Employee> nonHREmployees = new List<Employee>();
                nonHREmployees = _context.Employee.Where(x => !x.Department.Name.Equals("HR Department")).ToList();

                List<TicketType> ticketTypes = new List<TicketType>();
                ticketTypes = _context.TicketType.ToList();

                DateTime startTicket = new DateTime(2019, 1, 1);
                DateTime endTicket = DateTime.Now;

                //set random ticket dates
                List<int> ticketDates = new List<int>();
                for (int i = 0; i < 5; i++)
                {
                    int randomDate = new Random().Next(1, 30);
                    if (!ticketDates.Contains(randomDate))
                    {
                        ticketDates.Add(randomDate);
                    }

                }

                foreach (var item in nonHREmployees)
                {
                    for (DateTime date = startTicket.Date; date < endTicket.Date; date = date.AddDays(1))
                    {
                        if (ticketDates.Contains(date.Day)
                            && (date.DayOfWeek != DayOfWeek.Saturday || date.DayOfWeek != DayOfWeek.Sunday))
                        {
                            Ticket ticket = new Ticket();
                            ticket.TicketType = ticketTypes[new Random().Next(0, ticketTypes.Count - 1)];
                            ticket.OnBehalf = item;
                            ticket.Agent = agents[new Random().Next(0, agents.Count - 1)];
                            ticket.TicketName = date.ToString("yyyy-MM-dd") + " " + ticket.OnBehalf.EmployeeIDNumber;
                            ticket.Description = ticket.OnBehalf.FirstName + " " + ticket.OnBehalf.LastName + " Asking About " + ticket.TicketType.Name;
                            ticket.SubmitDate = date;
                            ticket.IsSolve = new Random().Next(0, 2) == 1 ? true : false;
                            ticket.SolutionNote = "Update info with adjustment.";
                            await _context.Ticket.AddAsync(ticket);
                        }

                    }

                    await _context.SaveChangesAsync();

                }

                //insert todo
                List<TodoType> todoTypes = new List<TodoType>();
                todoTypes = _context.TodoType.ToList();

                DateTime startTodo = new DateTime(2019, 1, 1);
                DateTime endTodo = DateTime.Now;

                //set random todo dates
                List<int> todoDates = new List<int>();
                for (int i = 0; i < 8; i++)
                {
                    int randomDate = new Random().Next(1, 30);
                    if (!todoDates.Contains(randomDate))
                    {
                        todoDates.Add(randomDate);
                    }

                }

                for (DateTime date = startTodo.Date; date < endTodo.Date; date = date.AddDays(1))
                {
                    if (todoDates.Contains(date.Day)
                        && (date.DayOfWeek != DayOfWeek.Saturday || date.DayOfWeek != DayOfWeek.Sunday))
                    {
                        Todo todo = new Todo();
                        todo.TodoType = todoTypes[new Random().Next(0, todoTypes.Count - 1)];
                        todo.OnBehalf = agents[new Random().Next(0, agents.Count - 1)];
                        todo.TodoItem = date.ToString("yyyy-MM-dd") + " " + todo.OnBehalf.EmployeeIDNumber;
                        todo.Description = todo.OnBehalf.FirstName + " " + todo.OnBehalf.LastName + " Handling " + todo.TodoType.Name;
                        todo.StartDate = date;
                        todo.EndDate = date;
                        todo.IsDone = new Random().Next(0, 2) == 1 ? true : false;
                        await _context.Todo.AddAsync(todo);
                    }

                }
                await _context.SaveChangesAsync();

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
