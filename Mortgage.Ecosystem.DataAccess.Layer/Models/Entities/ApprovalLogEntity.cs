using System.ComponentModel.DataAnnotations.Schema;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Base;

namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Entities
{
    // Approval Log table
    [Table("tbl_ApprovalLog")]
    public class ApprovalLogEntity : IdentityExtensionEntity
    {
        // Company
        [Column("Company")]
        public long Company { get; set; }

        // Branch department
        [Column("Branch")]
        public long? Branch { get; set; }

        // MenuId
        [Column("MenuId")]
        public long MenuId { get; set; }

        // Menu type (2 for page, 3 for button)
        [Column("MenuType")]
        public int MenuType { get; set; }

        // Authority (Approver)
        [Column("Authority")]
        public long Authority { get; set; }

        // Approval count
        [Column("ApprovalCount")]
        public int ApprovalCount { get; set; }

        [Column("ApprovalLevel")]
        public int ApprovalLevel { get; set; }

        // Status (1 Rejected, 2 Cancelled, 3 Approved)
        [Column("Status")]
        public int Status { get; set; }

        // Remark
        [Column("Remark")]
        public string? Remark { get; set; }
    }
}