﻿using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Entities
{
    // Agent type table
    [Table("st_CreditType")]
    public class CreditTypeEntity : IdentityExtensionEntity
    {
        // Name
        [Column("Name")]
        public string? Name { get; set; }

        // Description
        [Column("Description")]
        public string? Description { get; set; }

        // Code
        [Column("Code")]
        public string? Code { get; set; }
        
        [Column("ProductId")]
        public int ProductId { get; set; }

        [Column("ProductScheme")]
        public int ProductScheme { get; set; }
    }
}