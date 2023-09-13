using System.ComponentModel.DataAnnotations.Schema;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Base;

namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Entities
{
    // Company Information table
    [Table("tbl_CustomerProfileUpdate")]
    public class CustomerProfileUpdateEntity : BaseExtensionEntity
    {
        // ** Old Customer Details **

        // FullName
        [Column("FullName")]
        public string? FullName { get; set; }
        // Phone Number
        [Column("MobileNumber")]
        public string? MobileNumber { get; set; }

        // Email address
        [Column("EmailAddress")]
        public string? EmailAddress { get; set; }

        // Address
        [Column("Address")]
        public string? Address { get; set; }

        // Marital Status
        [Column("MaritalStatus")]
        public int MaritalStatus { get; set; }

        // Account Name
        [Column("AccountName")]
        public string? AccountName { get; set; }

        // Account Number
        [Column("BankAccountNumber")]
        public string? BankAccountNumber { get; set; }

        // Bank Name
        [Column("CustomerBank")]
        public string? CustomerBank { get; set; }

        //Monthly Income
        [Column("MonthlyIncome")]
        public decimal MonthlyIncome { get; set; }

        // Company Subsector
        [Column("Subsector")]
        public int Subsector { get; set; }

        // Next of Kin Name
        [Column("NOKName")]
        public string? NOKName { get; set; }

        // Next of Kin Number
        [Column("NOKNumber")]
        public string? NOKNumber { get; set; }

        // Next of Kin EmailAddress
        [Column("NOKEmailAddress")]
        public string? NOKEmailAddress { get; set; }

        // Next of Kin Address
        [Column("NOKAddress")]
        public string? NOKAddress { get; set; }

        // Relationship
        [Column("Relationship")]
        public string? Relationship { get; set; }

        // Document Upload
        [Column("DocumentsUpload")]
        public int DocumentsUpload { get; set; }

        // DocumentUploadFiles
        [Column("Files")]
        public byte[]? Files { get; set; }

        // NHF Number
        [Column("NHFNumber")]
        public string? NHFNumber { get; set; }

        // Approval Status
        [Column("ApprovalStatus")]
        public string? ApprovalStatus { get; set; }

        // ** New Customer Details **

        // New FullName
        [Column("NewFullName")]
        public string? NewFullName { get; set; }
        // New Phone Number
        [Column("NewMobileNumber")]
        public string? NewMobileNumber { get; set; }

        // New Email address
        [Column("NewEmailAddress")]
        public string? NewEmailAddress { get; set; }

        // New Address
        [Column("NewAddress")]
        public string? NewAddress { get; set; }

        // New Marital Status
        [Column("NewMaritalStatus")]
        public int NewMaritalStatus { get; set; }

        // New Account Name
        [Column("NewAccountName")]
        public string? NewAccountName { get; set; }

        // New Account Number
        [Column("NewBankAccountNumber")]
        public string? NewBankAccountNumber { get; set; }

        // New Bank Name
        [Column("NewCustomerBank")]
        public string? NewCustomerBank { get; set; }

        // New Monthly Income
        [Column("NewMonthlyIncome")]
        public decimal NewMonthlyIncome { get; set; }

        // New Company Subsector
        [Column("NewSubsector")]
        public int NewSubsector { get; set; }

        // New Next of Kin Name
        [Column("NewNOKName")]
        public string? NewNOKName { get; set; }

        // New Next of Kin Number
        [Column("NewNOKNumber")]
        public string? NewNOKNumber { get; set; }

        // New Next of Kin EmailAddress
        [Column("NewNOKEmailAddress")]
        public string? NewNOKEmailAddress { get; set; }

        // New Next of Kin Address
        [Column("NewNOKAddress")]
        public string? NewNOKAddress { get; set; }

        // New Relationship
        [Column("NewRelationship")]
        public string? NewRelationship { get; set; }

    }
}