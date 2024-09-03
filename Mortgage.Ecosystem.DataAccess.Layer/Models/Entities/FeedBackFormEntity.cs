using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Base;

namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Entities
{
    // Company Information table
    [Table("tbl_FeedBackForm")]
    public class FeedBackFormEntity : BaseExtensionEntity
    {
        [Column("Company"), Description("Company")]
        public long Company { get; set; }

        [Column("Branch")]
        public int? Branch { get; set; }

        [Column("NHFNumber")]
        public long NHFNumber { get; set; }

        // Employee Type
        [Column("EmploymentType"), Description("EmploymentType")]
        public int EmploymentType { get; set; }

        // Employee name
        [Column("Name")]
        public string? Name { get; set; }

        // Email address
        [Column("EmailAddress")]
        public string? EmailAddress { get; set; }

        // Feedback Date Sent
        [Column("DateSent")]
        public DateTime? DateSent { get; set; }

        // Status
        [Column("Status"), Description("Status")]
        public int Status { get; set; }

        // Rating Score
        [Column("RatingScore")]
        public decimal? RatingScore { get; set; }

        // Used Application Feature
        [Column("UsedApplicationFeature")]
        public string? UsedApplicationFeature { get; set; }

        // Overall Experience
        [Column("OverallRating")]
        public string? OverallRating { get; set; }

        // Navigation Rating
        [Column("NavigationRating")]
        public string? NavigationRating { get; set; }

        // Design Rating
        [Column("DesignRating")]
        public string? DesignRating { get; set; }

        // Application Performance
        [Column("PerformanceRating")]
        public string? PerformanceRating { get; set; }

        // Application Most Liked
        [Column("ApplicationMostLiked")]
        public string? ApplicationMostLiked { get; set; }

        // Application Least Liked
        [Column("ApplicationLeastLiked")]
        public string? ApplicationLeastLiked { get; set; }

        // Suggested Improvements
        [Column("SuggestedImprovements")]
        public string? SuggestedImprovements { get; set; }

        // Additional Comments
        [Column("AdditionalComments")]
        public string? AdditionalComments { get; set; }

        // Response Message
        [Column("ResponseMessage")]
        public string? ResponseMessage { get; set; }
    }
}