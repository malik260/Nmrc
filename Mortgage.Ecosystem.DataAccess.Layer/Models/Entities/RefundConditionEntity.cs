using System.ComponentModel.DataAnnotations.Schema;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Base;

namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Entities
{
    // Company Information table
    [Table("Refund_TblRefundcondition")]
    public class RefundConditionEntity : BaseExtensionEntity
    {
        [Column("Name")]
        public string Name { get; set; }

        [Column("Year")]
        public byte? Year { get; set; }

        [Column("Code")]
        public string Code { get; set; }

        [Column("Remark")]
        public string Remark { get; set; }

        [Column("IsDeleted")]
        public bool? IsDeleted { get; set; }
    }
}