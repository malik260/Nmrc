using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Result;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces
{
    public interface IStatementOfAccountService
    {
        Task<TData<List<StatementOfAccountEntity>>> GetList(StatementOfAccountListParam param);
        Task<TData<List<StatementOfAccountEntity>>> GetPageList(StatementOfAccountListParam param, Pagination pagination);
        Task<TData<List<ZtreeInfo>>> GetZtreeStatementOfAccountList(StatementOfAccountListParam param);
        Task<TData<List<ZtreeInfo>>> GetZtreeUserList(StatementOfAccountListParam param);
        Task<TData<StatementOfAccountEntity>> GetEntity(long id);
        Task<TData<int>> GetMaxSort();
        Task<TData<string>> SaveForm(StatementOfAccountEntity entity);
        Task<TData> DeleteForm(string ids);
    }
}
