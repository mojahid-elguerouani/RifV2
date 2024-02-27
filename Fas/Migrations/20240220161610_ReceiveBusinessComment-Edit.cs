using Microsoft.EntityFrameworkCore.Migrations;

namespace Fas.Migrations
{
    public partial class ReceiveBusinessCommentEdit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReceiveBusinessComment_ReceiveBusiness_ReceiveBusinessId",
                table: "ReceiveBusinessComment");

            migrationBuilder.DropColumn(
                name: "PurchaseId",
                table: "ReceiveBusinessComment");

            migrationBuilder.AlterColumn<int>(
                name: "ReceiveBusinessId",
                table: "ReceiveBusinessComment",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ReceiveBusinessComment_ReceiveBusiness_ReceiveBusinessId",
                table: "ReceiveBusinessComment",
                column: "ReceiveBusinessId",
                principalTable: "ReceiveBusiness",
                principalColumn: "ReceiveBusinessId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReceiveBusinessComment_ReceiveBusiness_ReceiveBusinessId",
                table: "ReceiveBusinessComment");

            migrationBuilder.AlterColumn<int>(
                name: "ReceiveBusinessId",
                table: "ReceiveBusinessComment",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "PurchaseId",
                table: "ReceiveBusinessComment",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_ReceiveBusinessComment_ReceiveBusiness_ReceiveBusinessId",
                table: "ReceiveBusinessComment",
                column: "ReceiveBusinessId",
                principalTable: "ReceiveBusiness",
                principalColumn: "ReceiveBusinessId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
