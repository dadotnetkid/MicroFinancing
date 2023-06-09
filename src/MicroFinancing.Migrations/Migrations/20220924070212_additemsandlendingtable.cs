using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MicroFinancing.DataMigrations.Migrations
{
    public partial class additemsandlendingtable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_ChildInfo_ChildInfoChildId",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_CustomerAddress_CustomerAddressAddressId",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_EducationalBackground_EducationalBackgroundId",
                table: "Customers");

            migrationBuilder.DropTable(
                name: "ChildInfo");

            migrationBuilder.DropTable(
                name: "CustomerAddress");

            migrationBuilder.DropTable(
                name: "EducationalBackground");

            migrationBuilder.DropIndex(
                name: "IX_Customers_ChildInfoChildId",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Customers_CustomerAddressAddressId",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Customers_EducationalBackgroundId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "AgencyEmloyeeNo",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "BloodType",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "ChildInfoChildId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "Citizenship",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "CivilStatus",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "CustomerAddressAddressId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "EducationalBackgroundId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "FathersName",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "Gsis",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "Height",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "Mobile",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "MothersName",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "NameExtension",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "Pagibig",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "PhilHeath",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "SpouseName",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "Sss",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "Telephone",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "Tin",
                table: "Customers");

            migrationBuilder.RenameColumn(
                name: "Weight",
                table: "Customers",
                newName: "Address");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Customers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SKU = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Barcode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Lendings",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<long>(type: "bigint", nullable: false),
                    Category = table.Column<int>(type: "int", nullable: false),
                    LendingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lendings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lendings_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "B461BCC5-BEDD-41DA-B137-5CCA0E50E2A6",
                column: "ConcurrencyStamp",
                value: "3ccbbe1b-f8ac-4ee7-a4ce-4b70735e5448");

            migrationBuilder.CreateIndex(
                name: "IX_Lendings_CustomerId",
                table: "Lendings",
                column: "CustomerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "Lendings");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Customers");

            migrationBuilder.RenameColumn(
                name: "Address",
                table: "Customers",
                newName: "Weight");

            migrationBuilder.AddColumn<string>(
                name: "AgencyEmloyeeNo",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BloodType",
                table: "Customers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ChildInfoChildId",
                table: "Customers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Citizenship",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CivilStatus",
                table: "Customers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CustomerAddressAddressId",
                table: "Customers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EducationalBackgroundId",
                table: "Customers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FathersName",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Gsis",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Height",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Mobile",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MothersName",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NameExtension",
                table: "Customers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Pagibig",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhilHeath",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SpouseName",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Sss",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Telephone",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Tin",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ChildInfo",
                columns: table => new
                {
                    ChildId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChildsDateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ChildsFirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChildsGender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChildsLastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChildsMiddleName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChildInfo", x => x.ChildId);
                });

            migrationBuilder.CreateTable(
                name: "CustomerAddress",
                columns: table => new
                {
                    AddressId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Barangay = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HouseNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Municipality = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Province = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Zipcode = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerAddress", x => x.AddressId);
                });

            migrationBuilder.CreateTable(
                name: "EducationalBackground",
                columns: table => new
                {
                    EducationalBackgroundId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CollegeHonorReceived = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CollegeNameOfSchool = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CollegePeriodOfAttendanceFrom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CollegePeriodOfAttendanceTo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CollegeYearGraduated = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ElementaryHonorReceived = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ElementaryNameOfSchool = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ElementaryPeriodOfAttendanceFrom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ElementaryPeriodOfAttendanceTo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ElementaryYearGraduated = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GraduateStudiesGraduated = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GraduateStudiesHonorReceived = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GraduateStudiesNameOfSchool = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GraduateStudiesPeriodOfAttendanceFrom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GraduateStudiesPeriodOfAttendanceTo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecondaryHonorReceived = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecondaryNameOfSchool = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecondaryPeriodOfAttendanceFrom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecondaryPeriodOfAttendanceTo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecondaryYearGraduated = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VocationalHonorReceived = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VocationalNameOfSchool = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VocationalPeriodOfAttendanceFrom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VocationalPeriodOfAttendanceTo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VocationalYearGraduated = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducationalBackground", x => x.EducationalBackgroundId);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "B461BCC5-BEDD-41DA-B137-5CCA0E50E2A6",
                column: "ConcurrencyStamp",
                value: "56398b71-a0d8-4dc3-ba9f-c7bbdbf6e0c7");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_ChildInfoChildId",
                table: "Customers",
                column: "ChildInfoChildId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_CustomerAddressAddressId",
                table: "Customers",
                column: "CustomerAddressAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_EducationalBackgroundId",
                table: "Customers",
                column: "EducationalBackgroundId");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_ChildInfo_ChildInfoChildId",
                table: "Customers",
                column: "ChildInfoChildId",
                principalTable: "ChildInfo",
                principalColumn: "ChildId");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_CustomerAddress_CustomerAddressAddressId",
                table: "Customers",
                column: "CustomerAddressAddressId",
                principalTable: "CustomerAddress",
                principalColumn: "AddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_EducationalBackground_EducationalBackgroundId",
                table: "Customers",
                column: "EducationalBackgroundId",
                principalTable: "EducationalBackground",
                principalColumn: "EducationalBackgroundId");
        }
    }
}
