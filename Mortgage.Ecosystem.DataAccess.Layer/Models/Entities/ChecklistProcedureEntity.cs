using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Entities
{
    // Agent type table
    [Table("tbl_ChecklistProcedure")]
    public class ChecklistProcedureEntity : IdentityExtensionEntity
    {
        // Name
        [Column("CheckList")]
        public string? Checklist { get; set; }     

        // Code
        [Column("Remark")]
        public string? Remark { get; set; }

        [Column("NHFNumber")]
        public string? NHFNo { get; set; }

        [Column("BranchCode")]
        public string? BranchCode { get; set; }

        [Column("ProductCode")]
        public string? ProductCode { get; set; }

        [Column("Applicable")]
        public string? Applicable { get; set; }

        [Column("NonApplicable")]
        public string? NonApplicable { get; set; }
    }
}