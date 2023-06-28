using System.ComponentModel.DataAnnotations.Schema;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Base;

namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Entities
{
    // Company Information table
    [Table("tbl_FeedBackForm")]
    public class FeedBackFormEntity : BaseExtensionEntity
    {
        // Employee name
        [Column("Name")]
        public string? Name { get; set; }

        // Email address
        [Column("EmailAddress")]
        public string? EmailAddress { get; set; }

        // Subject
        [Column("Subject")]
        public string? Subject { get; set; }

        // Messages
        [Column("Message")]
        public string? Messages { get; set; }       
    }
}