namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Params
{
    public class RiskAssessmentSetupListParam
    {
        public long Id { get; set; }
        public string? CreditType { get; set; }    
        public string? IndexHead { get; set; }
        public string? AssessmentFactors { get; set; }
        public int Index { get; set; }
        public string? IndexItem { get; set; }
        public int weight { get; set; }
    }
}
