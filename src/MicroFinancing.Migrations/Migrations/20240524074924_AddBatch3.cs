using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MicroFinancing.DataMigrations.Migrations
{
    public partial class AddBatch3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Batch_AspNetUsers_CreatorId",
                table: "Batch");

            migrationBuilder.DropForeignKey(
                name: "FK_Batch_AspNetUsers_DeleterUserId",
                table: "Batch");

            migrationBuilder.DropForeignKey(
                name: "FK_Batch_AspNetUsers_LastModifierId",
                table: "Batch");

            migrationBuilder.DropForeignKey(
                name: "FK_BatchInCustomer_AspNetUsers_CreatorId",
                table: "BatchInCustomer");

            migrationBuilder.DropForeignKey(
                name: "FK_BatchInCustomer_AspNetUsers_DeleterUserId",
                table: "BatchInCustomer");

            migrationBuilder.DropForeignKey(
                name: "FK_BatchInCustomer_AspNetUsers_LastModifierId",
                table: "BatchInCustomer");

            migrationBuilder.DropForeignKey(
                name: "FK_Term_AspNetUsers_CreatorId",
                table: "Term");

            migrationBuilder.DropForeignKey(
                name: "FK_Term_AspNetUsers_DeleterUserId",
                table: "Term");

            migrationBuilder.DropForeignKey(
                name: "FK_Term_AspNetUsers_LastModifierId",
                table: "Term");

            migrationBuilder.DropIndex(
                name: "IX_Term_CreatorId",
                table: "Term");

            migrationBuilder.DropIndex(
                name: "IX_Term_LastModifierId",
                table: "Term");

            migrationBuilder.DropIndex(
                name: "IX_BatchInCustomer_CreatorId",
                table: "BatchInCustomer");

            migrationBuilder.DropIndex(
                name: "IX_BatchInCustomer_LastModifierId",
                table: "BatchInCustomer");

            migrationBuilder.DropIndex(
                name: "IX_Batch_CreatorId",
                table: "Batch");

            migrationBuilder.DropIndex(
                name: "IX_Batch_LastModifierId",
                table: "Batch");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "Term");

            migrationBuilder.DropColumn(
                name: "LastModifierId",
                table: "Term");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "BatchInCustomer");

            migrationBuilder.DropColumn(
                name: "LastModifierId",
                table: "BatchInCustomer");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "Batch");

            migrationBuilder.DropColumn(
                name: "LastModifierId",
                table: "Batch");

            migrationBuilder.AlterColumn<string>(
                name: "LastModifierUserId",
                table: "Term",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatorUserId",
                table: "Term",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "LastModifierUserId",
                table: "BatchInCustomer",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatorUserId",
                table: "BatchInCustomer",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "LastModifierUserId",
                table: "Batch",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatorUserId",
                table: "Batch",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Term_CreatorUserId",
                table: "Term",
                column: "CreatorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Term_LastModifierUserId",
                table: "Term",
                column: "LastModifierUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BatchInCustomer_CreatorUserId",
                table: "BatchInCustomer",
                column: "CreatorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BatchInCustomer_LastModifierUserId",
                table: "BatchInCustomer",
                column: "LastModifierUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Batch_CreatorUserId",
                table: "Batch",
                column: "CreatorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Batch_LastModifierUserId",
                table: "Batch",
                column: "LastModifierUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Batch_AspNetUsers_CreatorUserId",
                table: "Batch",
                column: "CreatorUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Batch_AspNetUsers_DeleterUserId",
                table: "Batch",
                column: "DeleterUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Batch_AspNetUsers_LastModifierUserId",
                table: "Batch",
                column: "LastModifierUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BatchInCustomer_AspNetUsers_CreatorUserId",
                table: "BatchInCustomer",
                column: "CreatorUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BatchInCustomer_AspNetUsers_DeleterUserId",
                table: "BatchInCustomer",
                column: "DeleterUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BatchInCustomer_AspNetUsers_LastModifierUserId",
                table: "BatchInCustomer",
                column: "LastModifierUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Term_AspNetUsers_CreatorUserId",
                table: "Term",
                column: "CreatorUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Term_AspNetUsers_DeleterUserId",
                table: "Term",
                column: "DeleterUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Term_AspNetUsers_LastModifierUserId",
                table: "Term",
                column: "LastModifierUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Batch_AspNetUsers_CreatorUserId",
                table: "Batch");

            migrationBuilder.DropForeignKey(
                name: "FK_Batch_AspNetUsers_DeleterUserId",
                table: "Batch");

            migrationBuilder.DropForeignKey(
                name: "FK_Batch_AspNetUsers_LastModifierUserId",
                table: "Batch");

            migrationBuilder.DropForeignKey(
                name: "FK_BatchInCustomer_AspNetUsers_CreatorUserId",
                table: "BatchInCustomer");

            migrationBuilder.DropForeignKey(
                name: "FK_BatchInCustomer_AspNetUsers_DeleterUserId",
                table: "BatchInCustomer");

            migrationBuilder.DropForeignKey(
                name: "FK_BatchInCustomer_AspNetUsers_LastModifierUserId",
                table: "BatchInCustomer");

            migrationBuilder.DropForeignKey(
                name: "FK_Term_AspNetUsers_CreatorUserId",
                table: "Term");

            migrationBuilder.DropForeignKey(
                name: "FK_Term_AspNetUsers_DeleterUserId",
                table: "Term");

            migrationBuilder.DropForeignKey(
                name: "FK_Term_AspNetUsers_LastModifierUserId",
                table: "Term");

            migrationBuilder.DropIndex(
                name: "IX_Term_CreatorUserId",
                table: "Term");

            migrationBuilder.DropIndex(
                name: "IX_Term_LastModifierUserId",
                table: "Term");

            migrationBuilder.DropIndex(
                name: "IX_BatchInCustomer_CreatorUserId",
                table: "BatchInCustomer");

            migrationBuilder.DropIndex(
                name: "IX_BatchInCustomer_LastModifierUserId",
                table: "BatchInCustomer");

            migrationBuilder.DropIndex(
                name: "IX_Batch_CreatorUserId",
                table: "Batch");

            migrationBuilder.DropIndex(
                name: "IX_Batch_LastModifierUserId",
                table: "Batch");

            migrationBuilder.AlterColumn<string>(
                name: "LastModifierUserId",
                table: "Term",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatorUserId",
                table: "Term",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "CreatorId",
                table: "Term",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastModifierId",
                table: "Term",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "LastModifierUserId",
                table: "BatchInCustomer",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatorUserId",
                table: "BatchInCustomer",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "CreatorId",
                table: "BatchInCustomer",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastModifierId",
                table: "BatchInCustomer",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "LastModifierUserId",
                table: "Batch",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatorUserId",
                table: "Batch",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "CreatorId",
                table: "Batch",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastModifierId",
                table: "Batch",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Term_CreatorId",
                table: "Term",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Term_LastModifierId",
                table: "Term",
                column: "LastModifierId");

            migrationBuilder.CreateIndex(
                name: "IX_BatchInCustomer_CreatorId",
                table: "BatchInCustomer",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_BatchInCustomer_LastModifierId",
                table: "BatchInCustomer",
                column: "LastModifierId");

            migrationBuilder.CreateIndex(
                name: "IX_Batch_CreatorId",
                table: "Batch",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Batch_LastModifierId",
                table: "Batch",
                column: "LastModifierId");

            migrationBuilder.AddForeignKey(
                name: "FK_Batch_AspNetUsers_CreatorId",
                table: "Batch",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Batch_AspNetUsers_DeleterUserId",
                table: "Batch",
                column: "DeleterUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Batch_AspNetUsers_LastModifierId",
                table: "Batch",
                column: "LastModifierId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BatchInCustomer_AspNetUsers_CreatorId",
                table: "BatchInCustomer",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BatchInCustomer_AspNetUsers_DeleterUserId",
                table: "BatchInCustomer",
                column: "DeleterUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BatchInCustomer_AspNetUsers_LastModifierId",
                table: "BatchInCustomer",
                column: "LastModifierId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Term_AspNetUsers_CreatorId",
                table: "Term",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Term_AspNetUsers_DeleterUserId",
                table: "Term",
                column: "DeleterUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Term_AspNetUsers_LastModifierId",
                table: "Term",
                column: "LastModifierId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
