using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Entities
{
    // User's table
    [Table("tbl_UserBelong")]
    public class UserBelongEntity : BaseCreateEntity
    {
        // Company
        [Column("Company")]
        public long Company { get; set; }

        // Employee
        [Column("Employee")]
        public long Employee { get; set; }

        // User Id or Role Id
        [Column("Belong")]
        public long Belong { get; set; }

        // Type (1:User, 2:Role)
        [Column("BelongType")]
        public int BelongType { get; set; }

        // Multiple user IDs
        [NotMapped]
        public string? UserIds { get; set; }

        // Multiple employees
        [NotMapped]
        public string? Employees { get; set; }
    }
}