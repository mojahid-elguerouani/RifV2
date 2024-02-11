using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Fas.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    isSuperAdmin = table.Column<bool>(nullable: true),
                    userType = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ChatMessages",
                columns: table => new
                {
                    ChatMessageID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FromUserID = table.Column<string>(nullable: true),
                    ToUserID = table.Column<string>(nullable: true),
                    Message = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: false),
                    ViewedOn = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatMessages", x => x.ChatMessageID);
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    EventID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Subject = table.Column<string>(maxLength: 100, nullable: true),
                    Description = table.Column<string>(maxLength: 200, nullable: true),
                    Start = table.Column<DateTime>(nullable: false),
                    End = table.Column<DateTime>(nullable: true),
                    ThemeColor = table.Column<string>(maxLength: 10, nullable: true),
                    IsFullDay = table.Column<bool>(nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.EventID);
                });

            migrationBuilder.CreateTable(
                name: "FriendMappings",
                columns: table => new
                {
                    FriendMappingID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RequestorUserID = table.Column<string>(nullable: true),
                    EndUserID = table.Column<string>(nullable: true),
                    RequestStatus = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FriendMappings", x => x.FriendMappingID);
                });

            migrationBuilder.CreateTable(
                name: "Log",
                columns: table => new
                {
                    LogId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    OperationType = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Controller = table.Column<string>(nullable: true),
                    Action = table.Column<string>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedAtUtc = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Log", x => x.LogId);
                });

            migrationBuilder.CreateTable(
                name: "OnlineUsers",
                columns: table => new
                {
                    OnlineUserID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserID = table.Column<string>(nullable: true),
                    ConnectionID = table.Column<string>(nullable: true),
                    IsOnline = table.Column<bool>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OnlineUsers", x => x.OnlineUserID);
                });

            migrationBuilder.CreateTable(
                name: "Responses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ResponseId = table.Column<string>(nullable: true),
                    SurveyId = table.Column<string>(nullable: true),
                    QuestionId = table.Column<int>(nullable: false),
                    ChoiceText = table.Column<string>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ChoiceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Responses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Surveys",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Title = table.Column<string>(nullable: false),
                    Timestamp = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Surveys", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserImages",
                columns: table => new
                {
                    ImageID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserID = table.Column<string>(nullable: true),
                    ImagePath = table.Column<string>(nullable: true),
                    IsProfilePicture = table.Column<bool>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserImages", x => x.ImageID);
                });

            migrationBuilder.CreateTable(
                name: "UserNotifications",
                columns: table => new
                {
                    NotificationID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ToUserID = table.Column<string>(nullable: true),
                    FromUserID = table.Column<string>(nullable: true),
                    NotificationType = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserNotifications", x => x.NotificationID);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AllowanceType",
                columns: table => new
                {
                    AllowanceTypeId = table.Column<string>(nullable: false),
                    CreatedById = table.Column<string>(nullable: true),
                    CreatedAtUtc = table.Column<DateTime>(nullable: false),
                    UpdatedById = table.Column<string>(nullable: true),
                    UpdatedAtUtc = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AllowanceType", x => x.AllowanceTypeId);
                    table.ForeignKey(
                        name: "FK_AllowanceType_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AllowanceType_AspNetUsers_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Attachments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedById = table.Column<string>(nullable: true),
                    CreatedAtUtc = table.Column<DateTime>(nullable: false),
                    UpdatedById = table.Column<string>(nullable: true),
                    UpdatedAtUtc = table.Column<DateTime>(nullable: false),
                    Url = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    DateAdded = table.Column<DateTime>(nullable: true),
                    ProjectId = table.Column<string>(nullable: true),
                    IsApproved = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attachments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attachments_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Attachments_AspNetUsers_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BenefitTemplate",
                columns: table => new
                {
                    BenefitTemplateId = table.Column<string>(nullable: false),
                    CreatedById = table.Column<string>(nullable: true),
                    CreatedAtUtc = table.Column<DateTime>(nullable: false),
                    UpdatedById = table.Column<string>(nullable: true),
                    UpdatedAtUtc = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BenefitTemplate", x => x.BenefitTemplateId);
                    table.ForeignKey(
                        name: "FK_BenefitTemplate_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BenefitTemplate_AspNetUsers_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Contractors",
                columns: table => new
                {
                    ContractorId = table.Column<string>(nullable: false),
                    CreatedById = table.Column<string>(nullable: true),
                    CreatedAtUtc = table.Column<DateTime>(nullable: false),
                    UpdatedById = table.Column<string>(nullable: true),
                    UpdatedAtUtc = table.Column<DateTime>(nullable: false),
                    ContractorName = table.Column<string>(maxLength: 50, nullable: false),
                    ContractorCode = table.Column<string>(maxLength: 50, nullable: false),
                    LogoUrl = table.Column<string>(maxLength: 50, nullable: true),
                    Email = table.Column<string>(maxLength: 50, nullable: true),
                    PhoneNumber = table.Column<string>(maxLength: 50, nullable: false),
                    UserName = table.Column<string>(maxLength: 50, nullable: true),
                    Password = table.Column<string>(maxLength: 50, nullable: true),
                    ContractorUserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contractors", x => x.ContractorId);
                    table.ForeignKey(
                        name: "FK_Contractors_AspNetUsers_ContractorUserId",
                        column: x => x.ContractorUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Contractors_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Contractors_AspNetUsers_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DeductionType",
                columns: table => new
                {
                    DeductionTypeId = table.Column<string>(nullable: false),
                    CreatedById = table.Column<string>(nullable: true),
                    CreatedAtUtc = table.Column<DateTime>(nullable: false),
                    UpdatedById = table.Column<string>(nullable: true),
                    UpdatedAtUtc = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeductionType", x => x.DeductionTypeId);
                    table.ForeignKey(
                        name: "FK_DeductionType_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DeductionType_AspNetUsers_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Department",
                columns: table => new
                {
                    DepartmentId = table.Column<string>(nullable: false),
                    CreatedById = table.Column<string>(nullable: true),
                    CreatedAtUtc = table.Column<DateTime>(nullable: false),
                    UpdatedById = table.Column<string>(nullable: true),
                    UpdatedAtUtc = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Department", x => x.DepartmentId);
                    table.ForeignKey(
                        name: "FK_Department_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Department_AspNetUsers_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Designation",
                columns: table => new
                {
                    DesignationId = table.Column<string>(nullable: false),
                    CreatedById = table.Column<string>(nullable: true),
                    CreatedAtUtc = table.Column<DateTime>(nullable: false),
                    UpdatedById = table.Column<string>(nullable: true),
                    UpdatedAtUtc = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Designation", x => x.DesignationId);
                    table.ForeignKey(
                        name: "FK_Designation_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Designation_AspNetUsers_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProjectManagementConsultants",
                columns: table => new
                {
                    ProjectManagementConsultantId = table.Column<string>(nullable: false),
                    CreatedById = table.Column<string>(nullable: true),
                    CreatedAtUtc = table.Column<DateTime>(nullable: false),
                    UpdatedById = table.Column<string>(nullable: true),
                    UpdatedAtUtc = table.Column<DateTime>(nullable: false),
                    ProjectManagementConsultantName = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectManagementConsultants", x => x.ProjectManagementConsultantId);
                    table.ForeignKey(
                        name: "FK_ProjectManagementConsultants_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectManagementConsultants_AspNetUsers_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProjectPrograms",
                columns: table => new
                {
                    ProjectProgramId = table.Column<string>(nullable: false),
                    CreatedById = table.Column<string>(nullable: true),
                    CreatedAtUtc = table.Column<DateTime>(nullable: false),
                    UpdatedById = table.Column<string>(nullable: true),
                    UpdatedAtUtc = table.Column<DateTime>(nullable: false),
                    ProjectProgramName = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectPrograms", x => x.ProjectProgramId);
                    table.ForeignKey(
                        name: "FK_ProjectPrograms_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectPrograms_AspNetUsers_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReceiveBusinessSchedualTemplets",
                columns: table => new
                {
                    ReceiveBusinessSchedualTempletId = table.Column<string>(nullable: false),
                    CreatedById = table.Column<string>(nullable: true),
                    CreatedAtUtc = table.Column<DateTime>(nullable: false),
                    UpdatedById = table.Column<string>(nullable: true),
                    UpdatedAtUtc = table.Column<DateTime>(nullable: false),
                    ReceiveBusinessSchedualTempletName = table.Column<string>(maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceiveBusinessSchedualTemplets", x => x.ReceiveBusinessSchedualTempletId);
                    table.ForeignKey(
                        name: "FK_ReceiveBusinessSchedualTemplets_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReceiveBusinessSchedualTemplets_AspNetUsers_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SupervisionConsultants",
                columns: table => new
                {
                    SupervisionConsultantId = table.Column<string>(nullable: false),
                    CreatedById = table.Column<string>(nullable: true),
                    CreatedAtUtc = table.Column<DateTime>(nullable: false),
                    UpdatedById = table.Column<string>(nullable: true),
                    UpdatedAtUtc = table.Column<DateTime>(nullable: false),
                    SupervisionConsultantName = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupervisionConsultants", x => x.SupervisionConsultantId);
                    table.ForeignKey(
                        name: "FK_SupervisionConsultants_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SupervisionConsultants_AspNetUsers_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TicketType",
                columns: table => new
                {
                    TicketTypeId = table.Column<string>(nullable: false),
                    CreatedById = table.Column<string>(nullable: true),
                    CreatedAtUtc = table.Column<DateTime>(nullable: false),
                    UpdatedById = table.Column<string>(nullable: true),
                    UpdatedAtUtc = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketType", x => x.TicketTypeId);
                    table.ForeignKey(
                        name: "FK_TicketType_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TicketType_AspNetUsers_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TodoType",
                columns: table => new
                {
                    TodoTypeId = table.Column<string>(nullable: false),
                    CreatedById = table.Column<string>(nullable: true),
                    CreatedAtUtc = table.Column<DateTime>(nullable: false),
                    UpdatedById = table.Column<string>(nullable: true),
                    UpdatedAtUtc = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TodoType", x => x.TodoTypeId);
                    table.ForeignKey(
                        name: "FK_TodoType_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TodoType_AspNetUsers_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    QuestionId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    QuestionBody = table.Column<string>(nullable: true),
                    AnswerType = table.Column<string>(nullable: true),
                    SurveyId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.QuestionId);
                    table.ForeignKey(
                        name: "FK_Questions_Surveys_SurveyId",
                        column: x => x.SurveyId,
                        principalTable: "Surveys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ContractorImages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedById = table.Column<string>(nullable: true),
                    CreatedAtUtc = table.Column<DateTime>(nullable: false),
                    UpdatedById = table.Column<string>(nullable: true),
                    UpdatedAtUtc = table.Column<DateTime>(nullable: false),
                    ContractorImageUrl = table.Column<string>(nullable: true),
                    ContractorId = table.Column<int>(nullable: false),
                    ContractorId1 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractorImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContractorImages_Contractors_ContractorId1",
                        column: x => x.ContractorId1,
                        principalTable: "Contractors",
                        principalColumn: "ContractorId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ContractorImages_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ContractorImages_AspNetUsers_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BenefitTemplateLine",
                columns: table => new
                {
                    BenefitTemplateLineId = table.Column<string>(nullable: false),
                    CreatedById = table.Column<string>(nullable: true),
                    CreatedAtUtc = table.Column<DateTime>(nullable: false),
                    UpdatedById = table.Column<string>(nullable: true),
                    UpdatedAtUtc = table.Column<DateTime>(nullable: false),
                    BenefitTemplateId = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    AllowanceTypeId = table.Column<string>(nullable: true),
                    DeductionTypeId = table.Column<string>(nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BenefitTemplateLine", x => x.BenefitTemplateLineId);
                    table.ForeignKey(
                        name: "FK_BenefitTemplateLine_AllowanceType_AllowanceTypeId",
                        column: x => x.AllowanceTypeId,
                        principalTable: "AllowanceType",
                        principalColumn: "AllowanceTypeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BenefitTemplateLine_BenefitTemplate_BenefitTemplateId",
                        column: x => x.BenefitTemplateId,
                        principalTable: "BenefitTemplate",
                        principalColumn: "BenefitTemplateId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BenefitTemplateLine_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BenefitTemplateLine_DeductionType_DeductionTypeId",
                        column: x => x.DeductionTypeId,
                        principalTable: "DeductionType",
                        principalColumn: "DeductionTypeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BenefitTemplateLine_AspNetUsers_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    EmployeeId = table.Column<string>(nullable: false),
                    CreatedById = table.Column<string>(nullable: true),
                    CreatedAtUtc = table.Column<DateTime>(nullable: false),
                    UpdatedById = table.Column<string>(nullable: true),
                    UpdatedAtUtc = table.Column<DateTime>(nullable: false),
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: true),
                    Gender = table.Column<string>(nullable: false),
                    DateOfBirth = table.Column<DateTime>(nullable: false),
                    PlaceOfBirth = table.Column<string>(nullable: false),
                    MaritalStatus = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: false),
                    Phone = table.Column<string>(nullable: false),
                    Address1 = table.Column<string>(nullable: false),
                    Address2 = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: false),
                    StateProvince = table.Column<string>(nullable: false),
                    ZipCode = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true),
                    ProfilePicture = table.Column<string>(nullable: true),
                    EmployeeIDNumber = table.Column<string>(nullable: true),
                    DesignationId = table.Column<string>(nullable: false),
                    DepartmentId = table.Column<string>(nullable: false),
                    JoiningDate = table.Column<DateTime>(nullable: false),
                    LeavingDate = table.Column<DateTime>(nullable: true),
                    SupervisorId = table.Column<string>(nullable: true),
                    BasicSalary = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UnpaidLeavePerDay = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BenefitTemplateId = table.Column<string>(nullable: true),
                    AccountTitle = table.Column<string>(nullable: false),
                    BankName = table.Column<string>(nullable: false),
                    AccountNumber = table.Column<string>(nullable: false),
                    SwiftCode = table.Column<string>(nullable: true),
                    SystemUserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.EmployeeId);
                    table.ForeignKey(
                        name: "FK_Employee_BenefitTemplate_BenefitTemplateId",
                        column: x => x.BenefitTemplateId,
                        principalTable: "BenefitTemplate",
                        principalColumn: "BenefitTemplateId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employee_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employee_Department_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Department",
                        principalColumn: "DepartmentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Employee_Designation_DesignationId",
                        column: x => x.DesignationId,
                        principalTable: "Designation",
                        principalColumn: "DesignationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Employee_Employee_SupervisorId",
                        column: x => x.SupervisorId,
                        principalTable: "Employee",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employee_AspNetUsers_SystemUserId",
                        column: x => x.SystemUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employee_AspNetUsers_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReceiveBusinessScheduals",
                columns: table => new
                {
                    ReceiveBusinessSchedualId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedById = table.Column<string>(nullable: true),
                    CreatedAtUtc = table.Column<DateTime>(nullable: false),
                    UpdatedById = table.Column<string>(nullable: true),
                    UpdatedAtUtc = table.Column<DateTime>(nullable: false),
                    ReceiveBusinessSchedualTempletId = table.Column<string>(nullable: true),
                    TaskName = table.Column<string>(nullable: true),
                    TaskOrder = table.Column<int>(nullable: true),
                    ReceiveBusinessAssignToId = table.Column<string>(nullable: true),
                    toEmail = table.Column<string>(nullable: true),
                    ReceiveBusinessSchedualParentId = table.Column<int>(nullable: true),
                    ReceiveBusinessApprovedById = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceiveBusinessScheduals", x => x.ReceiveBusinessSchedualId);
                    table.ForeignKey(
                        name: "FK_ReceiveBusinessScheduals_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReceiveBusinessScheduals_AspNetUsers_ReceiveBusinessApprovedById",
                        column: x => x.ReceiveBusinessApprovedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReceiveBusinessScheduals_AspNetUsers_ReceiveBusinessAssignToId",
                        column: x => x.ReceiveBusinessAssignToId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReceiveBusinessScheduals_ReceiveBusinessScheduals_ReceiveBusinessSchedualParentId",
                        column: x => x.ReceiveBusinessSchedualParentId,
                        principalTable: "ReceiveBusinessScheduals",
                        principalColumn: "ReceiveBusinessSchedualId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReceiveBusinessScheduals_ReceiveBusinessSchedualTemplets_ReceiveBusinessSchedualTempletId",
                        column: x => x.ReceiveBusinessSchedualTempletId,
                        principalTable: "ReceiveBusinessSchedualTemplets",
                        principalColumn: "ReceiveBusinessSchedualTempletId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReceiveBusinessScheduals_AspNetUsers_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    ProjectId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedById = table.Column<string>(nullable: true),
                    CreatedAtUtc = table.Column<DateTime>(nullable: false),
                    UpdatedById = table.Column<string>(nullable: true),
                    UpdatedAtUtc = table.Column<DateTime>(nullable: false),
                    ProjectName = table.Column<string>(maxLength: 50, nullable: true),
                    ProjectCode = table.Column<string>(maxLength: 50, nullable: false),
                    ProjectDescription = table.Column<string>(nullable: true),
                    StartDate = table.Column<DateTime>(type: "date", nullable: true),
                    EndDate = table.Column<DateTime>(type: "date", nullable: true),
                    EstimatedBudget = table.Column<int>(nullable: true),
                    TotalAmountSpent = table.Column<int>(nullable: true),
                    ContractorId = table.Column<string>(nullable: false),
                    ProjectProgramId = table.Column<string>(nullable: false),
                    SupervisionConsultantId = table.Column<string>(nullable: false),
                    ProjectManagementConsultantId = table.Column<string>(nullable: false),
                    StatusId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.ProjectId);
                    table.ForeignKey(
                        name: "FK_Projects_Contractors_ContractorId",
                        column: x => x.ContractorId,
                        principalTable: "Contractors",
                        principalColumn: "ContractorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Projects_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Projects_ProjectManagementConsultants_ProjectManagementConsultantId",
                        column: x => x.ProjectManagementConsultantId,
                        principalTable: "ProjectManagementConsultants",
                        principalColumn: "ProjectManagementConsultantId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Projects_ProjectPrograms_ProjectProgramId",
                        column: x => x.ProjectProgramId,
                        principalTable: "ProjectPrograms",
                        principalColumn: "ProjectProgramId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Projects_SupervisionConsultants_SupervisionConsultantId",
                        column: x => x.SupervisionConsultantId,
                        principalTable: "SupervisionConsultants",
                        principalColumn: "SupervisionConsultantId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Projects_AspNetUsers_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Choices",
                columns: table => new
                {
                    ChoiceId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SurveyId = table.Column<string>(nullable: true),
                    QuestionId = table.Column<int>(nullable: false),
                    ChoiceText = table.Column<string>(nullable: true),
                    orderid = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Choices", x => x.ChoiceId);
                    table.ForeignKey(
                        name: "FK_Choices_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "QuestionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ticket",
                columns: table => new
                {
                    TicketId = table.Column<string>(nullable: false),
                    CreatedById = table.Column<string>(nullable: true),
                    CreatedAtUtc = table.Column<DateTime>(nullable: false),
                    UpdatedById = table.Column<string>(nullable: true),
                    UpdatedAtUtc = table.Column<DateTime>(nullable: false),
                    TicketName = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    IsSolve = table.Column<bool>(nullable: false),
                    SolutionNote = table.Column<string>(nullable: true),
                    TicketTypeId = table.Column<string>(nullable: false),
                    SubmitDate = table.Column<DateTimeOffset>(nullable: false),
                    OnBehalfId = table.Column<string>(nullable: false),
                    AgentId = table.Column<string>(nullable: true),
                    ParentTicketThreadId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ticket", x => x.TicketId);
                    table.ForeignKey(
                        name: "FK_Ticket_Employee_AgentId",
                        column: x => x.AgentId,
                        principalTable: "Employee",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ticket_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ticket_Employee_OnBehalfId",
                        column: x => x.OnBehalfId,
                        principalTable: "Employee",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ticket_Ticket_ParentTicketThreadId",
                        column: x => x.ParentTicketThreadId,
                        principalTable: "Ticket",
                        principalColumn: "TicketId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ticket_TicketType_TicketTypeId",
                        column: x => x.TicketTypeId,
                        principalTable: "TicketType",
                        principalColumn: "TicketTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ticket_AspNetUsers_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Todo",
                columns: table => new
                {
                    TodoId = table.Column<string>(nullable: false),
                    CreatedById = table.Column<string>(nullable: true),
                    CreatedAtUtc = table.Column<DateTime>(nullable: false),
                    UpdatedById = table.Column<string>(nullable: true),
                    UpdatedAtUtc = table.Column<DateTime>(nullable: false),
                    TodoItem = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    IsDone = table.Column<bool>(nullable: false),
                    TodoTypeId = table.Column<string>(nullable: false),
                    StartDate = table.Column<DateTimeOffset>(nullable: false),
                    EndDate = table.Column<DateTimeOffset>(nullable: false),
                    OnBehalfId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Todo", x => x.TodoId);
                    table.ForeignKey(
                        name: "FK_Todo_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Todo_Employee_OnBehalfId",
                        column: x => x.OnBehalfId,
                        principalTable: "Employee",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Todo_TodoType_TodoTypeId",
                        column: x => x.TodoTypeId,
                        principalTable: "TodoType",
                        principalColumn: "TodoTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Todo_AspNetUsers_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProjectComment",
                columns: table => new
                {
                    ProjectCommentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedById = table.Column<string>(nullable: true),
                    CreatedAtUtc = table.Column<DateTime>(nullable: false),
                    UpdatedById = table.Column<string>(nullable: true),
                    UpdatedAtUtc = table.Column<DateTime>(nullable: false),
                    Comment = table.Column<string>(nullable: true),
                    CommentFromId = table.Column<string>(nullable: true),
                    CommentToId = table.Column<string>(nullable: true),
                    ProjectId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectComment", x => x.ProjectCommentId);
                    table.ForeignKey(
                        name: "FK_ProjectComment_AspNetUsers_CommentFromId",
                        column: x => x.CommentFromId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectComment_AspNetUsers_CommentToId",
                        column: x => x.CommentToId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectComment_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectComment_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectComment_AspNetUsers_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReceiveBusiness",
                columns: table => new
                {
                    ReceiveBusinessId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedById = table.Column<string>(nullable: true),
                    CreatedAtUtc = table.Column<DateTime>(nullable: false),
                    UpdatedById = table.Column<string>(nullable: true),
                    UpdatedAtUtc = table.Column<DateTime>(nullable: false),
                    SerialNumber = table.Column<int>(nullable: false),
                    ReviewNumber = table.Column<int>(nullable: false),
                    TypeOfAccreditationRequest = table.Column<int>(nullable: false),
                    Specialization = table.Column<int>(nullable: false),
                    ProjectId = table.Column<int>(nullable: true),
                    ReceiveBusinessDate = table.Column<DateTime>(nullable: false),
                    StatusId = table.Column<int>(nullable: true),
                    ReceiveBusinessSchedualTempletId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceiveBusiness", x => x.ReceiveBusinessId);
                    table.ForeignKey(
                        name: "FK_ReceiveBusiness_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReceiveBusiness_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReceiveBusiness_ReceiveBusinessSchedualTemplets_ReceiveBusinessSchedualTempletId",
                        column: x => x.ReceiveBusinessSchedualTempletId,
                        principalTable: "ReceiveBusinessSchedualTemplets",
                        principalColumn: "ReceiveBusinessSchedualTempletId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReceiveBusiness_AspNetUsers_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CommentImage",
                columns: table => new
                {
                    CommentImageId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CommentImageUrl = table.Column<string>(nullable: true),
                    ProjectCommentId = table.Column<int>(nullable: false),
                    FileName = table.Column<string>(nullable: true),
                    ImageType = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommentImage", x => x.CommentImageId);
                    table.ForeignKey(
                        name: "FK_CommentImage_ProjectComment_ProjectCommentId",
                        column: x => x.ProjectCommentId,
                        principalTable: "ProjectComment",
                        principalColumn: "ProjectCommentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReceiveBusinessComment",
                columns: table => new
                {
                    ReceiveBusinessCommentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedById = table.Column<string>(nullable: true),
                    CreatedAtUtc = table.Column<DateTime>(nullable: false),
                    UpdatedById = table.Column<string>(nullable: true),
                    UpdatedAtUtc = table.Column<DateTime>(nullable: false),
                    Comment = table.Column<string>(nullable: true),
                    CommentFromId = table.Column<string>(nullable: true),
                    CommentToId = table.Column<string>(nullable: true),
                    PurchaseId = table.Column<int>(nullable: false),
                    ReceiveBusinessId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceiveBusinessComment", x => x.ReceiveBusinessCommentId);
                    table.ForeignKey(
                        name: "FK_ReceiveBusinessComment_AspNetUsers_CommentFromId",
                        column: x => x.CommentFromId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReceiveBusinessComment_AspNetUsers_CommentToId",
                        column: x => x.CommentToId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReceiveBusinessComment_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReceiveBusinessComment_ReceiveBusiness_ReceiveBusinessId",
                        column: x => x.ReceiveBusinessId,
                        principalTable: "ReceiveBusiness",
                        principalColumn: "ReceiveBusinessId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReceiveBusinessComment_AspNetUsers_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReceiveBusinessTasks",
                columns: table => new
                {
                    ReceiveBusinessTaskId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedById = table.Column<string>(nullable: true),
                    CreatedAtUtc = table.Column<DateTime>(nullable: false),
                    UpdatedById = table.Column<string>(nullable: true),
                    UpdatedAtUtc = table.Column<DateTime>(nullable: false),
                    TaskName = table.Column<string>(nullable: true),
                    TaskOrder = table.Column<int>(nullable: true),
                    TaskId = table.Column<int>(nullable: false),
                    ReceiveBusinessAssignToId = table.Column<string>(nullable: true),
                    toEmail = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: true),
                    StatusId = table.Column<int>(nullable: true),
                    IsApproved = table.Column<bool>(nullable: true),
                    ApprovedById = table.Column<string>(nullable: true),
                    TaskParentId = table.Column<int>(nullable: true),
                    Compleation = table.Column<int>(nullable: true),
                    ApproveDate = table.Column<DateTime>(nullable: true),
                    UpdateDate = table.Column<DateTime>(nullable: true),
                    ReceiveBusinessId = table.Column<int>(nullable: false),
                    GroupId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceiveBusinessTasks", x => x.ReceiveBusinessTaskId);
                    table.ForeignKey(
                        name: "FK_ReceiveBusinessTasks_AspNetUsers_ApprovedById",
                        column: x => x.ApprovedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReceiveBusinessTasks_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReceiveBusinessTasks_AspNetUsers_ReceiveBusinessAssignToId",
                        column: x => x.ReceiveBusinessAssignToId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReceiveBusinessTasks_ReceiveBusiness_ReceiveBusinessId",
                        column: x => x.ReceiveBusinessId,
                        principalTable: "ReceiveBusiness",
                        principalColumn: "ReceiveBusinessId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReceiveBusinessTasks_ReceiveBusinessTasks_TaskParentId",
                        column: x => x.TaskParentId,
                        principalTable: "ReceiveBusinessTasks",
                        principalColumn: "ReceiveBusinessTaskId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReceiveBusinessTasks_AspNetUsers_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReceiveBusinessCommentImages",
                columns: table => new
                {
                    ReceiveBusinessCommentImageId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CommentImageUrl = table.Column<string>(nullable: true),
                    PurchaseCommentId = table.Column<int>(nullable: false),
                    ReceiveBusinessCommentId = table.Column<int>(nullable: true),
                    FileName = table.Column<string>(nullable: true),
                    ImageType = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceiveBusinessCommentImages", x => x.ReceiveBusinessCommentImageId);
                    table.ForeignKey(
                        name: "FK_ReceiveBusinessCommentImages_ReceiveBusinessComment_ReceiveBusinessCommentId",
                        column: x => x.ReceiveBusinessCommentId,
                        principalTable: "ReceiveBusinessComment",
                        principalColumn: "ReceiveBusinessCommentId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReceiveBusinessTaskLog",
                columns: table => new
                {
                    ReceiveBusinessTaskLogId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ReceiveBusinessTaskComment = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    ReceiveBusinessTaskId = table.Column<int>(nullable: false),
                    ReceiveBusinessUserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceiveBusinessTaskLog", x => x.ReceiveBusinessTaskLogId);
                    table.ForeignKey(
                        name: "FK_ReceiveBusinessTaskLog_ReceiveBusinessTasks_ReceiveBusinessTaskId",
                        column: x => x.ReceiveBusinessTaskId,
                        principalTable: "ReceiveBusinessTasks",
                        principalColumn: "ReceiveBusinessTaskId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReceiveBusinessTaskLog_AspNetUsers_ReceiveBusinessUserId",
                        column: x => x.ReceiveBusinessUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReceiveBusinessTaskLogImages",
                columns: table => new
                {
                    ReceiveBusinessTaskLogImageId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedById = table.Column<string>(nullable: true),
                    CreatedAtUtc = table.Column<DateTime>(nullable: false),
                    UpdatedById = table.Column<string>(nullable: true),
                    UpdatedAtUtc = table.Column<DateTime>(nullable: false),
                    ReceiveBusinessTaskLogImageUrl = table.Column<string>(maxLength: 200, nullable: true),
                    ReceiveBusinessTaskLogId = table.Column<int>(nullable: false),
                    FileName = table.Column<string>(nullable: true),
                    ImageType = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceiveBusinessTaskLogImages", x => x.ReceiveBusinessTaskLogImageId);
                    table.ForeignKey(
                        name: "FK_ReceiveBusinessTaskLogImages_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReceiveBusinessTaskLogImages_ReceiveBusinessTaskLog_ReceiveBusinessTaskLogId",
                        column: x => x.ReceiveBusinessTaskLogId,
                        principalTable: "ReceiveBusinessTaskLog",
                        principalColumn: "ReceiveBusinessTaskLogId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReceiveBusinessTaskLogImages_AspNetUsers_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AllowanceType_CreatedById",
                table: "AllowanceType",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_AllowanceType_UpdatedById",
                table: "AllowanceType",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Attachments_CreatedById",
                table: "Attachments",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Attachments_UpdatedById",
                table: "Attachments",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_BenefitTemplate_CreatedById",
                table: "BenefitTemplate",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_BenefitTemplate_UpdatedById",
                table: "BenefitTemplate",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_BenefitTemplateLine_AllowanceTypeId",
                table: "BenefitTemplateLine",
                column: "AllowanceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_BenefitTemplateLine_BenefitTemplateId",
                table: "BenefitTemplateLine",
                column: "BenefitTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_BenefitTemplateLine_CreatedById",
                table: "BenefitTemplateLine",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_BenefitTemplateLine_DeductionTypeId",
                table: "BenefitTemplateLine",
                column: "DeductionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_BenefitTemplateLine_UpdatedById",
                table: "BenefitTemplateLine",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Choices_QuestionId",
                table: "Choices",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_CommentImage_ProjectCommentId",
                table: "CommentImage",
                column: "ProjectCommentId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractorImages_ContractorId1",
                table: "ContractorImages",
                column: "ContractorId1");

            migrationBuilder.CreateIndex(
                name: "IX_ContractorImages_CreatedById",
                table: "ContractorImages",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ContractorImages_UpdatedById",
                table: "ContractorImages",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Contractors_ContractorUserId",
                table: "Contractors",
                column: "ContractorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Contractors_CreatedById",
                table: "Contractors",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Contractors_UpdatedById",
                table: "Contractors",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_DeductionType_CreatedById",
                table: "DeductionType",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_DeductionType_UpdatedById",
                table: "DeductionType",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Department_CreatedById",
                table: "Department",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Department_UpdatedById",
                table: "Department",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Designation_CreatedById",
                table: "Designation",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Designation_UpdatedById",
                table: "Designation",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_BenefitTemplateId",
                table: "Employee",
                column: "BenefitTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_CreatedById",
                table: "Employee",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_DepartmentId",
                table: "Employee",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_DesignationId",
                table: "Employee",
                column: "DesignationId");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_SupervisorId",
                table: "Employee",
                column: "SupervisorId");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_SystemUserId",
                table: "Employee",
                column: "SystemUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_UpdatedById",
                table: "Employee",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectComment_CommentFromId",
                table: "ProjectComment",
                column: "CommentFromId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectComment_CommentToId",
                table: "ProjectComment",
                column: "CommentToId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectComment_CreatedById",
                table: "ProjectComment",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectComment_ProjectId",
                table: "ProjectComment",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectComment_UpdatedById",
                table: "ProjectComment",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectManagementConsultants_CreatedById",
                table: "ProjectManagementConsultants",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectManagementConsultants_UpdatedById",
                table: "ProjectManagementConsultants",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectPrograms_CreatedById",
                table: "ProjectPrograms",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectPrograms_UpdatedById",
                table: "ProjectPrograms",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_ContractorId",
                table: "Projects",
                column: "ContractorId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_CreatedById",
                table: "Projects",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_ProjectManagementConsultantId",
                table: "Projects",
                column: "ProjectManagementConsultantId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_ProjectProgramId",
                table: "Projects",
                column: "ProjectProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_SupervisionConsultantId",
                table: "Projects",
                column: "SupervisionConsultantId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_UpdatedById",
                table: "Projects",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_SurveyId",
                table: "Questions",
                column: "SurveyId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiveBusiness_CreatedById",
                table: "ReceiveBusiness",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiveBusiness_ProjectId",
                table: "ReceiveBusiness",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiveBusiness_ReceiveBusinessSchedualTempletId",
                table: "ReceiveBusiness",
                column: "ReceiveBusinessSchedualTempletId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiveBusiness_UpdatedById",
                table: "ReceiveBusiness",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiveBusinessComment_CommentFromId",
                table: "ReceiveBusinessComment",
                column: "CommentFromId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiveBusinessComment_CommentToId",
                table: "ReceiveBusinessComment",
                column: "CommentToId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiveBusinessComment_CreatedById",
                table: "ReceiveBusinessComment",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiveBusinessComment_ReceiveBusinessId",
                table: "ReceiveBusinessComment",
                column: "ReceiveBusinessId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiveBusinessComment_UpdatedById",
                table: "ReceiveBusinessComment",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiveBusinessCommentImages_ReceiveBusinessCommentId",
                table: "ReceiveBusinessCommentImages",
                column: "ReceiveBusinessCommentId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiveBusinessScheduals_CreatedById",
                table: "ReceiveBusinessScheduals",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiveBusinessScheduals_ReceiveBusinessApprovedById",
                table: "ReceiveBusinessScheduals",
                column: "ReceiveBusinessApprovedById");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiveBusinessScheduals_ReceiveBusinessAssignToId",
                table: "ReceiveBusinessScheduals",
                column: "ReceiveBusinessAssignToId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiveBusinessScheduals_ReceiveBusinessSchedualParentId",
                table: "ReceiveBusinessScheduals",
                column: "ReceiveBusinessSchedualParentId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiveBusinessScheduals_ReceiveBusinessSchedualTempletId",
                table: "ReceiveBusinessScheduals",
                column: "ReceiveBusinessSchedualTempletId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiveBusinessScheduals_UpdatedById",
                table: "ReceiveBusinessScheduals",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiveBusinessSchedualTemplets_CreatedById",
                table: "ReceiveBusinessSchedualTemplets",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiveBusinessSchedualTemplets_UpdatedById",
                table: "ReceiveBusinessSchedualTemplets",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiveBusinessTaskLog_ReceiveBusinessTaskId",
                table: "ReceiveBusinessTaskLog",
                column: "ReceiveBusinessTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiveBusinessTaskLog_ReceiveBusinessUserId",
                table: "ReceiveBusinessTaskLog",
                column: "ReceiveBusinessUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiveBusinessTaskLogImages_CreatedById",
                table: "ReceiveBusinessTaskLogImages",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiveBusinessTaskLogImages_ReceiveBusinessTaskLogId",
                table: "ReceiveBusinessTaskLogImages",
                column: "ReceiveBusinessTaskLogId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiveBusinessTaskLogImages_UpdatedById",
                table: "ReceiveBusinessTaskLogImages",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiveBusinessTasks_ApprovedById",
                table: "ReceiveBusinessTasks",
                column: "ApprovedById");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiveBusinessTasks_CreatedById",
                table: "ReceiveBusinessTasks",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiveBusinessTasks_ReceiveBusinessAssignToId",
                table: "ReceiveBusinessTasks",
                column: "ReceiveBusinessAssignToId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiveBusinessTasks_ReceiveBusinessId",
                table: "ReceiveBusinessTasks",
                column: "ReceiveBusinessId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiveBusinessTasks_TaskParentId",
                table: "ReceiveBusinessTasks",
                column: "TaskParentId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiveBusinessTasks_UpdatedById",
                table: "ReceiveBusinessTasks",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_SupervisionConsultants_CreatedById",
                table: "SupervisionConsultants",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_SupervisionConsultants_UpdatedById",
                table: "SupervisionConsultants",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_AgentId",
                table: "Ticket",
                column: "AgentId");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_CreatedById",
                table: "Ticket",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_OnBehalfId",
                table: "Ticket",
                column: "OnBehalfId");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_ParentTicketThreadId",
                table: "Ticket",
                column: "ParentTicketThreadId");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_TicketTypeId",
                table: "Ticket",
                column: "TicketTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_UpdatedById",
                table: "Ticket",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_TicketType_CreatedById",
                table: "TicketType",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_TicketType_UpdatedById",
                table: "TicketType",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Todo_CreatedById",
                table: "Todo",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Todo_OnBehalfId",
                table: "Todo",
                column: "OnBehalfId");

            migrationBuilder.CreateIndex(
                name: "IX_Todo_TodoTypeId",
                table: "Todo",
                column: "TodoTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Todo_UpdatedById",
                table: "Todo",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_TodoType_CreatedById",
                table: "TodoType",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_TodoType_UpdatedById",
                table: "TodoType",
                column: "UpdatedById");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Attachments");

            migrationBuilder.DropTable(
                name: "BenefitTemplateLine");

            migrationBuilder.DropTable(
                name: "ChatMessages");

            migrationBuilder.DropTable(
                name: "Choices");

            migrationBuilder.DropTable(
                name: "CommentImage");

            migrationBuilder.DropTable(
                name: "ContractorImages");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "FriendMappings");

            migrationBuilder.DropTable(
                name: "Log");

            migrationBuilder.DropTable(
                name: "OnlineUsers");

            migrationBuilder.DropTable(
                name: "ReceiveBusinessCommentImages");

            migrationBuilder.DropTable(
                name: "ReceiveBusinessScheduals");

            migrationBuilder.DropTable(
                name: "ReceiveBusinessTaskLogImages");

            migrationBuilder.DropTable(
                name: "Responses");

            migrationBuilder.DropTable(
                name: "Ticket");

            migrationBuilder.DropTable(
                name: "Todo");

            migrationBuilder.DropTable(
                name: "UserImages");

            migrationBuilder.DropTable(
                name: "UserNotifications");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AllowanceType");

            migrationBuilder.DropTable(
                name: "DeductionType");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "ProjectComment");

            migrationBuilder.DropTable(
                name: "ReceiveBusinessComment");

            migrationBuilder.DropTable(
                name: "ReceiveBusinessTaskLog");

            migrationBuilder.DropTable(
                name: "TicketType");

            migrationBuilder.DropTable(
                name: "Employee");

            migrationBuilder.DropTable(
                name: "TodoType");

            migrationBuilder.DropTable(
                name: "Surveys");

            migrationBuilder.DropTable(
                name: "ReceiveBusinessTasks");

            migrationBuilder.DropTable(
                name: "BenefitTemplate");

            migrationBuilder.DropTable(
                name: "Department");

            migrationBuilder.DropTable(
                name: "Designation");

            migrationBuilder.DropTable(
                name: "ReceiveBusiness");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "ReceiveBusinessSchedualTemplets");

            migrationBuilder.DropTable(
                name: "Contractors");

            migrationBuilder.DropTable(
                name: "ProjectManagementConsultants");

            migrationBuilder.DropTable(
                name: "ProjectPrograms");

            migrationBuilder.DropTable(
                name: "SupervisionConsultants");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
