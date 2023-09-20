using System.ComponentModel.DataAnnotations.Schema;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Base;

namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Entities
{
    // Approval Setup table
    [Table("tbl_ApprovalSetup")]
    public class ApprovalSetupEntity : BaseExtensionEntity
    {
        // Company
        [Column("Company")]
        public long Company { get; set; }

        // Branch department
        [Column("Branch")]
        public long Branch { get; set; }

        // MenuId
        [Column("MenuId")]
        public long MenuId { get; set; }

        // Authority (Approver)
        [Column("Authority")]
        public long Authority { get; set; }

        // Priority (Order of precedence)
        [Column("Priority")]
        public int Priority { get; set; }

        // Remark
        [Column("Remark")]
        public string? Remark { get; set; }

        // Menu type (2 for page, 3 for button)
        [NotMapped]
        public int MenuType { get; set; }

        [NotMapped]
        public string? CompanyName { get; set; }

        [NotMapped]
        public string? BranchName { get; set; }

        [NotMapped]
        public string? MenuName { get; set; }

        [NotMapped]
        public string? AuthorityName { get; set; }

        [NotMapped]
        public int ApprovalLevel { get; set; }

        [NotMapped]
        public long Authority1 { get; set; }

        [NotMapped]
        public long Authority2 { get; set; }

        [NotMapped]
        public long Authority3 { get; set; }

        [NotMapped]
        public string? Ids { get; set; }

        [NotMapped]
        public string? Authorities { get; set; }
    }
}