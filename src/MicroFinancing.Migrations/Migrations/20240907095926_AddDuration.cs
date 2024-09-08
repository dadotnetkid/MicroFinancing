using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MicroFinancing.DataMigrations.Migrations
{
    public partial class AddDuration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Duration",
                table: "Lendings",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Duration",
                table: "Lendings");
        }
    }
}
