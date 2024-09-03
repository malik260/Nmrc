using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels
{
    public class BatchUploadVM
    {
        public string? accountName { get; set; }
        public string? phoneNumber { get; set; }
        public string? BatchEmailAddress { get; set; }
        public string? Month { get; set; }
        public string? Year { get; set; }
        public int paymentOptionBatch { get; set; }
        public IFormFile ContributionTemplate { get; set; }
        public string? narration { get; set; }


    }
}
