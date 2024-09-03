using System.ComponentModel.DataAnnotations.Schema;

namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Params
{
    public class AuditTrailListParam
    {
        // User Name
        [Column("User Name")]
        public string? UserName { get; set; }

        // Company
        [Column("Company")]
        public string? Company { get; set; }

        // Action
        [Column("Action")]
        public string? Action { get; set; }

        // Action Route
        [Column("Action Route")]
        public string? ActionRoute { get; set; }

        // Ip Address
        [Column("IP Address")]
        public string? IpAddress { get; set; }

        // Mac Address
        [Column("Mac Address")]
        public string? MacAddress { get; set; }

        // Transaction Date
        [Column("Transaction Date")]
        public string? TransactionDate { get; set; }


    }
}
