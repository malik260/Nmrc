using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Entities
{
    // Branch table
    [Table("tbl_Branch")]
    public class BranchEntity : BaseExtensionEntity
    {
        // Company
        [Column("Company")]
        public long Company { get; set; }

        // Name
        [Column("Name")]
        public string? Name { get; set; }

        // Address
        [Column("Address")]
        public string? Address { get; set; }

        // Mobile number
        [Column("MobileNumber")]
        public string? MobileNumber { get; set; }

        // Location
        [Column("Location")]
        public string? Location { get; set; }

        // State
        [Column("State")]
        public string? State { get; set; }

        // Nationality
        [Column("Nationality")]
        public string? Nationality { get; set; }

        // Manager
        [Column("Manager")]
        public long Manager { get; set; }
    }
}