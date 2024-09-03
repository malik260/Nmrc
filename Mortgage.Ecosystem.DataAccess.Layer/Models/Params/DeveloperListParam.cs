using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Params
{
    public class DeveloperProcessParam
    {
        public long BaseProcessMenu { get; set; }
    }
    public class DeveloperListParam
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? MobileNumber { get; set; }
        public string? EmailAddress { get; set; }
        public string? RCNumber { get; set; }
    }
}
