using System.ComponentModel.DataAnnotations.Schema;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Base;

namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Entities
{
    // Login log table
    [Table("tbl_LogLogin")]
    public class LogLoginEntity : IdentityCreateEntity
    {
        // Company
        [Column("Company")]
        public long Company { get; set; }

        // Execution status (0 failed, 1 successful)
        [Column("LogStatus")]
        public int LogStatus { get; set; }

        // IP address
        [Column("IpAddress")]
        public string? IpAddress { get; set; }

        // IP location
        [Column("IpLocation")]
        public string? IpLocation { get; set; }

        // Browser
        [Column("Browser")]
        public string? Browser { get; set; }

        // Operating system
        [Column("OS")]
        public string? OS { get; set; }

        // Remark
        [Column("Remark")]
        public string? Remark { get; set; }

        // Additional remarks
        [Column("ExtraRemark")]
        public string? ExtraRemark { get; set; }

        // User name
        [NotMapped]
        public string? UserName { get; set; }
    }
}