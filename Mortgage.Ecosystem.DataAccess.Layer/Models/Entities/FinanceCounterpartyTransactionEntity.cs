using System.ComponentModel.DataAnnotations.Schema;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Base;

namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Entities
{
    // Company Information table
    [Table("Finance_TblFinanceCounterpartyTransaction")]
    public class FinanceCounterpartyTransactionEntity : BaseExtensionEntity
    {
        [Column("TransactionId")]
        public string? TransactionId { get; set; }

        [Column("TransactionDate")]
        public DateTime? TransactionDate { get; set; }

        [Column("Ref")]
        public string Ref { get; set; }

        [Column("CpId")]
        public string CpId { get; set; }

        [Column("DVolume")]
        public int? Dvolume { get; set; }

        [Column("CVolume")]
        public int? Cvolume { get; set; }

        [Column("DebitAmount")]
        public decimal? DebitAmount { get; set; }

        [Column("CreditAmount")]
        public decimal? CreditAmount { get; set; }

        [Column("Username")]
        public string Username { get; set; }

        [Column("AcctTransaction")]
        public int? AcctTransaction { get; set; }

        [Column("CustCode")]
        public string CustCode { get; set; }

        [Column("ProductCode")]
        public string ProductCode { get; set; }

        [Column("Branch")]
        public string Branch { get; set; }

        [Column("Coy")]
        public string Coy { get; set; }

        [Column("FormNo")]
        public string FormNo { get; set; }

        [Column("BatchRef")]
        public string BatchRef { get; set; }

        [Column("PostDate")]
        public DateTime? PostDate { get; set; }

        [Column("ApplicationId")]
        public string ApplicationId { get; set; }

        [Column("Approved")]
        public int? Approved { get; set; }

        [Column("Show")]
        public int? Show { get; set; }

        [Column("IsReversed")]
        public int IsReversed { get; set; }

        [Column("GlAccountId")]
        public string GlAccountId { get; set; }

        [Column("SystemDatetime")]
        public DateTime? SystemDatetime { get; set; }

        [Column("OldAccountNo")]
        public string OldAccountNo { get; set; }

        [Column("LegType")]
        public string LegType { get; set; }

        [Column("IsCleared")]
        public int? IsCleared { get; set; }

        [Column("Description")]
        public string Description { get; set; }

        [Column("TransactionType")]
        public string TransactionType { get; set; }
    }
}