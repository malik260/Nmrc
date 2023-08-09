using System.ComponentModel.DataAnnotations.Schema;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Base;

namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Entities
{
    // Company Information table
    [Table("tbl_ApproveEmployerAggregator")]
    public class ApproveEmployerAggregatorEntity : BaseExtensionEntity
    {
        // Approve Employer name
        [Column("Name")]
        public string? EmployerName { get; set; }

        // Approve Employer phone
        [Column("MobileNumber")]
        public string? MobileNumber { get; set; }

        // Approve Employer Email
        [Column("Email")]
        public string? Email { get; set; }

        // Approve Employer Date Email
        [Column("Date")]
        public DateTime? Date { get; set; }

        // Contribution Branch
        [Column("ContributionBranch")]
        public string? ContributionBranch { get; set; }

        // Contribution Frequency
        [Column("ContributionFrequency")]
        public string? ContributionFrequency { get; set; }

        // Remark
        [Column("Remark")]
        public string? Remark { get; set; }
    }
}