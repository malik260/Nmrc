using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos
{
    public class LoanProductResponse
    {
        public bool success { get; set; }
        public List<Result> result { get; set; }


    }

    public class Result
    {
        public int productId { get; set; }
        public string productCode { get; set; }
        public string productName { get; set; }
        public string productDescription { get; set; }
        public int productTenor { get; set; }
    }
}
