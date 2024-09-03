using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos
{
    public class AffordabilityResponseDto
    {
        public string amountRequested { get; set; }
        public string affordableAmount { get; set; }
        public string monthlyRepayment { get; set; }
        public int? proposedTenor { get; set; }
        public decimal rate { get; set; }
        public string message { get; set; }
    }
}
