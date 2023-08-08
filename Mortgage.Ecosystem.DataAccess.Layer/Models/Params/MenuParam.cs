using System.ComponentModel.DataAnnotations.Schema;

namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Params
{
    public class MenuListParam
    {
        public long Parent { get; set; }
        public string? MenuName { get; set; }
        public int? MenuStatus { get; set; }
        public string? MenuUrl { get; set; }

        [NotMapped]
        public List<long>? Ids { get; set; }
    }
}
