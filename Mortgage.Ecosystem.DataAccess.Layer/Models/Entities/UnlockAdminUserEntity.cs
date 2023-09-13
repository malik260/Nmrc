using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Base;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Entities
{
    // Account type table
    [Table("tbl_UnlockAdminUser")]
    public class UnlockAdminUserEntity : IdentityExtensionEntity
    {
        // Mobile No
        [Column("MobileNo"), Description("Mobile No")]
        public string? MobileNo { get; set; }

        // Account Name        
        [Column("AccountName"), Description("Account Name")]
        public string? AccountName { get; set; }

        // Date Created
        [Column("DateCreated"), Description("DateCreated")]
        public DateTime? DateCreated { get; set; }

        // Status
        [Column("Status"), Description("Status")]
        public int Status { get; set; }

    }
}