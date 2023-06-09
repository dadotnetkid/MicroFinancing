using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MicroFinancing.DataMigrations.Migrations
{
    public partial class AddColumnsToPaymentAndLending : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "LendingId",
                table: "Payments",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Lendings",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsPaid",
                table: "Lendings",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "B461BCC5-BEDD-41DA-B137-5CCA0E50E2A6",
                column: "ConcurrencyStamp",
                value: "30a32d1e-c3f6-4fdd-86ff-93c865e1283a");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_LendingId",
                table: "Payments",
                column: "LendingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Lendings_LendingId",
                table: "Payments",
                column: "LendingId",
                principalTable: "Lendings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Lendings_LendingId",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Payments_LendingId",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "LendingId",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Lendings");

            migrationBuilder.DropColumn(
                name: "IsPaid",
                table: "Lendings");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "B461BCC5-BEDD-41DA-B137-5CCA0E50E2A6",
                column: "ConcurrencyStamp",
                value: "7ae4a7a9-4d40-43a6-b326-2e03d5e22b21");
        }
    }
}
