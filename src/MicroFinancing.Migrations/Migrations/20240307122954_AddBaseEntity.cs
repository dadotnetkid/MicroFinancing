using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MicroFinancing.DataMigrations.Migrations
{
    public partial class AddBaseEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Payments",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<string>(
                name: "CreatorUserId",
                table: "Payments",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DeleterUserId",
                table: "Payments",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeletionAt",
                table: "Payments",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Payments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LastModifierUserId",
                table: "Payments",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "UpdateAt",
                table: "Payments",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Lendings",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<string>(
                name: "CreatorUserId",
                table: "Lendings",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DeleterUserId",
                table: "Lendings",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeletionAt",
                table: "Lendings",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifierUserId",
                table: "Lendings",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "UpdateAt",
                table: "Lendings",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "Items",
                type: "datetimeoffset",
                nullable: false,
                defaultValueSql: "GETUTCDATE()");

            migrationBuilder.AddColumn<string>(
                name: "CreatorUserId",
                table: "Items",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DeleterUserId",
                table: "Items",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeletionAt",
                table: "Items",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Items",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LastModifierUserId",
                table: "Items",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "UpdateAt",
                table: "Items",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "Customers",
                type: "datetimeoffset",
                nullable: false,
                defaultValueSql: "GETUTCDATE()");

            migrationBuilder.AddColumn<string>(
                name: "CreatorUserId",
                table: "Customers",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DeleterUserId",
                table: "Customers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeletionAt",
                table: "Customers",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifierUserId",
                table: "Customers",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "UpdateAt",
                table: "Customers",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "B461BCC5-BEDD-41DA-B137-5CCA0E50E2A6",
                column: "ConcurrencyStamp",
                value: "fbebd93c-f7f5-407c-822a-2703a38a3892");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_CreatorUserId",
                table: "Payments",
                column: "CreatorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_DeleterUserId",
                table: "Payments",
                column: "DeleterUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_IsDeleted_LendingId_CustomerId",
                table: "Payments",
                columns: new[] { "IsDeleted", "LendingId", "CustomerId" });

            migrationBuilder.CreateIndex(
                name: "IX_Payments_LastModifierUserId",
                table: "Payments",
                column: "LastModifierUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Lendings_CreatorUserId",
                table: "Lendings",
                column: "CreatorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Lendings_DeleterUserId",
                table: "Lendings",
                column: "DeleterUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Lendings_IsDeleted_CustomerId_Collector",
                table: "Lendings",
                columns: new[] { "IsDeleted", "CustomerId", "Collector" });

            migrationBuilder.CreateIndex(
                name: "IX_Lendings_LastModifierUserId",
                table: "Lendings",
                column: "LastModifierUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_CreatorUserId",
                table: "Items",
                column: "CreatorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_DeleterUserId",
                table: "Items",
                column: "DeleterUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_IsDeleted",
                table: "Items",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Items_LastModifierUserId",
                table: "Items",
                column: "LastModifierUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_CreatorUserId",
                table: "Customers",
                column: "CreatorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_DeleterUserId",
                table: "Customers",
                column: "DeleterUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_IsDeleted",
                table: "Customers",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_LastModifierUserId",
                table: "Customers",
                column: "LastModifierUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_AspNetUsers_CreatorUserId",
                table: "Customers",
                column: "CreatorUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_AspNetUsers_DeleterUserId",
                table: "Customers",
                column: "DeleterUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_AspNetUsers_LastModifierUserId",
                table: "Customers",
                column: "LastModifierUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Items_AspNetUsers_CreatorUserId",
                table: "Items",
                column: "CreatorUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Items_AspNetUsers_DeleterUserId",
                table: "Items",
                column: "DeleterUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Items_AspNetUsers_LastModifierUserId",
                table: "Items",
                column: "LastModifierUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Lendings_AspNetUsers_CreatorUserId",
                table: "Lendings",
                column: "CreatorUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Lendings_AspNetUsers_DeleterUserId",
                table: "Lendings",
                column: "DeleterUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Lendings_AspNetUsers_LastModifierUserId",
                table: "Lendings",
                column: "LastModifierUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_AspNetUsers_CreatorUserId",
                table: "Payments",
                column: "CreatorUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_AspNetUsers_DeleterUserId",
                table: "Payments",
                column: "DeleterUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_AspNetUsers_LastModifierUserId",
                table: "Payments",
                column: "LastModifierUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_AspNetUsers_CreatorUserId",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_AspNetUsers_DeleterUserId",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_AspNetUsers_LastModifierUserId",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_AspNetUsers_CreatorUserId",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_AspNetUsers_DeleterUserId",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_AspNetUsers_LastModifierUserId",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_Lendings_AspNetUsers_CreatorUserId",
                table: "Lendings");

            migrationBuilder.DropForeignKey(
                name: "FK_Lendings_AspNetUsers_DeleterUserId",
                table: "Lendings");

            migrationBuilder.DropForeignKey(
                name: "FK_Lendings_AspNetUsers_LastModifierUserId",
                table: "Lendings");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_AspNetUsers_CreatorUserId",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_AspNetUsers_DeleterUserId",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_AspNetUsers_LastModifierUserId",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Payments_CreatorUserId",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Payments_DeleterUserId",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Payments_IsDeleted_LendingId_CustomerId",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Payments_LastModifierUserId",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Lendings_CreatorUserId",
                table: "Lendings");

            migrationBuilder.DropIndex(
                name: "IX_Lendings_DeleterUserId",
                table: "Lendings");

            migrationBuilder.DropIndex(
                name: "IX_Lendings_IsDeleted_CustomerId_Collector",
                table: "Lendings");

            migrationBuilder.DropIndex(
                name: "IX_Lendings_LastModifierUserId",
                table: "Lendings");

            migrationBuilder.DropIndex(
                name: "IX_Items_CreatorUserId",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_DeleterUserId",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_IsDeleted",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_LastModifierUserId",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Customers_CreatorUserId",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Customers_DeleterUserId",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Customers_IsDeleted",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Customers_LastModifierUserId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "CreatorUserId",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "DeleterUserId",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "DeletionAt",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "LastModifierUserId",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "UpdateAt",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "CreatorUserId",
                table: "Lendings");

            migrationBuilder.DropColumn(
                name: "DeleterUserId",
                table: "Lendings");

            migrationBuilder.DropColumn(
                name: "DeletionAt",
                table: "Lendings");

            migrationBuilder.DropColumn(
                name: "LastModifierUserId",
                table: "Lendings");

            migrationBuilder.DropColumn(
                name: "UpdateAt",
                table: "Lendings");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "CreatorUserId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "DeleterUserId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "DeletionAt",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "LastModifierUserId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "UpdateAt",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "CreatorUserId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "DeleterUserId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "DeletionAt",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "LastModifierUserId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "UpdateAt",
                table: "Customers");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Payments",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETUTCDATE()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Lendings",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETUTCDATE()");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "B461BCC5-BEDD-41DA-B137-5CCA0E50E2A6",
                column: "ConcurrencyStamp",
                value: "38face15-bebc-44c9-8f88-9e312bd23b43");
        }
    }
}
