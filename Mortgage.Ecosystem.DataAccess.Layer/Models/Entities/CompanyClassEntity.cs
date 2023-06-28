using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Entities
{
    // Account type table
    [Table("st_CompanyClass")]
    public class CompanyClassEntity : IdentityExtensionEntity
    {
        // Name
        [Column("Name")]
        public string? Name { get; set; }

        // Description
        [Column("Description")]
        public string? Description { get; set; }
    }
}