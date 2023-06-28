using System.ComponentModel.DataAnnotations.Schema;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Base;

namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Entities
{
    // Company Information table
    [Table("tbl_PropertyRegistration")]
    public class PropertyRegistrationEntity : BaseExtensionEntity
    {
        // Company Name
        [Column("CompanyName")]
        public string? ComapnyName { get; set; }

        // Company Number
        [Column("CompanyNumber")]
        public string? ComapnyNumber { get; set; }
        // Property Type
        [Column("PropertyType")]
        public string? PropertyType { get; set; }

        // Property Location
        [Column("PropertyLocation")]
        public string? PropertyLocation { get; set; }

        // GeoTagging
        [Column("GeoTagging")]
        public string? GeoTagging { get; set; }


        // Phone Number
        [Column("PhoneNumber")]
        public string? PhoneNumber { get; set; }

        // Email Address
        [Column("Email")]
        public string? Email { get; set; }

        // Company Name
        [Column("DocumentTitle")]
        public string? DocumentTitle { get; set; }

        // Property Actions
        [NotMapped]
        [Column("Actions")]
        public string? Actions { get; set; }

       
    }
}