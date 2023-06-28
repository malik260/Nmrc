using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Entities
{
    // State table
    [Table("st_State")]
    public class StateEntity : IdentityExtensionEntity
    {
        // Code
        [Column("Code")]
        public string? Code { get; set; }

        // Name
        [Column("Name")]
        public string? Name { get; set; }

        // Capital
        [Column("Capital")]
        public string? Capital { get; set; }

        // Narration
        [Column("Narration")]
        public string? Narration { get; set; }

        // Nationality
        [Column("Nationality")]
        public string? Nationality { get; set; }
    }
}