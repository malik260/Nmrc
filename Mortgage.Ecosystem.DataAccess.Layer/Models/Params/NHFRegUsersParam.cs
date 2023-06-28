namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Params
{
    public class NHFRegUsersListParam
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? BVN { get; set; }
        public string? NIN { get; set; }
        public string? RCNumber { get; set; }
    }
}
