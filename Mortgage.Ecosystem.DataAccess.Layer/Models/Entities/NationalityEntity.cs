using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Entities
{
    // Nationality table
    [Table("st_Nationality")]
    public class NationalityEntity : IdentityExtensionEntity
    {
        // Code
        [Column("Code")]
        public string? Code { get; set; }

        // Short Name
        [Column("ShortName")]
        public string? ShortName { get; set; }

        // Full Name
        [Column("FullName")]
        public string? FullName { get; set; }

        // Capital
        [Column("Capital")]
        public string? Capital { get; set; }

        // Currency
        [Column("Currency")]
        public string? Currency { get; set; }
    }
}