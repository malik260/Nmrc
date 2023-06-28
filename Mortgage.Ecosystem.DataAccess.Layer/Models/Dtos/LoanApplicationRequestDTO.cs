using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos.LoanApplicationRequestDTO;

namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos
{
    public class LoanApplicationRequestDTO
    {

        public string customerCode { get; set; }
        public LoanApplicationViewModel2 loanApplicationDetail { get; set; }
        public AffordabilityDetails affordabilityDetails { get; set; }
    }

    public class LoanApplicationViewModel2
    {
        public int proposedProductId { get; set; }
        public int proposedTenor { get; set; }
        public decimal proposedAmount { get; set; }
        public int subSectorId { get; set; }
        public string loanPurpose { get; set; }
        public string operatingAccountNo { get; set; }
        public decimal? requestedAmount { get; set; }
        public string repaymentDate { get; set; }
    }

    public class AffordabilityDetails
    {
        public int productId { get; set; }
        public int casaAccountId { get; set; }
        public int age { get; set; }
        public int yearsInService { get; set; }
        public int repaymentPeriod { get; set; }
        public decimal presentValue { get; set; }
        public decimal affordableAmount { get; set; }
        public double monthlyRepayment { get; set; }
        public double profitability { get; set; }
        public decimal rate { get; set; }
        public decimal amountRequested { get; set; }
        public bool tenorOverride { get; set; }

    }
}
