using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
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
        public long ComapnyNumber { get; set; }

        // Property Type
        [Column("PropertyType")]
        public string? PropertyType { get; set; }

        // Property Location
        [Column("PropertyLocation")]
        public string? PropertyLocation { get; set; }

        // GeoTagging
        [NotMapped]
        [Column("GeoTagging")]
        public string? GeoTagging { get; set; }

        // Latitude Number
        [Column("Latitude"), Description("GeoTagging")]
        public double Latitude { get; set; }

        // Longitude Number
        [Column("Longitude"), Description("GeoTagging")]
        public double Longitude { get; set; }


        // Phone Number
        [Column("PhoneNumber")]
        public string? PhoneNumber { get; set; }

        // Email Address
        [Column("Email")]
        public string? Email { get; set; }

        // Property Description
        [Column("PropertyDescription")]
        public string? PropertyDescription { get; set; }

        // Neighbourhood
        [Column("Address")]
        public string? Address { get; set; }

        [Column("AvailableUnits")]
        public int? AvailableUnits { get; set; }


        // Property Title
        [NotMapped]
        [Column("PropertyTitle")]
        public string? PropertyTitle { get; set; }

        // Property Image
        [NotMapped]
        [Column("Images")]
        public string? Images { get; set; }

        [NotMapped]
        [Column("Size")]
        public double Size { get; set; }

        [NotMapped]
        [Column("file")]
        public List<IFormFile>? file { get; set; }

        [NotMapped]
        [Column("DocumentTitle")]
        public string? DocumentTitle { get; set; }


    }
}