namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Params
{
    public class RefundListParam
    {
        public long Id { get; set; }
        public string? NhfNumber { get; set; }
        public string? Name { get; set; }

    }

    public class RefundParam
    {
        public string? NhfNumber { get; set; }
        public string? Name { get; set; }
    }
}
