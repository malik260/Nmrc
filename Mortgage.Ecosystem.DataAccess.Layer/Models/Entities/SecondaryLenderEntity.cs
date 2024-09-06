using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Base;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Entities
{
    // Company Information table
    [Table("tbl_SecondaryLender")]
    public class SecondaryLenderEntity : BaseExtensionEntity
    {
        // Company name
        [Column("Name")]
        public string? Name { get; set; }

        // Company name
        //[Column("PmbNhfNumber")]
        //public string? PmbNhfNumber { get; set; }

        // Company Sector
        [Column("Sector")]
        public int Sector { get; set; }

        // Company Subsector
        [Column("Subsector")]
        public int Subsector { get; set; }

        // Company Subsector
        [Column("Tin")]
        public string? Tin { get; set; }

        // Company Subsector
        [Column("Website")]
        public string? Website { get; set; }


        // Company address
        [Column("Address")]
        public string? Address { get; set; }

        // Company Mobile/Telephone
        [Column("MobileNumber")]
        public string? MobileNumber { get; set; }

        // Company Fax
        [Column("Fax")]
        public string? Fax { get; set; }

        // Company Email Address
        [Column("EmailAddress")]
        public string? EmailAddress { get; set; }

        // Contribution Frequency
        [Column("ContributionFrequency")]
        public int ContributionFrequency { get; set; }

         [Column("SecondaryLenderCode")]
        public string? SecondaryLenderCode { get; set; }


        // Company RCNumber
        [Column("RCNumber")]
        public string? RCNumber { get; set; }

        // Incorporation Date
        [Column("DateOfIncorporation")]
        public DateTime? DateOfIncorporation { get; set; }

        // Status
        [Column("Status"), Description("Status")]
        public int Status { get; set; }

        [NotMapped]
        public string? AgentType { get; set; }

        // Individual NHF Number
        [Column("NHFNumber")]
        public string? NHFNumber { get; set; }

        // Individual BVN
        [NotMapped]
        public string? IndBVN { get; set; }

        // Individual First Name
        [NotMapped]
        public string? IndFirstName { get; set; }

        // Individual Last Name
        [NotMapped]
        public string? IndLastName { get; set; }

        // Individual DateOfBirth
        [NotMapped]
        public string? IndDateOfBirth { get; set; }

        // Username
        [NotMapped]
        public string? UserName { get; set; }

        // Role
        [NotMapped]
        public long Role { get; set; }

        [NotMapped]
        public string? Password { get; set; }

        [NotMapped]
        public string? DecryptedPassword { get; set; }

        [NotMapped]
        public string? Salt { get; set; }

        [NotMapped]
        public long Employee { get; set; }

        [NotMapped]
        public int User { get; set; }
        [NotMapped]
        public string? SectorName { get; set; }

        [NotMapped]
        public string? SubSectorName { get; set; }

        [NotMapped]
        public string? ContributionFrequencyName { get; set; }
    }
}