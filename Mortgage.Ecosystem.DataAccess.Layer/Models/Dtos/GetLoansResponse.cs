using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos
{
    public class GetLoansResponse
    {
        public bool success { get; set; }
        public int count { get; set; }
        public List<LoanApplications> result { get; set; }
    }
    public class LoanApplications
    {
        public string applicationReferenceNumber { get; set; }
        public string loanReferenceNumber { get; set; }
        public string nhfAccount { get; set; }
        public decimal loanAmount { get; set; }
        public decimal interestRate { get; set; }
        public string product { get; set; }


    }

}
