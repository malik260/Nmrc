using System;
using System.Collections.Generic;

namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Entities
{
    public partial class Nlog
    {
        public int Id { get; set; }
        public DateTime Datetime { get; set; }
        public string Message { get; set; } = null!;
        public string Lvl { get; set; } = null!;
    }
}
