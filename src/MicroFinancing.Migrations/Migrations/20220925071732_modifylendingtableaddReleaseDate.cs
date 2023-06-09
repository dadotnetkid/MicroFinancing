using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MicroFinancing.DataMigrations.Migrations
{
    public partial class modifylendingtableaddReleaseDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReleaseDate",
                table: "Lendings");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "B461BCC5-BEDD-41DA-B137-5CCA0E50E2A6",
                column: "ConcurrencyStamp",
                value: "a871fa13-d332-4ae3-b02d-87efa06cc397");
        }
    }
}
