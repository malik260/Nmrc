namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Params
{
    public class StatementOfAccountListParam
    {
        public string? FirstName { get; set; }    
        public string? LastName { get; set; }
        public DateOnly? ContributionDate { get; set; }
        public DateOnly? PaymentDate { get; set; }
        public decimal? Amount { get; set; }
    }
}
