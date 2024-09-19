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

        //  Name
        [Column("ProductName")]
        public string? ProductName { get; set; }

        // Tenor
        [Column("Tenor")]
        public string? Tenor { get; set; }

        // Interest Rate
        [Column("InterestRate")]
        public decimal InterestRate { get; set; }

        // Loan Amount
        [Column("LoanAmount")]
        public decimal LoanAmount { get; set; }

        // Document Title
        [Column("DocumentTitle")]
        public string? DocumentTitle { get; set; }

        // Document Upload
        //[Column("DocumentUpload")]
        //public byte[]? DocumentUpload { get; set; }

        //Comments
        [Column("Comments")]
        public string? Comments { get; set; }

        // Next Staff Level
        [Column("NextStafffLevel")]
        public string? NextStafffLevel { get; set; }

        // Check List
        [Column("CheckList")]
        public string? CheckList { get; set; }


        [Column("Rated")]
        public int? Rated { get; set; }


        [Column("Reviewed")]
        public int? Reviewed { get; set; }

        // Check List
        [Column("Remark")]
        public string? Remark { get; set; }

        // NHF Number
        [Column("NHFNumber")]
        public string NHFNumber { get; set; }

        [Column("LoanId")]
        public string? LoanId { get; set; }

        [Column("Company")]
        public long Company { get; set; }

        [Column("Approved")]
        public int Approved { get; set; }

        [Column("SchemeType")]
        public int SchemeType { get; set; }

        [Column("LoanRefNo")]
        public string? LoanRefNo { get; set; }

        [Column("BatchRefNo")]
        public string? BatchRefNo { get; set; }

        [Column("isBatched")]
        public int? isBatched { get; set; }

        [Column("Disbursed")]
        public int? Disbursed { get; set; }

        [NotMapped]
        public string? Branch { get; set; }
        [NotMapped]
        public decimal? totalAmount { get; set; }
        [NotMapped]
        public string? pmb { get; set; }
        [NotMapped]
        public string? Bvn { get; set; }
        [NotMapped]
        public decimal MonthlyIncome { get; set; }
        [NotMapped]
        public DateTime? DateofEmployment { get; set; }
        [NotMapped]
        public string? DOB { get; set; }
        [NotMapped]
        public string? Rating{ get; set; }
        [NotMapped]
        public string? RiskScore{ get; set; }

        [NotMapped]
        public string? Scheme { get; set; }
        [NotMapped]
        public string? creditName { get; set; }

    }
}