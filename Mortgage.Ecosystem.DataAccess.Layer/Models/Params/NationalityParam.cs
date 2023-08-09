using System.ComponentModel.DataAnnotations.Schema;

namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Params
{
    public class NationalityListParam
    {
        public string? Code { get; set; }

        public string? ShortName { get; set; }

        public string? FullName { get; set; }

        public string? Capital { get; set; }

        public string? Currency { get; set; }
    }
}
