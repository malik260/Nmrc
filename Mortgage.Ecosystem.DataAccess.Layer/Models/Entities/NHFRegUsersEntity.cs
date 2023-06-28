using System;
using System.ComponentModel.DataAnnotations.Schema;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Base;

namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Entities
{
    // CNHF Reg. User table
    [Table("tbl_NHFRegUsers")]
    public class NHFRegUsersEntity : BaseExtensionEntity
    {
        // NHF Employer Number
        [Column("NHFEmployerNumber")]
        public string? NHFEmployerNumber { get; set; }

        // Employee Name
        [Column("Name")]
        public string? Name { get; set; }

        // Employee Email Address
        [Column("Email")]
        public string? Email { get; set; }

        // Effective Date
        [Column("EffectiveDate")]
        public DateTime? EffectiveDate { get; set; }

        // Phone Number
        [Column("Phone")]
        public string? Phone { get; set; }

        // Remark
        [Column("Remark")]
        public string? Remark { get; set; }

        // Marital Status
        [Column("MaritalStatus")]
        public int MaritalStatus { get; set; }

        // Gender
        [Column("Gender")]
        public int Gender { get; set; }

        // BVN
        [Column("BVN")]
        public  string? BVN { get; set; }

        // NIN
        [Column("NIN")]
        public string? NIN { get; set; }

        // Bank Name
        [Column("BankName")]
        public string? BankName { get; set; }

        // Bank Account Number
        [Column("BankAccountNumber")]
        public string? BankAccountNumber { get; set; }

        // Contribution Location
        [Column("ContributionLocation")]
        public string? ContributionLocation { get; set; }

        // Registration Location
        [Column("RegistrationLocation")]
        public string? RegistrationLocation { get; set; }

        //Contact Person
        [Column("ContactPerson")]
        public string? ContactPerson { get; set; }

        // RC Number
        [Column("RCNumber")]
        public string? RCNumber { get; set; }

        // Sector
        [Column("Sector")]
        public int Sector { get; set; }

        // SubSector
        [Column("SubSector")]
        public int SubSector { get; set; }

        // Address
        [Column("Address")]
        public string? Address { get; set; }

        // Address 2
        [Column("Address2")]
        public string? Address2 { get; set; }

        // Contact Person Designation
        [Column("ContactPersonDesignation")]
        public int ContactPersonDesignation { get; set; }

        // Contribution Frequency
        [Column("ContributionFrequency")]
        public int ContributionFrequency { get; set; }

        // Postal Address
        [Column("PostalAddress")]
        public string? PostalAddress { get; set; }


    }
}