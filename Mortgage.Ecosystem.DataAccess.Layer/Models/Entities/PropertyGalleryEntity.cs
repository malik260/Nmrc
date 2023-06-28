using System.ComponentModel.DataAnnotations.Schema;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Base;

namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Entities
{
    // Company Information table
    [Table("tbl_PropertyGallery")]
    public class PropertyGalleryEntity : BaseExtensionEntity
    {
        // Property Type
        [Column("PropertyType")]
        public int PropertyType { get; set; }

        // Price RangeMin
        [Column("PriceRangeMin")]
        public decimal PriceRangeMin { get; set; }

        // Price RangeMax
        [Column("PriceRangeMax")]
        public decimal PriceRangeMax { get; set; }

        // Sort
        [Column("Sort")]
        public int Sort { get; set; }

        //Search By Location
        [Column("Location")]
        public int Location { get; set; }

        //Title
        [Column("Title")]
        public string? Title { get; set; }

        //Description
        [Column("Description")]
        public string? Description { get; set; }


    }  
}