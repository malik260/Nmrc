using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Entities
{
    [Table("tbl_LoanDisbursement")]
    public class LoanDisbursementEntity: BaseExtensionEntity
    {     

        [Column("ProductCode")]
        public string? ProductCode { get; set; }

        [Column("Tenor")]
        public int? Tenor { get; set; }

        [Column("Amount")]
        public decimal Amount { get; set; }

        [Column("Rate")]
        public int Rate { get; set; }

        [Column("CustomerNhf")]
        public string? CustomerNhf { get; set; }

        [Column("CustomerName")]
        public string? CustomerName { get; set; }

        [Column("RepaymentStatus")]
        public int? RepaymentStatus { get; set; }

        [Column("DisbursementDate")]
        public DateTime? DisbursementDate { get; set; }

         [Column("PmbNhfNumber")]
        public long PmbId { get; set; }

          [Column("LoanId")]
        public string LoanId { get; set; }



    }
}
