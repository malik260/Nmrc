namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Result
{
    public class MenuAuthorizeInfo
    {
        // MenuId
        public long? MenuId { get; set; }

        // User Id or Role Id
        public long? AuthorizeId { get; set; }

        // User or role
        public int? AuthorizeType { get; set; }

        // Permission ID
        public string? Authorize { get; set; }

    }
}