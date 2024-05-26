using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MicroFinancing.DataMigrations.Migrations
{
    public partial class Addbatch1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Admin",
                table: "Batch",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AdministratingUserId",
                table: "Batch",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Batch_AdministratingUserId",
                table: "Batch",
                column: "AdministratingUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Batch_AspNetUsers_AdministratingUserId",
                table: "Batch",
                column: "AdministratingUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Batch_AspNetUsers_AdministratingUserId",
                table: "Batch");

            migrationBuilder.DropIndex(
                name: "IX_Batch_AdministratingUserId",
                table: "Batch");

            migrationBuilder.DropColumn(
                name: "Admin",
                table: "Batch");

            migrationBuilder.DropColumn(
                name: "AdministratingUserId",
                table: "Batch");
        }
    }
}
