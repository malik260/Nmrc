using System.ComponentModel.DataAnnotations.Schema;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Base;

namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Entities
{
    // E-Ticket table
    [Table("tbl_ETicket")]
    public class ETicketEntity : BaseExtensionEntity
    {
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

        // E-Ticket Approval Status
        [Column("ApprovalStatus")]
        public string? ApprovalStatus { get; set; }

        // E-Ticket Approved
        [Column("Approved")]
        public int Approved { get; set; }

        // E-Ticket Disapproved
        [Column("Disapproved")]
        public int Disapproved { get; set; }


    }
}

