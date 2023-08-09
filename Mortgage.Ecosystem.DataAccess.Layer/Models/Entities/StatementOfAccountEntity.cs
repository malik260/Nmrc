using System.ComponentModel.DataAnnotations.Schema;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Base;

namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Entities
{
    //AccreditationFee table
    [Table("tbl_StatementOfAccount")]
    public class StatementOfAccountEntity : BaseExtensionEntity
    {
        // First Name
        [Column("FirstName")]
        public string? FirstName { get; set; }
        // Last Name
        [Column("LastName")]
        public string? LastName { get; set; }

        // Contribution Date
        [Column("ContributionDate")]
        public DateTime ContributionDate { get; set; }

        // Payment Date
        [Column("PaymentDate")]
        public DateTime PaymentDate { get; set; }

        // Amount
        [Column("Amount")]
        public decimal Amount { get; set; }

    }
}