namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Params
{
    public class UnlockAdminUserListParam
    {
        public long Id { get; set; }
        public string? AccountName { get; set; }
        public string? MobileNo { get; set; }
        public string? Email { get; set; }
        public int? Status { get; set; }
    }
}
