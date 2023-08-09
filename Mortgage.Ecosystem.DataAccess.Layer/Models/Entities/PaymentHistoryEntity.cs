using System.ComponentModel.DataAnnotations.Schema;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Base;

namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Entities
{
    // Payment History table
    [Table("tbl_PaymentHistory")]
    public class PaymentHistoryEntity : BaseExtensionEntity
    {
        // Payment NHFNumber
        [Column("NHFNumber")]
        public string? NHFNumber { get; set; }

        // Payment Date
        [Column("Date")]
        public DateTime? Date { get; set; }

        // Payment Amount 
        [Column("Amount")]
        public string? Amount { get; set; }

        // Payment RRR
        [Column("RRR")]
        public int RRR { get; set; }

        // Payment TransactionId
        [Column("TransactionId")]
        public int TransactionId { get; set; }

        // Payment Status
        [Column("Status")]
        public string? Status { get; set; }
    }
}

