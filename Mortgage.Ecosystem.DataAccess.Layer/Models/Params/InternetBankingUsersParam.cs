namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Params
{
    public class InternetBankingUsersListParam
    {
        public long Id { get; set; }
        public string? AccountNo { get; set; }
        public string? AccountName { get; set; }
        public string? CustomerCode { get; set; }
        public DateTime? DateCreated { get; set; }
        public string? Status { get; set; }
    }
}
