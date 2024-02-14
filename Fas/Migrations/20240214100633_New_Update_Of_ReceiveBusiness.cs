using Microsoft.EntityFrameworkCore.Migrations;

namespace Fas.Migrations
{
    public partial class New_Update_Of_ReceiveBusiness : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContractorSignature",
                table: "ReceiveBusiness");

            migrationBuilder.RenameColumn(
                name: "WorkId",
                table: "ReceiveBusiness",
                newName: "WorkToBeExaminedId");

            migrationBuilder.RenameColumn(
                name: "Specialization",
                table: "ReceiveBusiness",
                newName: "Signature");

            migrationBuilder.AddColumn<bool>(
                name: "IsSigned",
                table: "ReceiveBusinessTasks",
                nullable: true);

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

            migrationBuilder.AddColumn<bool>(
                name: "IsAgricultural",
                table: "ReceiveBusiness",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsArchitectural",
                table: "ReceiveBusiness",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsCivil",
                table: "ReceiveBusiness",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsElectricity",
                table: "ReceiveBusiness",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsMechanics",
                table: "ReceiveBusiness",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsOthers",
                table: "ReceiveBusiness",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "RequiredExaminationDateId",
                table: "ReceiveBusiness",
                nullable: false,
                defaultValue: 0);

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
                principalColumn: "WorkId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReceiveBusiness_Buildings_BuildingId",
                table: "ReceiveBusiness",
                column: "BuildingId",
                principalTable: "Buildings",
                principalColumn: "WorkId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReceiveBusiness_Floors_FloorId",
                table: "ReceiveBusiness",
                column: "FloorId",
                principalTable: "Floors",
                principalColumn: "WorkId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReceiveBusiness_RequiredExaminationDate_RequiredExaminationDateId",
                table: "ReceiveBusiness",
                column: "RequiredExaminationDateId",
                principalTable: "RequiredExaminationDate",
                principalColumn: "WorkId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReceiveBusiness_WorkToBeExamined_WorkToBeExaminedId",
                table: "ReceiveBusiness",
                column: "WorkToBeExaminedId",
                principalTable: "WorkToBeExamined",
                principalColumn: "WorkId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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
                name: "IsSigned",
                table: "ReceiveBusinessTasks");

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
                name: "IsAgricultural",
                table: "ReceiveBusiness");

            migrationBuilder.DropColumn(
                name: "IsArchitectural",
                table: "ReceiveBusiness");

            migrationBuilder.DropColumn(
                name: "IsCivil",
                table: "ReceiveBusiness");

            migrationBuilder.DropColumn(
                name: "IsElectricity",
                table: "ReceiveBusiness");

            migrationBuilder.DropColumn(
                name: "IsMechanics",
                table: "ReceiveBusiness");

            migrationBuilder.DropColumn(
                name: "IsOthers",
                table: "ReceiveBusiness");

            migrationBuilder.DropColumn(
                name: "RequiredExaminationDateId",
                table: "ReceiveBusiness");

            migrationBuilder.RenameColumn(
                name: "WorkToBeExaminedId",
                table: "ReceiveBusiness",
                newName: "WorkId");

            migrationBuilder.RenameColumn(
                name: "Signature",
                table: "ReceiveBusiness",
                newName: "Specialization");

            migrationBuilder.AddColumn<string>(
                name: "ContractorSignature",
                table: "ReceiveBusiness",
                nullable: true);
        }
    }
}
