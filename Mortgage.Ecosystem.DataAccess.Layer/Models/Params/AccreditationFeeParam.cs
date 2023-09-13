namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Params
{
    public class AccreditationFeeListParam
    {
        public long Id { get; set; }
        public string? AgentName { get; set; }
        public string? MobileNumber { get; set; }
        public string? EmailAddress { get; set; }
        public decimal FeeAmount { get; set; }
        public int PaymentOption { get; set; }
    }
}
