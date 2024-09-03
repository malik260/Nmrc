namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Params
{
    public class ContributionListParam
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public DateTime EndTime { get; set; }
        public string? Email { get; set; }
        public string? EmployeeNumber { get; set; }
        public string? EmployerNumber { get; set; }
        public DateTime StartTime { get; set; }
        public string? NHFNumber { get; set; }
    }
}
