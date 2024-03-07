using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MicroFinancing.DataMigrations.Migrations
{
    public partial class RemoveComputedColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "TotalCredit",
                table: "Lendings",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldComputedColumnSql: "((amount+ItemAmount)*(InterestRate/100)) + amount +ItemAmount");

            migrationBuilder.AlterColumn<decimal>(
                name: "Interest",
                table: "Lendings",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldComputedColumnSql: "(amount+ItemAmount)*(InterestRate/100)");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "B461BCC5-BEDD-41DA-B137-5CCA0E50E2A6",
                column: "ConcurrencyStamp",
                value: "e9c6dde8-a3ef-48df-8146-eb989caba8bf");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "TotalCredit",
                table: "Lendings",
                type: "decimal(18,2)",
                nullable: false,
                computedColumnSql: "((amount+ItemAmount)*(InterestRate/100)) + amount +ItemAmount",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Interest",
                table: "Lendings",
                type: "decimal(18,2)",
                nullable: false,
                computedColumnSql: "(amount+ItemAmount)*(InterestRate/100)",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "B461BCC5-BEDD-41DA-B137-5CCA0E50E2A6",
                column: "ConcurrencyStamp",
                value: "7c90378f-6af0-40db-b404-5110f9179bd8");
        }
    }
}
