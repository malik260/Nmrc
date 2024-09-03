using System.ComponentModel.DataAnnotations.Schema;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Base;

namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Entities
{
    // Company Information table
    [Table("tbl_LoanInitiationUpload")]
    public class LoanInitiationUploadEntity : BaseExtensionEntity
    {
        // UserId
        [Column("Company")]
        public long Company { get; set; }

        // UserId
        [Column("Pmb")]
        public long Pmb { get; set; }

        // FileId
        [Column("FileId")]
        public long FileId { get; set; }

        // LoanId
        [Column("LoanId")]
        public long LoanId { get; set; }

        // NHFNo
        [Column("NHFNo")]
        public string? NHFNo { get; set; }

        // Images
        [Column("Images")]
        public string? Images { get; set; }

        // ImageSize
        [Column("Size")]
        public long Size { get; set; }

        //Image Type
        [Column("Type")]
        public string? Type { get; set; }

        //Label
        [Column("Label")]
        public string? Label { get; set; }


        [Column("filedata")]
        public byte[] filedata { get; set; }

    }
}