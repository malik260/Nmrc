using Mortgage.Ecosystem.DataAccess.Layer.Models.Entities;
using Mortgage.Ecosystem.DataAccess.Layer.Models.Params;
using Mortgage.Ecosystem.DataAccess.Layer.Models.ViewModels;

namespace Mortgage.Ecosystem.DataAccess.Layer.Interfaces.Repositories
{
    public interface IContributionRepository
    {
        Task<List<ContributionEntity>> GetList(ContributionParam param);
        Task<List<ContributionEntity>> GetPageList(ContributionParam param, Pagination pagination);
        Task<ContributionEntity> GetEntity(long id);
        Task<int> GetMaxSort();
        Task SaveForm(ContributionEntity entity);
        Task DeleteForm(string ids);
    }
}
