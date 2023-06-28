using System.ComponentModel.DataAnnotations.Schema;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Base;

namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Entities
{
    // Internet Banking Users table
    [Table("tbl_InternetBankingUsers")]
    public class InternetBankingUsersEntity : BaseExtensionEntity
    {
        // User Account Number
        [Column("AccountNo")]
        public string? AccountNo { get; set; }

        //Use Account Name
        [Column("AccountName")]
        public string? AccountName { get; set; }

        // User Code 
        [Column("CustomerCode")]
        public string? CustomerCode { get; set; }

        // User Date Created
        [Column("DateCreated")]
        public DateTime? DateCreated { get; set; }

        // User Status
        [Column("Status")]
        public string? Status { get; set; }
    }
}

