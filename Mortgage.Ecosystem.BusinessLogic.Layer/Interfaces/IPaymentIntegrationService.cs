using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces
{
    public interface IPaymentIntegrationService
    {
        Task<TData<TransactionDetails>> GenerateRRR(RemitaPaymentDTO remitaPayment);
        Task<TData<GetRemitaResponse>> CheckRRRStatus(string Rrr);
        Task<TData<TransactionDetails>> Generate(RemitaPaymentDTO remitaPayment);

    }
}
