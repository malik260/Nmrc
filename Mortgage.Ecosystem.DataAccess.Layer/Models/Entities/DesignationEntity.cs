using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Entities
{
    // Designation table
    [Table("st_Designation")]
    public class DesignationEntity : IdentityExtensionEntity
    {
        // Company
        [Column("Company")]
        public long Company { get; set; }

        // Name
        [Column("Name")]
        public string? Name { get; set; }

        // Sort
        [Column("Sort")]
        public int Sort { get; set; }

        // Status
        [Column("Status")]
        public int Status { get; set; }

        // Remark
        [Column("Remark")]
        public string? Remark { get; set; }
    }
}