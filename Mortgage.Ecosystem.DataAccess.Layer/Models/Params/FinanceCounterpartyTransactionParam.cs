namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Params
{
    public class FinanceCounterpartyTransactionListParam
    {
        public int TransactionId { get; set; }
        public DateTime? TransactionDate { get; set; }
        public string Ref { get; set; }
        public string CpId { get; set; }
        public int? DVolume { get; set; }
        public int? CVolume { get; set; }
        public decimal? DebitAmount { get; set; }
        public decimal? CreditAmount { get; set; }
        public string Username { get; set; }
        public int? AcctTransaction { get; set; }
        public string CustCode { get; set; }
        public string ProductCode { get; set; }
        public string Branch { get; set; }
        public string Coy { get; set; }
        public string FormNo { get; set; }
        public string BatchRef { get; set; }
        public DateTime? PostDate { get; set; }
        public string ApplicationId { get; set; }
        public int? Approved { get; set; }
        public int? Show { get; set; }
        public int IsReversed { get; set; }
        public string GlaccountId { get; set; }
        public DateTime? SystemDatetime { get; set; }
        public string OldAccountNo { get; set; }
        public string LegType { get; set; }
        public int? IsCleared { get; set; }
        public string Description { get; set; }
        public string TransactionType { get; set; }

    }
}
