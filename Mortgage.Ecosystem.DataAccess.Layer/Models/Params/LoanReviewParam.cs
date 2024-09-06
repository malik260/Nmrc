using System.ComponentModel.DataAnnotations.Schema;

namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Params
{
    public class LoanReviewListParam
    {
        public long Id { get; set; }
        public string? LenderID { get; set; }
        public string? NHFNumber { get; set; }
        public string? RefinanceNumber { get; set; }
        public string? LoanReferenceNumber { get; set; }
        public string? Amount { get; set; }
        public string? Loan { get; set; }
        public string? Status { get; set; }
        public string? ApplicationDate { get; set; }


    }
}
