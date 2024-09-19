using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Entities
{
    // Lender Setup table
    [Table("st_Lender")]
    public class LenderSetupEntity : IdentityExtensionEntity
    {
       
        [Column("LenderCategory")]
        public int LenderCategory { get; set; }
        
        [Column("LenderTypeId")]
        public int? LenderTypeId { get; set; }

        [NotMapped]
        [Column("Lender")]
        public List<int>? Lender { get; set; }
    }
}