using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels
{
    public class BacklogSingleContributionResultVM
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

        public List <ErrorListBacklog>  ErrorLists { get; set; }
        
    }

    public class ErrorListBacklog
    {
        public int SN { get; set; }
        public string? Error { get; set; }
    }
}
