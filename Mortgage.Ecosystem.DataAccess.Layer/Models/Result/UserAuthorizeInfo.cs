namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Result
{
    public class UserAuthorizeInfo
    {
        public int? IsSystem { get; set; }
        public List<MenuAuthorizeInfo>? MenuAuthorize { get; set; }
    }
}
