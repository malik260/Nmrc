namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Params
{
    public class NHFCustomerRequestListParam
    {
        public long Id { get; set; }
        public string? PhoneNumber { get; set; }
        public string? AccountNumber { get; set; }
        public string? Email { get; set; }
        public string? NextOfKinName { get; set; }
        public decimal MonthlyIncome { get; set; }

    }
}
