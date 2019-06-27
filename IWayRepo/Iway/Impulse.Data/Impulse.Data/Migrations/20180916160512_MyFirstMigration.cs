using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Impulse.Data.Migrations
{
    public partial class MyFirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EventType",
                columns: table => new
                {
                    EventTypeId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    EventTypeName = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventType", x => x.EventTypeId);
                });

            migrationBuilder.CreateTable(
                name: "LookUpAddressType",
                columns: table => new
                {
                    LookUpAddressTypeId = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AddressType = table.Column<string>(unicode: false, nullable: false),
                    RecordStatus = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    CreatedBy = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedBy = table.Column<string>(unicode: false, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LookUpAddressType", x => x.LookUpAddressTypeId);
                });

            migrationBuilder.CreateTable(
                name: "LookUpCompanyType",
                columns: table => new
                {
                    LookUpCompanyTypeId = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CompanyType = table.Column<string>(unicode: false, nullable: false),
                    RecordStatus = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    CreatedBy = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedBy = table.Column<string>(unicode: false, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LookUpCompanyType", x => x.LookUpCompanyTypeId);
                });

            migrationBuilder.CreateTable(
                name: "LookUpContactType",
                columns: table => new
                {
                    LookUpContactTypeId = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ContactType = table.Column<string>(unicode: false, nullable: false),
                    RecordStatus = table.Column<string>(unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LookUpContactType", x => x.LookUpContactTypeId);
                });

            migrationBuilder.CreateTable(
                name: "LookUpRateType",
                columns: table => new
                {
                    LookUpRateTypeId = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    RateType = table.Column<string>(nullable: true),
                    RecordStatus = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LookUpRateType", x => x.LookUpRateTypeId);
                });

            migrationBuilder.CreateTable(
                name: "LookUpUserStatus",
                columns: table => new
                {
                    LookUpUserStatusId = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserStatus = table.Column<string>(unicode: false, nullable: false),
                    RecordStatus = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    CreatedBy = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedBy = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LookUpUserStatus", x => x.LookUpUserStatusId);
                });

            migrationBuilder.CreateTable(
                name: "LookUpUserType",
                columns: table => new
                {
                    LookUpUserTypeId = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserType = table.Column<string>(unicode: false, nullable: false),
                    RecordStatus = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    CreatedBy = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedBy = table.Column<string>(unicode: false, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LookUpUserType", x => x.LookUpUserTypeId);
                });

            migrationBuilder.CreateTable(
                name: "tblCities",
                columns: table => new
                {
                    CityID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CityName = table.Column<string>(unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblCities", x => x.CityID);
                });

            migrationBuilder.CreateTable(
                name: "tblEmployee",
                columns: table => new
                {
                    EmployeeID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(unicode: false, maxLength: 20, nullable: false),
                    City = table.Column<string>(unicode: false, maxLength: 20, nullable: false),
                    Department = table.Column<string>(unicode: false, maxLength: 20, nullable: false),
                    Gender = table.Column<string>(unicode: false, maxLength: 6, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblEmployee", x => x.EmployeeID);
                });

            migrationBuilder.CreateTable(
                name: "VendorInterestRate",
                columns: table => new
                {
                    VendorInterestRateId = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    VendorInterestId = table.Column<long>(nullable: false),
                    LookupRateTypeId = table.Column<long>(nullable: true),
                    rate = table.Column<decimal>(nullable: false),
                    RecordStatus = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VendorInterestRate", x => x.VendorInterestRateId);
                });

            migrationBuilder.CreateTable(
                name: "EventSubType",
                columns: table => new
                {
                    EventSubTypeId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SubTypeName = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    EventTypeId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventSubType", x => x.EventSubTypeId);
                    table.ForeignKey(
                        name: "FK_EventSubType_EventType_EventTypeId",
                        column: x => x.EventTypeId,
                        principalTable: "EventType",
                        principalColumn: "EventTypeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Company",
                columns: table => new
                {
                    CompanyId = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CompanyName = table.Column<string>(unicode: false, nullable: false),
                    Pin = table.Column<long>(nullable: false),
                    UserId = table.Column<long>(nullable: false),
                    LookUpCompanyTypeId = table.Column<long>(nullable: false),
                    RecordStatus = table.Column<string>(unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company", x => x.CompanyId);
                    table.ForeignKey(
                        name: "FK_Address_LookUpCompanyType",
                        column: x => x.LookUpCompanyTypeId,
                        principalTable: "LookUpCompanyType",
                        principalColumn: "LookUpCompanyTypeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserId = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserName = table.Column<string>(unicode: false, nullable: true),
                    Password = table.Column<string>(unicode: false, nullable: true),
                    LookUpUserStatusId = table.Column<long>(nullable: true),
                    LookUpUserTypeId = table.Column<long>(nullable: false),
                    RecordStatus = table.Column<string>(unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_User_LookUpUserStatus",
                        column: x => x.LookUpUserStatusId,
                        principalTable: "LookUpUserStatus",
                        principalColumn: "LookUpUserStatusId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_User_LookUpUserType",
                        column: x => x.LookUpUserTypeId,
                        principalTable: "LookUpUserType",
                        principalColumn: "LookUpUserTypeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    AddressId = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Address = table.Column<string>(unicode: false, nullable: false),
                    Pin = table.Column<long>(nullable: false),
                    UserId = table.Column<long>(nullable: false),
                    LookUpAddressTypeId = table.Column<long>(nullable: false),
                    Type = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    RecordStatus = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    CreatedBy = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedBy = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.AddressId);
                    table.ForeignKey(
                        name: "FK_Address_LookUpAddressType",
                        column: x => x.LookUpAddressTypeId,
                        principalTable: "LookUpAddressType",
                        principalColumn: "LookUpAddressTypeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Address_User",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Contact",
                columns: table => new
                {
                    ContactId = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    LookUpContactTypeId = table.Column<long>(nullable: true),
                    UserId = table.Column<long>(nullable: true),
                    Value = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    RecordStatus = table.Column<string>(unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contact", x => x.ContactId);
                    table.ForeignKey(
                        name: "FK_Contact_LookUpContactType",
                        column: x => x.LookUpContactTypeId,
                        principalTable: "LookUpContactType",
                        principalColumn: "LookUpContactTypeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Contact_User",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Organiser",
                columns: table => new
                {
                    OrganiserId = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    OrganiserName = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    OrganiserDescription = table.Column<string>(unicode: false, nullable: true),
                    UserId = table.Column<long>(nullable: true),
                    OrganiserLogo = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    RecordStatus = table.Column<string>(unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organiser", x => x.OrganiserId);
                    table.ForeignKey(
                        name: "FK_Organiser_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Team",
                columns: table => new
                {
                    TeamId = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TeamName = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    UserId = table.Column<long>(nullable: true),
                    TeamDescription = table.Column<string>(unicode: false, nullable: true),
                    TeamLogo = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    CompanyId = table.Column<long>(nullable: true),
                    RecordStatus = table.Column<string>(unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Team", x => x.TeamId);
                    table.ForeignKey(
                        name: "FK_Team_User",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VendorInterest",
                columns: table => new
                {
                    VendorInterestId = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<long>(nullable: true),
                    PlaceId = table.Column<long>(nullable: true),
                    Place = table.Column<string>(nullable: true),
                    EventTypeId = table.Column<long>(nullable: true),
                    EventSubTypeId = table.Column<long>(nullable: true),
                    RecordStatus = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VendorInterest", x => x.VendorInterestId);
                    table.ForeignKey(
                        name: "FK_VendorInterest_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Address_LookUpAddressTypeId",
                table: "Address",
                column: "LookUpAddressTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Address_UserId",
                table: "Address",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Company_LookUpCompanyTypeId",
                table: "Company",
                column: "LookUpCompanyTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Contact_LookUpContactTypeId",
                table: "Contact",
                column: "LookUpContactTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Contact_UserId",
                table: "Contact",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_EventSubType_EventTypeId",
                table: "EventSubType",
                column: "EventTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Organiser_UserId",
                table: "Organiser",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Team_UserId",
                table: "Team",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_User_LookUpUserStatusId",
                table: "User",
                column: "LookUpUserStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_User_LookUpUserTypeId",
                table: "User",
                column: "LookUpUserTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_VendorInterest_UserId",
                table: "VendorInterest",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropTable(
                name: "Company");

            migrationBuilder.DropTable(
                name: "Contact");

            migrationBuilder.DropTable(
                name: "EventSubType");

            migrationBuilder.DropTable(
                name: "LookUpRateType");

            migrationBuilder.DropTable(
                name: "Organiser");

            migrationBuilder.DropTable(
                name: "tblCities");

            migrationBuilder.DropTable(
                name: "tblEmployee");

            migrationBuilder.DropTable(
                name: "Team");

            migrationBuilder.DropTable(
                name: "VendorInterest");

            migrationBuilder.DropTable(
                name: "VendorInterestRate");

            migrationBuilder.DropTable(
                name: "LookUpAddressType");

            migrationBuilder.DropTable(
                name: "LookUpCompanyType");

            migrationBuilder.DropTable(
                name: "LookUpContactType");

            migrationBuilder.DropTable(
                name: "EventType");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "LookUpUserStatus");

            migrationBuilder.DropTable(
                name: "LookUpUserType");
        }
    }
}
