using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Entities
{
    // Assessment Factor table
    [Table("st_CreditAssessmentRiskFactor")]
    public class CreditAssessmentRiskFactorEntity : IdentityExtensionEntity
    {

        [Column("RiskFactorId")]
        public int RiskFactorId { get; set; }

        [Column("RiskFactorsDescription")]
        public string? RiskFactorsDescription { get; set; }

        [Column("Weight")]
        public int Weight { get; set; }

        [Column("Productcode")]
        public string? ProductCode { get; set; }
    }
}