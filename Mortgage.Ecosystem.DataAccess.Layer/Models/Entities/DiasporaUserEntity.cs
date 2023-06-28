using System.ComponentModel.DataAnnotations.Schema;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Base;

namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Entities
{
    // Loan Initiation table
    [Table("tbl_DiasporaUser")]
    public class DiasporaUserEntity : BaseExtensionEntity
    {
        // NIDCOMNumber
        [Column("NIDCOMNumber")]
        public string? NIDCOMNumber { get; set; }

        // Surname
        [Column("Surname")]
        public string? Surname { get; set; }

        // FirstName
        [Column("FirstName")]
        public string? FirstName { get; set; }
        // EmployerStatus
        [Column("EmployerStatus")]
        public string? EmployerStatus { get; set; }
        

    }
}