using System.ComponentModel.DataAnnotations.Schema;

namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Params
{
    public class StateListParam
    {
        public string? Code { get; set; }

        public string? Name { get; set; }

        public string? Capital { get; set; }

        public string? Narration { get; set; }

        public string? Nationality { get; set; }

        [NotMapped]
        public List<int>? Ids { get; set; }
    }
}
