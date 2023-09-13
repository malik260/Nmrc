using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Entities
{
    // Agent type table
    [Table("st_Checklist")]
    public class ChecklistEntity : IdentityExtensionEntity
    {
        // Name
        [Column("ProductName")]
        public string? ProductName { get; set; }

        [Column("ProductCode")]
        public string? ProductCode { get; set; }

        // Description
        [Column("Checklist")]
        public string? Checklist { get; set; }

        // Code
        [Column("Remark")]
        public string? Remark { get; set; }
    }
}