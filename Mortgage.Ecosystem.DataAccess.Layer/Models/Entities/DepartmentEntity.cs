using System.ComponentModel.DataAnnotations.Schema;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Base;

namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Entities
{
    // Department table
    [Table("tbl_Department")]
    public class DepartmentEntity : IdentityExtensionEntity
    {
        // Company
        [Column("Company")]
        public long Company { get; set; }

        // Branch department
        [Column("Branch")]
        public long? Branch { get; set; }

        // Department name
        [Column("Name")]
        public string? Name { get; set; }

        // Department phone
        [Column("Telephone")]
        public string? Telephone { get; set; }

        // Department Fax
        [Column("Fax")]
        public string? Fax { get; set; }

        // Department Email
        [Column("Email")]
        public string? Email { get; set; }

        // Department head Id
        [Column("Principal")]
        public long? Principal { get; set; }

        // Sort by department
        [Column("Sort")]
        public int Sort { get; set; }

        // Remark
        [Column("Remark")]
        public string? Remark { get; set; }

        // Multiple department IDs
        [NotMapped]
        public string? Ids { get; set; }

        // Name of the person in charge
        [NotMapped]
        public string? PrincipalName { get; set; }
    }
}