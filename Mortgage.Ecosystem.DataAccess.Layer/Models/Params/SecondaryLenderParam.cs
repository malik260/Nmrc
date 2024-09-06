using System.ComponentModel.DataAnnotations.Schema;

namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Params
{
    public class SecondaryLenderProcessParam
    {
        public long BaseProcessMenu { get; set; }
    }
    public class SecondaryLenderListParam
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? MobileNumber { get; set; }
        public string? EmailAddress { get; set; }
        public string? RCNumber { get; set; }
        public string? NHFNumber { get; set; }

        [NotMapped]
        public List<long>? Ids { get; set; }
    }
}
