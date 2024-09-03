namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Params
{
    public class UserListParam : DateTimeParam
    {
        public long Company { get; set; }

        public long Employee { get; set; }

        public string? UserName { get; set; }

        public string? Mobile { get; set; }

        public int UserStatus { get; set; }

        public List<int>? UserIdList { get; set; }

        public string? UserIds { get; set; }
    }

    public class ChangePasswordParam
    {
        public int Id { get; set; }
        public string? OldPassword { get; set; }
        public string? NewPassword { get; set; }
        public string? ConfirmPassword { get; set; }

        public string? Username { get; set; }
    }

}
