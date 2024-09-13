using System.ComponentModel.DataAnnotations.Schema;

namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Params
{
    public class SecondaryLenderProcedureChecklistListParam
    {
        public long PmbId { get; set; }
        public string? EmployeeNhfNumber { get; set; }
        public string? Item { get; set; }
        public string? Description { get; set; }
        public bool Applicable { get; set; }
    }
}
