using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Entities
{
    // Agent type table
    [Table("tbl_AuditTrail")]
    public class AuditTrailEntity : IdentityExtensionEntity
    {

        // User Name
        [Column("User Name")]
        public string? UserName { get; set; }

        // Company
        [Column("Company")]
        public string? Company { get; set; }

        [Column("Browser")]
        public string? Browser { get; set; }

        [Column("TargetUserName")]
        public string? TargetUserName { get; set; }

         [Column("TargetUserId")]
        public string? TargetUserId { get; set; }


        // Action
        [Column("Action")]
        public string? Action { get; set; }

        // Action Route
        [Column("Action Route")]
        public string? ActionRoute { get; set; }

        // Ip Address
        [Column("IP Address")]
        public string? IpAddress { get; set; }

        // Mac Address
        [Column("Mac Address")]
        public string? MacAddress { get; set; }

        // Transaction Date
        [Column("Transaction Date")]
        public string? TransactionDate { get; set; }

        [NotMapped]
        [Column("Audit Trail Id")]
        public string? AdulTrailId { get; set; }
    }
}