using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.DataAccess.Layer.Interfaces.Repositories
{
    public interface IStatementOfAccountRepository
    {
        Task<List<FinanceCounterpartyTransactionEntity>> GetList(StatementOfAccountListParam param);
        Task<List<FinanceCounterpartyTransactionEntity>> GetPageList(StatementOfAccountListParam param, Pagination pagination);
        Task<StatementOfAccountEntity> GetEntity(long id);
        Task<int> GetMaxSort();
        Task SaveForm(StatementOfAccountEntity entity);
        Task DeleteForm(string ids);
    }
}
