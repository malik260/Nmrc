using System.ComponentModel.DataAnnotations.Schema;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Base;

namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Entities
{
    // Company Information table
    [Table("tbl_ContributionHistory")]
    public class ContributionHistoryEntity : BaseExtensionEntity
    {
        // Start Date
        [Column("StartDate")]
        public DateTime? StartDate { get; set; }

        // Start Date
        [Column("EndDate")]
        public DateTime? EndDate { get; set; }

        // NHF Number
        [Column("NHFNumber")]
        public string? NHFNumber { get; set; }

        // Employee Name
        [Column("Name")]
        public string? Name { get; set; }

        // Employer Name
        [Column("EmployerName")]
        public string? EmployerName { get; set; }

        // Amount Contributed
        [Column("AmountContributed")]
        public decimal? AmountContributed { get; set; }

        // Remark
        [Column("Remark")]
        public string? Remark { get; set; }

        // Month
        [Column("Month")]
        public string? Month { get; set; }

        // Year
        [Column("Year")]
        public string? Year { get; set; }
    }
}