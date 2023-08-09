using Mortgage.Ecosystem.DataAccess.Layer.Models.Dtos;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Result;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Interfaces
{
    public interface IContributionHistoryService
    {
        Task<TData<List<ContributionHistoryEntity>>> GetList(ContributionHistoryListParam param);
        Task<TData<List<ContributionHistoryEntity>>> GetPageList(ContributionHistoryListParam param, Pagination pagination);
        Task<TData<List<ZtreeInfo>>> GetZtreeContributionHistoryList(ContributionHistoryListParam param);
        Task<TData<List<ZtreeInfo>>> GetZtreeUserList(ContributionHistoryListParam param);
        Task<TData<ContributionHistoryEntity>> GetEntity(long id);
        Task<TData<int>> GetMaxSort();
        Task<TData<string>> SaveForm(ContributionHistoryEntity entity);
        Task<TData> DeleteForm(string ids);
    }
}
