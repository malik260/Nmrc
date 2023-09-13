namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Params
{
    public class CreditAssessmentIndexTitleListParam
    {
        public int IndexTitleId { get; set; }
        public string? IndexTitleDescription { get; set; }
        public int Weight { get; set; }
        public int FactorIndexId { get; set; }
        public string? ProductCode { get; set; }
    }
}