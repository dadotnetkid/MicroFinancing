using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MicroFinancing.DataMigrations.Migrations
{
    public partial class modifypaymenttableaddpaymentamount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "PaymentAmount",
                table: "Payments",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "B461BCC5-BEDD-41DA-B137-5CCA0E50E2A6",
                column: "ConcurrencyStamp",
                value: "9e878c0f-c9d7-49e4-a15c-7fde869d2579");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentAmount",
                table: "Payments");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "B461BCC5-BEDD-41DA-B137-5CCA0E50E2A6",
                column: "ConcurrencyStamp",
                value: "d1ba7700-16ff-499c-a61a-c862ee6b1845");
        }
    }
}
