using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Entities
{
    // Lender Setup table
    [Table("st_LenderType")]
    public class LenderTypeEntity : IdentityExtensionEntity
    {

       
        [Column("LenderType")]
        public string? LenderType { get; set; }
        
    }
}