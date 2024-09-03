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
        public string? NhfNumber { get; set; }
        public string? RealName { get; set; }
        public string? UserName { get; set; }
        public string? UserEmail { get; set; }
        public string? UserPassword { get; set; }
        public string? UserCompany { get; set; }
        public string? ProcessName { get; set; }
        public string? MessageType { get; set; }
        public string? TicketNumber { get; set; }
        public string? PmbName { get; set; }
        public string? ContactPerson { get; set; }
        public string? Underwriter { get; set; }
        public string? Reviewer { get; set; }
        public string? Approver { get; set; }
        public string? ContactPersonEmail { get; set; }
        public string? RegistrationApprover { get; set; }
        public string? NewCompany { get; set; }
        public string? UserToken { get; set; }
        public string? Remark { get; set; }
        public string? RequestNumber { get; set; }
        public string? Message { get; set; }
        public string? Subject { get; set; }
        public string? EticketApprover { get; set; }
        public string? ApproverEmail { get; set; }
        public string? UserPhoneNumber { get; set; }
        public string? DateSubmitted { get; set; }
        public string? CompanyName { get; set; }

        public string? EmployeeName { get; set; }
        public string? COmpanyMail { get; set; }
    }
}