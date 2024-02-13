using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Fas.Migrations
{
    public partial class Project_Update_Version : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_ProjectManagementConsultants_ProjectManagementConsultantId",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_ProjectPrograms_ProjectProgramId",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_SupervisionConsultants_SupervisionConsultantId",
                table: "Projects");

            migrationBuilder.DropTable(
                name: "ProjectManagementConsultants");

            migrationBuilder.DropTable(
                name: "ProjectPrograms");

            migrationBuilder.DropTable(
                name: "SupervisionConsultants");

            migrationBuilder.DropIndex(
                name: "IX_Projects_ProjectProgramId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "ProjectProgramId",
                table: "Projects");

            migrationBuilder.RenameColumn(
                name: "TotalAmountSpent",
                table: "Projects",
                newName: "ContractualBudget");

            migrationBuilder.AddColumn<int>(
                name: "Region",
                table: "Projects",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Sector",
                table: "Projects",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Employee_ProjectManagementConsultantId",
                table: "Projects",
                column: "ProjectManagementConsultantId",
                principalTable: "Employee",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Employee_SupervisionConsultantId",
                table: "Projects",
                column: "SupervisionConsultantId",
                principalTable: "Employee",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Employee_ProjectManagementConsultantId",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Employee_SupervisionConsultantId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "Region",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "Sector",
                table: "Projects");

            migrationBuilder.RenameColumn(
                name: "ContractualBudget",
                table: "Projects",
                newName: "TotalAmountSpent");

            migrationBuilder.AddColumn<string>(
                name: "ProjectProgramId",
                table: "Projects",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "ProjectManagementConsultants",
                columns: table => new
                {
                    ProjectManagementConsultantId = table.Column<string>(nullable: false),
                    CreatedAtUtc = table.Column<DateTime>(nullable: false),
                    CreatedById = table.Column<string>(nullable: true),
                    ProjectManagementConsultantName = table.Column<string>(maxLength: 50, nullable: false),
                    UpdatedAtUtc = table.Column<DateTime>(nullable: false),
                    UpdatedById = table.Column<string>(nullable: true)
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
                    CreatedAtUtc = table.Column<DateTime>(nullable: false),
                    CreatedById = table.Column<string>(nullable: true),
                    ProjectProgramName = table.Column<string>(maxLength: 50, nullable: false),
                    UpdatedAtUtc = table.Column<DateTime>(nullable: false),
                    UpdatedById = table.Column<string>(nullable: true)
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
                name: "SupervisionConsultants",
                columns: table => new
                {
                    SupervisionConsultantId = table.Column<string>(nullable: false),
                    CreatedAtUtc = table.Column<DateTime>(nullable: false),
                    CreatedById = table.Column<string>(nullable: true),
                    SupervisionConsultantName = table.Column<string>(maxLength: 50, nullable: false),
                    UpdatedAtUtc = table.Column<DateTime>(nullable: false),
                    UpdatedById = table.Column<string>(nullable: true)
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

            migrationBuilder.CreateIndex(
                name: "IX_Projects_ProjectProgramId",
                table: "Projects",
                column: "ProjectProgramId");

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
                name: "IX_SupervisionConsultants_CreatedById",
                table: "SupervisionConsultants",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_SupervisionConsultants_UpdatedById",
                table: "SupervisionConsultants",
                column: "UpdatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_ProjectManagementConsultants_ProjectManagementConsultantId",
                table: "Projects",
                column: "ProjectManagementConsultantId",
                principalTable: "ProjectManagementConsultants",
                principalColumn: "ProjectManagementConsultantId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_ProjectPrograms_ProjectProgramId",
                table: "Projects",
                column: "ProjectProgramId",
                principalTable: "ProjectPrograms",
                principalColumn: "ProjectProgramId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_SupervisionConsultants_SupervisionConsultantId",
                table: "Projects",
                column: "SupervisionConsultantId",
                principalTable: "SupervisionConsultants",
                principalColumn: "SupervisionConsultantId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
