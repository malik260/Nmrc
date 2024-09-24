using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Entities
{
    [Table("st_LenderCategory")]

    public  class LenderCategoryEntity: IdentityExtensionEntity
    {
        // Name
        [Column("LenderInstitution")]
        public string? LenderInstitution { get; set; }
        
    }
}
