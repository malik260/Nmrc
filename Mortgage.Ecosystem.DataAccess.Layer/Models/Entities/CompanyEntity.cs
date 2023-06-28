using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Base;

namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Entities
{
    // Company Information table
    [Table("tbl_Company")]
    public class CompanyEntity : BaseExtensionEntity
    {
        // Company name
        [Column("Name")]
        public string? Name { get; set; }

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
        public int CompanyClass { get; set; }

        // Company Type
        [Column("CompanyType")]
        public int CompanyType { get; set; }

        // Contribution Frequency
        [Column("ContributionFrequency")]
        public int ContributionFrequency { get; set; }

        // Webbsite
        [Column("Website")]
        public string? Website { get; set; }

        // Company's Logo
        [Column("Logo")]
        public byte[]? Logo { get; set; }

        // Company's LogoType
        [Column("LogoType")]
        public string? LogoType { get; set; }

        // Remark
        [Column("Remark")]
        public string? Remark { get; set; }

        [NotMapped]
        public string? AgentType { get; set; }

        [NotMapped]
        public string? SectorName { get; set; }

        [NotMapped]
        public string? CompanyClassName { get; set; }

        [NotMapped]
        public string? CompanyTypeName { get; set; }

        // Individual NHF Number
        [NotMapped]
        public long NHFNumber { get; set; }

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
    }
}