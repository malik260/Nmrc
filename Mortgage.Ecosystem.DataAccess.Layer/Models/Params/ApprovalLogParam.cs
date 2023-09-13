namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Params
{
    public class ApprovalLogListParam
    {
        public long Company { get; set; }
        public long? Branch { get; set; }
        public long MenuId { get; set; }
        public int MenuType { get; set; }
        public long Authority { get; set; }
        public long Record { get; set; }
        public int ApprovalCount { get; set; }
        public int ApprovalLevel { get; set; }
        public int Status { get; set; }
        public string? Remark { get; set; }
    }
}
