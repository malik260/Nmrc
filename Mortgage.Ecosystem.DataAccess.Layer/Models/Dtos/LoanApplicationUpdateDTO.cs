using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos
{
    //public class LoanApplicationUpdateDTO
    //{
    public class LoanApplicationResult2
    {
        public string applicationReferenceNumber { get; set; }
        public string customerCode { get; set; }
        public string customerName { get; set; }
        public string relationshipOfficerName { get; set; }
        public string relationshipManagerName { get; set; }
        public string approvalStatus { get; set; }
        public string loanBranch { get; set; }
        public DateTime applicationDate { get; set; }
        public bool submittedForAppraisal { get; set; }
        public string currentApprovalLevel { get; set; }
        public string applicationStatus { get; set; }
        public bool isOfferLetterAvailable { get; set; }
        public List<LoanApplicationResult3> loanApplicationDetails { get; set; }

    }

    public class LoanApplicationResult3
    {
        public decimal amount { get; set; }
        public int productId { get; set; }
        public string productName { get; set; }
        public int approvedTenor { get; set; }
    }

    public class LoanApplicationUpdateDTO
    {
        public bool success { get; set; }
        public LoanApplicationResult2 result { get; set; }

    }
}
