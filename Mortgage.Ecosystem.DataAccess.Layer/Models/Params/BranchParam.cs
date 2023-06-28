using System.ComponentModel.DataAnnotations.Schema;

namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Params
{
    public class BranchListParam
    {
        public long Id { get; set; }
        public long Company { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? MobileNumber { get; set; }
        public string? Location { get; set; }
        public string? State { get; set; }
        public string? Nationality { get; set; }
        public long Manager { get; set; }

        [NotMapped]
        public List<long>? Ids { get; set; }
    }
}
