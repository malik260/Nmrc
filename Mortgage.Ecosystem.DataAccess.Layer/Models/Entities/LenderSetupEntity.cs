using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Entities
{
    // Lender Setup table
    [Table("st_Lender")]
    public class LenderSetupEntity : IdentityExtensionEntity
    {
        [Key]
        [Column("LenderId")]
        public int LenderId { get; set; }
        // Name
        [Column("LenderName")]
        public string? LenderName { get; set; }
        
        [Column("LenderTypeId")]
        public string? LenderTypeId { get; set; }
    }
}