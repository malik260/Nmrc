using System.ComponentModel;

namespace Mortgage.Ecosystem.DataAccess.Layer.Enums
{
    public enum LoanRepaymentEnum
    {
        [Description("Single Loan Repayment")]
        SingleLoanRepayment = 1,

        [Description("Batch Loan Repayment")]
        BatchLoanRepayment = 2,

    }
}