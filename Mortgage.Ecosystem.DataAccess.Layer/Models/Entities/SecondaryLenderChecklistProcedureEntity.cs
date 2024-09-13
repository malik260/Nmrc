using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Entities
{
    // Agent type table
    [Table("tbl_SecondaryLenderChecklistProcedure")]
    public class SecondaryLenderChecklistProcedureEntity : IdentityExtensionEntity
    {
        // PMB ID
        [Column("PmbId")]
        public long PmbId { get; set; }     

        // NHF Number
        [Column("EmployeeNhfNumber")]
        public string? EmployeeNhfNumber { get; set; }

        // Item
        [Column("Item")]
        public string? Item { get; set; }

        // Description
        [Column("Description")]
        public string? Description { get; set; }

        // Applicable
        [Column("Applicable")]
        public bool Applicable { get; set; }

    }
}