namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Params
{
    public class LogLoginListParam : DateTimeParam
    {
        public long Company { get; set; }
        public string? UserName { get; set; }
        public int? LogStatus { get; set; }
        public string? IpAddress { get; set; }
        public long? employee { get; set; }
    }

    public class LogOperateListParam : DateTimeParam
    {
        public long Company { get; set; }
        public string? UserName { get; set; }
        public string? ExecuteUrl { get; set; }
        public int? LogStatus { get; set; }

    }

}
