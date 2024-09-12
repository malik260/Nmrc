using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Entities
{
    // Scheme Setup table
    [Table("st_NmrcEligibility")]
    public class NmrcEligibilityEntity : IdentityExtensionEntity
    {

        // Category
        [Column("Category")]
        public int Category { get; set; }

        // Name
        [Column("Item")]
        public string? Item { get; set; }

        // Description
        [Column("Description")]
        public string? Description { get; set; }

        [NotMapped]
        public string? CategoryName { get; set; }



    }
}