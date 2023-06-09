using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MicroFinancing.DataMigrations.Migrations
{
    public partial class modifylendingtablefields1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lendings_AspNetUsers_CollectorUserId",
                table: "Lendings");

            migrationBuilder.DropIndex(
                name: "IX_Lendings_CollectorUserId",
                table: "Lendings");

            migrationBuilder.DropColumn(
                name: "CollectorUserId",
                table: "Lendings");

            migrationBuilder.AlterColumn<string>(
                name: "Collector",
                table: "Lendings",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "B461BCC5-BEDD-41DA-B137-5CCA0E50E2A6",
                column: "ConcurrencyStamp",
                value: "ba58e2ec-870e-474e-9879-480a40681fa1");

            migrationBuilder.CreateIndex(
                name: "IX_Lendings_Collector",
                table: "Lendings",
                column: "Collector");

            migrationBuilder.AddForeignKey(
                name: "FK_Lendings_AspNetUsers_Collector",
                table: "Lendings",
                column: "Collector",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lendings_AspNetUsers_Collector",
                table: "Lendings");

            migrationBuilder.DropIndex(
                name: "IX_Lendings_Collector",
                table: "Lendings");

            migrationBuilder.AlterColumn<string>(
                name: "Collector",
                table: "Lendings",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "CollectorUserId",
                table: "Lendings",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "B461BCC5-BEDD-41DA-B137-5CCA0E50E2A6",
                column: "ConcurrencyStamp",
                value: "21776025-84a5-45a9-ac67-f6c5c0033afe");

            migrationBuilder.CreateIndex(
                name: "IX_Lendings_CollectorUserId",
                table: "Lendings",
                column: "CollectorUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Lendings_AspNetUsers_CollectorUserId",
                table: "Lendings",
                column: "CollectorUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
