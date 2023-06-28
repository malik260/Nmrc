using System.ComponentModel.DataAnnotations.Schema;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Base;

namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Entities
{
    // Company Information table
    [Table("Remita_TblRemitaPaymentDetails")]
    public class RemitaPaymentDetailsEntity : BaseExtensionEntity
    {
        [Column("TransactionId")]
        public string TransactionId { get; set; }

        [Column("TransactionDate")]
        public DateTime? TransactionDate { get; set; }

        [Column("EmployeeNumber")]
        public string EmployeeNumber { get; set; }

        [Column("EmployerNumber")]
        public string EmployerNumber { get; set; }

        [Column("Status")]
        public decimal? Status { get; set; }

        [Column("Rrr")]
        public string Rrr { get; set; }

        [Column("LoggedUser")]
        public string LoggedUser { get; set; }

        [Column("Device")]
        public string Device { get; set; }

        [Column("Amount")]
        public string Amount { get; set; }
    }
}