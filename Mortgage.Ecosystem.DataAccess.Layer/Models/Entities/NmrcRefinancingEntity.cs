using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Entities
{
    [Table("tbl_NmrcRefinancing")]

    public class NmrcRefinancingEntity
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

        [Column("ApplicationStatus")]
        public int? ApplicationStatus { get; set; }
        
        [Column("Reviewed")]
        public int? Reviewed { get; set; } 
        
        [Column("Checklisted")]
        public int? Checklisted { get; set; }
        
        [Column("Disbursed")]
        public int? Disbursed { get; set; }
    }
}
