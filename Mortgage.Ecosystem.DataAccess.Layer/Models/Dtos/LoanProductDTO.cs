using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos
{
    public class LoanApplicationDTO
    {
        public class LoanProduct
        {
            public int productId { get; set; }
            public string? productCode { get; set; }
            public string? productName { get; set; }
            public string? productDescription { get; set; }
            public string? productTenor { get; set; }
        }

        public class Response
        {
            public bool success { get; set; }
            public List<LoanProduct> result { get; set; }
        }

    }
}
