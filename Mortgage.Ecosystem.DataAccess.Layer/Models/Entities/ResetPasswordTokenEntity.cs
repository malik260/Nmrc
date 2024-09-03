using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Entities
{
    // Reset Password Token table
    [Table("tbl_ResetPasswordToken")]
    public class ResetPasswordTokenEntity : IdentityExtensionEntity
    {
        // Email Address
        [Column("EmailAddress")]
        public string? EmailAddress { get; set; }

        // Token
        [Column("PasswordToken")]
        public string? PasswordToken { get; set; }

    }
}