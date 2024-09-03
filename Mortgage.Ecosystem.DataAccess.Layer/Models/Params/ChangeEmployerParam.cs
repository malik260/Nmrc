using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Params
{
    public class ChangeEmployerListParam
    {
        public string? NhfNumber { get; set; }
        public string? OldEmployer { get; set; }
        public string? OldEmployerNo { get; set; }
        public string? NewEmployer { get; set; }
        public long Company { get; set; }
        public DateTime? DateCreated { get; set; }
    }
}
