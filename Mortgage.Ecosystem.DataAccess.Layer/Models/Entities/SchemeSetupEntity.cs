using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Entities
{
    // Scheme Setup table
    [Table("st_Scheme")]
    public class SchemeSetupEntity : IdentityExtensionEntity
    {

        // Name
        [Column("SchemeName")]
        public string? SchemeName { get; set; }

        [NotMapped]
        public List<long> LendersId { get; set; }

    }
}