using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels
{
    public class PropertyRegViewModel
    {
        public string? ComapnyNumber { get; set; }
        public string? ComapnyName { get; set; }
        public string? propertyType { get; set; }
        public string? propertyDescription { get; set; }
        public string?   propertyLocation { get; set; }
        public string? phoneNumber { get; set; }
        public string? neighbourhood { get; set; }
        public string? longitude { get; set; }
        public string? latitude { get; set; }
        public string? email { get; set; }
        //public List<string>? title { get; set; }
        public IFormFile file { get; set; }

    }
}
