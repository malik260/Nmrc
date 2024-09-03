using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels
{
    public class ErrorListVM
    {
        public int SN { get; set; }
        public string? Error { get; set; }
    }


    public class AffordabilityDetailss
    {
        public int productId { get; set; }
        public int casaAccountId { get; set; }
        public int age { get; set; }
        public int yearsInService { get; set; }
        public int repaymentPeriod { get; set; }
        public decimal presentValue { get; set; }
        public decimal? affordableAmount { get; set; }
        public double monthlyRepayment { get; set; }
        public double profitability { get; set; }
        public decimal rate { get; set; }
        public decimal? amountRequested { get; set; }
        public bool tenorOverride { get; set; }
    }

    public class LoanApplicationDetailss
    {
        public string customerCode { get; set; }
        public int proposedProductId { get; set; }
        public int proposedTenor { get; set; }
        public decimal proposedAmount { get; set; }
        public int subSectorId { get; set; }
        public string loanPurpose { get; set; }
        public string operatingAccountNo { get; set; }
        public decimal requestedAmount { get; set; }
        public string repaymentDate { get; set; }
        public int creditScore { get; set; }
        public string creditRating { get; set; }
        public AffordabilityDetailss affordabilityDetails { get; set; }
    }

    public class LoanApplicationsVM
    {
        public string customerCode { get; set; }
        public int loanApplicationSourceId { get; set; }
        public List<LoanApplicationDetailss> loanApplicationDetails { get; set; }
    }

}
