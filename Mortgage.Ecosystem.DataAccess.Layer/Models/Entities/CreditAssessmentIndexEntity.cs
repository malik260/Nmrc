using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Entities
{
    // Credit Assessment Index
    [Table("st_CreditAssessmentIndex")]
    public class CreditAssessmentIndexEntity : IdentityExtensionEntity
    {
        [Column("IndexId")]
        public int Indexid { get; set; }

        [Column("AssessmentIndex")]
        public string? Assessmentindex { get; set; }

        [Column("Weight")]
        public int Weight { get; set; }

        [Column("IndexTitleId")]
        public int Indextitleid { get; set; }

        [Column("ProductCode")]
        public string? Productcode { get; set; }
    }
}