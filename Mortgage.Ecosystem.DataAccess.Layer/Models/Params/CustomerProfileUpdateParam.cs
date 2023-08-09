namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Params
{
    public class CustomerProfileUpdateListParam
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? Telephone { get; set; }
        public string? Email { get; set; }
        public byte[]? Logo { get; set; }
        public string? LogoType { get; set; }
        public string? RCNumber { get; set; }
    }
}
