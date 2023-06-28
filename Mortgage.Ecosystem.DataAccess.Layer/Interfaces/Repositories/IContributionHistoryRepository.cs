using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.DataAccess.Layer.Interfaces.Repositories
{
    public interface IContributionHistoryRepository
    {
        Task<List<ContributionHistoryEntity>> GetList(ContributionHistoryListParam param);
        Task<List<ContributionHistoryEntity>> GetPageList(ContributionHistoryListParam param, Pagination pagination);
        Task<ContributionHistoryEntity> GetEntity(long id);
        Task<int> GetMaxSort();
        Task SaveForm(ContributionHistoryEntity entity);
        Task DeleteForm(string ids);
    }
}
