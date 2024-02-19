using Microsoft.EntityFrameworkCore.Migrations;

namespace Fas.Migrations
{
    public partial class ReceiveBusinessBoolNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsAgricultural",
                table: "ReceiveBusiness",
                nullable: true,
                defaultValue: false);

            migrationBuilder.AlterColumn<bool>(
                name: "IsArchitectural",
                table: "ReceiveBusiness",
                nullable: true,
                defaultValue: false);

            migrationBuilder.AlterColumn<bool>(
                name: "IsCivil",
                table: "ReceiveBusiness",
                nullable: true,
                defaultValue: false);

            migrationBuilder.AlterColumn<bool>(
                name: "IsElectricity",
                table: "ReceiveBusiness",
                nullable: true,
                defaultValue: false);

            migrationBuilder.AlterColumn<bool>(
                name: "IsMechanics",
                table: "ReceiveBusiness",
                nullable: true,
                defaultValue: false);

            migrationBuilder.AlterColumn<bool>(
                name: "IsOthers",
                table: "ReceiveBusiness",
                nullable: true,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AlterColumn<bool>(
                name: "IsAgricultural",
                table: "ReceiveBusiness",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<bool>(
                name: "IsArchitectural",
                table: "ReceiveBusiness",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<bool>(
                name: "IsCivil",
                table: "ReceiveBusiness",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<bool>(
                name: "IsElectricity",
                table: "ReceiveBusiness",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<bool>(
                name: "IsMechanics",
                table: "ReceiveBusiness",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<bool>(
                name: "IsOthers",
                table: "ReceiveBusiness",
                nullable: false,
                defaultValue: false);
        }
    }
}
