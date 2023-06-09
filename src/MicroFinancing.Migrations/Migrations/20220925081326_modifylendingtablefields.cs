using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MicroFinancing.DataMigrations.Migrations
{
    public partial class modifylendingtablefields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReleaseDate",
                table: "Lendings");

            migrationBuilder.AddColumn<string>(
                name: "Collector",
                table: "Lendings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CollectorUserId",
                table: "Lendings",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Interest",
                table: "Lendings",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ItemAmount",
                table: "Lendings",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalCredit",
                table: "Lendings",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lendings_AspNetUsers_CollectorUserId",
                table: "Lendings");

            migrationBuilder.DropIndex(
                name: "IX_Lendings_CollectorUserId",
                table: "Lendings");

            migrationBuilder.DropColumn(
                name: "Collector",
                table: "Lendings");

            migrationBuilder.DropColumn(
                name: "CollectorUserId",
                table: "Lendings");

            migrationBuilder.DropColumn(
                name: "Interest",
                table: "Lendings");

            migrationBuilder.DropColumn(
                name: "ItemAmount",
                table: "Lendings");

            migrationBuilder.DropColumn(
                name: "TotalCredit",
                table: "Lendings");

            migrationBuilder.AddColumn<DateTime>(
                name: "ReleaseDate",
                table: "Lendings",
                type: "datetime2",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "B461BCC5-BEDD-41DA-B137-5CCA0E50E2A6",
                column: "ConcurrencyStamp",
                value: "af63dd6d-8193-45c7-a488-f23d112dcd7d");
        }
    }
}
