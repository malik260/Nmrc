using System.ComponentModel.DataAnnotations.Schema;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Base;

namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Entities
{
    // Company Information table
    [Table("tbl_ChangeEmployer")]
    public class ChangeEmployerEntity : BaseExtensionEntity
    {
        // NHF Number
        [Column("NhfNumber")]
        public string? NhfNumber { get; set; }

        // Current Employer
        [Column("CurrentEmployer")]
        public string? CurrentEmployer { get; set; }

        // Current Employer Number
        [Column("CurrentEmployerNo")]
        public string? CurrentEmployerNo { get; set; }

        // New Employer
        [Column("NewEmployer")]
        public string? NewEmployer { get; set; }

        // Employer Number
        [Column("OldEmployerNumber")]
        public string? OldEmployerNo { get; set; }

        // Date Created
        [Column("DateCreated")]
        public DateTime? DateCreated { get; set; }

    }
}
