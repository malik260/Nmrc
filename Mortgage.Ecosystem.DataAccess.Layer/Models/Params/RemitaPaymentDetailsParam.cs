namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Params
{
    public class RemitaPaymentDetailsListParam
    {
        public decimal Id { get; set; }
        public string TransactionId { get; set; }
        public DateTime? TransactionDate { get; set; }
        public string EmployeeNumber { get; set; }
        public string EmployerNumber { get; set; }
        public decimal? Status { get; set; }
        public string Rrr { get; set; }
        public string LoggedUser { get; set; }
        public string Device { get; set; }
        public string Amount { get; set; }

    }
}
