using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Params
{
    public class CustomerProfileUpdateListParam
    {
        public long Company { get; set; }
        public long Employee { get; set; }
        public long Branch { get; set; }
        public int Department { get; set; }
        public long NHFNumber { get; set; }
        public string? BVN { get; set; }
        public string? NIN { get; set; }
        public int EmploymentType { get; set; }
        public DateTime? DateOfEmployment { get; set; }
        public int Title { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? OtherName { get; set; }
        public int Gender { get; set; }
        public string? DateOfBirth { get; set; }
        public int MaritalStatus { get; set; }
        public string? PostalAddress { get; set; }
        public string? EmailAddress { get; set; }
        public string? MobileNumber { get; set; }
        public string? StaffNumber { get; set; }
        public string? CustomerBank { get; set; }
        public string? BankAccountNumber { get; set; }
        public int AccountType { get; set; }
        public decimal MonthlyIncome { get; set; }
        public int AlertType { get; set; }
        public int ContributionBranch { get; set; }
        public byte[]? Portrait { get; set; }
        public string? PortraitType { get; set; }
        public int UserType { get; set; }
        public int Status { get; set; }
    }
}
