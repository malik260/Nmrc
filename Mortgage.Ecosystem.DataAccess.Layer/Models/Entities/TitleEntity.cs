using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Entities
{
    // Title table
    [Table("st_Title")]
    public class TitleEntity : IdentityExtensionEntity
    {
        // Name
        [Column("Name")]
        public string? Name { get; set; }

        // Description
        [Column("Description")]
        public string? Description { get; set; }
    }
}