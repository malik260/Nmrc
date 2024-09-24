using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Base;

namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Entities
{
    // Refinance Information table
    [Table("tbl_Refinancing")]
    public class RefinancingEntity : BaseExtensionEntity
    {
        // Lender
        [Column("LenderID")]
        public long LenderID { get; set; }

        // NHF Number
        [Column("NHFNumber")]
        public string? NHFNumber { get; set; }

        // Refinance Number
        [Column("RefinanceNumber")]
        public string? RefinanceNumber { get; set; }

        [Column("Amount")]
        public decimal? Amount { get; set; }

        [Column("TotalAmount")]
        public decimal? TotalAmount { get; set; }

        [Column("LoanId")]
        public string? LoanId { get; set; }

        [Column("Status")]
        public int? Status { get; set; }

        [Column("ApplicationDate")]
        public DateTime? ApplicationDate { get; set; }

        [Column("Tenor")]
        public int? Tenor { get; set; }

        [Column("Rate")]
        public int? Rate { get; set; }

        [Column("PmbId")]
        public long PmbId { get; set; }

          [Column("ProductCode")]
        public string? ProductCode { get; set; }

        [NotMapped]
        public string? CustomerName { get; set; }
        [NotMapped]
        public string? MortgageBank { get; set; }

         [NotMapped]
        public string? ProductName { get; set; }



    }
}