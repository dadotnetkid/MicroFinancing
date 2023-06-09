using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MicroFinancing.DataMigrations.Migrations
{
    public partial class addcomputedcolumnInterestAndTotalCredit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
      

            migrationBuilder.AlterColumn<decimal>(
                name: "Interest",
                table: "Lendings",
                type: "decimal(18,2)",
                nullable: false,
                computedColumnSql: "(amount+ItemAmount)*(.10)",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");
            migrationBuilder.AlterColumn<decimal>(
                name: "TotalCredit",
                table: "Lendings",
                type: "decimal(18,2)",
                nullable: false,
                computedColumnSql: "((amount+ItemAmount)*(.10)) + amount +ItemAmount",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "B461BCC5-BEDD-41DA-B137-5CCA0E50E2A6",
                column: "ConcurrencyStamp",
                value: "a64c83c3-3ccd-43eb-b656-aa1b7b5ffedb");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "TotalCredit",
                table: "Lendings",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldComputedColumnSql: "interest + amount +ItemAmount");

            migrationBuilder.AlterColumn<decimal>(
                name: "Interest",
                table: "Lendings",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldComputedColumnSql: "(amount+ItemAmount)*(.10)");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "B461BCC5-BEDD-41DA-B137-5CCA0E50E2A6",
                column: "ConcurrencyStamp",
                value: "a5698349-9c63-443e-9773-6d9c8029bd00");
        }
    }
}
