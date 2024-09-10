using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Base;

namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Entities
{
    // User table
    [Table("tbl_User")]
    public class UserEntity : IdentityExtensionEntity
    {
        // Company
        [Column("Company")]
        public long Company { get; set; }

        // Employee
        [Column("Employee")]
        public long Employee { get; set; }

        // Pmb
        [Column("Pmb")]
        public long Pmb { get; set; }

        

        [Column("Developer")]
        public long Developer { get; set; }

        // Username
        [Column("UserName"), Description("UserName")]
        public string? UserName { get; set; }

        // Password
        [Column("Password")]
        public string? Password { get; set; }

        // Password salt value
        [Column("Salt"), JsonIgnore]
        public string? Salt { get; set; }

        // Name
        [Column("RealName"), Description("RealName")]
        public string? RealName { get; set; }

        // Number of logins
        [Column("LoginCount")]
        public int LoginCount { get; set; }

        // User status (0 disable 1 enable)
        [Column("UserStatus")]
        public int UserStatus { get; set; }

        // System user (0 is not system while 1 is system [system user has all permissions])
        [Column("IsSystem")]
        public int IsSystem { get; set; }

        // online (0 is not Online, 1 is Online)
        [Column("IsOnline")]
        public int IsOnline { get; set; }

        // First login time
        [Column("FirstVisit")]
        public DateTime? FirstVisit { get; set; }

        // Last login time
        [Column("PreviousVisit")]
        public DateTime? PreviousVisit { get; set; }

        // Last login time
        [Column("LastVisit")]
        public DateTime? LastVisit { get; set; }

        // Background Token
        [Column("WebToken")]
        public string? WebToken { get; set; }

        // ApiToken
        [Column("ApiToken")]
        public string? ApiToken { get; set; }

        // Company Name
        [NotMapped]
        public string? CompanyName { get; set; }

        // Company Name
        [NotMapped]
        public long Branch { get; set; }

        // Company Name
        [NotMapped]
        public long Dept { get; set; }

        // Designation
        [NotMapped]
        public string? DesignationIds { get; set; }

        // Role
        [NotMapped]
        public string? RoleId { get; set; }

        [NotMapped]
        public string? RoleIds { get; set; }

        [NotMapped]
        public string? DecryptedPassword { get; set; }
    }
}