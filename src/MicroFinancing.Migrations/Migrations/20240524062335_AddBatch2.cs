using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MicroFinancing.DataMigrations.Migrations
{
    public partial class AddBatch2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Admin",
                table: "Batch");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Term",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "TermEnum",
                table: "Term",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "StartAt",
                table: "Batch",
                type: "datetimeoffset",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Term");

            migrationBuilder.DropColumn(
                name: "TermEnum",
                table: "Term");

            migrationBuilder.DropColumn(
                name: "StartAt",
                table: "Batch");

            migrationBuilder.AddColumn<string>(
                name: "Admin",
                table: "Batch",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
