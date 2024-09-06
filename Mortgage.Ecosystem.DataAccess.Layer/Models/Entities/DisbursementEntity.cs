using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Base;

namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Entities
{
    // Loan Review table
    [Table("tbl_Disbursement")]
    public class DisbursementEntity : BaseExtensionEntity
    {
        // Lender
        [Column("LenderID")]
        public string? LenderID { get; set; }

        // NHF Number
        [Column("NHFNumber")]
        public string? NHFNumber { get; set; }

        // Refinance Number
        [Column("RefinanceNumber")]
        public string? RefinanceNumber { get; set; }

        // Reference Number
        [Column("LoanReferenceNumber")]
        public string? LoanReferenceNumber { get; set; }

        [Column("Amount")]
        public string? Amount { get; set; }

        [Column("Loan")]
        public string? Loan { get; set; }

        [Column("Status")]
        public string? Status { get; set; }

        [Column("ApplicationDate")]
        public string? ApplicationDate { get; set; }



    }
}