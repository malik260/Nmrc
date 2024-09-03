using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
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

        // Loan Principal
        [Column("Principal")]
        public decimal Principal { get; set; }

        // Loan Rate
        [Column("Rate")]
        public decimal Rate { get; set; }

        // Loan Tenor
        [Column("Tenor")]
        public int Tenor { get; set; }

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

        // Loan Amount
        [Column("Amount")]
        public decimal Amount { get; set; }

        // Application Status
        [Column("Status")]
        public string? Status { get; set; }

        // NHF Number
        [Column("NHFNumber")]
        public string? NHFNumber { get; set; }

        // NHF Number
        [Column("PMB")]
        public string? PMB { get; set; }

        [Column("ApplicationReferenceNo")]
        public string? ApplicationReferenceNo { get; set; }

        [NotMapped]
        [Column("file")]
        public List<IFormFile>? file { get; set; }



    }
}