namespace Mortgage.Ecosystem.DataAccess.Layer.Request
{
    public class MailRequest
    {
        public string? ToEmail { get; set; }
        public string? Subject { get; set; }
        public string? Body { get; set; }
    }

    public class MailParameter
    {
        public string? RealName { get; set; }
        public string? UserName { get; set; }
        public string? UserEmail { get; set; }
        public string? UserPassword { get; set; }
        public string? UserCompany { get; set; }
    }
}