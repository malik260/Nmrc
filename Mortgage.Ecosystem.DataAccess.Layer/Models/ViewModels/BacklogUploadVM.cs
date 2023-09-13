using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels
{
    public class BacklogUploadVM
    {
        public string? AccountName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? EmailAddress { get; set; }
        public int BacklogPaymentOption { get; set; }
        public IFormFile BacklogTemplate { get; set; }
        public string? BacklogNarration { get; set; }
    }
}
