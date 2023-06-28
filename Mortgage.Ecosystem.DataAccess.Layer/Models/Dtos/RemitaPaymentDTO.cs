using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos
{
    public class RemitaPaymentDTO
    {
        //This is a unique identifier for the Biller
        [Required]
        public string merchantId { get; set; }

        //This is a unique identifier for service type receiving the payment
        [Required]
        public string serviceTypeId { get; set; }

        //This is the Biller Transaction ID
        [Required]
        public string orderId { get; set; }


        //remitaConsumerKey={merchantID},remitaConsumerToken={SHA512( merchantId+ serviceTypeId+ orderId+totalAmount+apiKey)}
        [Required]
        public string Authorization { get; set; }

        //This is the name of the customer to be displayed on the payment page.
        [Required]
        public string payerName { get; set; }

        // his is the Payer’s Email Address
        [Required]
        public string payerEmail { get; set; }

        //This is the Payer’s Phone Number
        public string payerPhone { get; set; }


        //This is the total monetary value of the transaction
        [Required]
        public decimal amount { get; set; }

        //Details of the service your customer is paying for.
        [Required]
        public string description { get; set; }



    }

    public class RemitaResponse
    {
        public string statuscode { get; set; }
        public string RRR { get; set; }
        public string status { get; set; }

    }

    public class TransactionDetails
    {
        public string RRR { get; set; }
        public string TransactionId { get; set; }
    }


}