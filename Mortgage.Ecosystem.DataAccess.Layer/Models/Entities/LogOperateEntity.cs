using System.ComponentModel.DataAnnotations.Schema;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Base;

namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Entities
{
    // Operation log table
    [Table("tbl_LogOperate")]
    public class LogOperateEntity : IdentityCreateEntity
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

        // Remark
        [Column("Remark")]
        public string? Remark { get; set; }

        // Log type (not used yet)
        [Column("LogType")]
        public string? LogType { get; set; }

        // Business type (not used yet)
        [Column("BusinessType")]
        public string? BusinessType { get; set; }

        // Page address
        [Column("ExecuteUrl")]
        public string? ExecuteUrl { get; set; }

        // Request parameters
        [Column("ExecuteParam")]
        public string? ExecuteParam { get; set; }

        // Request result
        [Column("ExecuteResult")]
        public string? ExecuteResult { get; set; }

        // Execution time
        [Column("ExecuteTime")]
        public int ExecuteTime { get; set; }

        // User name
        [NotMapped]
        public string? UserName { get; set; }

        // Department
        [NotMapped]
        public string? Dept { get; set; }

        // Department
        [NotMapped]
        public string? DeptName { get; set; }

    }
}