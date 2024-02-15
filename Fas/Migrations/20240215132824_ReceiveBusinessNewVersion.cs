using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Fas.Migrations
{
    public partial class ReceiveBusinessNewVersion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReceiveBusiness_ApprovedPlates_ApprovedPlatesId",
                table: "ReceiveBusiness");

            migrationBuilder.DropForeignKey(
                name: "FK_ReceiveBusiness_Buildings_BuildingId",
                table: "ReceiveBusiness");

            migrationBuilder.DropForeignKey(
                name: "FK_ReceiveBusiness_Floors_FloorId",
                table: "ReceiveBusiness");

            migrationBuilder.DropForeignKey(
                name: "FK_ReceiveBusiness_RequiredExaminationDate_RequiredExaminationDateId",
                table: "ReceiveBusiness");

            migrationBuilder.DropForeignKey(
                name: "FK_ReceiveBusiness_WorkToBeExamined_WorkToBeExaminedId",
                table: "ReceiveBusiness");

            migrationBuilder.DropTable(
                name: "ApprovedPlates");

            migrationBuilder.DropTable(
                name: "Buildings");

            migrationBuilder.DropTable(
                name: "Floors");

            migrationBuilder.DropTable(
                name: "RequiredExaminationDate");

            migrationBuilder.DropTable(
                name: "WorkToBeExamined");

            migrationBuilder.DropIndex(
                name: "IX_ReceiveBusiness_ApprovedPlatesId",
                table: "ReceiveBusiness");

            migrationBuilder.DropIndex(
                name: "IX_ReceiveBusiness_BuildingId",
                table: "ReceiveBusiness");

            migrationBuilder.DropIndex(
                name: "IX_ReceiveBusiness_FloorId",
                table: "ReceiveBusiness");

            migrationBuilder.DropIndex(
                name: "IX_ReceiveBusiness_RequiredExaminationDateId",
                table: "ReceiveBusiness");

            migrationBuilder.DropIndex(
                name: "IX_ReceiveBusiness_WorkToBeExaminedId",
                table: "ReceiveBusiness");

            migrationBuilder.DropColumn(
                name: "ApprovedPlatesId",
                table: "ReceiveBusiness");

            migrationBuilder.DropColumn(
                name: "BuildingId",
                table: "ReceiveBusiness");

            migrationBuilder.DropColumn(
                name: "FloorId",
                table: "ReceiveBusiness");

            migrationBuilder.DropColumn(
                name: "RequiredExaminationDateId",
                table: "ReceiveBusiness");

            migrationBuilder.DropColumn(
                name: "WorkToBeExaminedId",
                table: "ReceiveBusiness");

            migrationBuilder.RenameColumn(
                name: "Signature",
                table: "ReceiveBusiness",
                newName: "WorkToBeExaminedStatement");

            migrationBuilder.AddColumn<string>(
                name: "ApprovedPlatesComments",
                table: "ReceiveBusiness",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApprovedPlatesStatement",
                table: "ReceiveBusiness",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BuildingComments",
                table: "ReceiveBusiness",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BuildingStatement",
                table: "ReceiveBusiness",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FloorComments",
                table: "ReceiveBusiness",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FloorStatement",
                table: "ReceiveBusiness",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RequiredExaminationDateComments",
                table: "ReceiveBusiness",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RequiredExaminationDateStatement",
                table: "ReceiveBusiness",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WorkToBeExaminedComments",
                table: "ReceiveBusiness",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApprovedPlatesComments",
                table: "ReceiveBusiness");

            migrationBuilder.DropColumn(
                name: "ApprovedPlatesStatement",
                table: "ReceiveBusiness");

            migrationBuilder.DropColumn(
                name: "BuildingComments",
                table: "ReceiveBusiness");

            migrationBuilder.DropColumn(
                name: "BuildingStatement",
                table: "ReceiveBusiness");

            migrationBuilder.DropColumn(
                name: "FloorComments",
                table: "ReceiveBusiness");

            migrationBuilder.DropColumn(
                name: "FloorStatement",
                table: "ReceiveBusiness");

            migrationBuilder.DropColumn(
                name: "RequiredExaminationDateComments",
                table: "ReceiveBusiness");

            migrationBuilder.DropColumn(
                name: "RequiredExaminationDateStatement",
                table: "ReceiveBusiness");

            migrationBuilder.DropColumn(
                name: "WorkToBeExaminedComments",
                table: "ReceiveBusiness");

            migrationBuilder.RenameColumn(
                name: "WorkToBeExaminedStatement",
                table: "ReceiveBusiness",
                newName: "Signature");

            migrationBuilder.AddColumn<int>(
                name: "ApprovedPlatesId",
                table: "ReceiveBusiness",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BuildingId",
                table: "ReceiveBusiness",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FloorId",
                table: "ReceiveBusiness",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RequiredExaminationDateId",
                table: "ReceiveBusiness",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "WorkToBeExaminedId",
                table: "ReceiveBusiness",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ApprovedPlates",
                columns: table => new
                {
                    ApprovedPlatesId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Comments = table.Column<string>(nullable: true),
                    Statement = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApprovedPlates", x => x.ApprovedPlatesId);
                });

            migrationBuilder.CreateTable(
                name: "Buildings",
                columns: table => new
                {
                    BuildingId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Comments = table.Column<string>(nullable: true),
                    Statement = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Buildings", x => x.BuildingId);
                });

            migrationBuilder.CreateTable(
                name: "Floors",
                columns: table => new
                {
                    FloorId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Comments = table.Column<string>(nullable: true),
                    Statement = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Floors", x => x.FloorId);
                });

            migrationBuilder.CreateTable(
                name: "RequiredExaminationDate",
                columns: table => new
                {
                    RequiredExaminationDateId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Comments = table.Column<string>(nullable: true),
                    Statement = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequiredExaminationDate", x => x.RequiredExaminationDateId);
                });

            migrationBuilder.CreateTable(
                name: "WorkToBeExamined",
                columns: table => new
                {
                    WorkToBeExaminedId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Comments = table.Column<string>(nullable: true),
                    Statement = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkToBeExamined", x => x.WorkToBeExaminedId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReceiveBusiness_ApprovedPlatesId",
                table: "ReceiveBusiness",
                column: "ApprovedPlatesId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiveBusiness_BuildingId",
                table: "ReceiveBusiness",
                column: "BuildingId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiveBusiness_FloorId",
                table: "ReceiveBusiness",
                column: "FloorId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiveBusiness_RequiredExaminationDateId",
                table: "ReceiveBusiness",
                column: "RequiredExaminationDateId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiveBusiness_WorkToBeExaminedId",
                table: "ReceiveBusiness",
                column: "WorkToBeExaminedId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReceiveBusiness_ApprovedPlates_ApprovedPlatesId",
                table: "ReceiveBusiness",
                column: "ApprovedPlatesId",
                principalTable: "ApprovedPlates",
                principalColumn: "ApprovedPlatesId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReceiveBusiness_Buildings_BuildingId",
                table: "ReceiveBusiness",
                column: "BuildingId",
                principalTable: "Buildings",
                principalColumn: "BuildingId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReceiveBusiness_Floors_FloorId",
                table: "ReceiveBusiness",
                column: "FloorId",
                principalTable: "Floors",
                principalColumn: "FloorId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReceiveBusiness_RequiredExaminationDate_RequiredExaminationDateId",
                table: "ReceiveBusiness",
                column: "RequiredExaminationDateId",
                principalTable: "RequiredExaminationDate",
                principalColumn: "RequiredExaminationDateId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReceiveBusiness_WorkToBeExamined_WorkToBeExaminedId",
                table: "ReceiveBusiness",
                column: "WorkToBeExaminedId",
                principalTable: "WorkToBeExamined",
                principalColumn: "WorkToBeExaminedId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
