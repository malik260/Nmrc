using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Entities
{
    // Sub sector table
    [Table("st_SubSector")]
    public class SubSectorEntity : IdentityExtensionEntity
    {
        // Sector
        [Column("Sector")]
        public int Sector { get; set; }

        // Name
        [Column("Name")]
        public string? Name { get; set; }

        // Description
        [Column("Description")]
        public string? Description { get; set; }
    }
}