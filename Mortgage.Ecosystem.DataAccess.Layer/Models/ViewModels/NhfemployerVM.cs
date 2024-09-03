using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels
{
    public class NhfemployerVM
    {
        public int Employerid { get; set; }
        public string Employernumber { get; set; }
        public string Employername { get; set; }
        public string Addressline1 { get; set; }
        public string Addressline2 { get; set; }
        public string Postaladdress { get; set; }
        public string Economicsector { get; set; }
        public string Subsector { get; set; }
        public string Telephonenumber { get; set; }
        public string Mobilenumber { get; set; }
        public string Faxnumber { get; set; }
        public string Telexnumber { get; set; }
        public string Emailaddress { get; set; }
        public string Noofemployees { get; set; }
        public string Contactperson { get; set; }
        public string Contactpersondesignation { get; set; }
        public string Contributionmode { get; set; }
        public string Contributionlocation { get; set; }
        public string Rcnumber { get; set; }
        public int? Deleted { get; set; }
        public int? Operationid { get; set; }
        public string Accountofficer { get; set; }
        public string Batchrefr { get; set; }
        public DateTime? Datecreated { get; set; }
        //public byte? Approved { get; set; }
        public int? Approved { get; set; }
        public DateTime? Dateapproved { get; set; }
        public string Approvedby { get; set; }
        public string Approvedcomment { get; set; }
        public string Branchcode { get; set; }
        public int? Companycode { get; set; }
        public string Registratiolocation { get; set; }
        public string Createdby { get; set; }
        public string Employercode { get; set; }
        public string Nuban { get; set; }
        public DateTime? DateUpdated { get; set; }


    }

    public class CusDetailsToCheck
    {
        public string EmailAddress { get; set; }
        public string MobileNo { get; set; }
        public string BVN { get; set; }
    }
    public class CustomerCreationResponses
    {
        public string responseCode { get; set; }
        public string responseText { get; set; }
        public string message { get; set; }
        public CustomerProfile CustomerProfile { get; set; }
    }

    public class CustomerProfile
    {
        public string NhfNumber { get; set; }
        public string CustomerCode { get; set; }
    }
}
