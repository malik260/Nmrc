using System.ComponentModel.DataAnnotations.Schema;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Base;

namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Entities
{
    // Company Information table
    [Table("Refund_TblContributionrefundposting")]
    public class ContributionRefundPostingEntity : BaseExtensionEntity
    {
        [Column("LedgeDr")]
        public string LedgerDr { get; set; }

        [Column("Ledgercr")]
        public string LedgerCr { get; set; }

        [Column("Postingtype")]
        public string PostingType { get; set; }
    }
}