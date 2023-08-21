using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels
{
    public class RemitaKeysVM
    {

        public string? merchantId = "2547916";
        public string? apiKey = "1946";
        public string? serviceTypeId = "4430731";
        public string? Baseurl = "https://remitademo.net";
        public string? PaymentInit = "/remita/exapp/api/v1/send/api/echannelsvc/merchant/api/paymentinit";
    }

    public class GenerateRRRVM
    {
        public string? serviceTypeId { get; set; }
        public string? amount { get; set; }
        public string? orderId { get; set; }
        public string? payerName { get; set; }
        public string? payerEmail { get; set; }
        public string? payerPhone { get; set; }
        public string? description { get; set; }
    }

}
