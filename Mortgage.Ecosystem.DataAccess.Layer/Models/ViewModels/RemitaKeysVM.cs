using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels
{
    public class RemitaKeysVM
    {

        public string? merchantId = "2547916";
        public string? apiKey = "1946";
        public string? serviceTypeId = "4430731";
        public string? Baseurl = "https://remitademo.net";
        public string? PaymentInit = "/remita/exapp/api/v1/send/api/echannelsvc/merchant/api/paymentinit";
    }

    public class GenerateRRRVM
    {
        public string? serviceTypeId { get; set; }
        public string? amount { get; set; }
        public string? orderId { get; set; }
        public string? payerName { get; set; }
        public string? payerEmail { get; set; }
        public string? payerPhone { get; set; }
        public string? description { get; set; }
    }
    
    public class IndividualAffordabilityDetails
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

    public class ContributionResponseVM
    {
        public int responseCode { get; set; }
        public string responseText { get; set; }
        public string message { get; set; }
    }


    public class CustomerContributionViewwModel
    {
        public DateTime ValueDate { get; set; }
        public DateTime PostDate { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
        public string EmployeeNhfNumber { get; set; }
        public string EmployerNhfNumber { get; set; }
        public decimal AmountContributed { get; set; }
        public string EmployeeName { get; set; }
        public string EmployerName { get; set; }
        public string RRR { get; set; }
        public string Narration { get; set; }

    }
}
