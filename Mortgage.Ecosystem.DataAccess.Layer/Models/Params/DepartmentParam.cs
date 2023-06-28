using System.ComponentModel.DataAnnotations.Schema;

namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Params
{
    public class DepartmentListParam
    {
        public int Id { get; set; }
        public long Company { get; set; }
        public long? Branch { get; set; }
        public string? Name { get; set; }
        public string? Telephone { get; set; }
        public string? Email { get; set; }
        public long? Principal { get; set; }

        [NotMapped]
        public List<int>? Ids { get; set; }
    }
}
