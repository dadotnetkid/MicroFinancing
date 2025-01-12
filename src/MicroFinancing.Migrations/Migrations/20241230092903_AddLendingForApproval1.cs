using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MicroFinancing.DataMigrations.Migrations
{
    public partial class AddLendingForApproval1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LendingForApproval_Customers_CustomersId",
                table: "LendingForApproval");

            migrationBuilder.DropIndex(
                name: "IX_LendingForApproval_CustomersId",
                table: "LendingForApproval");

            migrationBuilder.DropColumn(
                name: "CustomersId",
                table: "LendingForApproval");

            migrationBuilder.CreateIndex(
                name: "IX_LendingForApproval_CustomerId",
                table: "LendingForApproval",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_LendingForApproval_Customers_CustomerId",
                table: "LendingForApproval",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LendingForApproval_Customers_CustomerId",
                table: "LendingForApproval");

            migrationBuilder.DropIndex(
                name: "IX_LendingForApproval_CustomerId",
                table: "LendingForApproval");

            migrationBuilder.AddColumn<long>(
                name: "CustomersId",
                table: "LendingForApproval",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_LendingForApproval_CustomersId",
                table: "LendingForApproval",
                column: "CustomersId");

            migrationBuilder.AddForeignKey(
                name: "FK_LendingForApproval_Customers_CustomersId",
                table: "LendingForApproval",
                column: "CustomersId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
