namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Params
{
    public class CreditAssessmentFactorIndexListParam
    {
        public string? FactorIndexDescription { get; set; }
        public int Weight { get; set; }
        public int RiskFactorId { get; set; }
        public string? ProductCode { get; set; }
    }
}