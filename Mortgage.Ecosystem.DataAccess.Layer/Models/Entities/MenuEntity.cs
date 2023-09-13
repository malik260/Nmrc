using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Entities
{
    // Menu table
    [Table("tbl_Menu")]
    public class MenuEntity : BaseExtensionEntity
    {
        // Parent menu Id (0 means root menu)
        [Column("Parent")]
        public long Parent { get; set; }

        // Menu name
        [Column("MenuName")]
        public string? MenuName { get; set; }

        // Menu icon
        [Column("MenuIcon")]
        public string? MenuIcon { get; set; }

        // Menu Url
        [Column("MenuUrl")]
        public string? MenuUrl { get; set; }

        // How to open the link
        [Column("MenuTarget")]
        public string? MenuTarget { get; set; }

        // Menu sorting
        [Column("MenuSort")]
        public int MenuSort { get; set; }

        // Menu type (1 directory 2 pages 3 buttons)
        [Column("MenuType")]
        public int MenuType { get; set; }

        // Menu status (0 disable 1 enable)
        [Column("MenuStatus")]
        public int MenuStatus { get; set; }

        // Menu permission ID
        [Column("Authorize")]
        public string? Authorize { get; set; }

        // Assign each step in the workflow an approval level
        [Column("ApprovalLevel")]
        public int ApprovalLevel { get; set; }

        // Remark
        [Column("Remark")]
        public string? Remark { get; set; }

        // Parent name
        [NotMapped]
        public string? ParentName { get; set; }

        // Approval Log List
        [NotMapped]
        public List<ApprovalLogEntity>? ApprovalLogList { get; set; }
    }
}