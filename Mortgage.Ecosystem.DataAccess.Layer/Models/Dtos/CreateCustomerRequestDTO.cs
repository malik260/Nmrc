using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos
{
    public class CreateCustomerRequestDTO
    {
        public string CustomerCode { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string EmailAddress { get; set; }
        public int CrmsLegalStatusId { get; set; }
        public int CrmsRelationshipTypeId { get; set; }
        public string TaxNumber { get; set; }
        public string BranchCode { get; set; }
        public bool IsEmailValidated { get; set; }
        public bool IsBvnValidated { get; set; }
        public bool IsPhoneValidated { get; set; }
        public string Gender { get; set; }
        public string PlaceOfBirth { get; set; }
        public string Nationality { get; set; }
        public bool IsPoliticallyExposed { get; set; }
        public string Occupation { get; set; }
        public string CustomerBVN { get; set; }
        public ContactAddress contactAddress { get; set; }
        public ContactPhone contactPhone { get; set; }
        public int? SubSectorId { get; set; }
        public AccountDetails AccountDetails { get; set; }
        public NextofKin nextOfKin { get; set; }
    }

    public class AccountDetails
    {
        public string AccountStatusName { get; set; }
        public string AccountNumber { get; set; }
        public string ProductAccountName { get; set; }
        public DateTime? DateOfEmployment { get; set; }
        public string MonthlyIncome { get; set; }
        public string OtherBankAccountNumber { get; set; }
        public string OtherBankSortCode { get; set; }
    }
    public class NextofKin
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string emailAddress { get; set; }
        public string gender { get; set; }
        public DateTime dateOfBirth { get; set; }
        public string relationship { get; set; }
        public string nearestLandmark { get; set; }
        public int cityId { get; set; }
        public string contactAddress { get; set; }
        public string mobilePhoneNo { get; set; }
    }

    public class ContactAddress
    {
        public int StateId { get; set; }
        public string NearestLandmark { get; set; }
        public int CityId { get; set; }
        public string UtilityBillNo { get; set; }
        public string MailingAddress { get; set; }
        public string contactAddress { get; set; }
        public int AddressTypeId { get; set; }
    }

    public class ContactPhone
    {
        public string MobilePhoneNo { get; set; }
        public string OfficeLandNo { get; set; }
    }




}
