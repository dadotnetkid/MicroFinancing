using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MicroFinancing.DataMigrations.Migrations
{
    public partial class InterestRateColumnForDynamicInterest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "InterestRate",
                table: "Lendings",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 10m);

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalCredit",
                table: "Lendings",
                type: "decimal(18,2)",
                nullable: false,
                computedColumnSql: "((amount+ItemAmount)*(InterestRate/100)) + amount +ItemAmount",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldComputedColumnSql: "interest + amount +ItemAmount");

            migrationBuilder.AlterColumn<decimal>(
                name: "Interest",
                table: "Lendings",
                type: "decimal(18,2)",
                nullable: false,
                computedColumnSql: "(amount+ItemAmount)*(InterestRate/100)",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldComputedColumnSql: "(amount+ItemAmount)*(.10)");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "B461BCC5-BEDD-41DA-B137-5CCA0E50E2A6",
                column: "ConcurrencyStamp",
                value: "7c90378f-6af0-40db-b404-5110f9179bd8");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InterestRate",
                table: "Lendings");

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalCredit",
                table: "Lendings",
                type: "decimal(18,2)",
                nullable: false,
                computedColumnSql: "interest + amount +ItemAmount",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldComputedColumnSql: "((amount+ItemAmount)*(InterestRate/100)) + amount +ItemAmount");

            migrationBuilder.AlterColumn<decimal>(
                name: "Interest",
                table: "Lendings",
                type: "decimal(18,2)",
                nullable: false,
                computedColumnSql: "(amount+ItemAmount)*(.10)",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldComputedColumnSql: "(amount+ItemAmount)*(InterestRate/100)");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "B461BCC5-BEDD-41DA-B137-5CCA0E50E2A6",
                column: "ConcurrencyStamp",
                value: "a64c83c3-3ccd-43eb-b656-aa1b7b5ffedb");
        }
    }
}
