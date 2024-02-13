using Microsoft.EntityFrameworkCore.Migrations;

namespace Fas.Migrations
{
    public partial class ReceiveBusiness_Update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TypeOfAccreditationRequest",
                table: "ReceiveBusiness",
                newName: "WorkId");

            migrationBuilder.AlterColumn<string>(
                name: "Specialization",
                table: "ReceiveBusiness",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<string>(
                name: "ContractorSignature",
                table: "ReceiveBusiness",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContractorSignature",
                table: "ReceiveBusiness");

            migrationBuilder.RenameColumn(
                name: "WorkId",
                table: "ReceiveBusiness",
                newName: "TypeOfAccreditationRequest");

            migrationBuilder.AlterColumn<int>(
                name: "Specialization",
                table: "ReceiveBusiness",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
