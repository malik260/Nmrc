using System.ComponentModel.DataAnnotations.Schema;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Base;

namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Entities
{
    // Company Information table
    [Table("tbl_Refund")]
    public class RefundEntity : BaseExtensionEntity
    {
        // NHF Number
        [Column("NhfNumber")]
        public string? NhfNumber { get; set; }

        // Customer Name
        [Column("Name")]
        public string? Name { get; set; }

        // Date Of BIRTH
        [Column("DateOfBirth")]
        public DateTime? DateOfBirth { get; set; }

        // Employer Number
        [Column("EmployerNumber")]
        public string? EmployerNumber { get; set; }

        // Employment Date
        [Column("EmploymentDate")]
        public DateTime? EmploymentDate { get; set; }

        //BVN
        [Column("BVN")]
        public string? BVN { get; set; }

        // NIN
        [Column("NIN")]
        public string? NIN { get; set; }

        // Mobile Number
        [Column("MobileNumber")]
        public string? MobileNumber { get; set; }

        // Contact Address
        [Column("ContactAddress")]
        public string? ContactAddress{ get; set; }

        // Employer Name
        [Column("EmployerName")]
        public string? EmployerName { get; set; }

        // Condition For Application 
        [Column("ConditionForApplication")]
        public int ConditionForApplication { get; set; }

        // Bank Name
        [Column("BankName")]
        public string? BankNmae { get; set; }

        // Bank Account Number
        [Column("BankAccountNumber")]
        public string? BankAccountNumber { get; set; }

        //Bank Code
        [Column("BankCode")]
        public string? BankCode { get; set; }

        // Customer Number
        [Column("CustomerNumber")]
        public string? CustomerNumber { get; set; }

        // Age
        [Column("Age")]
        public int Age { get; set; }

        // Years of Service
        [Column("YearOfService")]
        public int YearOfService { get; set; }

        // Available Balance
        [Column("AvailableBalance")]
        public int AvailableBalance { get; set; }

        // Document Upload
        [Column("DocumentsUpload")]
        public int DocumentsUpload { get; set; }

        // DocumentUploadFiles
        [Column("Files")]
        public byte[]? Files { get; set; }

       
    }
}