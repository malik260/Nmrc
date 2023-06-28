using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos
{
    public class GetRemitaResponse
    {
        public decimal amount { get; set; }
        public string RRR { get; set; }
        public string orderId { get; set; }
        public string message { get; set; }
        public string transactiontime { get; set; }
        public string status { get; set; }
    }
    public class GenerateRRR
    {
        //This uniquely identifies the biller.
        [Required]
        public string merchantId { get; set; }

        //The Remita Retrieval Reference
        [Required]
        public string rrr { get; set; }


        //SHA512 (RRR/OrderID+api_key+merchantId)
        [Required]
        public string hash { get; set; }

        //This is the billers transaction ID
        [Required]
        public decimal OrderID { get; set; }
    }

    public class Header
    {
        public string header { get; set; }
        public string value { get; set; }

    }
}
