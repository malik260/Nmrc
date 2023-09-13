using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces;
using Mortgage.Ecosystem.BusinessLogic.Layer.Resources;
using Mortgage.Ecosystem.DataAccess.Layer.Conversion;
using Mortgage.Ecosystem.DataAccess.Layer.Helpers;
using Mortgage.Ecosystem.DataAccess.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;
using MySqlX.XDevAPI.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using static Mortgage.Ecosystem.DataAccess.Layer.Helpers.WebClientUtil;
using Header = Mortgage.Ecosystem.DataAccess.Layer.Helpers.WebClientUtil.Header;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Services
{
    public class PaymentIntegrationService : IPaymentIntegrationService
    {
        private readonly IUnitOfWork _iUnitOfWork;
        private readonly HttpClient _client;
        private readonly IConfiguration _configuration;
        public static Headers _header;
        public static List<Header> headers;

        public PaymentIntegrationService(IUnitOfWork iUnitOfWork, IConfiguration configuration)
        {
            _iUnitOfWork = iUnitOfWork;
            _client = new HttpClient
            {
                BaseAddress = new Uri(ApiResource.generateRRR)
            };

            _configuration = configuration;
        }
        public static string SHA512(string hash_string)
        {
            System.Security.Cryptography.SHA512Managed sha512 = new System.Security.Cryptography.SHA512Managed();
            Byte[] EncryptedSHA512 = sha512.ComputeHash(System.Text.Encoding.UTF8.GetBytes(hash_string));
            sha512.Clear();
            string hashed = BitConverter.ToString(EncryptedSHA512).Replace("-", "").ToLower();
            return hashed;
        }
        public static string GetNumber(int length)
        {
            var rng = new Random(Environment.TickCount);
            return string.Concat(Enumerable.Range(0, length).Select((index) => rng.Next(10).ToString()));
        }


        public async Task<TData<TransactionDetails>> Generate(RemitaPaymentDTO remitaPayment)
        {
            RemitaKeysVM remitaCredentials = new RemitaKeysVM();
            var baseurl = remitaCredentials.Baseurl;
            string merchantId = remitaCredentials.merchantId;
            string apiKey = remitaCredentials.apiKey;
            string serviceTypeId = remitaCredentials.serviceTypeId;
            var generateRRR = remitaCredentials.PaymentInit;

            string orderId = GetNumber(11);
            string apiHashString = merchantId + serviceTypeId + orderId + remitaPayment.amount.ToStr() + apiKey;
            string apiHash = WebClientUtil.SHA512(apiHashString);

            _header = new Headers();
            headers = new List<Header>();
            headers.Add(new Header { header = "Content-Type", value = "application/json" });
            headers.Add(new Header { header = "Authorization", value = "remitaConsumerKey=" + merchantId + ",remitaConsumerToken=" + apiHash });
            _header.headers = headers;
            var request = new GenerateRRRVM
            {
                amount = remitaPayment.amount.ToStr(),
                serviceTypeId = serviceTypeId,
                description = remitaPayment.description,
                orderId = orderId,
                payerEmail = remitaPayment.payerEmail,
                payerPhone = remitaPayment.payerPhone,
                payerName = remitaPayment.payerName,

            };

            String jsonGenerateRRRRequest = JsonConvert.SerializeObject(request);
            var response = WebClientUtil.PostResponse(baseurl, generateRRR, jsonGenerateRRRRequest, _header);
            response = response.Replace("jsonp (", "");
            response = response.Replace(")", "");
            response = response.Trim();
            var deserialized = JsonConvert.DeserializeObject<RemitaResponse>(response);
            TransactionDetails Results = new TransactionDetails();
            Results.TransactionId = orderId;
            Results.RRR = deserialized.RRR;
            if (deserialized.statuscode == "025")
            {
                // GenerateRRR was successful
                TData<TransactionDetails> obj = new TData<TransactionDetails>();

                obj.Data = Results;
                obj.Tag = 1;
                return obj;
            }
            else
            {
                TData<TransactionDetails> obj = new TData<TransactionDetails>();

                obj.Data = Results;
                obj.Tag = 0;
                return obj;
            }




        }


        public async Task<TData<GetRemitaResponse>> CheckRRRStatus(string Rrr)
        {
            RemitaKeysVM remitaCredentials = new RemitaKeysVM();
            string merchantId = remitaCredentials.merchantId;
            string apiKey = remitaCredentials.apiKey;
            string apiHash = SHA512(Rrr + apiKey + merchantId);

            try
            {
                var response = await _client.GetAsync(ApiResource.rrrStatus + merchantId + "/" + Rrr + "/" + apiHash + "/status.reg");
                string response1 = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<GetRemitaResponse>(response1);

                if (result.status == "025")
                {
                    TData<GetRemitaResponse> obj = new TData<GetRemitaResponse>();
                    obj.Data = result;
                    obj.Tag = 1;
                    return obj;
                }
                else
                {
                    TData<GetRemitaResponse> obj = new TData<GetRemitaResponse>();
                    obj.Data = result;
                    obj.Tag = 0;
                    return obj;
                }
            }
            catch (Exception ex)
            {
                throw;
            };

        }
    }
}
