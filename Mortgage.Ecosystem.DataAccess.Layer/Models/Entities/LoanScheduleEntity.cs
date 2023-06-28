using System.ComponentModel.DataAnnotations.Schema;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Base;

namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Entities
{
    // Payment History table
    [Table("tbl_LoanSchedule")]
    public class LoanScheduleEntity : BaseExtensionEntity
    {
        // Payment NHFNumber
        [Column("CreditId")]
        public int CreditId { get; set; }

        // Payment Date
        [Column("Customer")]
        public string? Customer { get; set; }

        // Payment Amount 
        [Column("Product")]
        public string? Product { get; set; }

        // Payment RRR
        [Column("AccountNo")]
        public int AccountNo { get; set; }

        // Payment TransactionId
        [Column("AmountGranted")]
        public string? AmountGranted { get; set; }

        // Payment Status
        [Column("ViewSchedule")]
        public string? ViewSchedule { get; set; }

        // Payment Date
        [Column("PaymentDate")]
        public DateTime? PaymentDate { get; set; }

        // Principal Amount
        [Column("StartPrincipalAmount")]
        public string? StartPrincipalAmount { get; set; }

        // Payment Amount
        [Column("PeriodPaymentAmount")]
        public string? PeriodPaymentAmount { get; set; }

        // Interest Amount
        [Column("PeriodInterestAmount")]
        public string? PeriodInterestAmount { get; set; }

        // Period Principal Amount
        [Column("PeriodPrincipalAmount")]
        public string? PeriodPrincipalAmount { get; set; }

        // End Principal Amount
        [Column("EndPrincipalAmount")]
        public string? EndPrincipalAmount { get; set; }
    }
}

