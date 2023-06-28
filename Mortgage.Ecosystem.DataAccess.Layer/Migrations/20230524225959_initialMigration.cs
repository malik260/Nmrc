using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mortgage.Ecosystem.DataAccess.Layer.Migrations
{
    public partial class initialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "st_AccountType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BaseCreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BaseCreatorId = table.Column<long>(type: "bigint", nullable: false),
                    BaseVersion = table.Column<int>(type: "int", nullable: false),
                    BaseModifyTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BaseModifierId = table.Column<long>(type: "bigint", nullable: false),
                    BaseIsDelete = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_st_AccountType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "st_AgentType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BaseCreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BaseCreatorId = table.Column<long>(type: "bigint", nullable: false),
                    BaseVersion = table.Column<int>(type: "int", nullable: false),
                    BaseModifyTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BaseModifierId = table.Column<long>(type: "bigint", nullable: false),
                    BaseIsDelete = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_st_AgentType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "st_AlertType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BaseCreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BaseCreatorId = table.Column<long>(type: "bigint", nullable: false),
                    BaseVersion = table.Column<int>(type: "int", nullable: false),
                    BaseModifyTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BaseModifierId = table.Column<long>(type: "bigint", nullable: false),
                    BaseIsDelete = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_st_AlertType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "st_Bank",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BaseCreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BaseCreatorId = table.Column<long>(type: "bigint", nullable: false),
                    BaseVersion = table.Column<int>(type: "int", nullable: false),
                    BaseModifyTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BaseModifierId = table.Column<long>(type: "bigint", nullable: false),
                    BaseIsDelete = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_st_Bank", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "st_CompanyClass",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BaseCreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BaseCreatorId = table.Column<long>(type: "bigint", nullable: false),
                    BaseVersion = table.Column<int>(type: "int", nullable: false),
                    BaseModifyTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BaseModifierId = table.Column<long>(type: "bigint", nullable: false),
                    BaseIsDelete = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_st_CompanyClass", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "st_CompanyType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BaseCreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BaseCreatorId = table.Column<long>(type: "bigint", nullable: false),
                    BaseVersion = table.Column<int>(type: "int", nullable: false),
                    BaseModifyTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BaseModifierId = table.Column<long>(type: "bigint", nullable: false),
                    BaseIsDelete = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_st_CompanyType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "st_ContributionFrequency",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BaseCreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BaseCreatorId = table.Column<long>(type: "bigint", nullable: false),
                    BaseVersion = table.Column<int>(type: "int", nullable: false),
                    BaseModifyTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BaseModifierId = table.Column<long>(type: "bigint", nullable: false),
                    BaseIsDelete = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_st_ContributionFrequency", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "st_Designation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Company = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sort = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BaseCreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BaseCreatorId = table.Column<long>(type: "bigint", nullable: false),
                    BaseVersion = table.Column<int>(type: "int", nullable: false),
                    BaseModifyTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BaseModifierId = table.Column<long>(type: "bigint", nullable: false),
                    BaseIsDelete = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_st_Designation", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "st_Gender",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BaseCreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BaseCreatorId = table.Column<long>(type: "bigint", nullable: false),
                    BaseVersion = table.Column<int>(type: "int", nullable: false),
                    BaseModifyTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BaseModifierId = table.Column<long>(type: "bigint", nullable: false),
                    BaseIsDelete = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_st_Gender", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "st_MaritalStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BaseCreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BaseCreatorId = table.Column<long>(type: "bigint", nullable: false),
                    BaseVersion = table.Column<int>(type: "int", nullable: false),
                    BaseModifyTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BaseModifierId = table.Column<long>(type: "bigint", nullable: false),
                    BaseIsDelete = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_st_MaritalStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "st_Nationality",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShortName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Capital = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BaseCreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BaseCreatorId = table.Column<long>(type: "bigint", nullable: false),
                    BaseVersion = table.Column<int>(type: "int", nullable: false),
                    BaseModifyTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BaseModifierId = table.Column<long>(type: "bigint", nullable: false),
                    BaseIsDelete = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_st_Nationality", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "st_Relation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BaseCreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BaseCreatorId = table.Column<long>(type: "bigint", nullable: false),
                    BaseVersion = table.Column<int>(type: "int", nullable: false),
                    BaseModifyTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BaseModifierId = table.Column<long>(type: "bigint", nullable: false),
                    BaseIsDelete = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_st_Relation", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "st_Sector",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BaseCreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BaseCreatorId = table.Column<long>(type: "bigint", nullable: false),
                    BaseVersion = table.Column<int>(type: "int", nullable: false),
                    BaseModifyTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BaseModifierId = table.Column<long>(type: "bigint", nullable: false),
                    BaseIsDelete = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_st_Sector", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "st_State",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Capital = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Narration = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nationality = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BaseCreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BaseCreatorId = table.Column<long>(type: "bigint", nullable: false),
                    BaseVersion = table.Column<int>(type: "int", nullable: false),
                    BaseModifyTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BaseModifierId = table.Column<long>(type: "bigint", nullable: false),
                    BaseIsDelete = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_st_State", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "st_SubSector",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Sector = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BaseCreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BaseCreatorId = table.Column<long>(type: "bigint", nullable: false),
                    BaseVersion = table.Column<int>(type: "int", nullable: false),
                    BaseModifyTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BaseModifierId = table.Column<long>(type: "bigint", nullable: false),
                    BaseIsDelete = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_st_SubSector", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "st_Title",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BaseCreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BaseCreatorId = table.Column<long>(type: "bigint", nullable: false),
                    BaseVersion = table.Column<int>(type: "int", nullable: false),
                    BaseModifyTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BaseModifierId = table.Column<long>(type: "bigint", nullable: false),
                    BaseIsDelete = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_st_Title", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_AutoJob",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JobGroupName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JobName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JobStatus = table.Column<int>(type: "int", nullable: true),
                    CronExpression = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NextStartTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BaseCreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BaseCreatorId = table.Column<long>(type: "bigint", nullable: false),
                    BaseVersion = table.Column<int>(type: "int", nullable: false),
                    BaseModifyTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BaseModifierId = table.Column<long>(type: "bigint", nullable: false),
                    BaseIsDelete = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_AutoJob", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_AutoJobLog",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JobGroupName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JobName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LogStatus = table.Column<int>(type: "int", nullable: true),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BaseCreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BaseCreatorId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_AutoJobLog", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Branch",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Company = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MobileNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nationality = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Manager = table.Column<long>(type: "bigint", nullable: false),
                    BaseCreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BaseCreatorId = table.Column<long>(type: "bigint", nullable: false),
                    BaseVersion = table.Column<int>(type: "int", nullable: false),
                    BaseModifyTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BaseModifierId = table.Column<long>(type: "bigint", nullable: false),
                    BaseIsDelete = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Branch", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Company",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MobileNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fax = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sector = table.Column<int>(type: "int", nullable: false),
                    Subsector = table.Column<int>(type: "int", nullable: false),
                    RCNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfIncorporation = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ContactPerson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactPersonDesignation = table.Column<int>(type: "int", nullable: false),
                    NatureOfBusiness = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameOfRegistrar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameOfTrustees = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfCommencement = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CompanyClass = table.Column<int>(type: "int", nullable: false),
                    CompanyType = table.Column<int>(type: "int", nullable: false),
                    ContributionFrequency = table.Column<int>(type: "int", nullable: false),
                    Website = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Logo = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    LogoType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BaseCreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BaseCreatorId = table.Column<long>(type: "bigint", nullable: false),
                    BaseVersion = table.Column<int>(type: "int", nullable: false),
                    BaseModifyTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BaseModifierId = table.Column<long>(type: "bigint", nullable: false),
                    BaseIsDelete = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Company", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Department",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Company = table.Column<long>(type: "bigint", nullable: false),
                    Branch = table.Column<long>(type: "bigint", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Telephone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fax = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Principal = table.Column<long>(type: "bigint", nullable: true),
                    Sort = table.Column<int>(type: "int", nullable: false),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BaseCreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BaseCreatorId = table.Column<long>(type: "bigint", nullable: false),
                    BaseVersion = table.Column<int>(type: "int", nullable: false),
                    BaseModifyTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BaseModifierId = table.Column<long>(type: "bigint", nullable: false),
                    BaseIsDelete = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Department", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Employee",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Company = table.Column<long>(type: "bigint", nullable: false),
                    Branch = table.Column<long>(type: "bigint", nullable: false),
                    Department = table.Column<int>(type: "int", nullable: false),
                    BVN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NIN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmploymentType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfEmployment = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Title = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OtherName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    DateOfBirth = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaritalStatus = table.Column<int>(type: "int", nullable: false),
                    PostalAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MobileNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StaffNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerBank = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BankAccountNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccountType = table.Column<int>(type: "int", nullable: false),
                    MonthlySalary = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AlertType = table.Column<int>(type: "int", nullable: false),
                    Portrait = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    PortraitType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BaseCreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BaseCreatorId = table.Column<long>(type: "bigint", nullable: false),
                    BaseVersion = table.Column<int>(type: "int", nullable: false),
                    BaseModifyTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BaseModifierId = table.Column<long>(type: "bigint", nullable: false),
                    BaseIsDelete = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Employee", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_LogLogin",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Company = table.Column<long>(type: "bigint", nullable: false),
                    LogStatus = table.Column<int>(type: "int", nullable: false),
                    IpAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IpLocation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Browser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OS = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExtraRemark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BaseCreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BaseCreatorId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_LogLogin", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_LogOperate",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Company = table.Column<long>(type: "bigint", nullable: false),
                    LogStatus = table.Column<int>(type: "int", nullable: false),
                    IpAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IpLocation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LogType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BusinessType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExecuteUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExecuteParam = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExecuteResult = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExecuteTime = table.Column<int>(type: "int", nullable: false),
                    BaseCreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BaseCreatorId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_LogOperate", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Menu",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Parent = table.Column<long>(type: "bigint", nullable: false),
                    MenuName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MenuIcon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MenuUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MenuTarget = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MenuSort = table.Column<int>(type: "int", nullable: false),
                    MenuType = table.Column<int>(type: "int", nullable: false),
                    MenuStatus = table.Column<int>(type: "int", nullable: false),
                    Authorize = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApprovalLevel = table.Column<int>(type: "int", nullable: false),
                    Approved = table.Column<int>(type: "int", nullable: false),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BaseCreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BaseCreatorId = table.Column<long>(type: "bigint", nullable: false),
                    BaseVersion = table.Column<int>(type: "int", nullable: false),
                    BaseModifyTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BaseModifierId = table.Column<long>(type: "bigint", nullable: false),
                    BaseIsDelete = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Menu", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_MenuAuthorize",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MenuId = table.Column<long>(type: "bigint", nullable: false),
                    AuthorizeId = table.Column<long>(type: "bigint", nullable: false),
                    AuthorizeType = table.Column<int>(type: "int", nullable: false),
                    BaseCreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BaseCreatorId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_MenuAuthorize", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_NextOfKin",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Company = table.Column<long>(type: "bigint", nullable: false),
                    Employee = table.Column<long>(type: "bigint", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MobileNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Relationship = table.Column<int>(type: "int", nullable: false),
                    BaseCreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BaseCreatorId = table.Column<long>(type: "bigint", nullable: false),
                    BaseVersion = table.Column<int>(type: "int", nullable: false),
                    BaseModifyTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BaseModifierId = table.Column<long>(type: "bigint", nullable: false),
                    BaseIsDelete = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_NextOfKin", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_PaymentHistory",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NHFNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Amount = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RRR = table.Column<int>(type: "int", nullable: false),
                    TransactionId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BaseCreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BaseCreatorId = table.Column<long>(type: "bigint", nullable: false),
                    BaseVersion = table.Column<int>(type: "int", nullable: false),
                    BaseModifyTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BaseModifierId = table.Column<long>(type: "bigint", nullable: false),
                    BaseIsDelete = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_PaymentHistory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Role",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Company = table.Column<long>(type: "bigint", nullable: false),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoleSort = table.Column<int>(type: "int", nullable: false),
                    RoleStatus = table.Column<int>(type: "int", nullable: false),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BaseCreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BaseCreatorId = table.Column<long>(type: "bigint", nullable: false),
                    BaseVersion = table.Column<int>(type: "int", nullable: false),
                    BaseModifyTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BaseModifierId = table.Column<long>(type: "bigint", nullable: false),
                    BaseIsDelete = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Company = table.Column<long>(type: "bigint", nullable: false),
                    Employee = table.Column<long>(type: "bigint", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Salt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RealName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LoginCount = table.Column<int>(type: "int", nullable: false),
                    UserStatus = table.Column<int>(type: "int", nullable: false),
                    IsSystem = table.Column<int>(type: "int", nullable: false),
                    IsOnline = table.Column<int>(type: "int", nullable: false),
                    FirstVisit = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PreviousVisit = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastVisit = table.Column<DateTime>(type: "datetime2", nullable: false),
                    WebToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApiToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BaseCreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BaseCreatorId = table.Column<long>(type: "bigint", nullable: false),
                    BaseVersion = table.Column<int>(type: "int", nullable: false),
                    BaseModifyTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BaseModifierId = table.Column<long>(type: "bigint", nullable: false),
                    BaseIsDelete = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_UserBelong",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Company = table.Column<long>(type: "bigint", nullable: false),
                    User = table.Column<long>(type: "bigint", nullable: false),
                    Belong = table.Column<long>(type: "bigint", nullable: false),
                    BelongType = table.Column<int>(type: "int", nullable: false),
                    BaseCreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BaseCreatorId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_UserBelong", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "st_AccountType");

            migrationBuilder.DropTable(
                name: "st_AgentType");

            migrationBuilder.DropTable(
                name: "st_AlertType");

            migrationBuilder.DropTable(
                name: "st_Bank");

            migrationBuilder.DropTable(
                name: "st_CompanyClass");

            migrationBuilder.DropTable(
                name: "st_CompanyType");

            migrationBuilder.DropTable(
                name: "st_ContributionFrequency");

            migrationBuilder.DropTable(
                name: "st_Designation");

            migrationBuilder.DropTable(
                name: "st_Gender");

            migrationBuilder.DropTable(
                name: "st_MaritalStatus");

            migrationBuilder.DropTable(
                name: "st_Nationality");

            migrationBuilder.DropTable(
                name: "st_Relation");

            migrationBuilder.DropTable(
                name: "st_Sector");

            migrationBuilder.DropTable(
                name: "st_State");

            migrationBuilder.DropTable(
                name: "st_SubSector");

            migrationBuilder.DropTable(
                name: "st_Title");

            migrationBuilder.DropTable(
                name: "tbl_AutoJob");

            migrationBuilder.DropTable(
                name: "tbl_AutoJobLog");

            migrationBuilder.DropTable(
                name: "tbl_Branch");

            migrationBuilder.DropTable(
                name: "tbl_Company");

            migrationBuilder.DropTable(
                name: "tbl_Department");

            migrationBuilder.DropTable(
                name: "tbl_Employee");

            migrationBuilder.DropTable(
                name: "tbl_LogLogin");

            migrationBuilder.DropTable(
                name: "tbl_LogOperate");

            migrationBuilder.DropTable(
                name: "tbl_Menu");

            migrationBuilder.DropTable(
                name: "tbl_MenuAuthorize");

            migrationBuilder.DropTable(
                name: "tbl_NextOfKin");

            migrationBuilder.DropTable(
                name: "tbl_PaymentHistory");

            migrationBuilder.DropTable(
                name: "tbl_Role");

            migrationBuilder.DropTable(
                name: "tbl_User");

            migrationBuilder.DropTable(
                name: "tbl_UserBelong");
        }
    }
}
