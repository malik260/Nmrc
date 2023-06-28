using System.ComponentModel.DataAnnotations.Schema;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Base;

namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Entities
{
    // Approve Agents table
    [Table("tbl_ApproveAgents")]
    public class ApproveAgentsEntity : BaseExtensionEntity
    {
        // Approve Agents Full Name
        [Column("FullName")]
        public string? FullName { get; set; }

        // Approve Agents Company
        [Column("Company")]
        public string? Company { get; set; }

        // Approve Agents Company Number
        [Column("CompanyNumber")]
        public string? CompanyNumber { get; set; }

        // Approve Agents Email Address 
        [Column("Email")]
        public string? Email { get; set; }

        // Approve Agents Email Address 
        [Column("PhoneNo")]
        public string? PhoneNo { get; set; }

        // Approve Agents Date Of Incorporation
        [Column("DateOfIncorporation")]
        public DateTime? DateOfIncorporation { get; set; }

        // Approve Agents Status
        [Column("Status")]
        public string? Status { get; set; }

        // Approve Agents Remark
        [Column("Remark")]
        public string? Remark { get; set; }
    }
}

