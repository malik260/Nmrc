using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Entities
{
    // Bank table
    [Table("st_Bank")]
    public class BankEntity : IdentityExtensionEntity
    {
        // Code
        [Column("Code")]
        public string? Code { get; set; }

        // Name
        [Column("Name")]
        public string? Name { get; set; }

        // Description
        [Column("Description")]
        public string? Description { get; set; }
    }
}