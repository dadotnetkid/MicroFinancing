using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MicroFinancing.DataMigrations.Migrations
{
    public partial class AddLendingForApproval : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Lendings_IsDeleted_CustomerId_Collector",
                table: "Lendings");

            migrationBuilder.CreateTable(
                name: "LendingForApproval",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<long>(type: "bigint", nullable: false),
                    Category = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LendingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Interest = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalCredit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ItemAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Collector = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsPaid = table.Column<bool>(type: "bit", nullable: false),
                    InterestRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CustomersId = table.Column<long>(type: "bigint", nullable: false),
                    NumberOfDays = table.Column<int>(type: "int", nullable: false),
                    PaymentDays = table.Column<int>(type: "int", nullable: false),
                    DailyDueAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    DsTax = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false),
                    PreviousBalance = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    UpdateAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeletionAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeleterUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    LastModifierUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatorUserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LendingForApproval", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LendingForApproval_AspNetUsers_Collector",
                        column: x => x.Collector,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LendingForApproval_AspNetUsers_CreatorUserId",
                        column: x => x.CreatorUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LendingForApproval_AspNetUsers_DeleterUserId",
                        column: x => x.DeleterUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LendingForApproval_AspNetUsers_LastModifierUserId",
                        column: x => x.LastModifierUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LendingForApproval_Customers_CustomersId",
                        column: x => x.CustomersId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Lendings_IsDeleted_CustomerId_Collector_IsActive",
                table: "Lendings",
                columns: new[] { "IsDeleted", "CustomerId", "Collector", "IsActive" });

            migrationBuilder.CreateIndex(
                name: "IX_LendingForApproval_Collector",
                table: "LendingForApproval",
                column: "Collector");

            migrationBuilder.CreateIndex(
                name: "IX_LendingForApproval_CreatorUserId",
                table: "LendingForApproval",
                column: "CreatorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_LendingForApproval_CustomersId",
                table: "LendingForApproval",
                column: "CustomersId");

            migrationBuilder.CreateIndex(
                name: "IX_LendingForApproval_DeleterUserId",
                table: "LendingForApproval",
                column: "DeleterUserId");

            migrationBuilder.CreateIndex(
                name: "IX_LendingForApproval_IsDeleted_CustomerId_Collector_IsActive",
                table: "LendingForApproval",
                columns: new[] { "IsDeleted", "CustomerId", "Collector", "IsActive" });

            migrationBuilder.CreateIndex(
                name: "IX_LendingForApproval_LastModifierUserId",
                table: "LendingForApproval",
                column: "LastModifierUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LendingForApproval");

            migrationBuilder.DropIndex(
                name: "IX_Lendings_IsDeleted_CustomerId_Collector_IsActive",
                table: "Lendings");

            migrationBuilder.CreateIndex(
                name: "IX_Lendings_IsDeleted_CustomerId_Collector",
                table: "Lendings",
                columns: new[] { "IsDeleted", "CustomerId", "Collector" });
        }
    }
}
