using System.ComponentModel.DataAnnotations.Schema;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Base;

namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Entities
{
    //AccreditationFee table
    [Table("tbl_Underwriting")]
    public class UnderwritingEntity : BaseExtensionEntity
    {
        // Customer Name
        [Column("CustomerName")]
        public string? Name { get; set; }

        // Product Name
        [Column("ProductName")]
        public string? ProductName { get; set; }

        // Tenor
        [Column("Tenor")]
        public string? Tenor { get; set; }

        // Interest Rate
        [Column("InterestRate")]
        public string? InterestRate { get; set; }

        // Loan Amount
        [Column("LoanAmount")]
        public decimal LoanAmount { get; set; }

        // Document Title
        [Column("DocumentTitle")]
        public string? DocumentTitle { get; set; }

        // Document Upload
        [Column("DocumentUpload")]
        public byte[]? DocumentUpload { get; set; }

        //Comments
        [Column("Comments")]
        public string? Comments { get; set; }

        // Next Staff Level
        [Column("NextStafffLevel")]
        public string? NextStafffLevel { get; set; }

        // Check List
        [Column("CheckList")]
        public string? CheckList { get; set; }

        // Check List
        [Column("Remark")]
        public string? Remark { get; set; }
    }
}