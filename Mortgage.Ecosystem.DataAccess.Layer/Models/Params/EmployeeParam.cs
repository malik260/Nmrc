using System.ComponentModel.DataAnnotations.Schema;

namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Params
{
    public class EmployeeProcessParam
    {
        public long BaseProcessMenu { get; set; }
    }

    public class EmployeeListParam
    {
        public long Id { get; set; }
        public long Company { get; set; }
        public long? Branch { get; set; }
        public int Department { get; set; }
        public long NHFNumber { get; set; }
        public string? BVN { get; set; }
        public string? NIN { get; set; }
        public int? EmploymentType { get; set; }
        public int Title { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? EmailAddress { get; set; }
        public string? StaffNumber { get; set; }
        public string? DateOfBirth { get; set; }
        public int Gender { get; set; }
        public byte[]? Portrait { get; set; }
        public string? PortraitType { get; set; }

        [NotMapped]
        public List<long>? Ids { get; set; }
    }
}
