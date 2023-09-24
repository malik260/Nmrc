using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Entities
{
    // RiskAssessment type table
    [Table("tbl_RiskAssessmentProcedure")]
    public class RiskAssessmentProcedureEntity : IdentityExtensionEntity
    {
        // Name
        [Column("RiskId")]
        public string? RiskId { get; set; }     

        // NHF NUMBER
        [Column("NHFNumber")]
        public string? NHFNo { get; set; }

        [Column("BranchCode")]
        public string? BranchCode { get; set; }

        [Column("AverageScore")]
        public decimal AverageScore { get; set; }

        [Column("Rating")]
        public string? Rating { get; set; }

        [Column("Remark")]
        public string? Remark { get; set; }

        [Column("InterestRate")]
        public decimal InterestRate { get; set; }

        [Column("ApprovedBy")]
        public string? ApprovedBy { get; set; }

        [Column("Comment")]
        public string? Comment { get; set; }

        [Column("Status")]
        public string? Status { get; set; }

        [Column("RiskOfficer")]
        public string? RiskOfficer { get; set; }

        [Column("Date")]
        public DateTime Date { get; set; }
    }
}