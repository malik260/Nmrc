namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Params
{
    public class CreditScoreListParam
    {
        public long Id { get; set; }
        public string? CreditType { get; set; }
        public int ProductCode { get; set; }
        public int RangeMax { get; set; }
        public int RangeMin { get; set; }
        public string? Rating { get; set; }
        public string? Remark { get; set; }
        public string? InterestRate { get; set; }
        public string? CreditGrade { get; set; }
        public string? CreditGradeDefinition { get; set; }
    }
}
