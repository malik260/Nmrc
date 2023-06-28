using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos
{
    public class AffordabilityVM
    {
        public bool success { get; set; }
        public List<messages> result { get; set; }
    }

    public class messages
    {
        public DateTime? dateOfEmployment { get; set; }
        public string nhfAccount { get; set; }
        public string customerName { get; set; }
        public int customerId { get; set; }
        public DateTime? dateOfBirth { get; set; }
        public decimal monthlyIncome { get; set; }
        public string sortCode { get; set; }
        public int quotient { get; set; }
        public int age { get; set; }
        public int yearsToClockSixty { get; set; }
        public int yearsInService { get; set; }
        public int yearsToClockThirtyFive { get; set; }
        public int minYearsInServiceYearsToClockSixty { get; set; }
        public int repaymentPeriod { get; set; }
        public decimal presentValue { get; set; }
        public decimal affordableAmount { get; set; }
        public double monthlyRepayment { get; set; }
        public double profitability { get; set; }
        public int casaAccountId { get; set; }
        public decimal rate { get; set; }
        public decimal amountRequested { get; set; }
        public int productId { get; set; }
        public bool tenorOverride { get; set; }
        public int proposedTenor { get; set; }
        public int loanAffordabilityId { get; set; }
        public int loanApplicationDetailId { get; set; }
        public string productName { get; set; }
        public int companyId { get; set; }
        public string companyName { get; set; }
        public string company { get; set; }
        public int createdBy { get; set; }
        public int lastUpdatedBy { get; set; }
        public DateTime? dateTimeCreated { get; set; }
        public DateTime? dateTimeUpdated { get; set; }
        public bool deleted { get; set; }
        public string updatedBy { get; set; }
        public string deletedBy { get; set; }
        public DateTime? dateTimeDeleted { get; set; }
        public bool canModified { get; set; }
        public int userBranchId { get; set; }
        public string userBranchName { get; set; }
        public int sourceBranchId { get; set; }
        public string userIPAddress { get; set; }
        public string applicationUrl { get; set; }
        public string utility { get; set; }
        public int staffId { get; set; }
        public string username { get; set; }
        public string passCode { get; set; }
        public string hostName { get; set; }
        public string url { get; set; }

    }
    public class checkafford
    {
        public string nhfAccount { get; set; }
        public decimal amountRequested { get; set; }
        public int productId { get; set; }
        public int proposedTenor { get; set; }
    }


}
