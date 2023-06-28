using System.ComponentModel.DataAnnotations.Schema;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Base;

namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Entities
{
    [Table("tbl_AutoJobLog")]
    public class AutoJobLogEntity : BaseCreateEntity
    {
        public string? JobGroupName { get; set; }
        public string? JobName { get; set; }
        public int? LogStatus { get; set; }
        public string? Remark { get; set; }
    }
}
