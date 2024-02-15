using Microsoft.EntityFrameworkCore.Migrations;

namespace Fas.Migrations
{
    public partial class SimpleUpdateInProjectAndReceiveBusiness : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OtherSpecialization",
                table: "ReceiveBusiness",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Sector",
                table: "Projects",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "Region",
                table: "Projects",
                nullable: false,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OtherSpecialization",
                table: "ReceiveBusiness");

            migrationBuilder.AlterColumn<int>(
                name: "Sector",
                table: "Projects",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<int>(
                name: "Region",
                table: "Projects",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
