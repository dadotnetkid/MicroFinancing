using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MicroFinancing.DataMigrations.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ChildInfo",
                columns: table => new
                {
                    ChildId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChildsFirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChildsMiddleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChildsLastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChildsDateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ChildsGender = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    HouseNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Barangay = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Municipality = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Province = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    ElementaryNameOfSchool = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecondaryNameOfSchool = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VocationalNameOfSchool = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CollegeNameOfSchool = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GraduateStudiesNameOfSchool = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ElementaryPeriodOfAttendanceFrom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ElementaryPeriodOfAttendanceTo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecondaryPeriodOfAttendanceFrom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecondaryPeriodOfAttendanceTo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VocationalPeriodOfAttendanceFrom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VocationalPeriodOfAttendanceTo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CollegePeriodOfAttendanceFrom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CollegePeriodOfAttendanceTo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GraduateStudiesPeriodOfAttendanceFrom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GraduateStudiesPeriodOfAttendanceTo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ElementaryYearGraduated = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecondaryYearGraduated = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VocationalYearGraduated = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CollegeYearGraduated = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GraduateStudiesGraduated = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ElementaryHonorReceived = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecondaryHonorReceived = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VocationalHonorReceived = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CollegeHonorReceived = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GraduateStudiesHonorReceived = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducationalBackground", x => x.EducationalBackgroundId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MiddleName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PlaceOfBirth = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    CivilStatus = table.Column<int>(type: "int", nullable: false),
                    Height = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Weight = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameExtension = table.Column<int>(type: "int", nullable: false),
                    BloodType = table.Column<int>(type: "int", nullable: false),
                    Gsis = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pagibig = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhilHeath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sss = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tin = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AgencyEmloyeeNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Citizenship = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Telephone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mobile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SpouseName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FathersName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MothersName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerAddressAddressId = table.Column<int>(type: "int", nullable: true),
                    ChildInfoChildId = table.Column<int>(type: "int", nullable: true),
                    EducationalBackgroundId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Customers_ChildInfo_ChildInfoChildId",
                        column: x => x.ChildInfoChildId,
                        principalTable: "ChildInfo",
                        principalColumn: "ChildId");
                    table.ForeignKey(
                        name: "FK_Customers_CustomerAddress_CustomerAddressAddressId",
                        column: x => x.CustomerAddressAddressId,
                        principalTable: "CustomerAddress",
                        principalColumn: "AddressId");
                    table.ForeignKey(
                        name: "FK_Customers_EducationalBackground_EducationalBackgroundId",
                        column: x => x.EducationalBackgroundId,
                        principalTable: "EducationalBackground",
                        principalColumn: "EducationalBackgroundId");
                });
            var insertSeed =
                "INSERT INTO [MicroFinancing-4297407B-3C91-4356-9FCA-5E544B3F39AC].[dbo].[AspNetUsers] ([Id], [FirstName], [LastName], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'c0ea7b0e-e5db-4f17-9c35-87dd2ac20895', N'Test', N'User', N'test@gmail.com', N'TEST@GMAIL.COM', N'test@gmail.com', N'TEST@GMAIL.COM', '1', N'AQAAAAEAACcQAAAAEP1IWpfNtUnsd6FEO5UFS0XwQ6GFkRu9RWEqPLRWA2GCsHUkptpjKChsIACMStwsJg==', N'BWSAH2YX4PNHBUE7EDHP4AXP53NVU2RV', N'0cf4a7d2-902d-4b93-bd5f-82b529459e04', NULL, '0', '0', NULL, '0', '0');";
            migrationBuilder.Sql(insertSeed);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "B461BCC5-BEDD-41DA-B137-5CCA0E50E2A6", "56398b71-a0d8-4dc3-ba9f-c7bbdbf6e0c7", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoleClaims",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "RoleId" },
                values: new object[] { 2, "Permission", "Administrator", "B461BCC5-BEDD-41DA-B137-5CCA0E50E2A6" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "B461BCC5-BEDD-41DA-B137-5CCA0E50E2A6", "c0ea7b0e-e5db-4f17-9c35-87dd2ac20895" });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "ChildInfo");

            migrationBuilder.DropTable(
                name: "CustomerAddress");

            migrationBuilder.DropTable(
                name: "EducationalBackground");
        }
    }
}
