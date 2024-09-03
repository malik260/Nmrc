using System.ComponentModel;
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
        [Column("OldEmployer")]
        public string? OldEmployer { get; set; }

        // Current Employer Number
        [Column("OldEmployerNumber")]
        public string? OldEmployerNo { get; set; }

        // New Employer
        [Column("NewEmployer")]
        public string? NewEmployer { get; set; }

        // New Employer Number
        [Column("NewEmployerNumber"), Description("Company")]
        public long Company { get; set; }

        // Date Created
        [Column("DateCreated")]
        public DateTime? DateCreated { get; set; }

        [NotMapped]
        public string? CompanyName { get; set; }

    }
}
