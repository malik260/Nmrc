using System.ComponentModel.DataAnnotations.Schema;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Base;

namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Entities
{
    // Company Information table
    [Table("tbl_PropertySubscription")]
    public class PropertySubscriptionEntity : BaseExtensionEntity
    {
        // Property Type
        [Column("PropertyType")]
        public string? PropertyType { get; set; }

        // Property Location
        [Column("PropertyLocation")]
        public string? PropertyLocation { get; set; }

        // Phone Number
        [Column("PhoneNumber")]
        public string? PhoneNumber { get; set; }

        // Email Address
        [Column("Email")]
        public string? Email { get; set; }

        // Geo-Tagging
        [Column("GeoTagging")]
        public string? GeoTagging { get; set; }

        // Email Address
        [Column("Developer")]
        public string? Developer { get; set; }

        [Column("Subscriber")]
        public string? Subscriber { get; set; }

        // Property Description
        [Column("PropertyDescription")]
        public string? PropertyDescription { get; set; }

        // Property Pictures
        //[Column("ViewPictures")]
        //public byte? ViewPictures { get; set; }

        // Property Actions
        [NotMapped]
        [Column("Actions")]
        public string? Actions { get; set; }

       
    }
}