using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Entities
{
    // Scheme Setup table
    [Table("st_SchemeLender")]
    public class SchemeLenderEntity : IdentityExtensionEntity
    {

       
        public int SchemeId { get; set; }
        
     
        public long LendersId { get; set; }
    }
}