namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Params
{
    public class LoanInitiationListParam
    {
        public long Id { get; set; }
        public string? LoanProduct { get; set; }
        public string? Sector { get; set; }
        public string? Principal { get; set; }
        public string? Rate { get; set; }
        public byte[]? File { get; set; }
        public string? Tenor { get; set; }
        public string? ReferenceNumber { get; set; }
    }
}
