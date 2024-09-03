namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Params
{
    public class UnderwritingListParam
    {
        public long Id { get; set; }
        public string? ProductName { get; set; }
        public string? Tenor { get; set; }
        public string? InterestRate { get; set; }
        public decimal LoanAmount { get; set; }
        public string? pmb { get; set; }

    }
}
