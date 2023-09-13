using System.ComponentModel.DataAnnotations.Schema;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Base;

namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Entities
{
    // Risk Assessment Setup table
    [Table("tbl_CreditScore")]
    public class CreditScoreEntity : BaseExtensionEntity
    {
        
        [Column("CreditType")]
        public string? CreditType { get; set; }

      
        [Column("RangeMax")]
        public decimal RangeMax { get; set; }

        
        [Column("RangeMin")]
        public decimal RangeMin { get; set; }

        
        [Column("ProductCode")]
        public int ProductCode { get; set; }

        
        [Column("Rating")]
        public string? Rating { get; set; }

       
        [Column("Remark")]
        public string? Remark { get; set; }

        [Column("InterestRate")]
        public string? InterestRate { get; set; }

        [Column("CreditGrade")]
        public string? CreditGrade { get; set; }

        [Column("CreditGradeDefinition")]
        public string? CreditGradeDefinition { get; set; }





    }
}

