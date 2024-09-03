using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels
{
    public class NhfEmployeeTemp
    {
        public string Employeeid { get; set; }
        public string Employernumber { get; set; }
        public string EmployeeNhfNumber { get; set; }
        public string Title { get; set; }
        public string Gender { get; set; }
        public string Surname { get; set; }

        public string Firstname { get; set; }

        public string Othername { get; set; }

        public DateTime? Dateofbirth { get; set; }

        public string Bvn { get; set; }

        public string Nin { get; set; }
        public string Addressline1 { get; set; }
        public string Addressline2 { get; set; }
        public string Contributionlocation { get; set; }
        public string Staffid { get; set; }
        public string Type { get; set; }

        public string Monthlysalary { get; set; }

        public string Bankaccountno { get; set; }
        public string Bankname { get; set; }
        public string Mobilenumber { get; set; }

        public string Emailaddress { get; set; }
        public string Registrationlocation { get; set; }
       
        public string Nextofkinsurname { get; set; }

        public string Nextofkinaddress { get; set; }

        public string Nextofkinphonenumber { get; set; }

        public string NextofkinFirstname { get; set; }
        public string NextofkinRelationship { get; set; }
        public int? Deleted { get; set; }
        public string Employername { get; set; }
        public string Employeremail { get; set; }
        public string Employermobile { get; set; }
        public string Batchref { get; set; }
        public DateTime? Datecreated { get; set; }
        public byte? Approved { get; set; }
        public byte? Disapproved { get; set; }
        public DateTime? Dateapproved { get; set; }
        public string Approvedby { get; set; }
        public string Approvedcomment { get; set; }
        public string Branchcode { get; set; }
        public string Companycode { get; set; }
        public string AccountNumber { get; set; }
        public string Nuban { get; set; }
        public int? Operationid { get; set; }
        public string Maritalstatus { get; set; }
        public string Accounttype { get; set; }
        public string Alerttype { get; set; }
        public string Statement { get; set; }
        public DateTime? DateofEmployment { get; set; }
        public string Createdby { get; set; }
        public DateTime? DateUpdated { get; set; }
        public byte? Referback { get; set; }
        public int? Ismerged { get; set; }
        public string Primarynhfnumber { get; set; }
        public int? UserConsent { get; set; }
        public int? Sent { get; set; }
        public int? IsEmailSent { get; set; }

    }
}
