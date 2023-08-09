namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Params
{
    public class ChargeSetupListParam
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? FeeCatergory { get; set; }
        public string? ReferenceNumber { get; set; }
        public string? Email { get; set; }
        public decimal? FeeRate { get; set; }
        public string? LogoType { get; set; }
        public string? RCNumber { get; set; }
    }
}
