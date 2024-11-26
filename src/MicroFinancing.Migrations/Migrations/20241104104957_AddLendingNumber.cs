using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MicroFinancing.DataMigrations.Migrations
{
    public partial class AddLendingNumber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "Items");

            migrationBuilder.AddColumn<string>(
                name: "LendingNumber",
                table: "Lendings",
                type: "nvarchar(450)",
                nullable: false,
                computedColumnSql: "concat('LND-',Id)");

            migrationBuilder.CreateIndex(
                name: "IX_Lendings_LendingNumber",
                table: "Lendings",
                column: "LendingNumber",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Lendings_LendingNumber",
                table: "Lendings");

            migrationBuilder.DropColumn(
                name: "LendingNumber",
                table: "Lendings");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Items",
                type: "decimal(18,2)",
                nullable: true);
        }
    }
}
