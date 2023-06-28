namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Params
{
    public class LoanScheduleListParam
    {
        public long Id { get; set; }
        public int CreditId { get; set; }
        public string? Customer { get; set; }
        public string? Product { get; set; }
        public int AccountNo { get; set; }
        public string? AmountGranted { get; set; }
        public string? ViewSchedule { get; set; }
    }
}
