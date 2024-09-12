using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Entities
{
    // Agent type table
    [Table("tbl_SecondaryLenderChecklist")]
    public class SecondaryLenderChecklistEntity : IdentityExtensionEntity
    {
        // Name
        [Column("PmbId")]
        public string? PmbId { get; set; }     

        // Code
        [Column("Employee Nhf Number")]
        public string? EmployeeNhfNumber { get; set; }

        [Column("Item")]
        public string? Item { get; set; }

        [Column("Description")]
        public string? Description { get; set; }

        [Column("Applicable")]
        public bool? Applicable  { get; set; }

 
    }
}