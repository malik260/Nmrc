using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Entities
{
    // Agent type table
    [Table("tbl_AddDocumentProcedure")]
    public class AddDocumentProcedureEntity : IdentityExtensionEntity
    {
        // Name
        [Column("DocumentTitle")]
        public string? DocumentTitle { get; set; }

        // Microsoft Text Editor
        [Column("TextEditor")]
        public string? TextEditor { get; set; }

        // Comment
        [Column("Comment")]
        public string? Comment { get; set; }

        // LevelOfficer
        [Column("LevelOfficer")]
        public int LevelOfficer { get; set; }

        [Column("NHFNumber")]
        public string? NHFNo { get; set; }

        [Column("BranchCode")]
        public string? BranchCode { get; set; }

        [Column("ProductCode")]
        public string? ProductCode { get; set; }

        [Column("LoanId")]
        public string? LoanId { get; set; }

        // DocumentUploadFiles
        [Column("Files")]
        public byte[]? Files { get; set; }


    }
}