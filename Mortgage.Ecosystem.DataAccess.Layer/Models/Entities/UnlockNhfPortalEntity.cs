using System.ComponentModel.DataAnnotations.Schema;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Base;

namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Entities
{
    // Company Information table
    [Table("tbl_UnlockNhfPortal")]
    public class UnlockNhfPortalEntity : BaseExtensionEntity
    {
        // Account No
        [Column("Account No")]
        public string? AccountNo { get; set; }

        // Account Name
        [Column("Account Name")]
        public string? AccountName { get; set; }

        // Customer Code
        [Column("Customer Code")]
        public string? CustomerCode { get; set; }

        // Unlock By
        [Column("Unlock By")]
        public string? UnlockBy { get; set; }

    }
}