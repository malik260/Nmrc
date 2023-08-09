using Microsoft.AspNetCore.Mvc;
using Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces;
using Mortgage.Ecosystem.BusinessLogic.Layer.Resources;
using Mortgage.Ecosystem.DataAccess.Layer.Interfaces;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Services
{
    public class PaymentIntegrationService: IPaymentIntegrationService
    {
        private readonly IUnitOfWork _iUnitOfWork;
        private readonly HttpClient _client;

        public PaymentIntegrationService(IUnitOfWork iUnitOfWork)
        {
            _iUnitOfWork = iUnitOfWork;
            _client = new HttpClient
            {
                BaseAddress = new Uri(ApiResource.generateRRR)
            };
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(ApiResource.ApplicationJson));
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



        public async Task<TData<TransactionDetails>> GenerateRRR(RemitaPaymentDTO remitaPayment)

        {
            string merchantId = "2547916";
            string apiKey = "1946";
            string serviceTypeId = "4430731";
            string orderId = GetNumber(11);
            string apiHashString = merchantId + serviceTypeId + orderId + remitaPayment.amount + apiKey;
            string apiHash = SHA512(apiHashString);

            var values = "remitaConsumerKey=" + merchantId + ",remitaConsumerToken=" + apiHash;


            try
            {
                _client.DefaultRequestHeaders.Add("Authorization", values);
                var jsonPayload = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(remitaPayment), Encoding.UTF8, ApiResource.ApplicationJson);

                var response = await _client.PostAsync(ApiResource.generateRRR, jsonPayload);
                var responseContent = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<RemitaResponse>(responseContent);
                TransactionDetails Results = new TransactionDetails();
                Results.TransactionId = orderId;
                Results.RRR = result.RRR;
                if (response.IsSuccessStatusCode && result.statuscode == "025")
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
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<TData<GetRemitaResponse>> CheckRRRStatus(string Rrr)
        {
            string merchantId = "2547916";
            string apiKey = "1946";
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
                throw ex;
            };

        }
    }

}