using System.ComponentModel.DataAnnotations.Schema;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Base;

namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Entities
{
    // Company Information table
    [Table("tbl_NHFCustomerRequest")]
    public class NHFCustomerRequestEntity : BaseExtensionEntity
    {
        // Phone Number
        [Column("PhoneNumber")]
        public string? PhoneNumber { get; set; }

        // Account Number
        [Column("AccountNumber")]
        public string? AccountNumber { get; set; }

        // Email Address
        [Column("Email")]
        public string? Email { get; set; }

        // Next of Kin
        [Column("NextOfKinName")]
        public string? NextOfKinName { get; set; }

        // Monthly Income
        [Column("MonthlyIncome")]
        public decimal MonthlyIncome { get; set; }

        // Requested Date
        [Column("RequestedDate")]
        public DateTime? RequestedDate { get; set; }

        
    }
}