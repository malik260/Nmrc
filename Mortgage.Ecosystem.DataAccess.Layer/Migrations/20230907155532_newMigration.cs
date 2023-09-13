using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mortgage.Ecosystem.DataAccess.Layer.Migrations
{
    public partial class newMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.CreateTable(
            //    name: "Finance_TblFinanceCounterpartyTransaction",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "bigint", nullable: false),
            //        TransactionId = table.Column<int>(type: "int", nullable: false),
            //        TransactionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
            //        Ref = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        CpId = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        DVolume = table.Column<int>(type: "int", nullable: true),
            //        CVolume = table.Column<int>(type: "int", nullable: true),
            //        DebitAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
            //        CreditAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
            //        Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        AcctTransaction = table.Column<int>(type: "int", nullable: true),
            //        CustCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        ProductCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Branch = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Coy = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        FormNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        BatchRef = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        PostDate = table.Column<DateTime>(type: "datetime2", nullable: true),
            //        ApplicationId = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Approved = table.Column<int>(type: "int", nullable: true),
            //        Show = table.Column<int>(type: "int", nullable: true),
            //        IsReversed = table.Column<int>(type: "int", nullable: false),
            //        GlAccountId = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        SystemDatetime = table.Column<DateTime>(type: "datetime2", nullable: true),
            //        OldAccountNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        LegType = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        IsCleared = table.Column<int>(type: "int", nullable: true),
            //        Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        TransactionType = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        BaseCreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        BaseCreatorId = table.Column<long>(type: "bigint", nullable: false),
            //        BaseProcessMenu = table.Column<long>(type: "bigint", nullable: false),
            //        BaseVersion = table.Column<int>(type: "int", nullable: false),
            //        BaseModifyTime = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        BaseModifierId = table.Column<long>(type: "bigint", nullable: false),
            //        BaseIsDelete = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Finance_TblFinanceCounterpartyTransaction", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Finance_TblFinanceTransaction",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "bigint", nullable: false),
            //        TransactionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        TransactonType = table.Column<int>(type: "int", nullable: true),
            //        TransactonId = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Ref = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        DebitAmt = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
            //        CreditAmt = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
            //        AccountId = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        PostedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        PostingTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Approved = table.Column<int>(type: "int", nullable: false),
            //        ApprovedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Saved = table.Column<int>(type: "int", nullable: true),
            //        Sbu = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Deleted = table.Column<int>(type: "int", nullable: false),
            //        DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        ValueDate = table.Column<DateTime>(type: "datetime2", nullable: true),
            //        SourceBranch = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        DestinationBranch = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        ItemId = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        MisCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        LegType = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        BatchRef = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        ScoyCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        LCurrencyCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        CurrencyRate = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
            //        ApplicationId = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        NonbrAccountId = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        IsReversed = table.Column<int>(type: "int", nullable: false),
            //        BaseCreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        BaseCreatorId = table.Column<long>(type: "bigint", nullable: false),
            //        BaseProcessMenu = table.Column<long>(type: "bigint", nullable: false),
            //        BaseVersion = table.Column<int>(type: "int", nullable: false),
            //        BaseModifyTime = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        BaseModifierId = table.Column<long>(type: "bigint", nullable: false),
            //        BaseIsDelete = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Finance_TblFinanceTransaction", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Refund_TblContributionrefundposting",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "bigint", nullable: false),
            //        LedgeDr = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Ledgercr = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Postingtype = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        BaseCreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        BaseCreatorId = table.Column<long>(type: "bigint", nullable: false),
            //        BaseProcessMenu = table.Column<long>(type: "bigint", nullable: false),
            //        BaseVersion = table.Column<int>(type: "int", nullable: false),
            //        BaseModifyTime = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        BaseModifierId = table.Column<long>(type: "bigint", nullable: false),
            //        BaseIsDelete = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Refund_TblContributionrefundposting", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Refund_TblRefundcondition",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "bigint", nullable: false),
            //        Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Year = table.Column<byte>(type: "tinyint", nullable: true),
            //        Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Remark = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        IsDeleted = table.Column<bool>(type: "bit", nullable: true),
            //        BaseCreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        BaseCreatorId = table.Column<long>(type: "bigint", nullable: false),
            //        BaseProcessMenu = table.Column<long>(type: "bigint", nullable: false),
            //        BaseVersion = table.Column<int>(type: "int", nullable: false),
            //        BaseModifyTime = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        BaseModifierId = table.Column<long>(type: "bigint", nullable: false),
            //        BaseIsDelete = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Refund_TblRefundcondition", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Refund_TblRefundprofiling",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "bigint", nullable: false),
            //        NhfNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        CustName = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        ConditionToApply = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        DocumentTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        ApplicationType = table.Column<int>(type: "int", nullable: true),
            //        Comments = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        LevelId = table.Column<int>(type: "int", nullable: false),
            //        CustomerNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        BranchCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        BVN = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        LevelName = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Disapprove = table.Column<int>(type: "int", nullable: false),
            //        OperationId = table.Column<int>(type: "int", nullable: false),
            //        BranchComment = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        ManagerComment = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        HeadOfficeComment = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        UnitHeadComment = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        IcandcComment = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        ApprovalComment = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        FinconComment = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        BudgetsComment = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        AuditUnitComment = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        GroupHeadComment = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        BankAccountNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        BankName = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        BankCode = table.Column<int>(type: "int", nullable: true),
            //        AmountApproved = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
            //        InterestAmt = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
            //        ApplicationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
            //        BaseCreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        BaseCreatorId = table.Column<long>(type: "bigint", nullable: false),
            //        BaseProcessMenu = table.Column<long>(type: "bigint", nullable: false),
            //        BaseVersion = table.Column<int>(type: "int", nullable: false),
            //        BaseModifyTime = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        BaseModifierId = table.Column<long>(type: "bigint", nullable: false),
            //        BaseIsDelete = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Refund_TblRefundprofiling", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Remita_TblRemitaPaymentDetails",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "bigint", nullable: false),
            //        TransactionId = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        TransactionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
            //        EmployeeNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        EmployerNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Status = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
            //        Rrr = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        LoggedUser = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Device = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Amount = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        BaseCreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        BaseCreatorId = table.Column<long>(type: "bigint", nullable: false),
            //        BaseProcessMenu = table.Column<long>(type: "bigint", nullable: false),
            //        BaseVersion = table.Column<int>(type: "int", nullable: false),
            //        BaseModifyTime = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        BaseModifierId = table.Column<long>(type: "bigint", nullable: false),
            //        BaseIsDelete = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Remita_TblRemitaPaymentDetails", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "st_AccountType",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        BaseCreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        BaseCreatorId = table.Column<long>(type: "bigint", nullable: false),
            //        BaseProcessMenu = table.Column<long>(type: "bigint", nullable: false),
            //        BaseVersion = table.Column<int>(type: "int", nullable: false),
            //        BaseModifyTime = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        BaseModifierId = table.Column<long>(type: "bigint", nullable: false),
            //        BaseIsDelete = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_st_AccountType", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "st_AgentType",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        BaseCreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        BaseCreatorId = table.Column<long>(type: "bigint", nullable: false),
            //        BaseProcessMenu = table.Column<long>(type: "bigint", nullable: false),
            //        BaseVersion = table.Column<int>(type: "int", nullable: false),
            //        BaseModifyTime = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        BaseModifierId = table.Column<long>(type: "bigint", nullable: false),
            //        BaseIsDelete = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_st_AgentType", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "st_AlertType",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        BaseCreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        BaseCreatorId = table.Column<long>(type: "bigint", nullable: false),
            //        BaseProcessMenu = table.Column<long>(type: "bigint", nullable: false),
            //        BaseVersion = table.Column<int>(type: "int", nullable: false),
            //        BaseModifyTime = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        BaseModifierId = table.Column<long>(type: "bigint", nullable: false),
            //        BaseIsDelete = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_st_AlertType", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "st_Bank",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        BaseCreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        BaseCreatorId = table.Column<long>(type: "bigint", nullable: false),
            //        BaseProcessMenu = table.Column<long>(type: "bigint", nullable: false),
            //        BaseVersion = table.Column<int>(type: "int", nullable: false),
            //        BaseModifyTime = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        BaseModifierId = table.Column<long>(type: "bigint", nullable: false),
            //        BaseIsDelete = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_st_Bank", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "st_Checklist",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        ProductName = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        ProductCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Checklist = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Remark = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        BaseCreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        BaseCreatorId = table.Column<long>(type: "bigint", nullable: false),
            //        BaseProcessMenu = table.Column<long>(type: "bigint", nullable: false),
            //        BaseVersion = table.Column<int>(type: "int", nullable: false),
            //        BaseModifyTime = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        BaseModifierId = table.Column<long>(type: "bigint", nullable: false),
            //        BaseIsDelete = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_st_Checklist", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "st_CompanyClass",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        BaseCreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        BaseCreatorId = table.Column<long>(type: "bigint", nullable: false),
            //        BaseProcessMenu = table.Column<long>(type: "bigint", nullable: false),
            //        BaseVersion = table.Column<int>(type: "int", nullable: false),
            //        BaseModifyTime = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        BaseModifierId = table.Column<long>(type: "bigint", nullable: false),
            //        BaseIsDelete = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_st_CompanyClass", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "st_CompanyType",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        BaseCreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        BaseCreatorId = table.Column<long>(type: "bigint", nullable: false),
            //        BaseProcessMenu = table.Column<long>(type: "bigint", nullable: false),
            //        BaseVersion = table.Column<int>(type: "int", nullable: false),
            //        BaseModifyTime = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        BaseModifierId = table.Column<long>(type: "bigint", nullable: false),
            //        BaseIsDelete = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_st_CompanyType", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "st_ContributionFrequency",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        BaseCreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        BaseCreatorId = table.Column<long>(type: "bigint", nullable: false),
            //        BaseProcessMenu = table.Column<long>(type: "bigint", nullable: false),
            //        BaseVersion = table.Column<int>(type: "int", nullable: false),
            //        BaseModifyTime = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        BaseModifierId = table.Column<long>(type: "bigint", nullable: false),
            //        BaseIsDelete = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_st_ContributionFrequency", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "st_CreditAssessmentFactorIndex",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        FactorIndexId = table.Column<int>(type: "int", nullable: false),
            //        FactorIndexDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Weight = table.Column<int>(type: "int", nullable: false),
            //        RiskFactorId = table.Column<int>(type: "int", nullable: false),
            //        ProductCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        BaseCreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        BaseCreatorId = table.Column<long>(type: "bigint", nullable: false),
            //        BaseProcessMenu = table.Column<long>(type: "bigint", nullable: false),
            //        BaseVersion = table.Column<int>(type: "int", nullable: false),
            //        BaseModifyTime = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        BaseModifierId = table.Column<long>(type: "bigint", nullable: false),
            //        BaseIsDelete = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_st_CreditAssessmentFactorIndex", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "st_CreditAssessmentIndex",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        IndexId = table.Column<int>(type: "int", nullable: false),
            //        AssessmentIndex = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Weight = table.Column<int>(type: "int", nullable: false),
            //        IndexTitleId = table.Column<int>(type: "int", nullable: false),
            //        ProductCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        BaseCreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        BaseCreatorId = table.Column<long>(type: "bigint", nullable: false),
            //        BaseProcessMenu = table.Column<long>(type: "bigint", nullable: false),
            //        BaseVersion = table.Column<int>(type: "int", nullable: false),
            //        BaseModifyTime = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        BaseModifierId = table.Column<long>(type: "bigint", nullable: false),
            //        BaseIsDelete = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_st_CreditAssessmentIndex", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "st_CreditAssessmentIndexTitle",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        IndexTitleId = table.Column<int>(type: "int", nullable: false),
            //        IndexTitleDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Weight = table.Column<int>(type: "int", nullable: false),
            //        FactorIndexId = table.Column<int>(type: "int", nullable: false),
            //        ProductCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        BaseCreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        BaseCreatorId = table.Column<long>(type: "bigint", nullable: false),
            //        BaseProcessMenu = table.Column<long>(type: "bigint", nullable: false),
            //        BaseVersion = table.Column<int>(type: "int", nullable: false),
            //        BaseModifyTime = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        BaseModifierId = table.Column<long>(type: "bigint", nullable: false),
            //        BaseIsDelete = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_st_CreditAssessmentIndexTitle", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "st_CreditAssessmentRiskFactor",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        RiskFactorId = table.Column<int>(type: "int", nullable: false),
            //        RiskFactorsDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Weight = table.Column<int>(type: "int", nullable: false),
            //        Productcode = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        BaseCreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        BaseCreatorId = table.Column<long>(type: "bigint", nullable: false),
            //        BaseProcessMenu = table.Column<long>(type: "bigint", nullable: false),
            //        BaseVersion = table.Column<int>(type: "int", nullable: false),
            //        BaseModifyTime = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        BaseModifierId = table.Column<long>(type: "bigint", nullable: false),
            //        BaseIsDelete = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_st_CreditAssessmentRiskFactor", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "st_CreditType",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        BaseCreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        BaseCreatorId = table.Column<long>(type: "bigint", nullable: false),
            //        BaseProcessMenu = table.Column<long>(type: "bigint", nullable: false),
            //        BaseVersion = table.Column<int>(type: "int", nullable: false),
            //        BaseModifyTime = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        BaseModifierId = table.Column<long>(type: "bigint", nullable: false),
            //        BaseIsDelete = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_st_CreditType", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "st_Designation",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Company = table.Column<long>(type: "bigint", nullable: false),
            //        Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Sort = table.Column<int>(type: "int", nullable: false),
            //        Status = table.Column<int>(type: "int", nullable: false),
            //        Remark = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        BaseCreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        BaseCreatorId = table.Column<long>(type: "bigint", nullable: false),
            //        BaseProcessMenu = table.Column<long>(type: "bigint", nullable: false),
            //        BaseVersion = table.Column<int>(type: "int", nullable: false),
            //        BaseModifyTime = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        BaseModifierId = table.Column<long>(type: "bigint", nullable: false),
            //        BaseIsDelete = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_st_Designation", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "st_Gender",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        BaseCreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        BaseCreatorId = table.Column<long>(type: "bigint", nullable: false),
            //        BaseProcessMenu = table.Column<long>(type: "bigint", nullable: false),
            //        BaseVersion = table.Column<int>(type: "int", nullable: false),
            //        BaseModifyTime = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        BaseModifierId = table.Column<long>(type: "bigint", nullable: false),
            //        BaseIsDelete = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_st_Gender", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "st_MaritalStatus",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        BaseCreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        BaseCreatorId = table.Column<long>(type: "bigint", nullable: false),
            //        BaseProcessMenu = table.Column<long>(type: "bigint", nullable: false),
            //        BaseVersion = table.Column<int>(type: "int", nullable: false),
            //        BaseModifyTime = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        BaseModifierId = table.Column<long>(type: "bigint", nullable: false),
            //        BaseIsDelete = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_st_MaritalStatus", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "st_Nationality",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        ShortName = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        FullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Capital = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Currency = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        BaseCreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        BaseCreatorId = table.Column<long>(type: "bigint", nullable: false),
            //        BaseProcessMenu = table.Column<long>(type: "bigint", nullable: false),
            //        BaseVersion = table.Column<int>(type: "int", nullable: false),
            //        BaseModifyTime = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        BaseModifierId = table.Column<long>(type: "bigint", nullable: false),
            //        BaseIsDelete = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_st_Nationality", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "st_Relation",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        BaseCreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        BaseCreatorId = table.Column<long>(type: "bigint", nullable: false),
            //        BaseProcessMenu = table.Column<long>(type: "bigint", nullable: false),
            //        BaseVersion = table.Column<int>(type: "int", nullable: false),
            //        BaseModifyTime = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        BaseModifierId = table.Column<long>(type: "bigint", nullable: false),
            //        BaseIsDelete = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_st_Relation", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "st_Sector",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        BaseCreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        BaseCreatorId = table.Column<long>(type: "bigint", nullable: false),
            //        BaseProcessMenu = table.Column<long>(type: "bigint", nullable: false),
            //        BaseVersion = table.Column<int>(type: "int", nullable: false),
            //        BaseModifyTime = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        BaseModifierId = table.Column<long>(type: "bigint", nullable: false),
            //        BaseIsDelete = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_st_Sector", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "st_State",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Capital = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Narration = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Nationality = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        BaseCreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        BaseCreatorId = table.Column<long>(type: "bigint", nullable: false),
            //        BaseProcessMenu = table.Column<long>(type: "bigint", nullable: false),
            //        BaseVersion = table.Column<int>(type: "int", nullable: false),
            //        BaseModifyTime = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        BaseModifierId = table.Column<long>(type: "bigint", nullable: false),
            //        BaseIsDelete = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_st_State", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "st_SubSector",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Sector = table.Column<int>(type: "int", nullable: false),
            //        Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        BaseCreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        BaseCreatorId = table.Column<long>(type: "bigint", nullable: false),
            //        BaseProcessMenu = table.Column<long>(type: "bigint", nullable: false),
            //        BaseVersion = table.Column<int>(type: "int", nullable: false),
            //        BaseModifyTime = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        BaseModifierId = table.Column<long>(type: "bigint", nullable: false),
            //        BaseIsDelete = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_st_SubSector", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "st_Title",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        BaseCreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        BaseCreatorId = table.Column<long>(type: "bigint", nullable: false),
            //        BaseProcessMenu = table.Column<long>(type: "bigint", nullable: false),
            //        BaseVersion = table.Column<int>(type: "int", nullable: false),
            //        BaseModifyTime = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        BaseModifierId = table.Column<long>(type: "bigint", nullable: false),
            //        BaseIsDelete = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_st_Title", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "tbl_AccreditationFee",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "bigint", nullable: false),
            //        AgenName = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        MobileNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        FeeAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
            //        PaymentOption = table.Column<int>(type: "int", nullable: false),
            //        BaseCreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        BaseCreatorId = table.Column<long>(type: "bigint", nullable: false),
            //        BaseProcessMenu = table.Column<long>(type: "bigint", nullable: false),
            //        BaseVersion = table.Column<int>(type: "int", nullable: false),
            //        BaseModifyTime = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        BaseModifierId = table.Column<long>(type: "bigint", nullable: false),
            //        BaseIsDelete = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_tbl_AccreditationFee", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "tbl_AllNHFSubscriber",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "bigint", nullable: false),
            //        NHFEmployerNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        NHFEmployeeNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        ContactPerson = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        CompanyNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        DateOfIncorporation = table.Column<DateTime>(type: "datetime2", nullable: true),
            //        BaseCreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        BaseCreatorId = table.Column<long>(type: "bigint", nullable: false),
            //        BaseProcessMenu = table.Column<long>(type: "bigint", nullable: false),
            //        BaseVersion = table.Column<int>(type: "int", nullable: false),
            //        BaseModifyTime = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        BaseModifierId = table.Column<long>(type: "bigint", nullable: false),
            //        BaseIsDelete = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_tbl_AllNHFSubscriber", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "tbl_ApprovalLog",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Company = table.Column<long>(type: "bigint", nullable: false),
            //        Branch = table.Column<long>(type: "bigint", nullable: true),
            //        MenuId = table.Column<long>(type: "bigint", nullable: false),
            //        MenuType = table.Column<int>(type: "int", nullable: false),
            //        Authority = table.Column<long>(type: "bigint", nullable: false),
            //        Record = table.Column<long>(type: "bigint", nullable: false),
            //        ApprovalCount = table.Column<int>(type: "int", nullable: false),
            //        ApprovalLevel = table.Column<int>(type: "int", nullable: false),
            //        Status = table.Column<int>(type: "int", nullable: false),
            //        Remark = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        BaseCreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        BaseCreatorId = table.Column<long>(type: "bigint", nullable: false),
            //        BaseProcessMenu = table.Column<long>(type: "bigint", nullable: false),
            //        BaseVersion = table.Column<int>(type: "int", nullable: false),
            //        BaseModifyTime = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        BaseModifierId = table.Column<long>(type: "bigint", nullable: false),
            //        BaseIsDelete = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_tbl_ApprovalLog", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "tbl_ApprovalSetup",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "bigint", nullable: false),
            //        Company = table.Column<long>(type: "bigint", nullable: false),
            //        Branch = table.Column<long>(type: "bigint", nullable: false),
            //        MenuId = table.Column<long>(type: "bigint", nullable: false),
            //        Authority = table.Column<long>(type: "bigint", nullable: false),
            //        Remark = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        BaseCreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        BaseCreatorId = table.Column<long>(type: "bigint", nullable: false),
            //        BaseProcessMenu = table.Column<long>(type: "bigint", nullable: false),
            //        BaseVersion = table.Column<int>(type: "int", nullable: false),
            //        BaseModifyTime = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        BaseModifierId = table.Column<long>(type: "bigint", nullable: false),
            //        BaseIsDelete = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_tbl_ApprovalSetup", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "tbl_ApproveAgents",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "bigint", nullable: false),
            //        FullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Company = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        CompanyNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        PhoneNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        DateOfIncorporation = table.Column<DateTime>(type: "datetime2", nullable: true),
            //        Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Remark = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        BaseCreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        BaseCreatorId = table.Column<long>(type: "bigint", nullable: false),
            //        BaseProcessMenu = table.Column<long>(type: "bigint", nullable: false),
            //        BaseVersion = table.Column<int>(type: "int", nullable: false),
            //        BaseModifyTime = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        BaseModifierId = table.Column<long>(type: "bigint", nullable: false),
            //        BaseIsDelete = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_tbl_ApproveAgents", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "tbl_ApproveEmployerAggregator",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "bigint", nullable: false),
            //        Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        MobileNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Date = table.Column<DateTime>(type: "datetime2", nullable: true),
            //        ContributionBranch = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        ContributionFrequency = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Remark = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        BaseCreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        BaseCreatorId = table.Column<long>(type: "bigint", nullable: false),
            //        BaseProcessMenu = table.Column<long>(type: "bigint", nullable: false),
            //        BaseVersion = table.Column<int>(type: "int", nullable: false),
            //        BaseModifyTime = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        BaseModifierId = table.Column<long>(type: "bigint", nullable: false),
            //        BaseIsDelete = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_tbl_ApproveEmployerAggregator", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "tbl_AutoJob",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "bigint", nullable: false),
            //        JobGroupName = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        JobName = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        JobStatus = table.Column<int>(type: "int", nullable: true),
            //        CronExpression = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        StartTime = table.Column<DateTime>(type: "datetime2", nullable: true),
            //        EndTime = table.Column<DateTime>(type: "datetime2", nullable: true),
            //        NextStartTime = table.Column<DateTime>(type: "datetime2", nullable: true),
            //        Remark = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        BaseCreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        BaseCreatorId = table.Column<long>(type: "bigint", nullable: false),
            //        BaseProcessMenu = table.Column<long>(type: "bigint", nullable: false),
            //        BaseVersion = table.Column<int>(type: "int", nullable: false),
            //        BaseModifyTime = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        BaseModifierId = table.Column<long>(type: "bigint", nullable: false),
            //        BaseIsDelete = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_tbl_AutoJob", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "tbl_AutoJobLog",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "bigint", nullable: false),
            //        JobGroupName = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        JobName = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        LogStatus = table.Column<int>(type: "int", nullable: true),
            //        Remark = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        BaseCreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        BaseCreatorId = table.Column<long>(type: "bigint", nullable: false),
            //        BaseProcessMenu = table.Column<long>(type: "bigint", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_tbl_AutoJobLog", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "tbl_Branch",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "bigint", nullable: false),
            //        Company = table.Column<long>(type: "bigint", nullable: false),
            //        Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        MobileNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Location = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        State = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Nationality = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Manager = table.Column<long>(type: "bigint", nullable: false),
            //        BaseCreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        BaseCreatorId = table.Column<long>(type: "bigint", nullable: false),
            //        BaseProcessMenu = table.Column<long>(type: "bigint", nullable: false),
            //        BaseVersion = table.Column<int>(type: "int", nullable: false),
            //        BaseModifyTime = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        BaseModifierId = table.Column<long>(type: "bigint", nullable: false),
            //        BaseIsDelete = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_tbl_Branch", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "tbl_ChangeEmployer",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "bigint", nullable: false),
            //        NhfNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        CurrentEmployer = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        CurrentEmployerNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        NewEmployer = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        OldEmployerNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
            //        BaseCreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        BaseCreatorId = table.Column<long>(type: "bigint", nullable: false),
            //        BaseProcessMenu = table.Column<long>(type: "bigint", nullable: false),
            //        BaseVersion = table.Column<int>(type: "int", nullable: false),
            //        BaseModifyTime = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        BaseModifierId = table.Column<long>(type: "bigint", nullable: false),
            //        BaseIsDelete = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_tbl_ChangeEmployer", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "tbl_ChangePassword",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "bigint", nullable: false),
            //        OldPassword = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        NewPassword = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        ConfirmPassword = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        BaseCreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        BaseCreatorId = table.Column<long>(type: "bigint", nullable: false),
            //        BaseProcessMenu = table.Column<long>(type: "bigint", nullable: false),
            //        BaseVersion = table.Column<int>(type: "int", nullable: false),
            //        BaseModifyTime = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        BaseModifierId = table.Column<long>(type: "bigint", nullable: false),
            //        BaseIsDelete = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_tbl_ChangePassword", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "tbl_ChargeSetup",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "bigint", nullable: false),
            //        ReferenceNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        FeeCatergory = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        FeeRate = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        ChargeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Rate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
            //        PropertyType = table.Column<string>(name: "Property Type", type: "nvarchar(max)", nullable: true),
            //        Amount = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        BaseCreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        BaseCreatorId = table.Column<long>(type: "bigint", nullable: false),
            //        BaseProcessMenu = table.Column<long>(type: "bigint", nullable: false),
            //        BaseVersion = table.Column<int>(type: "int", nullable: false),
            //        BaseModifyTime = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        BaseModifierId = table.Column<long>(type: "bigint", nullable: false),
            //        BaseIsDelete = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_tbl_ChargeSetup", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "tbl_Company",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "bigint", nullable: false),
            //        Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        MobileNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Fax = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Sector = table.Column<int>(type: "int", nullable: false),
            //        Subsector = table.Column<int>(type: "int", nullable: false),
            //        RCNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        DateOfIncorporation = table.Column<DateTime>(type: "datetime2", nullable: true),
            //        ContactPerson = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        ContactPersonDesignation = table.Column<int>(type: "int", nullable: false),
            //        NatureOfBusiness = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        NameOfRegistrar = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        NameOfTrustees = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        DateOfCommencement = table.Column<DateTime>(type: "datetime2", nullable: true),
            //        CompanyClass = table.Column<int>(type: "int", nullable: false),
            //        CompanyType = table.Column<int>(type: "int", nullable: false),
            //        ContributionFrequency = table.Column<int>(type: "int", nullable: false),
            //        Website = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Logo = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
            //        LogoType = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Remark = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        BaseCreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        BaseCreatorId = table.Column<long>(type: "bigint", nullable: false),
            //        BaseProcessMenu = table.Column<long>(type: "bigint", nullable: false),
            //        BaseVersion = table.Column<int>(type: "int", nullable: false),
            //        BaseModifyTime = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        BaseModifierId = table.Column<long>(type: "bigint", nullable: false),
            //        BaseIsDelete = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_tbl_Company", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "tbl_Contribution",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "bigint", nullable: false),
            //        EmployeeNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        EmployerName = table.Column<string>(name: "Employer Name", type: "nvarchar(max)", nullable: true),
            //        ContributionAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
            //        EmployerNumber = table.Column<string>(name: "Employer Number", type: "nvarchar(max)", nullable: true),
            //        Month = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Year = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        PaymentOption = table.Column<int>(type: "int", nullable: false),
            //        AccountName = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Narration = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Document = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        File = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
            //        BaseCreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        BaseCreatorId = table.Column<long>(type: "bigint", nullable: false),
            //        BaseProcessMenu = table.Column<long>(type: "bigint", nullable: false),
            //        BaseVersion = table.Column<int>(type: "int", nullable: false),
            //        BaseModifyTime = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        BaseModifierId = table.Column<long>(type: "bigint", nullable: false),
            //        BaseIsDelete = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_tbl_Contribution", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "tbl_ContributionHistory",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "bigint", nullable: false),
            //        StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
            //        EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
            //        NHFNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        EmployerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        AmountContributed = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
            //        Remark = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Month = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Year = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        BaseCreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        BaseCreatorId = table.Column<long>(type: "bigint", nullable: false),
            //        BaseProcessMenu = table.Column<long>(type: "bigint", nullable: false),
            //        BaseVersion = table.Column<int>(type: "int", nullable: false),
            //        BaseModifyTime = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        BaseModifierId = table.Column<long>(type: "bigint", nullable: false),
            //        BaseIsDelete = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_tbl_ContributionHistory", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "tbl_CreditScore",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "bigint", nullable: false),
            //        CreditType = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        RangeMax = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
            //        RangeMin = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
            //        ProductCode = table.Column<int>(type: "int", nullable: false),
            //        Rating = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Remark = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        InterestRate = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        CreditGrade = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        CreditGradeDefinition = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        BaseCreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        BaseCreatorId = table.Column<long>(type: "bigint", nullable: false),
            //        BaseProcessMenu = table.Column<long>(type: "bigint", nullable: false),
            //        BaseVersion = table.Column<int>(type: "int", nullable: false),
            //        BaseModifyTime = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        BaseModifierId = table.Column<long>(type: "bigint", nullable: false),
            //        BaseIsDelete = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_tbl_CreditScore", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "tbl_CustomerProfileUpdate",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "bigint", nullable: false),
            //        FullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        MobileNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        MaritalStatus = table.Column<int>(type: "int", nullable: false),
            //        AccountName = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        BankAccountNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        CustomerBank = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        MonthlyIncome = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
            //        Subsector = table.Column<int>(type: "int", nullable: false),
            //        NOKName = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        NOKNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        NOKEmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        NOKAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Relationship = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        DocumentsUpload = table.Column<int>(type: "int", nullable: false),
            //        Files = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
            //        NHFNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        ApprovalStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        NewFullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        NewMobileNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        NewEmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        NewAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        NewMaritalStatus = table.Column<int>(type: "int", nullable: false),
            //        NewAccountName = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        NewBankAccountNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        NewCustomerBank = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        NewMonthlyIncome = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
            //        NewSubsector = table.Column<int>(type: "int", nullable: false),
            //        NewNOKName = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        NewNOKNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        NewNOKEmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        NewNOKAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        NewRelationship = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        BaseCreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        BaseCreatorId = table.Column<long>(type: "bigint", nullable: false),
            //        BaseProcessMenu = table.Column<long>(type: "bigint", nullable: false),
            //        BaseVersion = table.Column<int>(type: "int", nullable: false),
            //        BaseModifyTime = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        BaseModifierId = table.Column<long>(type: "bigint", nullable: false),
            //        BaseIsDelete = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_tbl_CustomerProfileUpdate", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "tbl_Department",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Company = table.Column<long>(type: "bigint", nullable: false),
            //        Branch = table.Column<long>(type: "bigint", nullable: true),
            //        Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Telephone = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Fax = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Principal = table.Column<long>(type: "bigint", nullable: true),
            //        Sort = table.Column<int>(type: "int", nullable: false),
            //        Remark = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        BaseCreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        BaseCreatorId = table.Column<long>(type: "bigint", nullable: false),
            //        BaseProcessMenu = table.Column<long>(type: "bigint", nullable: false),
            //        BaseVersion = table.Column<int>(type: "int", nullable: false),
            //        BaseModifyTime = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        BaseModifierId = table.Column<long>(type: "bigint", nullable: false),
            //        BaseIsDelete = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_tbl_Department", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "tbl_DiasporaUser",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "bigint", nullable: false),
            //        NIDCOMNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Surname = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        EmployerStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        BaseCreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        BaseCreatorId = table.Column<long>(type: "bigint", nullable: false),
            //        BaseProcessMenu = table.Column<long>(type: "bigint", nullable: false),
            //        BaseVersion = table.Column<int>(type: "int", nullable: false),
            //        BaseModifyTime = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        BaseModifierId = table.Column<long>(type: "bigint", nullable: false),
                //    BaseIsDelete = table.Column<int>(type: "int", nullable: false)
                //},
                //constraints: table =>
                //{
                //    table.PrimaryKey("PK_tbl_DiasporaUser", x => x.Id);
                //});

            migrationBuilder.CreateTable(
                name: "tbl_Employee",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Company = table.Column<long>(type: "bigint", nullable: false),
                    Branch = table.Column<long>(type: "bigint", nullable: false),
                    Department = table.Column<int>(type: "int", nullable: false),
                    NHFNumber = table.Column<long>(type: "bigint", nullable: false),
                    BVN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NIN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmploymentType = table.Column<int>(type: "int", nullable: false),
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
                    ContributionBranch = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Portrait = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    PortraitType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserType = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    BaseCreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BaseCreatorId = table.Column<long>(type: "bigint", nullable: false),
                    BaseProcessMenu = table.Column<long>(type: "bigint", nullable: false),
                    BaseVersion = table.Column<int>(type: "int", nullable: false),
                    BaseModifyTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BaseModifierId = table.Column<long>(type: "bigint", nullable: false),
                    BaseIsDelete = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Employee", x => x.Id);
                });

        //    migrationBuilder.CreateTable(
        //        name: "tbl_ETicket",
        //        columns: table => new
        //        {
        //            Id = table.Column<long>(type: "bigint", nullable: false),
        //            Branch = table.Column<int>(type: "int", nullable: true),
        //            NHFNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            MessageType = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            RequestNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            Subject = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            DateSent = table.Column<DateTime>(type: "datetime2", nullable: true),
        //            ApprovalStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            Approved = table.Column<int>(type: "int", nullable: false),
        //            Disapproved = table.Column<int>(type: "int", nullable: false),
        //            BaseCreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
        //            BaseCreatorId = table.Column<long>(type: "bigint", nullable: false),
        //            BaseProcessMenu = table.Column<long>(type: "bigint", nullable: false),
        //            BaseVersion = table.Column<int>(type: "int", nullable: false),
        //            BaseModifyTime = table.Column<DateTime>(type: "datetime2", nullable: false),
        //            BaseModifierId = table.Column<long>(type: "bigint", nullable: false),
        //            BaseIsDelete = table.Column<int>(type: "int", nullable: false)
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK_tbl_ETicket", x => x.Id);
        //        });

        //    migrationBuilder.CreateTable(
        //        name: "tbl_FeedBackForm",
        //        columns: table => new
        //        {
        //            Id = table.Column<long>(type: "bigint", nullable: false),
        //            Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            Subject = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            BaseCreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
        //            BaseCreatorId = table.Column<long>(type: "bigint", nullable: false),
        //            BaseProcessMenu = table.Column<long>(type: "bigint", nullable: false),
        //            BaseVersion = table.Column<int>(type: "int", nullable: false),
        //            BaseModifyTime = table.Column<DateTime>(type: "datetime2", nullable: false),
        //            BaseModifierId = table.Column<long>(type: "bigint", nullable: false),
        //            BaseIsDelete = table.Column<int>(type: "int", nullable: false)
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK_tbl_FeedBackForm", x => x.Id);
        //        });

        //    migrationBuilder.CreateTable(
        //        name: "tbl_InternetBankingUsers",
        //        columns: table => new
        //        {
        //            Id = table.Column<long>(type: "bigint", nullable: false),
        //            AccountNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            AccountName = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            CustomerCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
        //            Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            BaseCreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
        //            BaseCreatorId = table.Column<long>(type: "bigint", nullable: false),
        //            BaseProcessMenu = table.Column<long>(type: "bigint", nullable: false),
        //            BaseVersion = table.Column<int>(type: "int", nullable: false),
        //            BaseModifyTime = table.Column<DateTime>(type: "datetime2", nullable: false),
        //            BaseModifierId = table.Column<long>(type: "bigint", nullable: false),
        //            BaseIsDelete = table.Column<int>(type: "int", nullable: false)
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK_tbl_InternetBankingUsers", x => x.Id);
        //        });

        //    migrationBuilder.CreateTable(
        //        name: "tbl_LoanInitation",
        //        columns: table => new
        //        {
        //            Id = table.Column<long>(type: "bigint", nullable: false),
        //            LoanProduct = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            Principal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
        //            Rate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
        //            Tenor = table.Column<int>(type: "int", nullable: false),
        //            RepaymentPattern = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            LoanPurpose = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            DocumentTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            Files = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
        //            Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
        //            Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            BaseCreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
        //            BaseCreatorId = table.Column<long>(type: "bigint", nullable: false),
        //            BaseProcessMenu = table.Column<long>(type: "bigint", nullable: false),
        //            BaseVersion = table.Column<int>(type: "int", nullable: false),
        //            BaseModifyTime = table.Column<DateTime>(type: "datetime2", nullable: false),
        //            BaseModifierId = table.Column<long>(type: "bigint", nullable: false),
        //            BaseIsDelete = table.Column<int>(type: "int", nullable: false)
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK_tbl_LoanInitation", x => x.Id);
        //        });

        //    migrationBuilder.CreateTable(
        //        name: "tbl_LoanRepaymentEntity",
        //        columns: table => new
        //        {
        //            Id = table.Column<long>(type: "bigint", nullable: false),
        //            Totalamount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
        //            Valuedate = table.Column<DateTime>(type: "datetime2", nullable: false),
        //            Transactionid = table.Column<string>(type: "nvarchar(max)", nullable: false),
        //            Narration = table.Column<string>(type: "nvarchar(max)", nullable: false),
        //            Repaymentdate = table.Column<DateTime>(type: "datetime2", nullable: false),
        //            Month = table.Column<string>(type: "nvarchar(max)", nullable: false),
        //            Year = table.Column<string>(type: "nvarchar(max)", nullable: false),
        //            EmployerNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
        //            Lastname = table.Column<string>(type: "nvarchar(max)", nullable: false),
        //            Firstname = table.Column<string>(type: "nvarchar(max)", nullable: false),
        //            MiddleName = table.Column<string>(type: "nvarchar(max)", nullable: false),
        //            Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
        //            EmployeeNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
        //            Paymentoption = table.Column<string>(type: "nvarchar(max)", nullable: false),
        //            PaymentStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
        //            BaseCreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
        //            BaseCreatorId = table.Column<long>(type: "bigint", nullable: false),
        //            BaseProcessMenu = table.Column<long>(type: "bigint", nullable: false),
        //            BaseVersion = table.Column<int>(type: "int", nullable: false),
        //            BaseModifyTime = table.Column<DateTime>(type: "datetime2", nullable: false),
        //            BaseModifierId = table.Column<long>(type: "bigint", nullable: false),
        //            BaseIsDelete = table.Column<int>(type: "int", nullable: false)
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK_tbl_LoanRepaymentEntity", x => x.Id);
        //        });

        //    migrationBuilder.CreateTable(
        //        name: "tbl_LoanSchedule",
        //        columns: table => new
        //        {
        //            Id = table.Column<long>(type: "bigint", nullable: false),
        //            CreditId = table.Column<int>(type: "int", nullable: false),
        //            Customer = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            Product = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            AccountNo = table.Column<int>(type: "int", nullable: false),
        //            AmountGranted = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            ViewSchedule = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: true),
        //            StartPrincipalAmount = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            PeriodPaymentAmount = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            PeriodInterestAmount = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            PeriodPrincipalAmount = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            EndPrincipalAmount = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            BaseCreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
        //            BaseCreatorId = table.Column<long>(type: "bigint", nullable: false),
        //            BaseProcessMenu = table.Column<long>(type: "bigint", nullable: false),
        //            BaseVersion = table.Column<int>(type: "int", nullable: false),
        //            BaseModifyTime = table.Column<DateTime>(type: "datetime2", nullable: false),
        //            BaseModifierId = table.Column<long>(type: "bigint", nullable: false),
        //            BaseIsDelete = table.Column<int>(type: "int", nullable: false)
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK_tbl_LoanSchedule", x => x.Id);
        //        });

        //    migrationBuilder.CreateTable(
        //        name: "tbl_LogLogin",
        //        columns: table => new
        //        {
        //            Id = table.Column<int>(type: "int", nullable: false)
        //                .Annotation("SqlServer:Identity", "1, 1"),
        //            Company = table.Column<long>(type: "bigint", nullable: false),
        //            LogStatus = table.Column<int>(type: "int", nullable: false),
        //            IpAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            IpLocation = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            Browser = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            OS = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            Remark = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            ExtraRemark = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            BaseCreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
        //            BaseCreatorId = table.Column<long>(type: "bigint", nullable: false),
        //            BaseProcessMenu = table.Column<long>(type: "bigint", nullable: false)
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK_tbl_LogLogin", x => x.Id);
        //        });

        //    migrationBuilder.CreateTable(
        //        name: "tbl_LogOperate",
        //        columns: table => new
        //        {
        //            Id = table.Column<int>(type: "int", nullable: false)
        //                .Annotation("SqlServer:Identity", "1, 1"),
        //            Company = table.Column<long>(type: "bigint", nullable: false),
        //            LogStatus = table.Column<int>(type: "int", nullable: false),
        //            IpAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            IpLocation = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            Remark = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            LogType = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            BusinessType = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            ExecuteUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            ExecuteParam = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            ExecuteResult = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            ExecuteTime = table.Column<int>(type: "int", nullable: false),
        //            BaseCreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
        //            BaseCreatorId = table.Column<long>(type: "bigint", nullable: false),
        //            BaseProcessMenu = table.Column<long>(type: "bigint", nullable: false)
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK_tbl_LogOperate", x => x.Id);
        //        });

        //    migrationBuilder.CreateTable(
        //        name: "tbl_Menu",
        //        columns: table => new
        //        {
        //            Id = table.Column<long>(type: "bigint", nullable: false),
        //            Parent = table.Column<long>(type: "bigint", nullable: false),
        //            MenuName = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            MenuIcon = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            MenuUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            MenuTarget = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            MenuSort = table.Column<int>(type: "int", nullable: false),
        //            MenuType = table.Column<int>(type: "int", nullable: false),
        //            MenuStatus = table.Column<int>(type: "int", nullable: false),
        //            Authorize = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            ApprovalLevel = table.Column<int>(type: "int", nullable: false),
        //            Remark = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            BaseCreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
        //            BaseCreatorId = table.Column<long>(type: "bigint", nullable: false),
        //            BaseProcessMenu = table.Column<long>(type: "bigint", nullable: false),
        //            BaseVersion = table.Column<int>(type: "int", nullable: false),
        //            BaseModifyTime = table.Column<DateTime>(type: "datetime2", nullable: false),
        //            BaseModifierId = table.Column<long>(type: "bigint", nullable: false),
        //            BaseIsDelete = table.Column<int>(type: "int", nullable: false)
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK_tbl_Menu", x => x.Id);
        //        });

        //    migrationBuilder.CreateTable(
        //        name: "tbl_MenuAuthorize",
        //        columns: table => new
        //        {
        //            Id = table.Column<long>(type: "bigint", nullable: false),
        //            MenuId = table.Column<long>(type: "bigint", nullable: false),
        //            AuthorizeId = table.Column<long>(type: "bigint", nullable: false),
        //            AuthorizeType = table.Column<int>(type: "int", nullable: false),
        //            BaseCreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
        //            BaseCreatorId = table.Column<long>(type: "bigint", nullable: false),
        //            BaseProcessMenu = table.Column<long>(type: "bigint", nullable: false)
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK_tbl_MenuAuthorize", x => x.Id);
        //        });

        //    migrationBuilder.CreateTable(
        //        name: "tbl_NextOfKin",
        //        columns: table => new
        //        {
        //            Id = table.Column<int>(type: "int", nullable: false)
        //                .Annotation("SqlServer:Identity", "1, 1"),
        //            Company = table.Column<long>(type: "bigint", nullable: false),
        //            Employee = table.Column<long>(type: "bigint", nullable: false),
        //            FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            MobileNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            Relationship = table.Column<int>(type: "int", nullable: false),
        //            BaseCreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
        //            BaseCreatorId = table.Column<long>(type: "bigint", nullable: false),
        //            BaseProcessMenu = table.Column<long>(type: "bigint", nullable: false),
        //            BaseVersion = table.Column<int>(type: "int", nullable: false),
        //            BaseModifyTime = table.Column<DateTime>(type: "datetime2", nullable: false),
        //            BaseModifierId = table.Column<long>(type: "bigint", nullable: false),
        //            BaseIsDelete = table.Column<int>(type: "int", nullable: false)
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK_tbl_NextOfKin", x => x.Id);
        //        });

        //    migrationBuilder.CreateTable(
        //        name: "tbl_NHFCustomerRequest",
        //        columns: table => new
        //        {
        //            Id = table.Column<long>(type: "bigint", nullable: false),
        //            PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            AccountNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            NextOfKinName = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            MonthlyIncome = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
        //            RequestedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
        //            BaseCreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
        //            BaseCreatorId = table.Column<long>(type: "bigint", nullable: false),
        //            BaseProcessMenu = table.Column<long>(type: "bigint", nullable: false),
        //            BaseVersion = table.Column<int>(type: "int", nullable: false),
        //            BaseModifyTime = table.Column<DateTime>(type: "datetime2", nullable: false),
        //            BaseModifierId = table.Column<long>(type: "bigint", nullable: false),
        //            BaseIsDelete = table.Column<int>(type: "int", nullable: false)
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK_tbl_NHFCustomerRequest", x => x.Id);
        //        });

        //    migrationBuilder.CreateTable(
        //        name: "tbl_NHFRegCompany",
        //        columns: table => new
        //        {
        //            Id = table.Column<long>(type: "bigint", nullable: false),
        //            NHFEmployerNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            EffectiveDate = table.Column<DateTime>(type: "datetime2", nullable: true),
        //            Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            Remark = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            MaritalStatus = table.Column<int>(type: "int", nullable: false),
        //            Gender = table.Column<int>(type: "int", nullable: false),
        //            BVN = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            NIN = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            BankName = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            BankAccountNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            ContributionLocation = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            RegistrationLocation = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            ContactPerson = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            RCNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            Sector = table.Column<int>(type: "int", nullable: false),
        //            SubSector = table.Column<int>(type: "int", nullable: false),
        //            Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            Address2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            ContactPersonDesignation = table.Column<int>(type: "int", nullable: false),
        //            ContributionFrequency = table.Column<int>(type: "int", nullable: false),
        //            PostalAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            BaseCreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
        //            BaseCreatorId = table.Column<long>(type: "bigint", nullable: false),
        //            BaseProcessMenu = table.Column<long>(type: "bigint", nullable: false),
        //            BaseVersion = table.Column<int>(type: "int", nullable: false),
        //            BaseModifyTime = table.Column<DateTime>(type: "datetime2", nullable: false),
        //            BaseModifierId = table.Column<long>(type: "bigint", nullable: false),
        //            BaseIsDelete = table.Column<int>(type: "int", nullable: false)
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK_tbl_NHFRegCompany", x => x.Id);
        //        });

        //    migrationBuilder.CreateTable(
        //        name: "tbl_NHFRegUsers",
        //        columns: table => new
        //        {
        //            Id = table.Column<long>(type: "bigint", nullable: false),
        //            NHFEmployerNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            EffectiveDate = table.Column<DateTime>(type: "datetime2", nullable: true),
        //            Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            Remark = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            MaritalStatus = table.Column<int>(type: "int", nullable: false),
        //            Gender = table.Column<int>(type: "int", nullable: false),
        //            BVN = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            NIN = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            BankName = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            BankAccountNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            ContributionLocation = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            RegistrationLocation = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            ContactPerson = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            RCNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            Sector = table.Column<int>(type: "int", nullable: false),
        //            SubSector = table.Column<int>(type: "int", nullable: false),
        //            Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            Address2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            ContactPersonDesignation = table.Column<int>(type: "int", nullable: false),
        //            ContributionFrequency = table.Column<int>(type: "int", nullable: false),
        //            PostalAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            BaseCreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
        //            BaseCreatorId = table.Column<long>(type: "bigint", nullable: false),
        //            BaseProcessMenu = table.Column<long>(type: "bigint", nullable: false),
        //            BaseVersion = table.Column<int>(type: "int", nullable: false),
        //            BaseModifyTime = table.Column<DateTime>(type: "datetime2", nullable: false),
        //            BaseModifierId = table.Column<long>(type: "bigint", nullable: false),
        //            BaseIsDelete = table.Column<int>(type: "int", nullable: false)
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK_tbl_NHFRegUsers", x => x.Id);
        //        });

        //    migrationBuilder.CreateTable(
        //        name: "tbl_PaymentHistory",
        //        columns: table => new
        //        {
        //            Id = table.Column<long>(type: "bigint", nullable: false),
        //            NHFNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            Date = table.Column<DateTime>(type: "datetime2", nullable: true),
        //            Amount = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            RRR = table.Column<int>(type: "int", nullable: false),
        //            TransactionId = table.Column<int>(type: "int", nullable: false),
        //            Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            BaseCreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
        //            BaseCreatorId = table.Column<long>(type: "bigint", nullable: false),
        //            BaseProcessMenu = table.Column<long>(type: "bigint", nullable: false),
        //            BaseVersion = table.Column<int>(type: "int", nullable: false),
        //            BaseModifyTime = table.Column<DateTime>(type: "datetime2", nullable: false),
        //            BaseModifierId = table.Column<long>(type: "bigint", nullable: false),
        //            BaseIsDelete = table.Column<int>(type: "int", nullable: false)
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK_tbl_PaymentHistory", x => x.Id);
        //        });

        //    migrationBuilder.CreateTable(
        //        name: "tbl_PropertyGallery",
        //        columns: table => new
        //        {
        //            Id = table.Column<long>(type: "bigint", nullable: false),
        //            PropertyType = table.Column<int>(type: "int", nullable: false),
        //            PriceRangeMin = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
        //            PriceRangeMax = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
        //            Sort = table.Column<int>(type: "int", nullable: false),
        //            Location = table.Column<int>(type: "int", nullable: false),
        //            Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            BaseCreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
        //            BaseCreatorId = table.Column<long>(type: "bigint", nullable: false),
        //            BaseProcessMenu = table.Column<long>(type: "bigint", nullable: false),
        //            BaseVersion = table.Column<int>(type: "int", nullable: false),
        //            BaseModifyTime = table.Column<DateTime>(type: "datetime2", nullable: false),
        //            BaseModifierId = table.Column<long>(type: "bigint", nullable: false),
        //            BaseIsDelete = table.Column<int>(type: "int", nullable: false)
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK_tbl_PropertyGallery", x => x.Id);
        //        });

        //    migrationBuilder.CreateTable(
        //        name: "tbl_PropertyRegistration",
        //        columns: table => new
        //        {
        //            Id = table.Column<long>(type: "bigint", nullable: false),
        //            CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            CompanyNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            PropertyType = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            PropertyLocation = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            GeoTagging = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            DocumentTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            BaseCreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
        //            BaseCreatorId = table.Column<long>(type: "bigint", nullable: false),
        //            BaseProcessMenu = table.Column<long>(type: "bigint", nullable: false),
        //            BaseVersion = table.Column<int>(type: "int", nullable: false),
        //            BaseModifyTime = table.Column<DateTime>(type: "datetime2", nullable: false),
        //            BaseModifierId = table.Column<long>(type: "bigint", nullable: false),
        //            BaseIsDelete = table.Column<int>(type: "int", nullable: false)
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK_tbl_PropertyRegistration", x => x.Id);
        //        });

        //    migrationBuilder.CreateTable(
        //        name: "tbl_PropertySubscription",
        //        columns: table => new
        //        {
        //            Id = table.Column<long>(type: "bigint", nullable: false),
        //            PropertyType = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            PropertyLocation = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            GeoTagging = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            Developer = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            PropertyDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            ViewPictures = table.Column<byte>(type: "tinyint", nullable: true),
        //            BaseCreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
        //            BaseCreatorId = table.Column<long>(type: "bigint", nullable: false),
        //            BaseProcessMenu = table.Column<long>(type: "bigint", nullable: false),
        //            BaseVersion = table.Column<int>(type: "int", nullable: false),
        //            BaseModifyTime = table.Column<DateTime>(type: "datetime2", nullable: false),
        //            BaseModifierId = table.Column<long>(type: "bigint", nullable: false),
        //            BaseIsDelete = table.Column<int>(type: "int", nullable: false)
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK_tbl_PropertySubscription", x => x.Id);
        //        });

        //    migrationBuilder.CreateTable(
        //        name: "tbl_Refund",
        //        columns: table => new
        //        {
        //            Id = table.Column<long>(type: "bigint", nullable: false),
        //            NhfNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: true),
        //            EmployerNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            EmploymentDate = table.Column<DateTime>(type: "datetime2", nullable: true),
        //            BVN = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            NIN = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            MobileNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            ContactAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            EmployerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            ConditionForApplication = table.Column<int>(type: "int", nullable: false),
        //            BankName = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            BankAccountNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            BankCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            CustomerNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            Age = table.Column<int>(type: "int", nullable: false),
        //            YearOfService = table.Column<int>(type: "int", nullable: false),
        //            AvailableBalance = table.Column<int>(type: "int", nullable: false),
        //            DocumentsUpload = table.Column<int>(type: "int", nullable: false),
        //            Files = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
        //            BaseCreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
        //            BaseCreatorId = table.Column<long>(type: "bigint", nullable: false),
        //            BaseProcessMenu = table.Column<long>(type: "bigint", nullable: false),
        //            BaseVersion = table.Column<int>(type: "int", nullable: false),
        //            BaseModifyTime = table.Column<DateTime>(type: "datetime2", nullable: false),
        //            BaseModifierId = table.Column<long>(type: "bigint", nullable: false),
        //            BaseIsDelete = table.Column<int>(type: "int", nullable: false)
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK_tbl_Refund", x => x.Id);
        //        });

        //    migrationBuilder.CreateTable(
        //        name: "tbl_RiskAssessmentSetup",
        //        columns: table => new
        //        {
        //            Id = table.Column<long>(type: "bigint", nullable: false),
        //            CreditType = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            AssessmentFactors = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            Index = table.Column<int>(type: "int", nullable: false),
        //            IndexHead = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            IndexItem = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            Weight = table.Column<int>(type: "int", nullable: false),
        //            BaseCreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
        //            BaseCreatorId = table.Column<long>(type: "bigint", nullable: false),
        //            BaseProcessMenu = table.Column<long>(type: "bigint", nullable: false),
        //            BaseVersion = table.Column<int>(type: "int", nullable: false),
        //            BaseModifyTime = table.Column<DateTime>(type: "datetime2", nullable: false),
        //            BaseModifierId = table.Column<long>(type: "bigint", nullable: false),
        //            BaseIsDelete = table.Column<int>(type: "int", nullable: false)
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK_tbl_RiskAssessmentSetup", x => x.Id);
        //        });

        //    migrationBuilder.CreateTable(
        //        name: "tbl_Role",
        //        columns: table => new
        //        {
        //            Id = table.Column<long>(type: "bigint", nullable: false),
        //            Company = table.Column<long>(type: "bigint", nullable: false),
        //            Mode = table.Column<int>(type: "int", nullable: false),
        //            RoleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            RoleSort = table.Column<int>(type: "int", nullable: false),
        //            RoleStatus = table.Column<int>(type: "int", nullable: false),
        //            Remark = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            BaseCreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
        //            BaseCreatorId = table.Column<long>(type: "bigint", nullable: false),
        //            BaseProcessMenu = table.Column<long>(type: "bigint", nullable: false),
        //            BaseVersion = table.Column<int>(type: "int", nullable: false),
        //            BaseModifyTime = table.Column<DateTime>(type: "datetime2", nullable: false),
        //            BaseModifierId = table.Column<long>(type: "bigint", nullable: false),
        //            BaseIsDelete = table.Column<int>(type: "int", nullable: false)
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK_tbl_Role", x => x.Id);
        //        });

        //    migrationBuilder.CreateTable(
        //        name: "tbl_StatementOfAccount",
        //        columns: table => new
        //        {
        //            Id = table.Column<long>(type: "bigint", nullable: false),
        //            FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            ContributionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
        //            PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
        //            Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
        //            BaseCreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
        //            BaseCreatorId = table.Column<long>(type: "bigint", nullable: false),
        //            BaseProcessMenu = table.Column<long>(type: "bigint", nullable: false),
        //            BaseVersion = table.Column<int>(type: "int", nullable: false),
        //            BaseModifyTime = table.Column<DateTime>(type: "datetime2", nullable: false),
        //            BaseModifierId = table.Column<long>(type: "bigint", nullable: false),
        //            BaseIsDelete = table.Column<int>(type: "int", nullable: false)
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK_tbl_StatementOfAccount", x => x.Id);
        //        });

        //    migrationBuilder.CreateTable(
        //        name: "tbl_Underwriting",
        //        columns: table => new
        //        {
        //            Id = table.Column<long>(type: "bigint", nullable: false),
        //            CustomerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            ProductName = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            Tenor = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            InterestRate = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            LoanAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
        //            DocumentTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            DocumentUpload = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
        //            Comments = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            NextStafffLevel = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            CheckList = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            Remark = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            BaseCreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
        //            BaseCreatorId = table.Column<long>(type: "bigint", nullable: false),
        //            BaseProcessMenu = table.Column<long>(type: "bigint", nullable: false),
        //            BaseVersion = table.Column<int>(type: "int", nullable: false),
        //            BaseModifyTime = table.Column<DateTime>(type: "datetime2", nullable: false),
        //            BaseModifierId = table.Column<long>(type: "bigint", nullable: false),
        //            BaseIsDelete = table.Column<int>(type: "int", nullable: false)
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK_tbl_Underwriting", x => x.Id);
        //        });

        //    migrationBuilder.CreateTable(
        //        name: "tbl_UnlockAdminUser",
        //        columns: table => new
        //        {
        //            Id = table.Column<int>(type: "int", nullable: false)
        //                .Annotation("SqlServer:Identity", "1, 1"),
        //            MobileNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            AccountName = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
        //            Status = table.Column<int>(type: "int", nullable: false),
        //            BaseCreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
        //            BaseCreatorId = table.Column<long>(type: "bigint", nullable: false),
        //            BaseProcessMenu = table.Column<long>(type: "bigint", nullable: false),
        //            BaseVersion = table.Column<int>(type: "int", nullable: false),
        //            BaseModifyTime = table.Column<DateTime>(type: "datetime2", nullable: false),
        //            BaseModifierId = table.Column<long>(type: "bigint", nullable: false),
        //            BaseIsDelete = table.Column<int>(type: "int", nullable: false)
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK_tbl_UnlockAdminUser", x => x.Id);
        //        });

        //    migrationBuilder.CreateTable(
        //        name: "tbl_UnlockNhfPortal",
        //        columns: table => new
        //        {
        //            Id = table.Column<long>(type: "bigint", nullable: false),
        //            AccountNo = table.Column<string>(name: "Account No", type: "nvarchar(max)", nullable: true),
        //            AccountName = table.Column<string>(name: "Account Name", type: "nvarchar(max)", nullable: true),
        //            CustomerCode = table.Column<string>(name: "Customer Code", type: "nvarchar(max)", nullable: true),
        //            UnlockBy = table.Column<string>(name: "Unlock By", type: "nvarchar(max)", nullable: true),
        //            BaseCreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
        //            BaseCreatorId = table.Column<long>(type: "bigint", nullable: false),
        //            BaseProcessMenu = table.Column<long>(type: "bigint", nullable: false),
        //            BaseVersion = table.Column<int>(type: "int", nullable: false),
        //            BaseModifyTime = table.Column<DateTime>(type: "datetime2", nullable: false),
        //            BaseModifierId = table.Column<long>(type: "bigint", nullable: false),
        //            BaseIsDelete = table.Column<int>(type: "int", nullable: false)
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK_tbl_UnlockNhfPortal", x => x.Id);
        //        });

        //    migrationBuilder.CreateTable(
        //        name: "tbl_User",
        //        columns: table => new
        //        {
        //            Id = table.Column<int>(type: "int", nullable: false)
        //                .Annotation("SqlServer:Identity", "1, 1"),
        //            Company = table.Column<long>(type: "bigint", nullable: false),
        //            Employee = table.Column<long>(type: "bigint", nullable: false),
        //            UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            Salt = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            RealName = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            LoginCount = table.Column<int>(type: "int", nullable: false),
        //            UserStatus = table.Column<int>(type: "int", nullable: false),
        //            IsSystem = table.Column<int>(type: "int", nullable: false),
        //            IsOnline = table.Column<int>(type: "int", nullable: false),
        //            FirstVisit = table.Column<DateTime>(type: "datetime2", nullable: true),
        //            PreviousVisit = table.Column<DateTime>(type: "datetime2", nullable: true),
        //            LastVisit = table.Column<DateTime>(type: "datetime2", nullable: true),
        //            WebToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            ApiToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //            BaseCreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
        //            BaseCreatorId = table.Column<long>(type: "bigint", nullable: false),
        //            BaseProcessMenu = table.Column<long>(type: "bigint", nullable: false),
        //            BaseVersion = table.Column<int>(type: "int", nullable: false),
        //            BaseModifyTime = table.Column<DateTime>(type: "datetime2", nullable: false),
        //            BaseModifierId = table.Column<long>(type: "bigint", nullable: false),
        //            BaseIsDelete = table.Column<int>(type: "int", nullable: false)
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK_tbl_User", x => x.Id);
        //        });

        //    migrationBuilder.CreateTable(
        //        name: "tbl_UserBelong",
        //        columns: table => new
        //        {
        //            Id = table.Column<long>(type: "bigint", nullable: false),
        //            Company = table.Column<long>(type: "bigint", nullable: false),
        //            Employee = table.Column<long>(type: "bigint", nullable: false),
        //            Belong = table.Column<long>(type: "bigint", nullable: false),
        //            BelongType = table.Column<int>(type: "int", nullable: false),
        //            BaseCreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
        //            BaseCreatorId = table.Column<long>(type: "bigint", nullable: false),
        //            BaseProcessMenu = table.Column<long>(type: "bigint", nullable: false)
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK_tbl_UserBelong", x => x.Id);
        //        });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
        //    migrationBuilder.DropTable(
        //        name: "Finance_TblFinanceCounterpartyTransaction");

        //    migrationBuilder.DropTable(
        //        name: "Finance_TblFinanceTransaction");

        //    migrationBuilder.DropTable(
        //        name: "Refund_TblContributionrefundposting");

        //    migrationBuilder.DropTable(
        //        name: "Refund_TblRefundcondition");

        //    migrationBuilder.DropTable(
        //        name: "Refund_TblRefundprofiling");

        //    migrationBuilder.DropTable(
        //name: "Remita_TblRemitaPaymentDetails");

        //    migrationBuilder.DropTable(
        //        name: "st_AccountType");

        //    migrationBuilder.DropTable(
        //        name: "st_AgentType");

        //    migrationBuilder.DropTable(
        //        name: "st_AlertType");

        //    migrationBuilder.DropTable(
        //        name: "st_Bank");

        //    migrationBuilder.DropTable(
        //        name: "st_Checklist");

        //    migrationBuilder.DropTable(
        //        name: "st_CompanyClass");

        //    migrationBuilder.DropTable(
        //        name: "st_CompanyType");

            //migrationBuilder.DropTable(
            //    name: "st_ContributionFrequency");

            //migrationBuilder.DropTable(
            //    name: "st_CreditAssessmentFactorIndex");

            //migrationBuilder.DropTable(
            //    name: "st_CreditAssessmentIndex");

            //migrationBuilder.DropTable(
            //    name: "st_CreditAssessmentIndexTitle");

            //migrationBuilder.DropTable(
            //    name: "st_CreditAssessmentRiskFactor");

            //migrationBuilder.DropTable(
            //    name: "st_CreditType");

            //migrationBuilder.DropTable(
            //    name: "st_Designation");

            //migrationBuilder.DropTable(
            //    name: "st_Gender");

            //migrationBuilder.DropTable(
            //    name: "st_MaritalStatus");

            //migrationBuilder.DropTable(
            //    name: "st_Nationality");

            //migrationBuilder.DropTable(
            //    name: "st_Relation");

            //migrationBuilder.DropTable(
            //    name: "st_Sector");

            //migrationBuilder.DropTable(
            //    name: "st_State");

            //migrationBuilder.DropTable(
            //    name: "st_SubSector");

            //migrationBuilder.DropTable(
            //    name: "st_Title");

            //migrationBuilder.DropTable(
            //    name: "tbl_AccreditationFee");

            //migrationBuilder.DropTable(
            //    name: "tbl_AllNHFSubscriber");

            //migrationBuilder.DropTable(
            //    name: "tbl_ApprovalLog");

            //migrationBuilder.DropTable(
            //    name: "tbl_ApprovalSetup");

            //migrationBuilder.DropTable(
            //    name: "tbl_ApproveAgents");

            //migrationBuilder.DropTable(
            //    name: "tbl_ApproveEmployerAggregator");

            //migrationBuilder.DropTable(
            //    name: "tbl_AutoJob");

            //migrationBuilder.DropTable(
            //    name: "tbl_AutoJobLog");

            //migrationBuilder.DropTable(
            //    name: "tbl_Branch");

            //migrationBuilder.DropTable(
            //    name: "tbl_ChangeEmployer");

            //migrationBuilder.DropTable(
            //    name: "tbl_ChangePassword");

            //migrationBuilder.DropTable(
            //    name: "tbl_ChargeSetup");

            //migrationBuilder.DropTable(
            //    name: "tbl_Company");

            //migrationBuilder.DropTable(
            //    name: "tbl_Contribution");

            //migrationBuilder.DropTable(
            //    name: "tbl_ContributionHistory");

            //migrationBuilder.DropTable(
            //    name: "tbl_CreditScore");

            //migrationBuilder.DropTable(
            //    name: "tbl_CustomerProfileUpdate");

            //migrationBuilder.DropTable(
            //    name: "tbl_Department");

            //migrationBuilder.DropTable(
            //    name: "tbl_DiasporaUser");

            migrationBuilder.DropTable(
                name: "tbl_Employee");

            //migrationBuilder.DropTable(
            //    name: "tbl_ETicket");

            //migrationBuilder.DropTable(
            //    name: "tbl_FeedBackForm");

            //migrationBuilder.DropTable(
            //    name: "tbl_InternetBankingUsers");

            //migrationBuilder.DropTable(
            //    name: "tbl_LoanInitation");

            //migrationBuilder.DropTable(
            //    name: "tbl_LoanRepaymentEntity");

            //migrationBuilder.DropTable(
            //    name: "tbl_LoanSchedule");

            //migrationBuilder.DropTable(
            //    name: "tbl_LogLogin");

            //migrationBuilder.DropTable(
            //    name: "tbl_LogOperate");

            //migrationBuilder.DropTable(
            //    name: "tbl_Menu");

            //migrationBuilder.DropTable(
            //    name: "tbl_MenuAuthorize");

            //migrationBuilder.DropTable(
            //    name: "tbl_NextOfKin");

            //migrationBuilder.DropTable(
            //    name: "tbl_NHFCustomerRequest");

            //migrationBuilder.DropTable(
            //    name: "tbl_NHFRegCompany");

            //migrationBuilder.DropTable(
            //    name: "tbl_NHFRegUsers");

            //migrationBuilder.DropTable(
            //    name: "tbl_PaymentHistory");

            //migrationBuilder.DropTable(
            //    name: "tbl_PropertyGallery");

            //migrationBuilder.DropTable(
            //    name: "tbl_PropertyRegistration");

            //migrationBuilder.DropTable(
            //    name: "tbl_PropertySubscription");

            //migrationBuilder.DropTable(
            //    name: "tbl_Refund");

            //migrationBuilder.DropTable(
            //    name: "tbl_RiskAssessmentSetup");

            //migrationBuilder.DropTable(
            //    name: "tbl_Role");

            //migrationBuilder.DropTable(
            //    name: "tbl_StatementOfAccount");

            //migrationBuilder.DropTable(
            //    name: "tbl_Underwriting");

            //migrationBuilder.DropTable(
            //    name: "tbl_UnlockAdminUser");

            //migrationBuilder.DropTable(
            //    name: "tbl_UnlockNhfPortal");

            //migrationBuilder.DropTable(
            //    name: "tbl_User");

            //migrationBuilder.DropTable(
            //    name: "tbl_UserBelong");
        }
    }
}
