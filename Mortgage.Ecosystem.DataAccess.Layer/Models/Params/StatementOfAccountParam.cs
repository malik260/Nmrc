namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Params
{
    public class StatementOfAccountListParam
    {
        public DateTime StartDate { get; set; }    
        public DateTime EndDate { get; set; }    
        public string? NHFNumber { get; set; }
    }
}
