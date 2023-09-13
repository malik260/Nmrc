using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels
{
    public class NhfEmployerBatchContributionViewModel
    {
        public string Totalamount { get; set; }
        public DateTime Paymentdate { get; set; }
        public string Receiptnumber { get; set; }
        public string Narration { get; set; }
        //public string Employernumber { get; set; }
        //public byte[] Upload { get; set; }
        public DateTime Contributiondate { get; set; }
        public string Contributionmonth { get; set; }
        public string Contributionyear { get; set; }
        //public string Uploadmode { get; set; }
        public string EmployerName { get; set; }
        public string EmployerNo { get; set; }
        public string Lastname { get; set; }
        public string Firstname { get; set; }
        public string MiddleName { get; set; }
        public string NhfNo { get; set; }
        public string Amount { get; set; }
        public string EmployeeNumber { get; set; }       
        public string Accountname { get; set; }
        public string Bankname { get; set; }
        public int Paymentoption { get; set; }
        public string EmailAddress { get; set; }
    }
}
