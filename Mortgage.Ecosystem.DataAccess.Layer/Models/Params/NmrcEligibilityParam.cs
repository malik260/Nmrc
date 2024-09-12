using System.ComponentModel.DataAnnotations.Schema;

namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Params
{
    public class NmrcEligibilityListParam
    {
        public int Category { get; set; }
        public string? Item { get; set; }
        public string? Description { get; set; }
        [NotMapped]
        public string? CategoryName { get; set; }
    }
}
