using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Entities
{
    // Next of kin table
    [Table("tbl_NextOfKin")]
    public class NextOfKinEntity : IdentityExtensionEntity
    {
        // Company
        [Column("Company")]
        public long Company { get; set; }

        // Employee
        [Column("Employee")]
        public long Employee { get; set; }

        // Kin's First Name
        [Column("FirstName")]
        public string? FirstName { get; set; }

        // Kin's Last Name
        [Column("LastName")]
        public string? LastName { get; set; }

        // Kin's Address
        [Column("Address")]
        public string? Address { get; set; }

        // Kin's Email address
        [Column("EmailAddress")]
        public string? EmailAddress { get; set; }

        // Kin's Mobile number
        [Column("MobileNumber")]
        public string? MobileNumber { get; set; }

        [Column("Relationship")]
        public int Relationship { get; set; }
    }
}