using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Base;

namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Entities
{
    // Company Information table
    [Table("tbl_CustomerProfileUpdate")]
    public class CustomerProfileUpdateEntity : BaseExtensionEntity
    {

        // Company Number
        [Column("Company")]
        public long Company { get; set; }

        // Employee
        [Column("Employee")]
        public long Employee { get; set; }

        // Branch Number
        [Column("Branch")]
        public long Branch { get; set; }

        // Department
        [Column("Department")]
        public int Department { get; set; }

        // NHF Number
        [Column("NHFNumber")]
        public long NHFNumber { get; set; }

        // BVN
        [Column("BVN")]
        public string? BVN { get; set; }

        // NIN
        [Column("NIN")]
        public string? NIN { get; set; }

        // Employee Type
        [Column("EmploymentType"), Description("EmploymentType")]
        public int EmploymentType { get; set; }

        // Date Of Employment
        [Column("DateOfEmployment"), Description("Employment Date")]
        public DateTime? DateOfEmployment { get; set; }

        // Title
        [Column("Title"), Description("Title")]
        public int Title { get; set; }

        // First Name
        [Column("FirstName")]
        public string? FirstName { get; set; }

        // Last Name
        [Column("LastName")]
        public string? LastName { get; set; }

        // Other Name(s)
        [Column("OtherName")]
        public string? OtherName { get; set; }

        // Gender
        [Column("Gender")]
        public int Gender { get; set; }

        // Date Of Birth
        [Column("DateOfBirth")]
        public string? DateOfBirth { get; set; }

        // Marital Status
        [Column("MaritalStatus")]
        public int MaritalStatus { get; set; }

        // Postal Address
        [Column("PostalAddress")]
        public string? PostalAddress { get; set; }

        // Email Address
        [Column("EmailAddress")]
        public string? EmailAddress { get; set; }

        // Mobile Number
        [Column("MobileNumber")]
        public string? MobileNumber { get; set; }

        // Staff Number
        [Column("StaffNumber")]
        public string? StaffNumber { get; set; }

        // Customer's Bank
        [Column("CustomerBank")]
        public string? CustomerBank { get; set; }

        // Bank Account Number
        [Column("BankAccountNumber")]
        public string? BankAccountNumber { get; set; }

        // Account Type
        [Column("AccountType")]
        public int AccountType { get; set; }

        // Monthly Salary
        [Column("MonthlyIncome")]
        public decimal MonthlyIncome { get; set; }

        // Alert Type
        [Column("AlertType")]
        public int AlertType { get; set; }

        // Contribution Branch
        [Column("ContributionBranch")]
        public int ContributionBranch { get; set; }

        // Portrait
        [Column("Portrait")]
        public byte[]? Portrait { get; set; }

        // Portrait Type
        [Column("PortraitType")]
        public string? PortraitType { get; set; }

        // User type
        [Column("UserType")]
        public int UserType { get; set; }

        // Status
        [Column("Status")]
        public int Status { get; set; }

        // Designation
        [NotMapped]
        public long? Designation { get; set; }

        // Username
        [NotMapped]
        public string? UserName { get; set; }

        // Role
        [NotMapped]
        public long Role { get; set; }

        // Role Ids
        [NotMapped]
        public string? RoleIds { get; set; }

        // Company name
        [NotMapped]
        public string? CoyName { get; set; }

        // Company address
        [NotMapped]
        public string? CoyAddress { get; set; }

        // Company RCNumber
        [NotMapped]
        public string? CoyRCNumber { get; set; }

        // Company Sector
        [NotMapped]
        public int CoySector { get; set; }

        // Company Subsector
        [NotMapped]
        public int CoySubsector { get; set; }

        // Kin's First Name
        [Column("KinFirstName")]
        public string? KinFirstName { get; set; }

        // Kin's Last Name
        [Column("KinLastName")]
        public string? KinLastName { get; set; }

        // Kin's Mobile number
        [Column("KinMobileNumber")]
        public string? KinMobileNumber { get; set; }

        [Column("KinRelationship")]
        public int KinRelationship { get; set; }

        [Column("KinAddress")]
        public string? KinAddress { get; set; }

        [Column("KinEmailAddress")]
        public string? KinEmailAddress { get; set; }

        [NotMapped]
        public int User { get; set; }

        [NotMapped]
        public string? Password { get; set; }

        [NotMapped]
        public string? DecryptedPassword { get; set; }

        [NotMapped]
        public string? Salt { get; set; }

        [NotMapped]
        public string? CompanyName { get; set; }

        [NotMapped]
        public string? BranchName { get; set; }

        [NotMapped]
        public string? ContributionBranchName { get; set; }

        [NotMapped]
        public string? DepartmentName { get; set; }

        [NotMapped]
        public string? TitleName { get; set; }

        [NotMapped]
        public string? GenderName { get; set; }
        [NotMapped]
        public string? RelationName { get; set; }

        [NotMapped]
        public string? MaritalStatusName { get; set; }

        [NotMapped]
        public string? BankName { get; set; }

        [NotMapped]
        public string? AccountTypeName { get; set; }

        [NotMapped]
        public string? AlertTypeName { get; set; }

        [NotMapped]
        public string? MenuIds { get; set; }

        [NotMapped]
        public string? FullName { get; set; }


        [NotMapped]
        [Column("Employer")]
        public string? Employer { get; set; }

        [NotMapped]
        [Column("EmployerNumber")]
        public string? EmployerNo { get; set; }
    }
}