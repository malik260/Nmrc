using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos
{
    public class LoanScheduleResponse
    {

        public bool success { get; set; }
        public int count { get; set; }
        public List<LoanSchedule> result { get; set; }
    }

    public class LoanSchedule
    {
        public int paymentNumber { get; set; }
        public DateTime paymentDate { get; set; }
        public double startPrincipalAmount { get; set; }
        public double periodPaymentAmount { get; set; }
        public double periodInterestAmount { get; set; }
        public double periodPrincipalAmount { get; set; }
        public double endPrincipalAmount { get; set; }
        public decimal interestRate { get; set; }
        public int loanId { get; set; }

    }

}