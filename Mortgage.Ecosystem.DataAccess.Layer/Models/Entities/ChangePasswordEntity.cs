using System.ComponentModel.DataAnnotations.Schema;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Base;

namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Entities
{
    // Change Password table
    [Table("tbl_ChangePassword")]
    public class ChangePasswordEntity : BaseExtensionEntity
    {
        // Old Password
        [Column("OldPassword")]
        public string? OldPassword { get; set; }

        // New Password
        [Column("NewPassword")]
        public string? NewPassword { get; set; }

        // Confirm Password
        [Column("ConfirmPassword")]
        public string? ConfirmPassword { get; set; }  
        

    }
}