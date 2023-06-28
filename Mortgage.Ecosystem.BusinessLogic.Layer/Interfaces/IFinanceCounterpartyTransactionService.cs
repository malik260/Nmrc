using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Result;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces
{
    public interface IFinanceCounterpartyTransactionService
    {
        Task<TData<List<FinanceCounterpartyTransactionEntity>>> GetList(FinanceCounterpartyTransactionListParam param);
        Task<TData<List<FinanceCounterpartyTransactionEntity>>> GetPageList(FinanceCounterpartyTransactionListParam param, Pagination pagination);
        Task<TData<List<ZtreeInfo>>> GetZtreeFinanceCounterpartyTransactionList(FinanceCounterpartyTransactionListParam param);
        Task<TData<List<ZtreeInfo>>> GetZtreeUserList(FinanceCounterpartyTransactionListParam param);
        Task<TData<FinanceCounterpartyTransactionEntity>> GetEntity(long id);
        Task<TData<int>> GetMaxSort();
        Task<TData<string>> SaveForm(FinanceCounterpartyTransactionEntity entity);
        Task<TData> DeleteForm(string ids);
    }
}
