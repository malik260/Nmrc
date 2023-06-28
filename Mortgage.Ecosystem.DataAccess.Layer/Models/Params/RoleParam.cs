namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Params
{
    public class RoleListParam : DateTimeParam
    {
        public long Company { get; set; }

        public int? Mode { get; set; }

        public string? RoleName { get; set; }

        public int? RoleStatus { get; set; }

        // Multiple role IDs
        public string? RoleIds { get; set; }
    }
}