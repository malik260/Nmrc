using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Base;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos
{
    public class RegistrationDto : BaseExtensionEntity
    {
        // Company name
        [Column("Name")]
        public string? Name { get; set; }

        // Company address
        [Column("Address")]
        public string? Address { get; set; }

        // Company phone
        [Column("Telephone")]
        public string? Telephone { get; set; }

        // Company Fax
        [Column("Fax")]
        public string? Fax { get; set; }

        // Company Email
        [Column("Email")]
        public string? Email { get; set; }

        // Company PostalAddress
        [Column("PostalAddress")]
        public string? PostalAddress { get; set; }

        // Company Sector
        [Column("Sector")]
        public int Sector { get; set; }

        // Company Subsector
        [Column("Subsector")]
        public int Subsector { get; set; }

        // Company RCNumber
        [Column("RCNumber")]
        public string? RCNumber { get; set; }

        // Incorporation Date
        [Column("DateOfIncorporation")]
        public DateTime? DateOfIncorporation { get; set; }

        // Company's Contact Person
        [Column("ContactPerson")]
        public string? ContactPerson { get; set; }

        // Contact Person Designation
        [Column("ContactPersonDesignation")]
        public int ContactPersonDesignation { get; set; }

        // Nature Of Business
        [Column("NatureOfBusiness")]
        public string? NatureOfBusiness { get; set; }

        // Name Of Registrar
        [Column("NameOfRegistrar")]
        public string? NameOfRegistrar { get; set; }

        // Name Of Trustees
        [Column("NameOfTrustees")]
        public string? NameOfTrustees { get; set; }

        // Commencement Date
        [Column("DateOfCommencement")]
        public DateTime? DateOfCommencement { get; set; }

        // Company Class
        [Column("CompanyClass")]
        public string? CompanyClass { get; set; }

        // Company Type
        [Column("CompanyType")]
        public string? CompanyType { get; set; }

        // Contribution Frequency
        [Column("ContributionFrequency")]
        public int ContributionFrequency { get; set; }

        // Webbsite
        [Column("Webbsite")]
        public string? Webbsite { get; set; }

        // Company's Logo
        [Column("Logo")]
        public byte[]? Logo { get; set; }

        // Company's LogoType
        [Column("LogoType")]
        public string? LogoType { get; set; }

        // Remark
        [Column("Remark")]
        public string? Remark { get; set; }

        // Company Number
        [Column("Company"), Description("Company")]
        public long Company { get; set; }

        // Branch Number
        [Column("Branch"), Description("Branch")]
        public long Branch { get; set; }

        // Department
        [Column("Department"), Description("Department")]
        public int Department { get; set; }

        // BVN
        [Column("BVN"), Description("BVN")]
        public string? BVN { get; set; }

        // NIN
        [Column("NIN"), Description("NIN")]
        public string? NIN { get; set; }

        // Employee Type
        [Column("EmploymentType"), Description("EmploymentType")]
        public string? EmploymentType { get; set; }

        // Date Of Employment
        [Column("DateOfEmployment"), Description("Employment Date")]
        public DateTime? DateOfEmployment { get; set; }

        // Title
        [Column("Title"), Description("Title")]
        public int Title { get; set; }

        // FirstName
        [Column("FirstName"), Description("First Name")]
        public string? FirstName { get; set; }

        // LastName
        [Column("LastName"), Description("Last Name")]
        public string? LastName { get; set; }

        // OtherName
        [Column("OtherName"), Description("Other Name(s)")]
        public string? OtherName { get; set; }

        // Gender
        [Column("Gender"), Description("Gender")]
        public int Gender { get; set; }

        // DateOfBirth
        [Column("DateOfBirth"), Description("DOB")]
        public string? DateOfBirth { get; set; }

        // MaritalStatus
        [Column("MaritalStatus"), Description("Marital Status")]
        public int MaritalStatus { get; set; }

        // EmailAddress
        [Column("EmailAddress"), Description("Email")]
        public string? EmailAddress { get; set; }

        // MobileNumber
        [Column("MobileNumber"), Description("Mobile")]
        public string? MobileNumber { get; set; }

        // StaffNumber
        [Column("StaffNumber"), Description("Staff Number")]
        public string? StaffNumber { get; set; }

        // CustomerBank
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

        // AlertType
        [Column("AlertType"), Description("Alert Type")]
        public int AlertType { get; set; }

        // Portrait
        [Column("Portrait")]
        public byte[]? Portrait { get; set; }

        // Portrait Type
        [Column("PortraitType")]
        public string? PortraitType { get; set; }

        // Designation
        [NotMapped]
        public long? Designation { get; set; }

        // Role
        [NotMapped]
        public string? RoleIds { get; set; }
    }
}