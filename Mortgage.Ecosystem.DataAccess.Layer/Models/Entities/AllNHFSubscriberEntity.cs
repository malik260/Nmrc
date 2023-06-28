using System.ComponentModel.DataAnnotations.Schema;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Base;

namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Entities
{
    // Company Information table
    [Table("tbl_AllNHFSubscriber")]
    public class AllNHFSubscriberEntity : BaseExtensionEntity
    {
        // NHF Employer Number
        [Column("NHFEmployerNumber")]
        public string? NHFEmployerNumber { get; set; }

        // NHF Employee Number
        [Column("NHFEmployeeNumber")]
        public string? NHFEmployeeNumber { get; set; }

        // Employee Name
        [Column("Name")]
        public string? Name { get; set; }

        // Email Address
        [Column("Email")]
        public string? Email { get; set; }

        // Phone Number
        [Column("Phone")]
        public string? Phone { get; set; }

        // Status
        [Column("Status")]
        public string? Status { get; set; }

        // Contact Person
        [Column("ContactPerson")]
        public string? ContactPerson { get; set; }

        // Company Name
        [Column("CompanyName")]
        public string? CompanyName { get; set; }

        // Company Number
        [Column("CompanyNumber")]
        public string? CompanyNumber { get; set; }

        // Incorporation Date
        [Column("DateOfIncorporation")]
        public DateTime? DateOfIncorporation { get; set; }

    }  
}