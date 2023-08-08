using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Base;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Entities
{
    // Employee table
    [Table("tbl_Employee")]
    public class EmployeeEntity : BaseExtensionEntity
    {
        // Company Number
        [Column("Company"), Description("Company")]
        public long Company { get; set; }

        // Branch Number
        [Column("Branch"), Description("Branch")]
        public long Branch { get; set; }

        // Department
        [Column("Department"), Description("Department")]
        public int Department { get; set; }

        // NHF Number
        [Column("NHFNumber"), Description("NHF Number")]
        public long NHFNumber { get; set; }

        // BVN
        [Column("BVN"), Description("BVN")]
        public string? BVN { get; set; }

        // NIN
        [Column("NIN"), Description("NIN")]
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
        [Column("FirstName"), Description("First Name")]
        public string? FirstName { get; set; }

        // Last Name
        [Column("LastName"), Description("Last Name")]
        public string? LastName { get; set; }

        // Other Name(s)
        [Column("OtherName"), Description("Other Name(s)")]
        public string? OtherName { get; set; }

        // Gender
        [Column("Gender"), Description("Gender")]
        public int Gender { get; set; }

        // Date Of Birth
        [Column("DateOfBirth"), Description("DOB")]
        public string? DateOfBirth { get; set; }

        // Marital Status
        [Column("MaritalStatus"), Description("Marital Status")]
        public int MaritalStatus { get; set; }

        // Postal Address
        [Column("PostalAddress"), Description("Postal Address")]
        public string? PostalAddress { get; set; }

        // Email Address
        [Column("EmailAddress"), Description("Email Address")]
        public string? EmailAddress { get; set; }

        // Mobile Number
        [Column("MobileNumber"), Description("Mobile/Telephone")]
        public string? MobileNumber { get; set; }

        // Staff Number
        [Column("StaffNumber"), Description("Staff Number")]
        public string? StaffNumber { get; set; }

        // Customer's Bank
        [Column("CustomerBank"), Description("Customer Bank")]
        public string? CustomerBank { get; set; }

        // Bank Account Number
        [Column("BankAccountNumber"), Description("Account Number")]
        public string? BankAccountNumber { get; set; }

        // Account Type
        [Column("AccountType"), Description("Account Type")]
        public int AccountType { get; set; }

        // Monthly Salary
        [Column("MonthlySalary"), Description("Monthly Salary")]
        public decimal MonthlySalary { get; set; }

        // Alert Type
        [Column("AlertType"), Description("Alert Type")]
        public int AlertType { get; set; }

        // Portrait
        [Column("Portrait")]
        public byte[]? Portrait { get; set; }

        // Portrait Type
        [Column("PortraitType")]
        public string? PortraitType { get; set; }

        // User type
        [Column("UserType"), Description("User type")]
        public int UserType { get; set; }

        // Status
        [Column("Status"), Description("Status")]
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
        [NotMapped]
        public string? KinFirstNumber { get; set; }

        // Kin's Last Name
        [NotMapped]
        public string? KinLastName { get; set; }

        // Kin's Mobile number
        [NotMapped]
        public string? KinMobileNumber { get; set; }

        [NotMapped]
        public int KinRelationship { get; set; }

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
        public string? DepartmentName { get; set; }

        [NotMapped]
        public string? TitleName { get; set; }

        [NotMapped]
        public string? GenderName { get; set; }

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
    }
}