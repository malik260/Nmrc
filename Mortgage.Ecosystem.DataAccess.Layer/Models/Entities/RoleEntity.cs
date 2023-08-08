using System.ComponentModel.DataAnnotations.Schema;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Base;

namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Entities
{
    // Role table
    [Table("tbl_Role")]
    public class RoleEntity : BaseExtensionEntity
    {
        // Company
        [Column("Company")]
        public long Company { get; set; }

        // Mode
        [Column("Mode")]
        public int Mode { get; set; }

        // Role Name
        [Column("RoleName")]
        public string? RoleName { get; set; }

        // Character sorting
        [Column("RoleSort")]
        public int RoleSort { get; set; }

        // Character status (0 disable 1 enable)
        [Column("RoleStatus")]
        public int RoleStatus { get; set; }

        // Remark
        [Column("Remark")]
        public string? Remark { get; set; }

        [NotMapped]
        public string? CompanyName { get; set; }

        [NotMapped]
        public string? MenuIds { get; set; } // The menu, page and button corresponding to the role
    }
}