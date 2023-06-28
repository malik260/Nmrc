using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Entities
{   // Alert type table
    [Table("tbl_Contribution")]
    public class ContributionEntity : BaseExtensionEntity
    {
        // Employee Number
        [Column("EmployeeNumber")]
        public string? employeeNumber { get; set; }

        // Employee Name
        [Column("Employer Name")]
        public string? Name { get; set; }

        // Contribution Amount
        [Column("ContributionAmount")]
        public decimal contributionAmount { get; set; }

        // Employer Number
        [Column("Employer Number")]
        public string? employerNumber { get; set; }

        // Month
        [Column("Month")]
        public string? month { get; set; }

        // Year
        [Column("Year")]
        public string? year { get; set; }

        // Year
        [Column("Employer Name")]
        public string? employerName { get; set; }

        // Year
        [Column("Remarks")]
        public string? remarks { get; set; }


        // Year
        [Column("PaymentOption")]
        public int paymentOption { get; set; }

        // Acount Name
        [Column("AccountName")]
        public string? accountName { get; set; }

        // Narration
        [Column("Narration")]
        public string? naration { get; set; }

        // Phone Number
        [Column("PhoneNumber")]
        public string? phoneNumber { get; set; }

        // Email
        [Column("Email")]
        public string? Email { get; set; }

        // Narration
        [Column("Document")]
        public string? document { get; set; }

        // File Logo
        [Column("File")]
        public byte[]? file { get; set; }
    }
}