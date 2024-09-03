using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Entities
{
    // Credit Assessment Index Title
    [Table("st_CreditAssessmentIndexTitle")]
    public class CreditAssessmentIndexTitleEntity : IdentityExtensionEntity
    {
        public int IndexTitleId { get; set; }
        public string? IndexTitleDescription { get; set; }
        public int Weight { get; set; }
        public int FactorIndexId { get; set; }
        [NotMapped]
        public string? FactorIndex { get; set; }
        [NotMapped]
        public string? RiskFactor { get; set; }
        public string? ProductCode { get; set; }
        [NotMapped]
        public string? ProductName { get; set; }
    }
}