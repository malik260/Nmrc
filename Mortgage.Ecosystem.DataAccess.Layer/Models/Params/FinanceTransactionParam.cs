namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Params
{
    public class FinanceTransactionListParam
    {
        public DateTime TransactionDate { get; set; }
        public int? TransactionType { get; set; }
        public string Description { get; set; }
        public string Ref { get; set; }
        public decimal DebitAmt { get; set; }
        public decimal CreditAmt { get; set; }
        public string AccountId { get; set; }
        public string PostedBy { get; set; }
        public string PostingTime { get; set; }
        public int Approved { get; set; }
        public string ApprovedBy { get; set; }
        public int? Saved { get; set; }
        public string Sbu { get; set; }
        public int Deleted { get; set; }
        public string DeletedBy { get; set; }
        public DateTime? ValueDate { get; set; }
        public string SourceBranch { get; set; }
        public string DestinationBranch { get; set; }
        public string ItemId { get; set; }
        public string MisCode { get; set; }
        public string LegType { get; set; }
        public string BatchRef { get; set; }
        public string ScoyCode { get; set; }
        public string LCurrencycode { get; set; }
        public decimal? CurrencyRate { get; set; }
        public string ApplicationId { get; set; }
        public string NonbraccountId { get; set; }
        public int IsReversed { get; set; }
        public int Id { get; set; }

    }
}
