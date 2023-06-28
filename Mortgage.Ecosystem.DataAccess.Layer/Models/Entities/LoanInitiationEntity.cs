using System.ComponentModel.DataAnnotations.Schema;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Base;

namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Entities
{
    // Company Information table
    [Table("tbl_LoanInitation")]
    public class LoanInitiationEntity : BaseExtensionEntity
    {
        // Loan Product
        [Column("LoanProduct")]
        public string? LoanProduct { get; set; }

        // Customer Sector
        [Column("Sector")]
        public string? Sector { get; set; }

        // Loan Principal
        [Column("Principal")]
        public string? Principal { get; set; }

        // Loan Rate
        [Column("Rate")]
        public string? Rate { get; set; }

        // Loan Tenor
        [Column("Tenor")]
        public string? Tenor { get; set; }

        // Net Income Monthly
        [Column("MonthlyNetIncome")]
        public string? MonthlyNetIncome { get; set; }

        // Loan Repayment Pattern
        [Column("RepaymentPattern")]
        public string? RepaymentPattern { get; set; }

        // Loan Purpose
        [Column("LoanPurpose")]
        public string? LoanPurpose { get; set; }

        // Document Title
        [Column("DocumentTitle")]
        public string? DocumentTitle { get; set; }

        //  Loan Files
        [Column("Files")]
        public byte[]? Files { get; set; }

        // Loan Type
        [Column("TypeOfLoan")]
        public string? TypeOfLoan { get; set; }

        // Loan Amount
        [Column("Amount")]
        public string? Amount { get; set; }

        // Application Status
        [Column("Status")]
        public string? Status { get; set; }

        // Loan Reference Number
        [Column("ReferenceNumber")]
        public string? ReferenceNumber { get; set; }

    }
}