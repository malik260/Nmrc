using System.ComponentModel.DataAnnotations.Schema;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Base;

namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Entities
{
    // Risk Assessment Setup table
    [Table("tbl_RiskAssessmentSetup")]
    public class RiskAssessmentSetupEntity : BaseExtensionEntity
    {
        
        [Column("CreditType")]
        public string? CreditType { get; set; }

      
        [Column("AssessmentFactors")]
        public string? AssessmentFactors { get; set; }

        
        [Column("Index")]
        public int Index { get; set; }

        
        [Column("IndexHead")]
        public string? IndexHead { get; set; }

        
        [Column("IndexItem")]
        public string? IndexItem { get; set; }

       
        [Column("Weight")]
        public int Weight { get; set; }

        
     


    }
}

