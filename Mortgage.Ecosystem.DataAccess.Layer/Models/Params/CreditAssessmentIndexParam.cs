namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Params
{
    public class CreditAssessmentIndexListParam
    {
        public int IndexId { get; set; }
        public string? AssessmentIndex { get; set; }
        public int Weight { get; set; }
        public int IndexTitleId { get; set; }
        public string? ProductCode { get; set; }
    }
}