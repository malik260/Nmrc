using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Entities
{
    // Credit Assessment Factor Index table
    [Table("st_CreditAssessmentFactorIndex")]
    public class CreditAssessmentFactorIndexEntity : IdentityExtensionEntity
    {
        [Column("FactorIndexId")]
        public int FactorIndexId { get; set; }

        [Column("FactorIndexDescription")]
        public string? FactorIndexDescription { get; set; }

        [Column("Weight")]
        public int Weight { get; set; }

        [Column("RiskFactorId")]
        public int RiskFactorId { get; set; }

        [Column("ProductCode")]
        public string? ProductCode { get; set; }
    }
}