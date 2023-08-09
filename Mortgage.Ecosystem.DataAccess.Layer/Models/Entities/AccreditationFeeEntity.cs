using System.ComponentModel.DataAnnotations.Schema;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Base;

namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Entities
{
    //AccreditationFee table
    [Table("tbl_AccreditationFee")]
    public class AccreditationFeeEntity : BaseExtensionEntity
    {
        // Agent Name
        [Column("AgenName")]
        public string? AgenName { get; set; }
        // Phone Number
        [Column("MobileNumber")]
        public string? MobileNumber { get; set; }

        // Email address
        [Column("EmailAddress")]
        public string? EmailAddress { get; set; }

        // Fee Amount
        [Column("FeeAmount")]
        public decimal FeeAmount { get; set; }

        // Payment Option
        [Column("PaymentOption")]
        public int PaymentOption { get; set; }

    }
}