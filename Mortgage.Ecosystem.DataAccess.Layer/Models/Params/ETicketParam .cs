namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Params
{
    public class ETicketListParam
    {
        public long Id { get; set; }
        public string? RequestNumber { get; set; }
        public string? Subject { get; set; }
        public string? Message { get; set; }
        public DateTime? DateSent { get; set; }
        public string? FeedbackStatus { get; set; }
    }
}
