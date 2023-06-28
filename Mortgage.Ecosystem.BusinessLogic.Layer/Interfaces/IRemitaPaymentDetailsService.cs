using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Result;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces
{
    public interface IRemitaPaymentDetailsService
    {
        Task<TData<List<RemitaPaymentDetailsEntity>>> GetList(RemitaPaymentDetailsListParam param);
        Task<TData<List<RemitaPaymentDetailsEntity>>> GetPageList(RemitaPaymentDetailsListParam param, Pagination pagination);
        Task<TData<List<ZtreeInfo>>> GetZtreeRemitaPaymentDetailsList(RemitaPaymentDetailsListParam param);
        Task<TData<List<ZtreeInfo>>> GetZtreeUserList(RemitaPaymentDetailsListParam param);
        Task<TData<RemitaPaymentDetailsEntity>> GetEntity(long id);
        Task<TData<int>> GetMaxSort();
        Task<TData<string>> SaveForm(RemitaPaymentDetailsEntity entity);
        Task<TData> DeleteForm(string ids);
    }
}
