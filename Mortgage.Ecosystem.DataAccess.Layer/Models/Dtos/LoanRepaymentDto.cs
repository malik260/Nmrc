using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos
{
    public class LoanRepaymentDto
    {
        public long EmployeeNhfNumber { get; set; }
        public string? EmployeeName { get; set; }
        public decimal Totalamount { get; set; }
        public decimal Amount { get; set; }

        public string Year { get; set; }

        public string Narration { get; set; }

        public string Month { get; set; }

        public string Paymentoption { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }

    }
}
