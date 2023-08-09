using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Result;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces
{
    public interface IPaymentHistoryService
    {
        Task<TData<List<PaymentHistoryEntity>>> GetList(PaymentHistoryListParam param);
        Task<TData<List<PaymentHistoryEntity>>> GetPageList(PaymentHistoryListParam param, Pagination pagination);
        Task<TData<List<ZtreeInfo>>> GetZtreePaymentHistoryList(PaymentHistoryListParam param);
        Task<TData<List<ZtreeInfo>>> GetZtreeUserList(PaymentHistoryListParam param);
        Task<TData<PaymentHistoryEntity>> GetEntity(long id);
        Task<TData<int>> GetMaxSort();
        Task<TData<string>> SaveForm(PaymentHistoryEntity entity);
        Task<TData> DeleteForm(string ids);
    }
}
