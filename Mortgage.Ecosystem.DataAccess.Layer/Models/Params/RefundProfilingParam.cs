namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Params
{
    public class RefundProfilingListParam
    {
        public int Id { get; set; }
        public string NhfNo { get; set; }
        public string CustName { get; set; }
        public string ConditionToApply { get; set; }
        public string DocumentTitle { get; set; }
        public int? ApplicationType { get; set; }
        public string Comments { get; set; }
        public int LevelId { get; set; }
        public string CustomerNumber { get; set; }
        public string BranchCode { get; set; }
        public string BVN { get; set; }
        public string LevelName { get; set; }
        public int Disapprove { get; set; }
        public int OperationId { get; set; }
        public string BranchComment { get; set; }
        public string ManagerComment { get; set; }
        public string HeadOfficeComment { get; set; }
        public string UnitHeadComment { get; set; }
        public string IcandcComment { get; set; }
        public string ApprovalComment { get; set; }
        public string FinconComment { get; set; }
        public string BudgetsComment { get; set; }
        public string AuditUnitComment { get; set; }
        public string GroupHeadComment { get; set; }
        public string BankAccountNumber { get; set; }
        public string BankName { get; set; }
        public int? BankCode { get; set; }
        public Decimal? AmountApproved { get; set; }
        public decimal? InterestAmt { get; set; }

        public DateTime? ApplicationDate { get; set; }

    }
}
