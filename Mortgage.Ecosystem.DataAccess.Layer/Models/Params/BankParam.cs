using System.ComponentModel.DataAnnotations.Schema;

namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Params
{
    public class BankListParam
    {
        public string? Code { get; set; }
        public string? Name { get; set; }

        [NotMapped]
        public List<string>? Codes { get; set; }
    }
}
