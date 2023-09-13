using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Base;

namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Entities
{
    // E-Ticket table
    [Table("tbl_ETicket")]
    public class ETicketEntity : BaseExtensionEntity
    {
        // Company Number
        [Column("Company"), Description("Company")]
        public long Company { get; set; }

        [Column("Branch")]
        public int? Branch { get; set; }

        [Column("NHFNumber")]
        public string? NHFNumber { get; set; }

        [Column("MessageType")]
        public string? MessageType { get; set; }

        // E-Ticket Request Number
        [Column("RequestNumber")]
        public string? RequestNumber { get; set; }

        // E-Ticket Subject
        [Column("Subject")]
        public string? Subject { get; set; }

        // E-Ticket Message
        [Column("Message")]
        public string? Message { get; set; }

        // E-Ticket Date Sent
        [Column("DateSent")]
        public DateTime? DateSent { get; set; }

        // Status
        [Column("Status"), Description("Status")]
        public int Status { get; set; }

        // Email Address
        [Column("EmailAddress"), Description("Email Address")]
        public string? EmailAddress { get; set; }

        [NotMapped]
        public string? CompanyName { get; set; }

        // Employee Type
        [Column("EmploymentType"), Description("EmploymentType")]
        public int EmploymentType { get; set; }

    }
}

