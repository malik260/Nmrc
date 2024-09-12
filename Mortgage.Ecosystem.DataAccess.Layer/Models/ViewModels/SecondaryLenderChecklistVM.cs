using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels
{
    public class SecondaryLenderChecklisVM
    {
        public string? PmbId { get; set; }
      
        public string? EmployeeNhfNumber { get; set; }
        public string? Item { get; set; }
        public string? Description { get; set; }
    }
}
