using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.DataAccess.Layer.Interfaces.Repositories
{
    public interface IFinanceCounterpartyTransactionRepository
    {
        Task<List<FinanceCounterpartyTransactionEntity>> GetList(FinanceCounterpartyTransactionListParam param);
        Task<List<FinanceCounterpartyTransactionEntity>> GetPageList(FinanceCounterpartyTransactionListParam param, Pagination pagination);
        Task<FinanceCounterpartyTransactionEntity> GetEntity(long id);
        Task<int> GetMaxSort();
        Task SaveForm(FinanceCounterpartyTransactionEntity entity);
        Task DeleteForm(string ids);
    }
}
