using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.DataAccess.Layer.Interfaces.Repositories
{
    public interface IPaymentHistoryRepository
    {
        Task<List<PaymentHistoryEntity>> GetList(PaymentHistoryListParam param);
        Task<List<PaymentHistoryEntity>> GetPageList(PaymentHistoryListParam param, Pagination pagination);
        Task<PaymentHistoryEntity> GetEntity(long id);
        Task<int> GetMaxSort();
        Task SaveForm(PaymentHistoryEntity entity);
        Task DeleteForm(string ids);
    }
}
