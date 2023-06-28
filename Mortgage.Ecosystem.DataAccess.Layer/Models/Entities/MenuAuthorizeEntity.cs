using System.ComponentModel.DataAnnotations.Schema;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Base;

namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Entities
{
    // Menu permission table
    [Table("tbl_MenuAuthorize")]
    public class MenuAuthorizeEntity : BaseCreateEntity
    {
        // MenuId
        [Column("MenuId")]
        public long MenuId { get; set; }

        // Authorization Id (role Id or user Id)
        [Column("AuthorizeId")]
        public long AuthorizeId { get; set; }

        // Authorization type (1 role 2 users)
        [Column("AuthorizeType")]
        public int AuthorizeType { get; set; }

        // Authorization ID
        [NotMapped]
        public string? AuthorizeIds { get; set; }
    }
}