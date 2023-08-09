using System.ComponentModel.DataAnnotations.Schema;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Base;

namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Entities
{
    // Company Information table
    [Table("Finance_TblFinanceTransaction")]
    public class FinanceTransactionEntity : BaseExtensionEntity
    {

        [Column("TransactionDate")]
        public DateTime TransactionDate { get; set; }

        [Column("TransactonType")]
        public int? TransactionType { get; set; } 

        [Column("TransactonId")]
        public string TransactionId { get; set; }

        [Column("Description")]
        public string Description { get; set; }

        [Column("Ref")]
        public string Ref { get; set; }

        [Column("DebitAmt")]
        public decimal DebitAmt { get; set; }

        [Column("CreditAmt")]
        public decimal CreditAmt { get; set; }

        [Column("AccountId")]
        public string AccountId { get; set; }

        [Column("PostedBy")]
        public string PostedBy { get; set; }

        [Column("PostingTime")]
        public string PostingTime { get; set; }

        [Column("Approved")]
        public int Approved { get; set; }

        [Column("ApprovedBy")]
        public string ApprovedBy { get; set; }

        [Column("Saved")]
        public int? Saved { get; set; }

        [Column("Sbu")]
        public string Sbu { get; set; }

        [Column("Deleted")]
        public int Deleted { get; set; }

        [Column("DeletedBy")]
        public string DeletedBy { get; set; }

        [Column("ValueDate")]
        public DateTime? ValueDate { get; set; }

        [Column("SourceBranch")]
        public string SourceBranch { get; set; }

        [Column("DestinationBranch")]
        public string DestinationBranch { get; set; }

        [Column("ItemId")]
        public string ItemId { get; set; }

        [Column("MisCode")]
        public string MisCode { get; set; }

        [Column("LegType")]
        public string LegType { get; set; }

        [Column("BatchRef")]
        public string BatchRef { get; set; }

        [Column("ScoyCode")]
        public string ScoyCode { get; set; }

        [Column("LCurrencyCode")]
        public string LCurrencycode { get; set; }

        [Column("CurrencyRate")]
        public decimal? CurrencyRate { get; set; }

        [Column("ApplicationId")]
        public string ApplicationId { get; set; }

        [Column("NonbrAccountId")]
        public string Nonbraccountid { get; set; }

        [Column("IsReversed")]
        public int Isreversed { get; set; }
       
    }
}