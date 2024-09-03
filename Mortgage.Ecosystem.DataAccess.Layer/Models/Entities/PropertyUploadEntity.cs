using System.ComponentModel.DataAnnotations.Schema;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Base;

namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Entities
{
    // Company Information table
    [Table("tbl_PropertyUpload")]
    public class PropertyUploadEntity : BaseExtensionEntity
    {
        // UserId
        [Column("Pmb")]
        public long Pmb { get; set; }

        // ParselId
        [Column("ParselId")]
        public long ParselId { get; set; }

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