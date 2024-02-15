using Microsoft.EntityFrameworkCore.Migrations;

namespace Fas.Migrations
{
    public partial class ReceiveBusinessForeinkeysUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "WorkId",
                table: "WorkToBeExamined",
                newName: "WorkToBeExaminedId");

            migrationBuilder.RenameColumn(
                name: "WorkId",
                table: "RequiredExaminationDate",
                newName: "RequiredExaminationDateId");

            migrationBuilder.RenameColumn(
                name: "WorkId",
                table: "Floors",
                newName: "FloorId");

            migrationBuilder.RenameColumn(
                name: "WorkId",
                table: "Buildings",
                newName: "BuildingId");

            migrationBuilder.RenameColumn(
                name: "WorkId",
                table: "ApprovedPlates",
                newName: "ApprovedPlatesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "WorkToBeExaminedId",
                table: "WorkToBeExamined",
                newName: "WorkId");

            migrationBuilder.RenameColumn(
                name: "RequiredExaminationDateId",
                table: "RequiredExaminationDate",
                newName: "WorkId");

            migrationBuilder.RenameColumn(
                name: "FloorId",
                table: "Floors",
                newName: "WorkId");

            migrationBuilder.RenameColumn(
                name: "BuildingId",
                table: "Buildings",
                newName: "WorkId");

            migrationBuilder.RenameColumn(
                name: "ApprovedPlatesId",
                table: "ApprovedPlates",
                newName: "WorkId");
        }
    }
}
