using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos
{
    public class RiskAssessmentProcedureDTO
    {
        public string? NhfNumber { get; set; }
        public int Weight { get; set; }
        public string? ProductName { get; set; }
        public string? Remark { get; set; }
    }
}
