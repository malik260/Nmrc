using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels
{
    public class NhfEmployeeStatementVM
    {
        public int Id { get; set; }
        public DateTime? TransactionDate { get; set; }
        public decimal? TotalCredit { get; set; }
        public decimal? TotalDebit { get; set; }
        public string CustomerName { get; set; }
        public decimal? DebitAmount { get; set; }
        public decimal? CreditAmount { get; set; }
        public string Description { get; set; }
        public decimal? Balance { get; set; }
        public decimal? AvailableBalance { get; set; }
        public string Ref { get; set; }
    }
}
