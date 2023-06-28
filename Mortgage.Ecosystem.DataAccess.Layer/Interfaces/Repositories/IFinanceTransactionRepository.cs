using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.DataAccess.Layer.Interfaces.Repositories
{
    public interface IFinanceTransactionRepository
    {
        Task<List<FinanceTransactionEntity>> GetList(FinanceTransactionListParam param);
        Task<List<FinanceTransactionEntity>> GetPageList(FinanceTransactionListParam param, Pagination pagination);
        Task<FinanceTransactionEntity> GetEntity(long id);
        Task<int> GetMaxSort();
        Task SaveForm(FinanceTransactionEntity entity);
        Task DeleteForm(string ids);
    }
}
