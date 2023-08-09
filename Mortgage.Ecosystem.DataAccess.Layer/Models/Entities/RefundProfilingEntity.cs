using System.ComponentModel.DataAnnotations.Schema;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Base;

namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Entities
{
    // Company Information table
    [Table("Refund_TblRefundprofiling")]
    public class RefundProfilingEntity : BaseExtensionEntity
    {
        [Column("NhfNo")]
        public string NhfNo { get; set; }

        [Column("CustName")]
        public string CustName { get; set; }

        [Column("ConditionToApply")]
        public string Conditiontoapply { get; set; }

        [Column("DocumentTitle")]
        public string DocumentTitle { get; set; }

        [Column("ApplicationType")]
        public int? ApplicationType { get; set; }

        [Column("Comments")]
        public string Comments { get; set; }

        [Column("LevelId")]
        public int LevelId { get; set; }

        [Column("CustomerNumber")]
        public string CustomerNumber { get; set; }

        [Column("BranchCode")]
        public string BranchCode { get; set; }

        [Column("BVN")]
        public string BVN { get; set; }

        [Column("LevelName")]
        public string LevelName { get; set; }

        [Column("Disapprove")]
        public int Disapprove { get; set; }

        [Column("OperationId")]
        public int OperationId { get; set; }

        [Column("BranchComment")]
        public string BranchComment { get; set; }

        [Column("ManagerComment")]
        public string ManagerComment { get; set; }

        [Column("HeadOfficeComment")]
        public string HeadOfficeComment { get; set; }

        [Column("UnitHeadComment")]
        public string UnitHeadComment { get; set; }

        [Column("IcandcComment")]
        public string IcandcComment { get; set; }

        [Column("ApprovalComment")]
        public string ApprovalComment { get; set; }

        [Column("FinconComment")]
        public string FinconComment { get; set; }

        [Column("BudgetsComment")]
        public string BudgetsComment { get; set; }

        [Column("AuditUnitComment")]
        public string AuditUnitComment { get; set; }

        [Column("GroupHeadComment")]
        public string GroupHeadComment { get; set; }

        [Column("BankAccountNumber")]
        public string BankAccountNumber { get; set; }

        [Column("BankName")]
        public string BankName { get; set; }

        [Column("BankCode")]
        public int? BankCode { get; set; }

        [Column("AmountApproved")]
        public Decimal? AmountApproved { get; set; }

        [Column("InterestAmt")]
        public decimal? InterestAmt { get; set; }

        [Column("ApplicationDate")]

        public DateTime? ApplicationDate { get; set; }
    }
}