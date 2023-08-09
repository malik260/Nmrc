using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Result;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces
{
    public interface IFinanceTransactionService
    {
        Task<TData<List<FinanceTransactionEntity>>> GetList(FinanceTransactionListParam param);
        Task<TData<List<FinanceTransactionEntity>>> GetPageList(FinanceTransactionListParam param, Pagination pagination);
        Task<TData<List<ZtreeInfo>>> GetZtreeFinanceTransactionList(FinanceTransactionListParam param);
        Task<TData<List<ZtreeInfo>>> GetZtreeUserList(FinanceTransactionListParam param);
        Task<TData<FinanceTransactionEntity>> GetEntity(long id);
        Task<TData<int>> GetMaxSort();
        Task<TData<string>> SaveForm(FinanceTransactionEntity entity);
        Task<TData> DeleteForm(string ids);
    }
}
